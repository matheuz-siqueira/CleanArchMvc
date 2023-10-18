using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace CleanArchMvc.Infra.IoC;

public static class DependecyInjectionSwagger
{
    public static void AddInfrastructureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c => 
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "CleanArchMvc.API",
                Version = "v1", 
                Description = "Api para um catálogo de produtos",
                Contact = new OpenApiContact
                    {
                    Name = "Matheus Siqueira", 
                    Email = "matheussiqueira.work@gmail.com",
                    Url = new Uri ("https://www.linkedin.com/in/matheussiqueira-me/")
                    }
                });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer", 
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Header de autorização JWT usando o esquema Bearer.\r\n\r\nInforme"
                + "'Bearer'[espaço] e o seu token.\r\n\r\nExemplo: Bearer NDczMjVjMDYtMWM5Yy00MDQ0LWE"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement 
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });
    }   
}
