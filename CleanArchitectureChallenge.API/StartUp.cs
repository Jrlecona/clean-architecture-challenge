using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CleanArchitectureChallenge.Domain.Interfaces;
using CleanArchitectureChallenge.Infrastructure.Repositories;
using CleanArchitectureChallenge.Application.Interfaces;
using CleanArchitectureChallenge.Application.Services;
using Microsoft.Extensions.Options;

namespace CleanArchitectureChallenge.API;

/// <summary>
/// Configures services and the middleware pipeline for the Clean Architecture Challenge API.
/// </summary>
public class Startup(IConfiguration configuration)
{
    public IConfiguration Configuration { get; } = configuration;

    /// <summary>
    /// Registers services into the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    public void ConfigureServices(IServiceCollection services)
    {
        //Config JWT
        var jwtSettings = Configuration.GetSection("JwtSettings");
        var key = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]);

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings["Issuer"],
                        ValidAudience = jwtSettings["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    };
                });
        services.AddAuthorization();
        
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

        //services.AddEndpointsApiExplorer();

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

        app.UseAuthentication();
        app.UseAuthorization();

        // 📌 Maps controller endpoints
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
