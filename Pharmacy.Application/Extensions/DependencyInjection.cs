using Microsoft.Extensions.DependencyInjection;

namespace Pharmacy.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(ctg =>
        {
            ctg.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly);
        });
        return services;
    }
}