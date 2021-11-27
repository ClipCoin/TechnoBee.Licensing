using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

namespace TechnoBee.Licensing.Utilities.Fingerprints
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFingerprints(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddTransient<IFingerprintAlgorithmProvider, FingerprintAlgorithmProvider>();

            return serviceCollection;
        }
    }
}
