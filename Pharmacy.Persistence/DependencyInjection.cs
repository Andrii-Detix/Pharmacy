using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pharmacy.Application.Abstractions;

namespace Pharmacy.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("Database");
        
        services.AddDbContext<PharmacyDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        services.AddScoped<IPharmacyDbContext, PharmacyDbContext>();
        
        return services;
    }
}