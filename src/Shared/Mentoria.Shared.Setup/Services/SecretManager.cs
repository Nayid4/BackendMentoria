using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentoria.Shared.Setup.Services
{
    public static class SecretManager
    {
        public static void AddSecretManager(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            string discoveredUrl = GetVaultUrl(serviceCollection.BuildServiceProvider()).Result;
            serviceCollection.AddVaultService(configuration, discoveredUrl);
        }

        private static async Task<string> GetVaultUrl(IServiceProvider serviceProvider)
        {
            var serviceDiscovery = serviceProvider.GetService<IServiceDiscovery>();
            return await serviceDiscovery?.GetFullAddress(DiscoveryServices.Secrets)!;
        }
    }
}
