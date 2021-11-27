using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Utilities.CertificateTool
{
    public class NoLicensesToPackException
        : CertificateToolException
    {
        private const String MESSAGE = "No license files to pack.";

        public NoLicensesToPackException() 
            : base(MESSAGE)
        {
        }
    }
}
