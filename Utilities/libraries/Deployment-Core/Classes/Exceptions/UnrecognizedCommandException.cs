using System;
using System.Collections.Generic;
using System.Text;

namespace TechnoBee.Licensing.Utilities.DeploymentCore.Exceptions
{
    class UnrecognizedCommandException : DeploymentBaseException
    {
        public UnrecognizedCommandException() : base()
        {
        }

        public UnrecognizedCommandException(string message) : base(message)
        {
        }

        public UnrecognizedCommandException(string message, Exception ex) : base(message, ex)
        {
        }
    }
}
