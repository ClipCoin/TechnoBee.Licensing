using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Utilities.CertificateTool
{
    public class UserFriendlyException
        : CertificateToolException
    {
        public UserFriendlyException()
            : base()
        {

        }

        public UserFriendlyException(String message)
            : base(message)
        {

        }

        public UserFriendlyException(String message, Exception innerException)
            : base(message, innerException)
        {

        }

        //public override string Message => ComposeMessage23(" ");

        public String ComposeMessage(String separator)
        {
            List<String> messageParts = new List<string>();

            UserFriendlyException ex = this;

            while (ex != null)
            {
                if (!String.IsNullOrEmpty((ex as Exception).Message))
                {
                    messageParts.Add((ex as Exception).Message);
                }

                ex = ex.InnerException as UserFriendlyException;
            }

            return String.Join(separator, messageParts);
        }
    }
}
