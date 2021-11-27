using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.CommandLineUtils;

namespace TechnoBee.Licensing.Utilities.CertificateTool
{
    public class RequiredParameterNotFoundException
        : CommandParameterException
    {
        private const String MESSAGE_TEMPLATE = "Required parameter '{0}' was not found.";

        public RequiredParameterNotFoundException(String command, String parameter)
            : base(command, parameter, String.Format(MESSAGE_TEMPLATE, parameter))
        {

        }

        public RequiredParameterNotFoundException(String command, CommandArgument parameter)
            : this(command, parameter.Name)
        {

        }

        public RequiredParameterNotFoundException(String command, String argument, Exception innerException)
            : base(command, argument, String.Format(MESSAGE_TEMPLATE, argument), innerException)
        {

        }

        public RequiredParameterNotFoundException(String command, CommandArgument argument, Exception innerException)
            : this(command, argument.Name, innerException)
        {

        }
    }
}
