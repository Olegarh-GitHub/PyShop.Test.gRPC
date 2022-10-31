using PyShop.gRPC.Domain.Models;

namespace PyShop.gRPC.Infrastructure.Interfaces;

public interface ICoinRepository : IReadRepository<Coin>
{
    public bool UpdateCoinHistory(int id, string newOwner);
    public IEnumerable<Coin> GetByOwner(string ownerName);
    public bool GiveCoins(string ownerName, long amount);
}