using PyShop.gRPC.Domain.Models;
using PyShop.gRPC.Infrastructure.Interfaces;

namespace PyShop.gRPC.Infrastructure.Repositories;

public class UserProfileRepository : IUserRepository
{
    private List<UserProfile> _userProfiles;
    private readonly ICoinRepository _coinRepository;

    public UserProfileRepository(ICoinRepository coinRepository)
    {
        _coinRepository = coinRepository;
        _userProfiles = new List<UserProfile>()
        {
            BULAT,
            DAMIR,
            OLEG
        };
    }

    public IQueryable<UserProfile> ReadAsQueryable()
    {
        IQueryable<UserProfile> profiles = _userProfiles.AsQueryable();

        foreach (UserProfile profile in profiles)
        {
            List<Coin> coins = _coinRepository.GetByOwner(profile.Name).ToList();

            int amount = coins.Count;

            profile.Amount = amount;
        }

        return profiles;
    }

    public bool UpdateUserCoinAmount(string userName, int amount)
    {
        UserProfile neededUser = GetByUserName(userName);

        if (neededUser is null)
            return false;

        _userProfiles.Remove(neededUser);

        neededUser.Amount += amount;
        _userProfiles.Add(neededUser);

        return true;
    }

    public UserProfile GetByUserName(string userName)
    {
        return _userProfiles.FirstOrDefault(user => user.Name == userName);
    }

    #region Mock data

    private UserProfile BULAT = new UserProfile("Bulat", 5000, 1);
    private UserProfile DAMIR = new UserProfile("Damir", 1000, 1);
    private UserProfile OLEG = new UserProfile("Oleg", 800, 1);

    #endregion
}