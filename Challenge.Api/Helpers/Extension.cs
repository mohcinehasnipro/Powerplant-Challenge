using Challenge.Service.Interfaces;
using Challenge.Service.Services;
using Microsoft.OpenApi.Models;
using Serilog;

namespace Challenge.Helpers
{
    public static class Extension
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IProductionPlanService, ProductionPlanService>();
        }

        public static void RegisterSeriLog()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("serilogconfig.json")
                .Build();
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .CreateLogger();
        }

        public static void RegisterSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1",  new OpenApiInfo{ Title = "CHALLENGE", Version = "v1", Description = "Using .Net6" });
            });
        }
    }
}
