using BloggerSample.Application.Common.Persistence;
using BloggerSample.Application.Common.Interfaces;
using BloggerSample.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using BloggerSample.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using BloggerSample.Application.Common.Models;

namespace BloggerSample.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureConfigs(
                this IServiceCollection services,
                IConfiguration configuration)
        {
            services.AddSingleton<IDateTimeOffsetProvider, DateTimeOffsetProvider>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped(typeof(IPaginationService<>), typeof(PaginationService<>));

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration
                .GetConnectionString("BlogsConnection")));

            return services;
        }
    }
}
