using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mentoria.Shared.Secrets
{
    public static class VaultDependencyInjection
    {
        public static void AddVaultService(this IServiceCollection serviceCollection, IConfiguration configuration, string? discoveredUrl = null)
        {
            serviceCollection.Configure<VaultSettings>(configuration.GetSection("SecretManager"));
            serviceCollection.PostConfigure<VaultSettings>(settings =>
            {
                if (!string.IsNullOrWhiteSpace(discoveredUrl))
                    settings.UpdateUrl(discoveredUrl);
            });

            serviceCollection.AddScoped<ISecretManager, VaultSecretManager>();
        }
    }
}
