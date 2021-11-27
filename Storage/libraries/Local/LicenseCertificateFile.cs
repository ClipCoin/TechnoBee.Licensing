using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoBee.Licensing.Framework;

using TechnoBee.Licensing.Helpers;

namespace TechnoBee.Counters.Storage
{
    public class LicenseCertificateFile
        : YamlAttributesDocumentSignatureFile<LicenseCertificate>
        , ILicenseCertificateFile
    {
        public ILicenseCertificate Content => Document;
    }
}
