using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;
using Xunit;
using Moq;

using Microsoft.Extensions.DependencyInjection;

using TechnoBee.Licensing.Utilities.CertificateTool;
using TechnoBee.Licensing.Utilities.Fingerprints;
using TechnoBee.Licensing.Utilities.Common;

using TechnoBee.Licensing.Helpers;

namespace TechnoBee.Licensing.Utilities.CertificateTool_UnitTests
{
    public class CertificateToolLogic_UnitTests
    {
        [Fact]
        public void CertificateToolLogic__ExecuteCommandNew__NonExistingTemplate()
        {
            //  Prepare
            IServiceProvider serviceProvider = new ServiceCollection()
                .BuildServiceProvider();

            CertificateToolLogic logic = new CertificateToolLogic(serviceProvider);
            String templateName = "non-existing-template-name";
            String fileName = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Boolean overwriteExistingFile = false;

            //  Pre-validate
            Assert.False(File.Exists(templateName));
            Assert.False(EmbeddedTemplateEndingWithExists(templateName));
            Assert.False(File.Exists(fileName));

            //  Perform

            LicenseTemplateNotFoundException ex = Assert
                .Throws<LicenseTemplateNotFoundException>(() => logic.ExecuteCommandNew(fileName, templateName, overwriteExistingFile));

            //  Post-validate
            Assert.Equal(templateName, ex.TemplateName);
            Assert.False(File.Exists(fileName));
        }

        [Fact]
        public void CertificateToolLogic__ExecuteCommandNew__NullTemplate()
        {
            //  Prepare
            IServiceProvider serviceProvider = new ServiceCollection()
                .BuildServiceProvider();

            CertificateToolLogic logic = new CertificateToolLogic(serviceProvider);
            String templateName = null;
            String fileName = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Boolean overwriteExistingFile = false;

            //  Pre-validate
            Assert.False(File.Exists(fileName));

            //  Perform

            logic.ExecuteCommandNew(fileName, templateName, overwriteExistingFile);

            //  Post-validate
            Assert.True(File.Exists(fileName));
        }

        [Fact]
        public void CertificateToolLogic__ExecuteCommandNew__ExistingFile__NoOverwrite()
        {
            //  Prepare
            IServiceProvider serviceProvider = new ServiceCollection()
                .BuildServiceProvider();

            CertificateToolLogic logic = new CertificateToolLogic(serviceProvider);
            String templateName = "default";
            String fileName = CreateTempFile();
            Boolean overwriteExistingFile = false;

            //  Pre-validate
            Assert.True(File.Exists(templateName) || EmbeddedTemplateEndingWithExists(templateName));
            Assert.True(File.Exists(fileName));

            //  Perform

            ExistingFileCannotBeOverwrittenException ex = Assert
                .Throws<ExistingFileCannotBeOverwrittenException>(() =>
                {
                    logic.ExecuteCommandNew(fileName, templateName, overwriteExistingFile);
                });

            //  Post-validate
            Assert.Equal(fileName, ex.FileName);
            Assert.True(File.Exists(fileName));
        }

        [Fact]
        public void CertificateToolLogic__ExecuteCommandNew__ExistingFile__Overwrite()
        {
            //  Prepare
            IServiceProvider serviceProvider = new ServiceCollection()
                .BuildServiceProvider();

            CertificateToolLogic logic = new CertificateToolLogic(serviceProvider);
            String templateName = "default";
            String fileName = CreateTempFile();
            Boolean overwriteExistingFile = true;

            //  Pre-validate
            Assert.True(File.Exists(templateName) || EmbeddedTemplateEndingWithExists(templateName));
            Assert.True(File.Exists(fileName));
            DateTime fileLastWriteTime = File.GetLastWriteTime(fileName);

            //  Perform

            logic.ExecuteCommandNew(fileName, templateName, overwriteExistingFile);

            //  Post-validate
            Assert.True(File.Exists(fileName));
            Assert.True(File.GetLastWriteTime(fileName) > fileLastWriteTime);
        }

        [Fact]
        public void CertificateToolLogic__ExecuteCommandPack__NoLicenseFiles()
        {
            //  Prepare
            IServiceProvider serviceProvider = new ServiceCollection()
                .BuildServiceProvider();

            CertificateToolLogic logic = new CertificateToolLogic(serviceProvider);
            String[] licenseFiles = new String[0];
            String hostInformationDigestFilePath = CreateTempFile();
            String[] fingerprints = new String[]
            {
                ""
            };
            String outputPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Boolean overwriteExistingFile = false;

            //  Pre-validate
            Assert.True(licenseFiles.Count() == 0);
            Assert.True(File.Exists(hostInformationDigestFilePath));
            Assert.True(fingerprints.Count() > 0);
            Assert.False(File.Exists(outputPath));

            //  Perform

            Assert.Throws<NoLicensesToPackException>(() =>
            {
                logic.ExecuteCommandPack(licenseFiles, hostInformationDigestFilePath, fingerprints, outputPath, overwriteExistingFile);
            });

            //  Post-validate
            Assert.False(File.Exists(outputPath));
        }

