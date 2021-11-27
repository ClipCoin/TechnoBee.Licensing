using System;
using Xunit;
using Moq;
using TechnoBee.Licensing.Utilities.DeploymentCore.Services;
using TechnoBee.Licensing.Utilities.DeploymentCore.Exceptions;
using TechnoBee.Licensing.Utilities.DeploymentCore.Commands;
using TechnoBee.Licensing.Utilities.DeploymentAbstractions.Commands;

namespace TechnoBee.Licensing.Utilities.Deployment_Core_Test {
    public class InstallLicenseCommand_Tests {

        [Fact]
        public void InstallLicenseCommand__Constructor__Baseline() {
            // prepare
            Mock<IFileService> fileService = new Mock<IFileService>();

            // pre-validate

            // perform
            ICommand command = new InstallLicenseCommand();

            // post-validate
        }

        [Fact]
        public void InstallLicenseCommand__NullInputArguments() {
            // Prepare 
            // Pre-validate

            // Perform
            ICommand command = new InstallLicenseCommand();

            // Post-validate
            Assert.Throws<ArgumentNullException>("Path", () => command.Execute());
        }

        [Fact]
        public void InstallLicenseCommand__InvalidLicensePath() {
            //  Prepare
            const string notExistingFilePath = @"C:\some-directory\license.yaml";

            Mock<IFileService> fileService = new Mock<IFileService>();
            fileService.Setup(x => x.IsFileExist(It.IsAny<string>())).Returns(false);

            // Pre-validate

            // Perform
            InstallLicenseCommand command = new InstallLicenseCommand();
            command.InputData = new DeploymentCore.InstallLicenseOptions() { Path = notExistingFilePath };
            Exception ex = Assert.Throws<LicenseInstallationException>(() => command.Execute());
            Assert.IsType<LicenseNotFoundException>(ex.InnerException);

            // Post-validate
        }

        [Fact]
        public void InstallLicenseCommand__LicenseAlreadyExist__ThrowException() {
            //  Prepare
            const string alreadyExistedFilePath = @"C:\some-directory\license.yaml";

            Mock<IFileService> fileService = new Mock<IFileService>();
            fileService.Setup(x => x.IsFileExist(It.IsAny<string>())).Returns(true);

            // Pre-validate

            // Perform
            InstallLicenseCommand command = new InstallLicenseCommand(fileService.Object);
            command.InputData = new DeploymentCore.InstallLicenseOptions() { Path = alreadyExistedFilePath };
            //Assert.Throws<LicenseInstallationException>(() => command.Execute());
            Exception ex = Assert.Throws<LicenseInstallationException>(() => command.Execute());
            Assert.IsType<LicenseAlreadyInstalledException>(ex.InnerException);

            // Post-validate
        }


        [Fact]
        public void InstallLicenseCommand__EnterInstallationOption__InstallLicenseFileSuccessfully() {
            //  Prepare
            const string licenseFilePath = @"C:\some-directory\license.yaml";

            Mock<IFileService> fileService = new Mock<IFileService>();
            fileService.Setup(x => x.IsFileExist(It.IsAny<string>())).Returns(true);

            // Pre-validate

            // Perform
            InstallLicenseCommand command = new InstallLicenseCommand(fileService.Object);
            command.InputData = new DeploymentCore.InstallLicenseOptions() { Path = licenseFilePath, ForcedInstallation = true };
            var result = command.Execute();
            Assert.True(result.Success);
            
            // Post-validate
        }
    }
}