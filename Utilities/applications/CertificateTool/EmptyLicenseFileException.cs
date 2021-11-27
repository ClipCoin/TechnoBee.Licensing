using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Utilities.CertificateTool
{
    public class EmptyLicenseFileException
        : CertificateToolException
    {
        private const String MESSAGE_TEMPLATE = "License file '{0}' is empty.";

        public EmptyLicenseFileException(String fileName) 
            : base(String.Format(MESSAGE_TEMPLATE, fileName))
        {
            FileName = fileName;
        }

        public readonly String FileName;
    }
}
