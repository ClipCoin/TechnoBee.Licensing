using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Utilities.CertificateTool
{
    public class CommandException
        : UserFriendlyException
    {
        public CommandException(String command, String message)
            : base(message)
        {
            Command = command;
        }
        public CommandException(String command, String message, Exception innerException)
            : base(message, innerException)
        {
            Command = command;
        }

        public readonly String Command;
    }
}
