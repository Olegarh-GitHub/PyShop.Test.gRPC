using PyShop.gRPC.Application.Interfaces.Inputs;
using PyShop.gRPC.Application.Interfaces.Responses;

namespace PyShop.gRPC.Application.Interfaces;

public interface IBillingService
{
    public IResponse ListUsers();
    
    public IResponse CoinsEmission(IAmountInput input);
    public IResponse MoveCoins(IMoveCoinInput input);

    public IResponse LongestHistoryCoin();
}