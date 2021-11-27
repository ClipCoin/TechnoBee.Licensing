using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Utilities.CertificateTool
{
    public class CommandParameterException
        : CommandException
    {
        public CommandParameterException(String command, String parameter, String message)
            : base(command, message)
        {
            Parameter = parameter;
        }

        public CommandParameterException(String command, String parameter, String message, Exception innerException)
            : base(command, message, innerException)
        {
            Parameter = parameter;
        }

        public readonly String Parameter;
    }
}
