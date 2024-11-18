using API.Configuration;
using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.ML;
using Microsoft.OpenApi.Models;
using Repository;
using Services.Email;

namespace API.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRepository<Usuario>, Repository<Usuario>>();
        services.AddScoped<IRepository<ConsumoEnergetico>, Repository<ConsumoEnergetico>>();
        services.AddScoped<IRepository<DicaEconomia>, Repository<DicaEconomia>>();

        return services;
    }

    public static IServiceCollection AddContext(this IServiceCollection services, APIConfiguration apiConfiguration)
    {
        //Oracle
        services.AddDbContext<OracleDbContext>(options =>
        {
            options.UseOracle(apiConfiguration.Oracle.ConnectionString);
        });

        return services;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection services, APIConfiguration apiConfiguration)
    {
        services.AddSwaggerGen(x =>
        {
            x.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = $"{apiConfiguration.Swagger.Title} {DateTime.Now.Year} ",
                Version = "v1",
                Description = apiConfiguration.Swagger.Description,
                Contact = new OpenApiContact() { Name = apiConfiguration.Swagger.Name, Email = apiConfiguration.Swagger.Email }
            });
        });

        return services;
    }
        
    public static void ConfigureServices(this IServiceCollection services)
    {
        // Registra o serviço de e-mail
        services.AddScoped<IEmailService, EmailService>();
    }

    public static IServiceCollection AddML(this IServiceCollection services)
    {
        // Add the ML.NET prediction engine pool
        services.AddPredictionEnginePool<MLModel.ModelInput, MLModel.ModelOutput>()
            .FromFile("MLModel.mlnet");

        return services;
    }
}