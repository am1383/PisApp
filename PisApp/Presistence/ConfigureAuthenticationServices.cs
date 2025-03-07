using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace PisApp.API.Persistence
{
    public static class AuthenticationServiceRegistration
    {
        public static void ConfigureAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme    = "JwtBearer";
            }).AddJwtBearer("JwtBearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer    = true,
                    ValidateAudience  = true,
                    ValidateLifetime  = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer      = configuration["JwtSettings:Issuer"],
                    ValidAudience    = configuration["JwtSettings:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["JwtSettings:Secret"]!)
                    )
                };
            });
        }
    }
}