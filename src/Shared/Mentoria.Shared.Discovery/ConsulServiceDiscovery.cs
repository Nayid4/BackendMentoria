using Consul;
using Microsoft.Extensions.Caching.Memory;
using System.Text;

namespace Mentoria.Shared.Discovery
{
    public record DiscoveryData(string Server, int Port);

    public interface IServiceDiscovery
    {
        Task<string> GetFullAddress(string serviceKey, CancellationToken cancellationToken = default);
        Task<DiscoveryData> GetDiscoveryData(string serviceKey, CancellationToken cancellationToken = default);
    }

    public class ConsulServiceDiscovery : IServiceDiscovery
    {
        private readonly IConsulClient _client;
        private readonly MemoryCache _cache;

        public ConsulServiceDiscovery(IConsulClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _cache = new MemoryCache(new MemoryCacheOptions());
        }

        public async Task<string> GetFullAddress(string serviceKey, CancellationToken cancellationToken = default)
        {
            if (_cache.TryGetValue(serviceKey, out DiscoveryData cacheData))
            {
                return GetAddressFromData(cacheData);
            }

            DiscoveryData data = await GetDiscoveryData(serviceKey, cancellationToken);
            return GetAddressFromData(data);
        }

        public async Task<DiscoveryData> GetDiscoveryData(string serviceKey, CancellationToken cancellationToken = default)
        {
            var services = await _client.Catalog.Service(serviceKey, cancellationToken);

            if (services.Response != null && services.Response.Any())
            {
                var service = services.Response.First();

                DiscoveryData data = new DiscoveryData(service.ServiceAddress, service.ServicePort);

                AddtoCache(serviceKey, data);

                return data;
            }

            throw new ArgumentException($"seems like the service your are trying to access ({serviceKey}) does not exist.");
        }

        private string GetAddressFromData(DiscoveryData data)
        {
            StringBuilder serviceAddress = new StringBuilder();
            serviceAddress.Append(data.Server);

            if (data.Port != 0)
            {
                serviceAddress.Append($":{data.Port}");
            }

            string serviceAddressString = serviceAddress.ToString();

            return serviceAddressString;
        }

        private void AddtoCache(string serviceKey, DiscoveryData serviceAddress)
        {
            _cache.Set(serviceKey, serviceAddress);
        }
        
    }
}