        [Fact]
        public void CertificateToolLogic__ExecuteCommandPack__NonExistingHostFile()
        {
            //  Prepare
            IServiceProvider serviceProvider = new ServiceCollection()
                .BuildServiceProvider();

            CertificateToolLogic logic = new CertificateToolLogic(serviceProvider);
            String[] licenseFiles = new String[] {
                CreateTempFile()
            };

            String hostInformationDigestFilePath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            String[] fingerprints = new String[]
            {
                ""
            };

            String outputPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Boolean overwriteExistingFile = false;

            //  Pre-validate
            Assert.True(licenseFiles.Count() > 0);
            Assert.False(File.Exists(hostInformationDigestFilePath));
            Assert.True(fingerprints.Count() > 0);
            Assert.False(File.Exists(outputPath));

            //  Perform

            Assert.Throws<HostInformationDigestFileNotFoundException>(() =>
            {
                logic.ExecuteCommandPack(licenseFiles, hostInformationDigestFilePath, fingerprints, outputPath, overwriteExistingFile);
            });

            //  Post-validate
            Assert.False(File.Exists(outputPath));
        }

        [Fact]
        public void CertificateToolLogic__ExecuteCommandPack__ExistingOutputFile__NoOverwrite()
        {
            //  Prepare
            IServiceProvider serviceProvider = new ServiceCollection()
                .BuildServiceProvider();

            CertificateToolLogic logic = new CertificateToolLogic(serviceProvider);
            String[] licenseFiles = new String[] {
                CreateTempFile()
            };

            String hostInformationDigestFilePath = CreateTempFile();
            String[] fingerprints = new String[]
            {
                ""
            };

            String outputPath = CreateTempFile();
            Boolean overwriteExistingFile = false;
            DateTime outputFileLastWriteTime = File.GetLastWriteTime(outputPath);

            //  Pre-validate
            Assert.True(licenseFiles.Count() > 0);
            Assert.True(File.Exists(hostInformationDigestFilePath));
            Assert.True(fingerprints.Count() > 0);
            Assert.True(File.Exists(outputPath));

            //  Perform

            ExistingFileCannotBeOverwrittenException ex = Assert.Throws<ExistingFileCannotBeOverwrittenException>(() =>
            {
                logic.ExecuteCommandPack(licenseFiles, hostInformationDigestFilePath, fingerprints, outputPath, overwriteExistingFile);
            });

            //  Post-validate
            Assert.True(File.GetLastWriteTime(outputPath) == outputFileLastWriteTime);
            Assert.Equal(outputPath, ex.FileName);
        }

        [Fact]
        public void CertificateToolLogic__ExecuteCommandPack__ExistingOutputFile__Overwrite()
        {
            //  Prepare
            Mock<IFingerprintAlgorithmProvider> algorithmProvider = 
                new Mock<IFingerprintAlgorithmProvider>();

            Mock<IFingerprintAlgorithm> fingerprintGenerationAlgorithmMock = new Mock<IFingerprintAlgorithm>();

            fingerprintGenerationAlgorithmMock
                .Setup(m => m.GenerateFingerprint(It.IsAny<HostInformationDigest>()))
                .Returns(new Byte[] { 0x01, 0x02 });

            algorithmProvider
                .Setup(ap => ap.GetAlgorithm(It.IsAny<String>()))
                .Returns(fingerprintGenerationAlgorithmMock.Object);

            IServiceProvider serviceProvider = new ServiceCollection()
                .AddSingleton(algorithmProvider.Object)
                .BuildServiceProvider();

            CertificateToolLogic logic = new CertificateToolLogic(serviceProvider);
            String licenseFile = CreateLicenseFromResource("license-01.tblic");

            String[] licenseFiles = new String[] {
                licenseFile
            };


            String hostInformationDigestFilePath = CreateLicenseFromResource("host-info-01.tbhid");
            String[] fingerprints = new String[]
            {
                ""
            };

            String outputPath = CreateTempFile();
            Boolean overwriteExistingFile = true;
            DateTime outputFileLastWriteTime = File.GetLastWriteTime(outputPath);

            //  Pre-validate
            Assert.True(licenseFiles.Count() > 0);
            Assert.True(File.Exists(hostInformationDigestFilePath));
            Assert.True(fingerprints.Count() > 0);
            Assert.True(File.Exists(outputPath));

            //  Perform
            logic.ExecuteCommandPack(licenseFiles, hostInformationDigestFilePath, fingerprints, outputPath, overwriteExistingFile);

            //  Post-validate
            Assert.True(File.GetLastWriteTime(outputPath) > outputFileLastWriteTime);
        }

