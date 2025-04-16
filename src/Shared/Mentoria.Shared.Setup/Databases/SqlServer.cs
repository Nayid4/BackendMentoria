using Microsoft.EntityFrameworkCore;
using Mentoria.Shared.Databases.SqlServer;

namespace Mentoria.Shared.Setup.Databases
{
    public static class SqlServer
    {
        public static IServiceCollection AddSqlServer<T>(this IServiceCollection serviceCollection, string databaseName)
            where T : DbContext
        {
            return serviceCollection
                .AddSqlServerDbContext<T>(serviceProvider =>  GetConnectionString(serviceProvider, databaseName))
                .AddSqlServerHealthCheck(serviceProvider => GetConnectionString(serviceProvider, databaseName));
        }

        public static async Task<string> GetConnectionString(IServiceProvider serviceProvider, string databaseName)
        {
            ISecretManager secretManager = serviceProvider.GetRequiredService<ISecretManager>();
            IServiceDiscovery serviceDiscovery = serviceProvider.GetRequiredService<IServiceDiscovery>();

            DiscoveryData sqlServerData = await serviceDiscovery.GetDiscoveryData(DiscoveryServices.SqlServer);
            SqlServerCredentials credentias = await secretManager.Get<SqlServerCredentials>("sqlserver");

            return $"Server={sqlServerData.Server};Database={databaseName};User Id={credentias.User};Password={credentias.Password};Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";
        }

        private record SqlServerCredentials
        {
            public string User { get; init; } = string.Empty;
            public string Password { get; init; } = string.Empty;
        }
    }
}
