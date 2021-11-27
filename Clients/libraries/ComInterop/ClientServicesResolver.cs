using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoBee.Licensing.Utilities.Deployment.Classes;
using TechnoBee.Licensing.Utilities.Fingerprints;
using TechnoBee.Licensing.Utilities.LicensingHelper;
using System.Runtime.CompilerServices;
using TechnoBee.Licensing.Utilities.DeploymentCore.Commands;

[assembly: InternalsVisibleTo("TechnoBee.Licensing.Clients-ComInterop_Tests")]

namespace TechnoBee.Licensing.Clients
{
    static internal class ClientServicesResolver
    {
        static public readonly Lazy<ServiceProvider> Provider
            = new Lazy<ServiceProvider>(() => {
                return new ServiceCollection()
                    .AddLicensing()
                    .AddHostInformationDigestProvider()
                    .AddTransient<GenerateFingerprintCommand>()
                    .AddLicensingHelpers()
                    .AddFileService()
                    .AddFingerprints()
                    .BuildServiceProvider();
            });
    }
}