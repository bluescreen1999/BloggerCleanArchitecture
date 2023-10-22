using BloggerSample.Application.Blogs.Commands.Add;
using BloggerSample.Application.Common.Behaviours;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using MediatR;

namespace BloggerSample.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationConfigs(
            this IServiceCollection services)
        {
            services.AddScoped<IAddBlogService, AddBlogService>();
            
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(_ =>
            {
                _.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                _.AddBehavior(
                    typeof(IPipelineBehavior<,>),
                    typeof(ValidationBehaviour<,>));
            });

            return services;
        }
    }
}
