using PyShop.gRPC.Domain.Enums;

namespace PyShop.gRPC.Application.Interfaces.Responses;

public interface IResponse
{
    public Status Status { get; set; }
    public string Comment { get; set; }
}