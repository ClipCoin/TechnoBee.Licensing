using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Utilities.DeploymentCore {
    public sealed class GenerateFingerprintOptions {
        public string Path { get; set; }
        public Boolean OverwriteExistingFile { get; set; }
    }
}
