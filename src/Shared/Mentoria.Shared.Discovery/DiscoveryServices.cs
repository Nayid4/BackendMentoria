namespace Mentoria.Shared.Discovery
{
    public class DiscoveryServices
    {
        public const string RabbitMQ = "RabbitMQ";  
        public const string Secrets = "SecretManager";
        public const string SqlServer = "SqlServer";
        public const string MongoDb = "MongoDb";
        public const string Graylog = "Graylog";
        public const string OpenTelemetry = "OpenTelemetryCollector";

        public class Microservices
        {
            public const string Emails = "EmailsApi";
            public const string Orders = "OrdersApi";
            public const string Subscriptions = "SubscriptionsApi";

            public class ProductsApi
            {
                public const string ApiRead = "ProductsApiRead";
                public const string apiWrite = "ProductsapiWrite";
            }
        }

    }
}
