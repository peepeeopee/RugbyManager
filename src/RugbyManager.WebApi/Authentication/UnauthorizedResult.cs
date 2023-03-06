using System.Net;

namespace RugbyManager.WebApi.Authentication;

public class UnauthorizedResult : IResult, IStatusCodeHttpResult
{
    private readonly object _body;

    public UnauthorizedResult(object body)
    {
        _body = body;
    }

    public async Task ExecuteAsync(HttpContext httpContext)
    {
        ArgumentNullException.ThrowIfNull(httpContext);

        httpContext.Response.StatusCode = StatusCode;

        if (_body is string s)
        {
            await httpContext.Response.WriteAsync(s);
            return;
        }

        await httpContext.Response.WriteAsJsonAsync(_body);
    }

    public int StatusCode => StatusCodes.Status401Unauthorized;

    int? IStatusCodeHttpResult.StatusCode => StatusCode;
}