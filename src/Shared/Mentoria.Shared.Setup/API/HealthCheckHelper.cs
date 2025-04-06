using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentoria.Shared.Setup.API
{
    public static class HealthCheckHelper
    {
        public static IConfiguration BuildBasicHealthcheck()
        {
            var myConfiguration = new Dictionary<string, string>
            {
                {"HealthCheckUI:HealthChecks:0:Name", "self"},
                {"HealthCheckUI:HealthChecks:0:uri", "/health"},
            };

            return new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();
        }
    }
}
