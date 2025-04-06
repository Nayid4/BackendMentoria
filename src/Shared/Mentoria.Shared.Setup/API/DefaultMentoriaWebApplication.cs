using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentoria.Shared.Setup.API
{
    public static class DefaultMentoriaWebApplication
    {
        public static WebApplication Create(string[] args, Action<WebApplicationBuilder>? webappBuilder = null)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            
            builder.Configuration.AddConfiguration(HealthCheckHelper.BuildBasicHealthcheck());
            builder.Services.AddHealthChecks();
            builder.Services.AddHealthChecksUI().AddInMemoryStorage();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddRouting(x => x.LowercaseUrls = true);
            builder.Services.AddOpenApi();
            //builder.Services.AddSerializer();

            /*
            builder.Services.AddServiceDiscovery(builder.Configuration);
            builder.Services.AddSecrectManager(builder.Configuration);
            builder.Services.AddLogging(logger => logger.AddSerilog());
            builder.Services.AddTracing(builder.Configuration);
            builder.Services.AddMetrics(builder.Configuration);
            */

            if (webappBuilder != null)
            {
                webappBuilder.Invoke(builder);
            }

            return builder.Build();
        }

        public static void Run(WebApplication webApp)
        {
            if (webApp.Environment.IsDevelopment())
            {
                webApp.MapOpenApi();
            }

            webApp.MapHealthChecks("/health");

            webApp.MapHealthChecks("/health", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            webApp.UseHealthChecksUI(config =>
            {
                config.UIPath = "health-ui";
            });

            webApp.UseHttpsRedirection();
            webApp.UseAuthorization();
            webApp.MapControllers();
            webApp.Run();
        }
    }
}
