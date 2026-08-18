using Iglesia.Infrastructure.Identity;
using Iglesia.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Iglesia.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Database (En Memoria para desarrollo rápido)
        services.AddDbContext<IglesiaDbContext>(options =>
            options.UseInMemoryDatabase("IglesiaDb_Dev"));

        // Identity
        services.AddIdentityCore<ApplicationUser>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;  
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 8;
            options.User.RequireUniqueEmail = true;
        })
        .AddRoles<ApplicationRole>()
        .AddEntityFrameworkStores<IglesiaDbContext>();

        // TODO: Register repository implementations here
        // services.AddScoped<IMiembroRepository, MiembroRepository>();

        return services;
    }
}