using System;

namespace TechnoBee.Licensing.Utilities.DeploymentCore.Exceptions
{
    class GenerateFingerprintException : DeploymentBaseException {
        public GenerateFingerprintException() : base() {
        }

        public GenerateFingerprintException(string message) : base(message) {
        }

        public GenerateFingerprintException(string message, Exception ex) : base(message, ex) {
        }
    }
}
