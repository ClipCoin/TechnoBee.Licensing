using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

namespace TechnoBee.Licensing.Clients
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLicensing(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ILicensing, LicensingService>();

            return serviceCollection;
        }
    }
}
