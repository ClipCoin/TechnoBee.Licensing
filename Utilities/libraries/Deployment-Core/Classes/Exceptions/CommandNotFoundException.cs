using System;
using System.Collections.Generic;
using System.Text;

namespace TechnoBee.Licensing.Utilities.DeploymentCore.Exceptions
{
    class CommandNotFoundException : ApplicationException
    {
        public CommandNotFoundException() : base() {
        }

        public CommandNotFoundException(string message) : base(message) {
        }

        public CommandNotFoundException(string message, Exception ex) : base(message, ex) {
        }
    }
}
