using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RugbyManager.Application.Common.Interfaces;
using RugbyManager.Infrastructure.DataPersistence;

namespace RugbyManager.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<AppDbContext>(
                    opt => opt.UseInMemoryDatabase("RugbyManagerDb"));
            }

            services.AddScoped<IAppDbContext>(x => x.GetRequiredService<AppDbContext>());

            return services;
        }
    }
}