using Microsoft.Extensions.DependencyInjection;
using TechnoBee.Licensing.Utilities.Deployment.Classes;
using TechnoBee.Licensing.Utilities.Deployment_Sdk;
using TechnoBee.Licensing.Utilities.DeploymentCore.Commands;
using TechnoBee.Licensing.Utilities.LicensingHelper;

namespace TechnoBee.Licensing.Utilities.DeploymentTool
{
    class Program
    {
        static int Main( string[] args )
        {
            return new ServiceCollection()
                .AddSingleton<DeploymentToolApplication>()
                .AddTransient<GenerateFingerprintCommand>()
                .AddTransient<InstallLicenseCommand>()
                .AddTransient<DeploymentLogger>()
                .AddLicensingHelpers()
                .AddFileService()
                .AddHostInformationDigestProvider()
                .BuildServiceProvider()
                .GetRequiredService<DeploymentToolApplication>()
                .Execute(args);
        }
    }
}
