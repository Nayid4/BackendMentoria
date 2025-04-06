using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentoria.Shared.Setup.API.Key
{
    public static class ApiKeyDependencyInjection
    {
        public static IServiceCollection AddApiToken(this IServiceCollection services, IConfiguration configuration)
        {
            return services.Configure<ApiKeyConfiguration>(configuration.GetSection("ApiKey"));
        }

        public static void UseApiTokenMiddleware(this WebApplication webApp)
        {
            // Do not act on /health ot /health-ui
            webApp.UseWhen(context => !context.Request.Path.StartsWithSegments("/health"),
                appBuilder => appBuilder.UseMiddleware<ApiKeyMiddleware>()
            );
        }
    }
}
