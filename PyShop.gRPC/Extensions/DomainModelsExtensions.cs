using Billing;

namespace PyShop.gRPC.Extensions;

public static class DomainModelsExtensions
{
    public static Coin ToRPCCoin(this Domain.Models.Coin domainCoin)
    {
        return new Coin()
        {
            History = domainCoin.History,
            Id = domainCoin.Id
        };
    }

    public static UserProfile ToRPCUserProfile(this Domain.Models.UserProfile domainUserProfile)
    {
        return new UserProfile()
        {
            Amount = (long) domainUserProfile.Amount,
            Rating = domainUserProfile.Rating,
            Name = domainUserProfile.Name
        };
    }
}