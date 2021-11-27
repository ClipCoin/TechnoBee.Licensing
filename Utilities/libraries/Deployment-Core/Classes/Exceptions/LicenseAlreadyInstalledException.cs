using System;

namespace TechnoBee.Licensing.Utilities.DeploymentCore.Exceptions
{
    public sealed class LicenseAlreadyInstalledException : LicenseInstallationException {
        public LicenseAlreadyInstalledException() : base() {
        }

        public LicenseAlreadyInstalledException(string message) : base(message) {
        }

        public LicenseAlreadyInstalledException(string message, Exception ex) : base(message, ex) {
        }
    }
}
