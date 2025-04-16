using Mentoria.Shared.EventSourcing;
using Microsoft.Extensions.Configuration;

namespace Mentoria.Shared.Setup.Services
{
    public static class EventSourcing
    {
        public static void AddEventSourcing(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddMongoEventSourcing(configuration);
        }
    }
}
