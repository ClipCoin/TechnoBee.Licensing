using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Utilities.CertificateTool
{
    public class HostInformationDigestFileNotFoundException
        : CertificateToolException
    {
        private const String MESSAGE_TEMPLATE = "Host information digest file '{0}' not found.";

        public HostInformationDigestFileNotFoundException(String filePath) 
            : base(String.Format(MESSAGE_TEMPLATE, filePath))
        {
            FilePath = filePath;
        }

        public readonly String FilePath;
    }
}
