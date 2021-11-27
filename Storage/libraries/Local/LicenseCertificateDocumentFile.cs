using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using YamlDotNet;
using YamlDotNet.Serialization.NamingConventions;

using YamlDeserializer = YamlDotNet.Serialization.Deserializer;

using TechnoBee.Licensing.Framework;

using TechnoBee.Licensing.Helpers;

namespace TechnoBee.Counters.Storage
{
    internal class LicenseCertificateDocumentFileSection
    {
        public LicenseCertificateDocumentFileSection(Object content)
        {
            Content = content;
        }

        public Object Content { get; set; }
    }

    internal class LicenseCertificateDocumentFileSection<T>
        : LicenseCertificateDocumentFileSection
        where T : class
    {
        public LicenseCertificateDocumentFileSection(T content)
            : base(content)
        {

        }

        public new T Content {
            get => base.Content as T;
            set => base.Content = value;
        }
    }

    //public class LicenseCertificateDocumentFile
    //    : AttributesDocumentSignatureFileBase<LicenseCertificate>
    //{

    //}
}
