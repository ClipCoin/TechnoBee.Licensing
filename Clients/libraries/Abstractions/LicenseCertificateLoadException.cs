using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TechnoBee.Licensing.Framework;

namespace TechnoBee.Licensing.Clients
{
    public class LicenseCertificateLoadException
        : LicensingException
    {
        private const String DEFAULT_MESSAGE = "Failed to load license certificate.";

        public LicenseCertificateLoadException(String message)
            : base(ComposeMessage(DEFAULT_MESSAGE, message))
        {

        }

        public LicenseCertificateLoadException(String message, Exception innerException)
            : base(ComposeMessage(DEFAULT_MESSAGE, message), innerException)
        {

        }

        public LicenseCertificateLoadException(Exception innerException)
            : base(DEFAULT_MESSAGE, innerException)
        {

        }
    }
}
