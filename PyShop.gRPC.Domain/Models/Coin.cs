namespace PyShop.gRPC.Domain.Models;

public class Coin
{
    public int Id { get; set; }
    public string History { get; set; }
    
    public Coin() { }

    public Coin(int id, string history)
    {
        Id = id;
        History = history;
    }
}