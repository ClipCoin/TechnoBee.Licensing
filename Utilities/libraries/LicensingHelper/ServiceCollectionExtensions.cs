using Microsoft.Extensions.DependencyInjection;
using TechnoBee.Licensing.Utilities.LicensingHelper.Services;

namespace TechnoBee.Licensing.Utilities.LicensingHelper
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLicensingHelpers(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddSingleton<IWmiService, WmiService>();
        }
    }
}