        [Fact]
        public void CertificateToolLogic__ExecuteCommandPack__EmptyLicenseFile()
        {
            //  Prepare
            IServiceProvider serviceProvider = new ServiceCollection()
                .BuildServiceProvider();

            CertificateToolLogic logic = new CertificateToolLogic(serviceProvider);
            String licenseFileName = CreateTempFile();
            String[] licenseFiles = new String[] {
                licenseFileName
            };

            String hostInformationDigestFilePath = CreateTempFile();
            String[] fingerprints = new String[]
            {
                ""
            };

            String outputPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Boolean overwriteExistingFile = true;
            DateTime outputFileLastWriteTime = File.GetLastWriteTime(outputPath);

            //  Pre-validate
            Assert.True(licenseFiles.Count() > 0);
            Assert.True(File.Exists(hostInformationDigestFilePath));
            Assert.True(fingerprints.Count() > 0);
            Assert.False(File.Exists(outputPath));

            //  Perform

            EmptyLicenseFileException ex = Assert.Throws<EmptyLicenseFileException>(() =>
            {
                logic.ExecuteCommandPack(licenseFiles, hostInformationDigestFilePath, fingerprints, outputPath, overwriteExistingFile);
            });

            //  Post-validate
            Assert.True(File.GetLastWriteTime(outputPath) == outputFileLastWriteTime);
            Assert.Equal(licenseFileName, ex.FileName);
        }

        [Theory]
        [YamlResourceData("TechnoBee.Licensing.Utilities.CertificateTool_UnitTests.Resources.pack-command-data.yaml")]
        public void CertificateToolLogic__ExecuteCommandPack__Baseline(String[] licenseResources, String hostInformationDigestResource, String[] fingerprints)
        {
            //  Prepare
            Mock<IFingerprintAlgorithm> fingerprintAlgorithm = new Mock<IFingerprintAlgorithm>();

            fingerprintAlgorithm
                .Setup(m => m.GenerateFingerprint(It.IsAny<IHostInformationDigest>()))
                .Returns(new Byte[] { 0xDE, 0xAD, 0xBE, 0xEF });

            Mock<IFingerprintAlgorithmProvider> fingerprintGenerationAlgorithmProvider 
                = new Mock<IFingerprintAlgorithmProvider>();

            fingerprintGenerationAlgorithmProvider
                .Setup(m => m.GetAlgorithm(It.IsAny<String>()))
                .Returns(fingerprintAlgorithm.Object);

            IServiceProvider serviceProvider = new ServiceCollection()
                .AddSingleton<IFingerprintAlgorithmProvider>(fingerprintGenerationAlgorithmProvider.Object)
                .BuildServiceProvider();

            CertificateToolLogic logic = new CertificateToolLogic(serviceProvider);

            IEnumerable<String> licenseFiles = licenseResources
                .Select(r => {
                    String licenseFilePath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

                    using (Stream outputStream = File.Open(licenseFilePath, FileMode.CreateNew, FileAccess.Write))
                    {
                        Assembly
                            .GetExecutingAssembly()
                            .GetManifestResourceStream($"TechnoBee.Licensing.Utilities.CertificateTool_UnitTests.Resources.{r}")
                            .CopyTo(outputStream);
                    }

                    return licenseFilePath;
                });

            String hostInformationDigestFile = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            using (Stream outputStream = File.Open(hostInformationDigestFile, FileMode.CreateNew, FileAccess.Write))
            {
                Assembly
                    .GetExecutingAssembly()
                    .GetManifestResourceStream($"TechnoBee.Licensing.Utilities.CertificateTool_UnitTests.Resources.{hostInformationDigestResource}")
                    .CopyTo(outputStream);
            }

            String outputPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            //  Pre-validate

            //  Perform

            logic.ExecuteCommandPack(licenseFiles, hostInformationDigestFile, fingerprints, outputPath, false);

            //  Post-validate
            
        }

        private static Boolean EmbeddedTemplateEndingWithExists(String str)
        {
            return Assembly
                .GetAssembly(typeof(Program))
                .GetManifestResourceNames()
                .Any(n => n.EndsWith(str, StringComparison.InvariantCultureIgnoreCase));
        }

        private static String CreateTempFile()
        {
            String fileName = Path.GetTempFileName();

            _temporaryFiles.Add(fileName);

            return fileName;
        }

        private static String CreateLicenseFromResource(String resourceShortName)
        {
            String fileName = Path.GetTempFileName();

            _temporaryFiles.Add(fileName);

            using (Stream resourceStream = Assembly
                .GetExecutingAssembly()
                .GetManifestResourceStream($"TechnoBee.Licensing.Utilities.CertificateTool_UnitTests.Resources.{resourceShortName}"))
            {
                using (Stream stream = File.Open(fileName, FileMode.Open, FileAccess.Write))
                {
                    resourceStream.CopyTo(stream);
                }
            }

            return fileName;
        }

        ~CertificateToolLogic_UnitTests()
        {
            foreach (String fileName in _temporaryFiles)
            {
                if (File.Exists(fileName))
                {
                    try
                    {
                        File.Delete(fileName);
                    } catch
                    {

                    }
                }
            }
        }

        private static List<String> _temporaryFiles = new List<string>();
    }
}
