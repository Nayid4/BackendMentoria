using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
