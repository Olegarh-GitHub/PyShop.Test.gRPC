using PyShop.gRPC.Application.Interfaces.Inputs;

namespace PyShop.gRPC.Models.Inputs;

public class AmountInput : IAmountInput
{
    public int Amount { get; set; }

    public AmountInput(int amount)
    {
        Amount = amount;
    }
}