using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Utilities.CertificateTool
{
    public class CertificateToolUsageException
        : CertificateToolException
    {
        public CertificateToolUsageException(String message)
            : base(message)
        {

        }

        public CertificateToolUsageException(String message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
