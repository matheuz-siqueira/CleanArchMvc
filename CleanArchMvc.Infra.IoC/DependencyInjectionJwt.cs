using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CleanArchMvc.Infra.IoC;

public static class DependencyInjectionJwt
{
   public static void AddInfrastructureJwt(this IServiceCollection services, 
    IConfiguration configuration)
    {
        var JWTKey = Encoding.ASCII.GetBytes(configuration["JWT:KEY"]);
        services.AddAuthentication(opt => 
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options => 
        {
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false, 
                ValidateAudience = false, 
                ValidateLifetime = true, 
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(JWTKey),
                ClockSkew = TimeSpan.Zero
            };
        });
    }     
}
