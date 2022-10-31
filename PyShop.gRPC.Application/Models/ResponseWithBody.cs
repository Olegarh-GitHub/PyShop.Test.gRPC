using PyShop.gRPC.Domain.Enums;

namespace PyShop.gRPC.Application.Models;

public class ResponseWithBody<TPayload> : Response
{
    public TPayload Payload { get; set; }

    public ResponseWithBody
    (
        TPayload payload,
        Status status = Status.STATUS_UNSPECIFIED, 
        string comment = null
    ) : base(status, comment)
    {
        Payload = payload;
    }
}