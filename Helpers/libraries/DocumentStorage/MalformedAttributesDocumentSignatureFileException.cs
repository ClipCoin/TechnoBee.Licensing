using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Helpers
{
    public class MalformedAttributesDocumentSignatureFileException
        : Exception
    {
        private const String DEFAULT_MESSAGE_TEMPLATE = "File '{0}' is not a valid attributes-document-signature file.";
        private const String INNER_EXCEPTION_NOTICE = "See inner exception for details.";

        public MalformedAttributesDocumentSignatureFileException(String fileName)
            : base(String.Format(DEFAULT_MESSAGE_TEMPLATE, fileName))
        {

        }

        public MalformedAttributesDocumentSignatureFileException(String fileName, Exception innerException)
            : base(String.Concat(String.Format(DEFAULT_MESSAGE_TEMPLATE, fileName), INNER_EXCEPTION_NOTICE), innerException)
        {

        }

        public MalformedAttributesDocumentSignatureFileException(String fileName, String detailedMessage)
            : base(String.Concat(String.Format(DEFAULT_MESSAGE_TEMPLATE, fileName), detailedMessage))
        {

        }

        public MalformedAttributesDocumentSignatureFileException(String fileName, String detailedMessage, Exception innerException)
            : base(String.Concat(String.Format(DEFAULT_MESSAGE_TEMPLATE, fileName), detailedMessage, INNER_EXCEPTION_NOTICE), innerException)
        {

        }
    }
}
