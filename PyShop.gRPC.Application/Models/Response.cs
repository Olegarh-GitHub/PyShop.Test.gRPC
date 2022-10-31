using System.Data;
using PyShop.gRPC.Application.Interfaces.Responses;
using PyShop.gRPC.Domain.Enums;

namespace PyShop.gRPC.Application.Models;

public class Response : IResponse
{
    public Status Status { get; set; }
    public string Comment { get; set; }

    public Response(Status status = Status.STATUS_UNSPECIFIED, string comment = null)
    {
        Status = status;
        Comment = comment;
    }
}