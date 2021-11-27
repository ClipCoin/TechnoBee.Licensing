using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Framework
{
    public abstract class LicensingException
        : Exception
    {
        private const String INNER_EXCEPTION_HINT = "See inner exception for details.";

        public LicensingException()
        {

        }

        public LicensingException(String message)
            : base(message)
        {

        }

        public LicensingException(String message, Exception innerException)
            : base(ComposeMessage(message, INNER_EXCEPTION_HINT), innerException)
        {

        }

        protected static String ComposeMessage(params String[] messageParts)
        {
            return String.Join(" ", messageParts);
        }
    }
}
