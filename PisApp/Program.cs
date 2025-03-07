using FluentValidation.AspNetCore;
using FluentValidation;
using PisApp.API.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();

builder.Services.ConfigurationSwaggerServices   (builder.Configuration);
builder.Services.ConfigurePersistenceServices   (builder.Configuration);
builder.Services.ConfigureAuthenticationServices(builder.Configuration);

builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();

app.UseCors(opt => opt.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
    });
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();