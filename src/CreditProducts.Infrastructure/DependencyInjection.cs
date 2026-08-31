using CreditProducts.Domain.Repository;
using CreditProducts.Infrastructure.Persistence;
using CreditProducts.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CreditProducts.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CreditProductDbContext>(opt =>
            opt.UseNpgsql(configuration.GetConnectionString("CreditProductConnection"))
        );

        services.AddScoped<ICreditProductRepository,CreditProductRepository>();

        return services;

    }
}