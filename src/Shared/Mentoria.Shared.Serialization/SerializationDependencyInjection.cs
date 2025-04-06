using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentoria.Shared.Serialization
{
    public static class SerializationDependencyInjection
    {
        public static void AddSerializer(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ISerializer, Serializer>();
        }
    }
}
