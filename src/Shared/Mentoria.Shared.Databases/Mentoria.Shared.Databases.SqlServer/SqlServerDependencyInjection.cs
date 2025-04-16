using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Mentoria.Shared.Databases.SqlServer
{
    public static class SqlServerDependencyInjection
    {
        public static IServiceCollection AddSqlServerDbContext<T>(this IServiceCollection serviceCollection,
        Func<IServiceProvider, Task<string>> connectionString)
        where T : DbContext
        {
            return serviceCollection.AddDbContext<T>((serviceProvider, builder) =>
            {
                builder.UseSqlServer(connectionString.Invoke(serviceProvider).Result);
            });
        }

        public static IServiceCollection AddSqlServerHealthCheck(this IServiceCollection serviceCollection,
            Func<IServiceProvider, Task<string>> connectionString)
        {
            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            string sqlServerConnectionString = connectionString.Invoke(serviceProvider).Result;
            serviceCollection.AddHealthChecks().AddSqlServer(sqlServerConnectionString);
            return serviceCollection;
        }
    }
}
