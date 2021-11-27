using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Utilities.DeploymentCore.Classes.Commands {
    public abstract class BaseCommandProvider {
        public static IKernel AppKernel;

        public BaseCommandProvider() {
            InitializeServiceProvider();
        }

        private void InitializeServiceProvider() {
            AppKernel = new StandardKernel(new DIContainerInitializationModule());
        }
    }
}
