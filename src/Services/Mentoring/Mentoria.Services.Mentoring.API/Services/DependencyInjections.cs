
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Mentoria.Services.Mentoring.API.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Mentoria.Services.Mentoring.API.Services
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddOpenApi();
            services.AddEndpointsApiExplorer();
            services.AddTransient<GlobalExceptionHandlingMiddleware>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = configuration["Jwt:Audience"],
                    ValidIssuer = configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
                };
            });

            services.AddCors(options =>
            {
                
                options.AddPolicy("web", policyBuilder =>
                {
                    policyBuilder.WithOrigins(
                        "http://localhost:4000",
                        "https://localhost:4000",
                        "http://localhost:3000",
                        "https://localhost:3000",
                        "http://localhost:4200",
                        "https://localhost:4200",
                        "https://localhost:8500",
                        "https://localhost:80"
                        );
                    policyBuilder.AllowAnyHeader();
                    policyBuilder.AllowAnyMethod();
                    policyBuilder.AllowCredentials();
                });
                

                /*options.AddPolicy("web", policyBuilder =>
                {
                    policyBuilder.AllowAnyOrigin();
                    policyBuilder.AllowAnyHeader();
                    policyBuilder.AllowAnyMethod();
                });*/

            });

            return services;
        }
    }
}
