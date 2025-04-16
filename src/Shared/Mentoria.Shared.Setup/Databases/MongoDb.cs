using Mentoria.Shared.Databases.MongoDb;
using Microsoft.Extensions.Configuration;

namespace Mentoria.Shared.Setup.Databases
{
    public static class MongoDb
    {
        public static IServiceCollection AddMentoriaMongoDbConnectionProvider(this IServiceCollection serviceCollection, IConfiguration configuration, string name = "mongodb")
        {
            return serviceCollection
                .AddMongoDbConnectionProvider()
                .AddMongoDbDatabaseConfiguration(configuration)
                .AddMongoHealthCheck(name);
        }
    }
}
