using Microsoft.EntityFrameworkCore;
using PisApp.API.DbContextes;
using PisApp.API.Interface;
using PisApp.API.Interface.UnitOfWork;
using PisApp.API.Repositories.UnitOfWork;
using PisApp.API.Services;

namespace MRH.Backend.Customers.Persistence
{
    public static class PersistenceServicesRegistration
    {
        public static void ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        
            services.AddDbContext<PisAppDb>(options =>
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