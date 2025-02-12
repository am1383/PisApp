using Microsoft.OpenApi.Models;

namespace MRH.Backend.Customers.Persistence
{
    public static class ConfigurationSwaggerServicesRegistration
    {
        public static void ConfigurationSwaggerServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PisApp API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name          = "Authorization",
                    Type          = SecuritySchemeType.ApiKey,
                    Scheme        = "Bearer",
                    BearerFormat  = "JWT",
                    In            = ParameterLocation.Header,
                    Description   = "Please enter your Bearer token"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id   = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
        }
    }
}