using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using YamlDotNet.Serialization.NamingConventions;

namespace TechnoBee.Licensing.Helpers
{
    public abstract class AttributesDocumentSignatureFileBase
    {
        public IDictionary<String, String> Attributes => _attributes;
        public Byte[] Signature => _signature;

        public const String ATTRIBUTES_SECTION_NAME = "attributes";
        public const String DOCUMENT_SECTION_NAME = "document";
        public const String SIGNATURE_SECTION_NAME = "signature";

        public const String CERTIFICATE_SERIAL_NUMBER = "edb163fd37fbe98f492c4219444cfb8c";

        public X509Certificate2 SignatureCertificate { get; set; }

        public void Load(String fileName)
        {
            try
            {
                _loadSectionsFromFile(fileName);
            }
            catch (Exception ex)
            {
                throw new AttributesDocumentSignatureFileLoadException(fileName, ex);
            }
        }

        public void Save(String fileName)
        {
            try
            {
                String destinationFileName = File.Exists(fileName)
                    ? Path.Combine(Path.GetTempPath(), Path.GetRandomFileName())
                    : fileName;

                _saveSectionsToFile(destinationFileName);

                if (destinationFileName != fileName)
                {
                    File.Replace(destinationFileName, fileName, Path.Combine(Path.GetTempPath(), Path.GetRandomFileName()));
                }
            }
            catch (Exception ex)
            {
                throw new AttributesDocumentSignatureFileSaveException(fileName, ex);
            } 
        }

        private void _loadSectionsFromFile(String fileName)
        {
            try
            {
                using (ContainerFile container = ContainerFile.Open(fileName, FileAccess.Read))
                {
                    _attributes = container
                        .GetSection(ATTRIBUTES_SECTION_NAME)
                        .GetStream()
                        .Parse()
                        .AsText()
                        .AsYaml(builder => {
                            builder.WithNamingConvention(new CamelCaseNamingConvention());
                        })
                        .To<Dictionary<String, String>>();

                    _signature = container
                        .GetSection(SIGNATURE_SECTION_NAME)
                        .GetStream()
                        .Parse()
                        .AsBytes();

                    InternalDocumentStream = container
                        .GetSection(DOCUMENT_SECTION_NAME)
                        .GetStream();
                }
            }
            catch (InvalidDataException ex)
            {
                throw new MalformedAttributesDocumentSignatureFileException(fileName, ex);
            }
        }

        private void _saveSectionsToFile(String fileName)
        {
            using (ContainerFile container = ContainerFile.Open(fileName, FileAccess.Write))
            {
                TechnoBeeDocumentContainerSection attributesSection = container
                    .CreateSection(ATTRIBUTES_SECTION_NAME);

                if (_attributes != null && _attributes.Count() > 0)
                {
                    attributesSection
                        .GetStream()
                        .Emit()
                        .AsText()
                        .AsYaml(builder => {
                            builder.EmitDefaults();
                            builder.WithNamingConvention(new CamelCaseNamingConvention());
                        })
                        .From(_attributes);
                }

                TechnoBeeDocumentContainerSection documentSection = container
                    .CreateSection(DOCUMENT_SECTION_NAME);

                InternalDocumentStream.CopyTo(documentSection.GetStream());

                documentSection.GetStream().Flush();

                X509Store store = new X509Store(StoreLocation.LocalMachine);
                store.Open(OpenFlags.OpenExistingOnly);
                X509Certificate2Collection collection = store.Certificates.Find(X509FindType.FindBySerialNumber, CERTIFICATE_SERIAL_NUMBER, false);
                if (collection.Count > 0)
                    SignatureCertificate = collection[0];
                store.Close();

                if (SignatureCertificate != null)
                {
                    using (Stream signatureDataBaseStream = new MemoryStream())
                    {
                        //attributesSection
                        //    .GetStream()
                        //    .CopyTo(signatureDataBaseStream);

                        Stream docSectionStream = documentSection.GetStream();
                        docSectionStream.Position = 0;
                        docSectionStream.CopyTo(signatureDataBaseStream);
                        signatureDataBaseStream.Position = 0;

                        // If 'Keyset does not exist' exception, reinstall certificate
                        using (RSACryptoServiceProvider csp = SignatureCertificate.PrivateKey as RSACryptoServiceProvider)
                        {
                            using (SHA1 hashAlgorithm = SHA1.Create())
                            {
                                _signature = csp.SignData(signatureDataBaseStream, hashAlgorithm);
                            }
                        }
                    }
                }

                TechnoBeeDocumentContainerSection signatureSection = container
                    .CreateSection(SIGNATURE_SECTION_NAME);

                if (_signature != null && _signature.Length > 0)
                {
                    signatureSection
                        .GetStream()
                        .Emit()
                        .AsBinary()
                        .From(_signature);
                }
            }
        }

        protected abstract Stream InternalDocumentStream { get; set; }

        private Dictionary<String, String> _attributes = new Dictionary<string, string>();
        private Byte[] _signature = new Byte[0];
    }
}
