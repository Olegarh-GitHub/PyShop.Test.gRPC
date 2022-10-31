using PyShop.gRPC.Domain.Models;
using PyShop.gRPC.Infrastructure.Interfaces;

namespace PyShop.gRPC.Infrastructure.Repositories;

public class CoinRepository : ICoinRepository
{
    private List<Coin> _coins;
    private int _currentId;

    public CoinRepository()
    {
        _coins = new List<Coin>()
        {
            BULAT_COIN,
            DAMIR_COIN,
            OLEG_COIN
        };

        _currentId = 1337;
    }

    public IQueryable<Coin> ReadAsQueryable()
    {
        return _coins.AsQueryable();
    }

    public bool UpdateCoinHistory(int id, string newOwner)
    {
        Coin neededCoin = _coins.FirstOrDefault(coin => coin.Id == id);

        if (neededCoin is null)
            return false;

        _coins.Remove(neededCoin);

        string newHistory = string.Join(",", neededCoin.History, newOwner);
        neededCoin.History = newHistory;
        
        _coins.Add(neededCoin);

        return true;
    }

    public IEnumerable<Coin> GetByOwner(string ownerName)
    {
        return _coins
            .Where
            (
                coin => coin.History
                    .Split(",")
                    .LastOrDefault() == ownerName
            );
    }

    public bool GiveCoins(string ownerName, long amount)
    {
        for (long index = 0; index < amount; index++)
        {
            _currentId += 1;

            Coin coin = new Coin(_currentId, ownerName);
            
            _coins.Add(coin);
        }

        return true;
    }

    #region Mock data

    private Coin BULAT_COIN = new Coin(228, "Bulat");
    private Coin DAMIR_COIN = new Coin(1337, "Damir");
    private Coin OLEG_COIN = new Coin(322, "Oleg");
    
    #endregion
}