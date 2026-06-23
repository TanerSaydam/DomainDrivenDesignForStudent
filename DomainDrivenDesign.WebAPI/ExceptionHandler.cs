using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using TS.Result;

namespace DomainDrivenDesign.WebAPI;

public sealed class ExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        Result<string> fail = Result<string>.Failure(exception.Message);

        httpContext.Response.StatusCode = 500;

        if (exception.GetType() == typeof(ValidationException))
        {
            httpContext.Response.StatusCode = 429;
        }
        await httpContext.Response.WriteAsJsonAsync(fail);

        return true;
    }
}
