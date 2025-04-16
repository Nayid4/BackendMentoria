using Mentoria.Shared.Databases.MongoDb;
using Mentoria.Shared.EventSourcing.EventStores;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Mentoria.Shared.EventSourcing
{
    public static class EventSourcingDependecyInjection
    {
        public static void AddMongoEventSourcing(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddTransient(typeof(IAggregateRepository<>), typeof(AggregateRepository<>));
            serviceCollection.AddTransient<IEventStore, EventStore>();
            serviceCollection.AddTransient<IEventStoreManager, MongoEventStoreManager>();
            serviceCollection.Configue<MongoEventStoreConfiguration>(configuration.GetSection("EventSourcing"));
        }
    }
}
