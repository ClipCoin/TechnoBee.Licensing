using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Utilities.CertificateTool
{
    public class CertificateToolException
        : ExceptionBase
    {
        public CertificateToolException()
            : base()
        {

        }

        public CertificateToolException(String message)
            : base(message)
        {

        }

        public CertificateToolException(String message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
