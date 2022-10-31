using PyShop.gRPC.Application.Interfaces;
using PyShop.gRPC.Application.Interfaces.Inputs;
using PyShop.gRPC.Application.Interfaces.Responses;
using PyShop.gRPC.Application.Models;
using PyShop.gRPC.Domain.Enums;
using PyShop.gRPC.Domain.Models;
using PyShop.gRPC.Infrastructure.Interfaces;

namespace PyShop.gRPC.Application.Services;

public class BillingService : IBillingService
{
    private readonly IUserRepository _userProfileRepository;
    private readonly ICoinRepository _coinRepository;
    
    public BillingService
    (
        IUserRepository userProfileRepository,
        ICoinRepository coinRepository
    )
    {
        _userProfileRepository = userProfileRepository;
        _coinRepository = coinRepository;
    }

    public IResponse ListUsers()
    {
        IQueryable<UserProfile> profiles = _userProfileRepository.ReadAsQueryable();

        return new ResponseWithBody<IQueryable<UserProfile>>(profiles, Status.STATUS_OK, "Users listed");
    }

    public IResponse CoinsEmission(IAmountInput input)
    {
        int amount = input.Amount;

        List<UserProfile> users = _userProfileRepository
            .ReadAsQueryable()
            .ToList();

        if (amount < users.Count)
        {
            return new Response
            (
                Status.STATUS_FAILED,
                "Each user must receive at least one coin"
            );
        }

        int ratingSum = users.Sum(user => user.Rating);

        foreach (UserProfile profile in users.OrderByDescending(user => user.Rating))
        {
            string profileName = profile.Name;
            int rating = profile.Rating;
            double ratingPercentage = ((double) rating / (double) ratingSum);

            int receivedCoins = (int) (Math.Floor(ratingPercentage * amount));

            _userProfileRepository.UpdateUserCoinAmount(profileName, receivedCoins);
            _coinRepository.GiveCoins(profileName, receivedCoins);
        }

        return new Response(Status.STATUS_OK, "Each user receive coins");
    }

    public IResponse MoveCoins(IMoveCoinInput input)
    {
        int amount = input.Amount;
        string previousOwnerName = input.SourceUser;
        string newOwnerName = input.DestinationUser;

        UserProfile previousOwner = _userProfileRepository.GetByUserName(previousOwnerName);
        if (previousOwner.Amount < amount)
            return new Response(Status.STATUS_FAILED, "Previous owner doesn't have enough coins");

        List<Coin> coins = _coinRepository.GetByOwner(previousOwnerName).Take(amount).ToList();

        foreach (Coin coin in coins)
        {
            _coinRepository.UpdateCoinHistory(coin.Id, newOwnerName);
        }

        return new Response(Status.STATUS_OK, $"{amount} coins have been moved to {newOwnerName}");
    }

    public IResponse LongestHistoryCoin()
    {
        List<Coin> coins = _coinRepository.ReadAsQueryable().ToList();

        int maxOwners = coins.Max(coin => coin.History.Split(",").Length);

        if (maxOwners == default)
            return new Response(Status.STATUS_FAILED, "There is no coin with longest history");

        return new ResponseWithBody<Coin>(coins.FirstOrDefault(coin => coin.History.Split(",").Length == maxOwners), Status.STATUS_OK);
    }
}