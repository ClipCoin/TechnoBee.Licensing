using System;
using System.Collections.Generic;
using System.Text;

namespace TechnoBee.Licensing.Utilities.DeploymentCore.Exceptions
{
    public class LicenseInstallationException : DeploymentBaseException{

        public LicenseInstallationException() : base() {
        }

        public LicenseInstallationException(string message) : base(message) {
        }

        public LicenseInstallationException(string message, Exception ex) : base(message, ex) {
        }
    }
}
