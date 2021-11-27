using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Helpers
{
    public class AttributesDocumentSignatureFileSaveException
        : Exception
    {
        private const String MESSAGE_TEMPLATE = "Failed to save file '{0}'.";
        private const String INNER_EXCEPTION_NOTICE = "See inner exception for details.";

        public AttributesDocumentSignatureFileSaveException(String fileName, Exception innerException)
            : base(String.Concat(String.Format(MESSAGE_TEMPLATE, fileName), INNER_EXCEPTION_NOTICE), innerException)
        {

        }
    }
}
