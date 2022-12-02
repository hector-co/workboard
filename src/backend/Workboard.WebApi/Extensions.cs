using System.Net;
using Workboard.WebApi.ExceptionHandling;
using Workboard.Domain.Abstractions;

namespace Workboard.WebApi;

public static class Extensions
{
    public static void Verify(this Response response)
    {
        VerifyResponse(response.IsSuccess, response.Error!);
    }

    public static void Verify<TValue>(this Response<TValue> response)
    {
        VerifyResponse(response.IsSuccess, response.Error!);
    }

    private static void VerifyResponse(bool isSuccess, Error error)
    {
        if (isSuccess)
            return;

        var statusCode = HttpStatusCode.UnprocessableEntity;
        var code = WebApiException.DomainException;
        if (error.Code.EndsWith(".NotFound"))
        {
            statusCode = HttpStatusCode.NotFound;
            code = WebApiException.DataAccessError;
        }

        throw new WebApiException("Error while executing the request.", statusCode, code, error,
            error?.InnerException);
    }
}