using Microsoft.OpenApi.Models;

namespace PisApp.API.Persistence
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
                    Description   = "Please Enter Your Bearer Token"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type  = ReferenceType.SecurityScheme,
                                Id    = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
        }
    }
}