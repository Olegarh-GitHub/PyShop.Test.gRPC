using PyShop.gRPC.Application.Interfaces.Inputs;

namespace PyShop.gRPC.Models.Inputs;

public class MoveCoinInput : AmountInput, IMoveCoinInput
{
    public string SourceUser { get; set; }
    public string DestinationUser { get; set; }

    public MoveCoinInput(int amount, string sourceUser, string destinationUser) : base(amount)
    {
        SourceUser = sourceUser;
        DestinationUser = destinationUser;
    }
}