using RugbyManager.WebApi.Authentication;

namespace RugbyManager.WebApi.Filters;

public class ApiKeyFilter : IEndpointFilter
{
    private readonly IConfiguration _configuration;

    public ApiKeyFilter(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue(
                AuthenticationConstants.ApiKeyHeaderName,
                out var receivedApiKey))
        {
            return new UnauthorizedResult("API Key not supplied");
        }

        var apiKey = _configuration.GetValue<string>(AuthenticationConstants.ApiKeyConfigPath);

        if (!apiKey.Equals(receivedApiKey, StringComparison.OrdinalIgnoreCase))
        {
            return new UnauthorizedResult("API Key supplied is not valid");
        }

        return await next(context);
    }
}