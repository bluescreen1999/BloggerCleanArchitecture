using BloggerSample.Application.Blogs.Queries.GetDetails;
using BloggerSample.Application.Blogs.Commands.Edit;
using BloggerSample.Application.Blogs.Commands.Add;
using BloggerSample.Application.Common.Behaviours;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using MediatR;
using BloggerSample.Application.Blogs.Commands.Delete;
using BloggerSample.Application.Blogs.Queries.GetAll;

namespace BloggerSample.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationConfigs(
        this IServiceCollection services)
    {
        //RegisterBlogServices(services);

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

    //private static void RegisterBlogServices(IServiceCollection services)
    //{
    //    services.AddScoped<IAddBlogService, AddBlogService>();
    //    services.AddScoped<IGetBlogDetailsService, GetBlogDetailsService>();
    //    services.AddScoped<IEditBlogService, EditBlogService>();
    //    services.AddScoped<IDeleteBlogService, DeleteBlogService>();
    //    services.AddScoped<IGetAllBlogsService, GetAllBlogsService>();
    //}
}
