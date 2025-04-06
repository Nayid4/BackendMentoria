using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentoria.Shared.Setup.Databases
{
    public static class SqlServer
    {
        public static IServiceCollection AddSqlServer<T>(this IServiceCollection serviceCollection, string databaseName)
            where T : DbContext
        {
            return serviceCollection
                .AddSqlServerContext<T>(serviceProvider =>  GetConnectionString(serviceProvider, databaseName))
                .AddSqlServerHealthCheck(serviceProvider => GetConnectionString(serviceProvider, databaseName));
        }

        public static async Task<string> GetconnectionString(IServiceProvider serviceProvider, string databaseName)
        {
            ISecretManager secretManager = serviceProvider.GetRequiredService<ISecretManager>();
            IServiceDiscovery servieDiscovery =serviceProvider.GetRequiredService<IServiceDiscovery>();

            DiscoveryData sqlServerData = await ServiceDiscovery.GetDiscoveryData(DiscoveryServices.SqlServer);
            SqlServerCredencials credentias = await secretManager.Get<SqlServerCredentials>("sqlserver");

            return $"Server={sqlServerData.Address};Database={databaseName};User Id={credentias.User};Password={credentias.Password};Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";
        }

        private record SqlServerCredentials
        {
            public string User { get; init; } = string.Empty;
            public string Password { get; init; } = string.Empty;
        }
    }
}
