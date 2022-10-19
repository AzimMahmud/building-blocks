using System.Net;

namespace BuildingBlocks.Core.Exception.Types;

public class ApiCustomException : CustomException
{
    public ApiCustomException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) : base(message)
    {
        StatusCode = statusCode;
    }
}