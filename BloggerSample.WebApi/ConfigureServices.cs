using Microsoft.OpenApi.Models;

namespace BloggerSample.WebApi
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApiConfigs(
            this IServiceCollection services)
        {
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1",
                    new OpenApiInfo 
                    { 
                        Title = "Blogger Sample",
                        Version = "v1"
                    });
            });

            return services;
        }
    }
}
