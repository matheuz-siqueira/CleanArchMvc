using System.Reflection;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.Mappings;
using CleanArchMvc.Application.Services;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Infra.Data.Context;
using CleanArchMvc.Infra.Data.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchMvc.Infra.IoC;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, 
        IConfiguration configuration)
    {
        AddContext(services, configuration); 
        AddRepositories(services); 
        AddServices(services); 
        AddMapper(services);
        AddMediator(services);
    }

    public static void AddContext(this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(
                    configuration.GetConnectionString("DefaultConnection"),
                    ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection")),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
                )
            );
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICategoryRepository, CategoryRepository>(); 
        services.AddScoped<IProductRepository, ProductRepository>();
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IProductService, ProductService>();
    }

    public static void AddMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(DomainToDTOMappingProfile));
        services.AddAutoMapper(typeof(DTOToCommandMappingProfile));
    }

    public static void AddMediator(this IServiceCollection services)
    {
        var assembly = AppDomain.CurrentDomain.Load("CleanArchMvc.Application");
        services.AddMediatR(assembly);  
    }
}
