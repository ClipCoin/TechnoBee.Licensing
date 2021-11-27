using System;
using System.Collections.Generic;
using System.Text;

namespace TechnoBee.Licensing.Utilities.DeploymentCore.Exceptions
{
    public sealed class LicenseNotFoundException : LicenseInstallationException {
        public LicenseNotFoundException() : base() {
        }

        public LicenseNotFoundException(string message) : base(message) {
        }

        public LicenseNotFoundException(string message, Exception ex) : base(message, ex) {
        }
    }
}
