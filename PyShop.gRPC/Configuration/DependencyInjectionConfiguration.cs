using PyShop.gRPC.Application.Interfaces;
using PyShop.gRPC.Application.Services;
using PyShop.gRPC.Infrastructure.Interfaces;
using PyShop.gRPC.Infrastructure.Repositories;

namespace PyShop.gRPC.Configuration;

public static class DependencyInjectionConfiguration
{
    public static void AddApplicationDependencies(this IServiceCollection services)
    {
        services.AddSingleton<ICoinRepository, CoinRepository>();
        services.AddSingleton<IUserRepository, UserProfileRepository>();

        services.AddSingleton<IBillingService, BillingService>();
    }
}