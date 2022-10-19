using System.Net;

namespace BuildingBlocks.Core.Exception.Types;

public class AppCustomException : CustomException
{
    public AppCustomException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base(message)
    {
        StatusCode = statusCode;
    }
}