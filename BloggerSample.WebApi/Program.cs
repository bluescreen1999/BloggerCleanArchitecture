using BloggerSample.Infrastructure;
using BloggerSample.Application;
using BloggerSample.WebApi;
using BloggerSample.WebApi.Filters;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using BloggerSample.Application.Blogs.Commands.Add;
using BloggerSample.Application.Common.Models;
using BloggerSample.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        builder =>
        {
            builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});

builder.Services
    .AddApplicationConfigs()
    .AddInfrastructureConfigs(builder.Configuration)
    .AddApiConfigs();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterAssemblyTypes(typeof(AddBlogService).Assembly)
        .AssignableTo<IService>()
        .AsImplementedInterfaces()
        .InstancePerLifetimeScope();

    builder.RegisterAssemblyTypes(typeof(BlogRepository).Assembly)
        .AssignableTo<IRepository>()
        .AsImplementedInterfaces()
        .InstancePerLifetimeScope();
});

builder.Services.AddControllers(options => options.Filters.Add<ApiExceptionFilterAttribute>());

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddRouting();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();