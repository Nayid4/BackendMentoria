using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentoria.Shared.Setup.Services
{
    public static class ServiceBus
    {
        public static void AddServiceBusIntegrationPublisher(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddRabbitMQ(GetRrabitMqSecretCredentials, GetRabbitMQHostName, 
                configuration, "IntegrationPublisher");
            serviceCollection.AddRabbitMQPublisher<IntegrationMessage>();
        }

        private static async Task<RabbitMQCredentials> GetRabbitMqCredentialsdromRabbitMQEngine( IServiceProvider serviceProvider)
        {
            var secretManager = serviceProvider.GetService<ISecretManager>();
            var credentials = await secretManager!.GetRabbitMQCredentials("mentoria-role");
            return new RabbitMQCredentials() { password = credentials.Password, username = credentials.Username };
        }

        public static void AddServiceBusIntegrationConsumer(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddRabbitMQ(GetRabbitMqSecretCredentials, GetRabbitMQHostName, configuration, "IntegrationConsumer");
            serviceCollection.AddRabbitMqConsumer<IntegrationMessage>();
        }

        public static void AddServiceBusDomainPublisher(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddRabbitMQ(GetRabbitMqSecretCredentials, GetRabbitMQHostName, configuration, "DomainPublisher");
            serviceCollection.AddRabbitMQPublisher<DomainMessage>();
        }

        public static void AddServiceBusDomainconsumer(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddRabbitMQ(GetRabbitMqSecretCredentials, GetRabbitMQHostName, configuration, "DomainConsumer");
            serviceCollection.AddRabbitMqConsumer<DomainMessage>();
        }

        public static void AddHandlersInAssembly<T>(this IServiceCollection serviceCollection)
        {
            serviceCollection.Scan(scan => scan.FromAssemblyOf<T>()
                .AddClasses(classes => classes.AssignableTo<IMessageHandler>())
                .AsImplementedInterfaces()
                .WithTransientLifetime()
            );

            ServiceProvider sp = serviceCollection.BuildServiceProvider();
            var listHandlers = sp.GetServices<IMessageHandler>();
            serviceCollection.AddConsumerHandlers(listHandlers);
        }

        private static async Task<string> GetRabbitMQHostName(IServiceProvider serviceProvider)
        {
            var serviceDiscovery = serviceProvider.GetService<IServiceDiscovery>();
            return await serviceDiscovery?.GetFullAddress(DiscoveryServices.RabbitMQ)!;
        }
    }
}
