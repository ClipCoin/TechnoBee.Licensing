using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Helpers
{
    public class AttributesDocumentSignatureFileLoadException
        : Exception
    {
        private const String MESSAGE_TEMPLATE = "Failed to load file '{0}'.";
        private const String INNER_EXCEPTION_NOTICE = "See inner exception for details.";

        public AttributesDocumentSignatureFileLoadException(String fileName, Exception innerException)
            : base(String.Concat(String.Format(MESSAGE_TEMPLATE, fileName), INNER_EXCEPTION_NOTICE), innerException)
        {

        }
    }
}
