using System;
using System.Collections.Generic;
using System.Text;

namespace TechnoBee.Licensing.Utilities.DeploymentCore.Exceptions
{
    public class DeploymentBaseException : Exception {
        public DeploymentBaseException() : base() {
        }

        public DeploymentBaseException(string message) : base(message) {
        }

        public DeploymentBaseException(string message, Exception ex) : base(message, ex) {
        }
    }
}
