using AutoMapper;
using Billing;
using Grpc.Core;
using PyShop.gRPC.Application.Interfaces;
using PyShop.gRPC.Application.Interfaces.Responses;
using PyShop.gRPC.Application.Models;
using PyShop.gRPC.Extensions;
using PyShop.gRPC.Models.Inputs;
using Coin = Billing.Coin;
using Response = Billing.Response;
using Status = PyShop.gRPC.Domain.Enums.Status;
using UserProfile = Billing.UserProfile;

namespace PyShop.gRPC.Services;

public class BillingService : Billing.Billing.BillingBase
{
    private readonly IBillingService _billingService;

    public BillingService(IBillingService billingService)
    {
        _billingService = billingService;
    }

    public override Task<Response> CoinsEmission(EmissionAmount request, ServerCallContext context)
    {
        int amount = (int) request.Amount;

        AmountInput input = new AmountInput(amount);

        IResponse response = _billingService.CoinsEmission(input); 
       
        Status responseStatus = response.Status;
        string comment = response.Comment;

        return Task.FromResult
        (
            new Response()
            {
                Status = (Response.Types.Status) responseStatus,
                Comment = comment
            }
        );
    }

    public override Task<Response> MoveCoins(MoveCoinsTransaction request, ServerCallContext context)
    {
        int amount = (int) request.Amount;
        string sourceUser = request.SrcUser;
        string destinationUser = request.DstUser;

        MoveCoinInput input = new MoveCoinInput(amount, sourceUser, destinationUser);
        
        IResponse response = _billingService.MoveCoins(input);

        Status responseStatus = response.Status;
        string comment = response.Comment;

        return Task.FromResult
        (
            new Response()
            {
                Status = (Response.Types.Status) responseStatus,
                Comment = comment
            }
        );
    }

    public override async Task ListUsers(None request, IServerStreamWriter<UserProfile> responseStream, ServerCallContext context)
    {
        IResponse response = _billingService.ListUsers();

        if (response is not ResponseWithBody<IQueryable<Domain.Models.UserProfile>> userResponse)
            return;

        List<Domain.Models.UserProfile> profiles = userResponse.Payload.ToList();

        foreach (Domain.Models.UserProfile profile in profiles)
        {
            await responseStream.WriteAsync(profile.ToRPCUserProfile());
        }
    }

    public override async Task<Coin> LongestHistoryCoin(None request, ServerCallContext context)
    {
        IResponse response = _billingService.LongestHistoryCoin();
        
        return response is not ResponseWithBody<Domain.Models.Coin> coinResponse 
            ? null 
            : coinResponse.Payload.ToRPCCoin();
    }
}