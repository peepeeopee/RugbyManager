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
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<AppDbContext>(
                    opt => opt.UseInMemoryDatabase("RugbyManagerDb"));
            }
            else
            {
                services.AddDbContext<AppDbContext>(opt =>
                    opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                        builder =>
                            builder.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName))
                );
            }

            services.AddScoped<IAppDbContext>(x => x.GetRequiredService<AppDbContext>());

            services.AddScoped<AppDbContextInitializer>();

            return services;
        }
    }
}