using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

namespace TechnoBee.Counters.Storage
{
    public static class DependencyInjectionHelpers
    {
        public static IServiceCollection AddLocalCertificateStorage(this IServiceCollection serviceCollection, Action<ILocalCertificateStoreConfig> cfg)
        {


            return serviceCollection;
        }
    }
}
