using Xunit;
using Moq;
using TechnoBee.Licensing.Utilities.DeploymentCore.Services;
using TechnoBee.Licensing.Utilities.DeploymentCore.Commands;
using TechnoBee.Licensing.Utilities.DeploymentAbstractions.Commands;
using System.Collections.Generic;
using TechnoBee.Licensing.Utilities.LicensingHelper.Services;
using System;
using TechnoBee.Licensing.Utilities.Common;
using TechnoBee.Licensing.Utilities.DeploymentAbstractions;
using TechnoBee.Licensing.Utilities.Deployment;

namespace TechnoBee.Licensing.Utilities.Deployment_Core_Test {
    public class GenerateFingerprintCommand_Tests {

        [Fact]
        public void GenerateFingerprintCommand__Constructor__Baseline() {
            // prepare
            Mock<IFileService> fileService = new Mock<IFileService>();
            Mock<IWmiService> wmiService = new Mock<IWmiService>();

            Mock<IHostInformationDigestProvider> mockHostInformationDigestProvider = new Mock<IHostInformationDigestProvider>();

            // pre-validate

            // perform
            GenerateFingerprintCommand command = new GenerateFingerprintCommand(mockHostInformationDigestProvider.Object, fileService.Object);

            // post-validate
        }

        [Fact]
        public void GenerateFingerprintCommand__NullInputArguments_ReturnFingerprint() {
            // Prepare 
            Mock<IFileService> fileService = new Mock<IFileService>();
            Mock<IWmiService> wmiService = new Mock<IWmiService>();
            wmiService.Setup(x => x.GetDiskDrives()).Returns(() => 
            {
                return new List<DiskDrive>()
                {
                    new DiskDrive()
                    {
                        DeviceId = "DEVICE-ID",
                        SerialNumber = "SERIALNUMBER"
                    }
                };
            });

            Mock<IHostInformationDigestProvider> mockHostInformationDigestProvider = new Mock<IHostInformationDigestProvider>();

            GenerateFingerprintCommand command = new GenerateFingerprintCommand(mockHostInformationDigestProvider.Object, fileService.Object);

            // Pre-validate

            // Perform
            CommandExecutionResult result = command.Execute();

            // Post-validate
            Assert.Equal(true, result.Success);
        }

        [Fact]
        public void GenerateFingerprintCommand_EnterOutputption_CreateFileWithFingerprint() {
            // Prepare 
            Mock<IFileService> fileService = new Mock<IFileService>();
            Mock<IWmiService> wmiService = new Mock<IWmiService>();
            //wmiService.Setup(x => x.GetHddSerialNumber()).Returns();
            Mock<IHostInformationDigestProvider> mockHostInformationDigestProvider = new Mock<IHostInformationDigestProvider>();

            throw new NotImplementedException();
            // use mock 

            // Pre-validate

            // Perform
            ICommand command = new GenerateFingerprintCommand(mockHostInformationDigestProvider.Object, fileService.Object);
            (command as GenerateFingerprintCommand).InputData = new DeploymentCore.GenerateFingerprintOptions() {
                Path = "FingerprintDirectory/"
            };

            // Post-validate
            var expectedOperationResult = true;
            Assert.Equal(expectedOperationResult, command.Execute().Success);

            bool fileExist = System.IO.File.Exists(System.IO.Path.Combine("FingerprintDirectory", "fingerprint.tbhid"));
            Assert.True(fileExist);
        }
    }
}