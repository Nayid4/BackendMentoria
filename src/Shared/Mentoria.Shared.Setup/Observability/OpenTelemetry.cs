using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Mentoria.Shared.Setup.Observability
{
    public static class OpenTelemetry
    {
        private static string? _openTelemetryUrl;

        public static void AddTracing(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddOpenTelemetryTracing(builder => builder
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(configuration["AppName"]))
                .AddAspNetCoreInstrumentation()
                .AddOtlpExporter(exporter =>
                {
                    string url = GetOpenTelemetryCollectionUrl(serviceCollection.BuildServiceProvider()).Result;
                    exporter.Endpoint = new Uri(url);
                })
            );
        }



        private static async Task<string> GetOpenTelemetryCollectionUrl(IServiceProvider serviceProvider)
        {
            if(_openTelemetryUrl != null)
                return _openTelemetryUrl;

            var serviceDiscovery = serviceProvider.GetService<IServiceDiscovery>();
            string openTelemetryLocation = await serviceDiscovery?.GetFullAddress(DiscoveryServices.OpenTelemetry)!;
            _openTelemetryUrl = $"http://{openTelemetryLocation}";
            return _openTelemetryUrl;
        }
    }
}
