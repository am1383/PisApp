using Microsoft.EntityFrameworkCore;
using PisApp.API.DbContextes;
using PisApp.API.Interfaces;
using PisApp.API.Interfaces.UnitOfWork;
using PisApp.API.Repositories.UnitOfWork;
using PisApp.API.Services;

namespace PisApp.API.Persistence
{
    public static class PersistenceServicesRegistration
    {
        public static void ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICompatibleService, CompatibleService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            services.AddDbContext<PisAppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("MainDB")));
            
            services.Configure<JwtService>(configuration.GetSection("JwtSettings"));
                services.AddSingleton(provider =>
                {
                    var secret = configuration["JwtSettings:Secret"];
                    var issuer = configuration["JwtSettings:Issuer"];
                    return new JwtService(secret, issuer);
                });
        }
    }
}