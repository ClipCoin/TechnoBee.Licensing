using System;
using System.Collections.Generic;
using System.Text;

namespace TechnoBee.Licensing.Utilities.DeploymentAbstractions {
    public enum Command {
        DisplayLicense = 1,
        GenerateLicense = 2,
        InstallLicense = 3,
        ForceToInstallLicense = 4,
        DisplayHelpInformation = 5, 
    }
}
