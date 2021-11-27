using System;
using Microsoft.Extensions.DependencyInjection;

using static System.Reflection.Assembly;
using static System.IO.Path;
using TechnoBee.Licensing.Utilities.DeploymentCore.Commands;
using TechnoBee.Licensing.Utilities.DeploymentCore;


namespace TechnoBee.Licensing.Clients
{
    //[Guid("226DDDD0-E770-4A58-9EF4-6CA85BC2C410")]
    //[ComVisible(true)]
    //[ClassInterface(ClassInterfaceType.None)]
    public class LicenseFacade : ILicenseFacade
    {
        public bool CheckLicense(string LicenseFileName, string LicenseApplicationName, string LicenseProductGuid, string LicenseProductVersion)
        {
            return ClientServicesResolver.Provider.Value.GetRequiredService<ILicensing>()
                .AttachRegistry(builder =>
                {
                    builder.AddLocalStore(cfg =>
                    {
                        cfg.AddFile(Combine(GetDirectoryName(GetExecutingAssembly().Location), LicenseFileName));
                    });
                })
                .ConstructApplicationLicenseQuery()
                .ForProduct(LicenseProductGuid)
                .ForProductVersion(LicenseProductVersion)
                .ForApplication(LicenseApplicationName)
                .ForThisHost()
                .EffectiveOn(DateTime.Now)
                .YieldSingle().LicenseState == Framework.FeatureLicenseState.Enabled;
        }

        public bool GenerateFingerprint(string outputFilepath, bool overwriteFile = true)
        {
            GenerateFingerprintOptions options = new GenerateFingerprintOptions()
            {
                Path = outputFilepath,
                OverwriteExistingFile = true
            };

            var command = ClientServicesResolver.Provider.Value.GetRequiredService<GenerateFingerprintCommand>();
            command.InputData = options;

            return command.Execute().Success;
        }
    }
}