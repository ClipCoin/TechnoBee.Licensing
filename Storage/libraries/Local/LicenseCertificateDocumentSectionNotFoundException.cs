using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TechnoBee.Licensing.Framework;

namespace TechnoBee.Counters.Storage
{
    public class LicenseCertificateDocumentSectionNotFoundException
        : LicensingException
    {
        private const String MESSAGE_TEMPLATE = "Section '{0}' is missing in the document.";

        public LicenseCertificateDocumentSectionNotFoundException(String sectionName)
            : base(String.Format(MESSAGE_TEMPLATE, sectionName))
        {

        }

        public LicenseCertificateDocumentSectionNotFoundException(String sectionName, Exception innerException)
            : base(String.Format(MESSAGE_TEMPLATE, sectionName), innerException)
        {

        }
    }
}
