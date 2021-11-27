using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Utilities.CertificateTool
{
    public class CertificateValidationException
        : CertificateToolException
    {
        public CertificateValidationException(Exception innerException)
            : base("no message", innerException)
        {

        }
    }
}
