namespace RugbyManager.WebApi.Filters;

using FluentValidation;
using System.Net;
using Results = Microsoft.AspNetCore.Http.Results;


public class ValidationFilter<T> : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {

        T? argToValidate = (T?)context.Arguments.Last();
        IValidator<T>? validator = context.HttpContext.RequestServices.GetService<IValidator<T>>();

        if (validator is not null)
        {
            var validationResult = await validator.ValidateAsync(argToValidate!);
            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary(),
                    statusCode: (int)HttpStatusCode.UnprocessableEntity);
            }
        }

        // Otherwise invoke the next filter in the pipeline
        return await next.Invoke(context);
    }
}