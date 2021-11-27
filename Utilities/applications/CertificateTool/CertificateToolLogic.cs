using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using TechnoBee.Licensing.Helpers;
using TechnoBee.Counters.Storage;
using TechnoBee.Licensing.Framework;
using TechnoBee.Licensing.Utilities.Fingerprints;

using TechnoBee.Licensing.Utilities.Common;
using YamlDotNet.Serialization.NamingConventions;

namespace TechnoBee.Licensing.Utilities.CertificateTool
{
    public class CertificateToolLogic
    {
        public CertificateToolLogic(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void ExecuteCommandNew
            (String fileName
            , String templateName
            , Boolean overwriteExistingFile)
        {
            if (File.Exists(fileName) && !overwriteExistingFile)
            {
                throw new ExistingFileCannotBeOverwrittenException(fileName);
            }

            using (Stream licenseTemplateStream = GetTemplateStream(templateName))
            {
                if (licenseTemplateStream != null)
                {
                    using (Stream destinationStream = File.OpenWrite(fileName))
                    {
                        licenseTemplateStream.CopyTo(destinationStream);
                    }
                }
                else
                {
                    throw new LicenseTemplateNotFoundException(templateName);
                }
            }

            Console.WriteLine($"License file '{fileName}' created.");
        }

        public void ExecuteCommandPack
            (IEnumerable<String> licenseFiles
            , String hostInformationDigestFilePath
            , IEnumerable<String> fingerprints
            , String outputPath
            , Boolean overwriteExistingFile)
        {
            if (licenseFiles.Count() == 0)
            {
                throw new NoLicensesToPackException();
            }

            if (String.IsNullOrEmpty(hostInformationDigestFilePath))
            {
                throw new RequiredParameterNotFoundException("pack", "<host>");
            }

            if (!File.Exists(hostInformationDigestFilePath))
            {
                throw new HostInformationDigestFileNotFoundException(hostInformationDigestFilePath);
            }

            if (File.Exists(outputPath) && !overwriteExistingFile)
            {
                throw new ExistingFileCannotBeOverwrittenException(outputPath);
            }

            List<ProductLicense> licenses = new List<ProductLicense>();

            foreach (String licenseFile in licenseFiles)
            {
                Stream licenseStream = File.Open(licenseFile, FileMode.Open, FileAccess.Read);

                HostInformationDigest hostInformationDigest =
                    File.Open(hostInformationDigestFilePath, FileMode.Open, FileAccess.Read)
                    .Parse()
                    .AsText()
                    .AsYaml(builder => {
                        builder.WithNamingConvention(new CamelCaseNamingConvention());
                        builder.WithTypeConverter(new YamlVersionConverter());
                    })
                    .To<HostInformationDigest>();

                ProductLicense license = licenseStream
                    .Parse()
                    .AsText()
                    .AsYaml(builder => {
                        builder.WithNamingConvention(new CamelCaseNamingConvention());
                        builder.WithTypeConverter(new YamlVersionConverter());
                    })
                    .To<ProductLicense>();

                if (license == null)
                {
                    throw new EmptyLicenseFileException(licenseFile);
                }

                if (license.Prerequisites.Host.Fingerprints.Count == 0)
                {
                    license
                        .Prerequisites
                        .Host
                        .Fingerprints
                        .AddRange(ComputeHostFingerprints(hostInformationDigest, fingerprints));
                }
                else
                {
                    throw new Exception();
                }

                licenses.Add(license);
            }

            LicenseCertificate licenseCertificate = new LicenseCertificate
            {
                Id = Guid.NewGuid(),
                IssueTime = DateTime.Now,
                Version = new Version(1, 0),
                Licenses = licenses
            };

            LicenseCertificateFile licenseCertificateFile = new LicenseCertificateFile
            {
                Document = licenseCertificate
            };

            licenseCertificateFile.Save(outputPath);
        }

        public void ExecuteCommandVerify()
        {
            PrintConsoleHeader();

            throw new NotImplementedException();
        }

        private IEnumerable<HostFingerprint> ComputeHostFingerprints(IHostInformationDigest digest, IEnumerable<String> fingerprintAlgorithms)
        {
            IFingerprintAlgorithmProvider algorithmProvider = _serviceProvider
                .GetRequiredService<IFingerprintAlgorithmProvider>();

            return fingerprintAlgorithms
                .Select(a =>
                {
                    return new HostFingerprint()
                    {
                        Algorithm = a,
                        Value = Convert.ToBase64String(algorithmProvider.GetAlgorithm($"{a}{(!HostStamp.IsValid?":Sha256":"")}").GenerateFingerprint(digest))
                    };
                });
        }

        private static void PrintConsoleHeader()
        {
            const String line = "=========================================";

            Console.WriteLine(line);

            Console.WriteLine("tblcert utility v.1.0.0");
            Console.WriteLine("TechnoBee Ltd. (c) 2017");

            Console.WriteLine(line);
            Console.WriteLine();
        }

        private static Stream GetEmbeddedTemplateStream(String resourceShortName)
        {
            return Assembly
                .GetExecutingAssembly()
                .GetManifestResourceStream($"TechnoBee.Licensing.Utilities.CertificateTool.Templates.{resourceShortName}");
        }

        private static Stream GetTemplateStream(String templateName)
        {
            Stream result;

            if (templateName != null)
            {
                if (File.Exists(templateName))
                {
                    result = File.Open(templateName, FileMode.Open, FileAccess.Read);
                }
                else
                {
                    result = GetEmbeddedTemplateStream(templateName);
                }
            }
            else
            {
                //result = GetEmbeddedTemplateStream(DEFAULT_TEMPLATE_NAME);
                result = new MemoryStream()
                    .Emit()
                    .AsText()
                    .AsYaml(builder => {
                        builder.EmitDefaults();
                        builder.WithNamingConvention(new CamelCaseNamingConvention());
                        builder.WithTypeConverter(new YamlVersionConverter());
                    })
                    .From(new ProductLicense());
            }

            if (result != null)
            {
                result.Flush();
                result.Seek(0, SeekOrigin.Begin);
            }

            return result;
        }

        private readonly IServiceProvider _serviceProvider;
    }
}
