using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RugbyManager.Application.Common.Mapping;
using RugbyManager.Application.Players.Commands;

namespace RugbyManager.Application;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(
            cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(AddPlayerCommand)));
        return services;
    }
}