using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TechnoBee.Licensing.Framework;

namespace TechnoBee.Counters.Storage
{
    public class LicenseCertificateDocumentFileCorruptException
        : LicensingException
    {
        private const String DEFAULT_MESSAGE = "License certificate document is currupt.";

        public LicenseCertificateDocumentFileCorruptException(Exception innerException)
            : base(DEFAULT_MESSAGE, innerException)
        {

        }
    }
}
