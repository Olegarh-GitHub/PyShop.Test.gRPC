namespace PyShop.gRPC.Domain.Models;

public class UserProfile
{
    public string Name { get; set; }
    public int Rating { get; set; }
    public int? Amount { get; set; }
    
    public UserProfile() { }

    public UserProfile(string name, int rating, int? amount = null)
    {
        Name = name;
        Rating = rating;
        Amount = amount;
    }
}