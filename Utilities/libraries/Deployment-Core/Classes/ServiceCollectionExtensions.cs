using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TechnoBee.Licensing.Utilities.DeploymentCore.Services;

namespace TechnoBee.Licensing.Utilities.Deployment.Classes
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHostInformationDigestProvider(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddSingleton<IHostInformationDigestProvider, HostInformationDigestProvider>();
        }
        public static IServiceCollection AddFileService( this IServiceCollection serviceCollection )
        {
            return serviceCollection.AddSingleton<IFileService, FileService>();
        }

    }
}
