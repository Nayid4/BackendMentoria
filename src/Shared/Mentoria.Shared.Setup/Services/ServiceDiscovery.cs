using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentoria.Shared.Setup.Services
{
    public static class ServiceDiscovery
    {
        public static void AddServiceDiscovery(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddServiceDiscovery(configuration);
        }
    }
}
