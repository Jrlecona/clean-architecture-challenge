using Microsoft.OpenApi.Models;
using System.Reflection;
using CleanArchitectureChallenge.Domain.Interfaces;
using CleanArchitectureChallenge.Infrastructure.Repositories;
using CleanArchitectureChallenge.Application.Interfaces;
using CleanArchitectureChallenge.Application.Services;

namespace CleanArchitectureChallenge.API;

/// <summary>
/// Configures services and the middleware pipeline for the Clean Architecture Challenge API.
/// </summary>
public class Startup
{
    /// <summary>
    /// Registers services into the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    public void ConfigureServices(IServiceCollection services)
    {
        // Register MVC Controllers
        services.AddControllers();

        // ✅ Register Swagger with XML Comments and Annotations
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc(
                "v1",
                new OpenApiInfo
                {
                    Title = "Clean Architecture Challenge API",
                    Version = "v1",
                    Description = "Clean Architecture Challenge API using ASP.NET Core",
                    Contact = new OpenApiContact
                    {
                        Name = "Ruben Lecona",
                        Email = "jrlecona@gmail.com",
                        Url = new Uri("https://github.com/jrlecona")
                    }
                }
            );

            // ✅ Enable [SwaggerOperation] attributes (requires Swashbuckle.Annotations)
            c.EnableAnnotations();
        });

        // 💉 Dependency Injection
        services.AddSingleton<IProductRepository, ProductRepository>(); // In-memory example
        services.AddScoped<IProductService, ProductService>(); // Service layer
    }

    /// <summary>
    /// Configures the HTTP request pipeline (middleware).
    /// </summary>
    /// <param name="app">Application builder.</param>
    /// <param name="env">Hosting environment.</param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            // Shows detailed errors in development
            app.UseDeveloperExceptionPage();
        }

        // 🌐 Enable middleware to serve generated Swagger as a JSON endpoint
        app.UseSwagger();

        // 🌐 Enable middleware to serve Swagger UI
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Clean Architecture Challenge API v1");
            c.RoutePrefix = string.Empty; // 👈 Sets Swagger UI at root
        });

        app.UseRouting();

        app.UseAuthorization();

        // 📌 Maps controller endpoints
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
