using Ninject.Modules;
using TechnoBee.Licensing.Utilities.DeploymentCore.Services;
using TechnoBee.Licensing.Utilities.LicensingHelper.Services;

namespace TechnoBee.Licensing.Utilities.DeploymentCore.Classes
{
    public class DIContainerInitializationModule : NinjectModule {
        public override void Load() {
            this.Bind<IFileService>().To<FileService>();
            this.Bind<IWmiService>().To<WmiService>();
        }
    }
}