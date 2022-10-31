namespace PyShop.gRPC.Application.Interfaces.Inputs;

public interface IMoveCoinInput : IAmountInput
{
    public string SourceUser { get; set; }
    public string DestinationUser { get; set; }
}