using PyShop.gRPC.Domain.Models;

namespace PyShop.gRPC.Infrastructure.Interfaces;

public interface IUserRepository : IReadRepository<UserProfile>
{
    public bool UpdateUserCoinAmount(string userName, int amount);
    public UserProfile GetByUserName(string userName);
}