using Ninject;
using System;
using System.IO;
using TechnoBee.Licensing.Utilities.DeploymentAbstractions;
using TechnoBee.Licensing.Utilities.DeploymentAbstractions.Commands;
using TechnoBee.Licensing.Utilities.DeploymentCore.Classes.Commands;
using TechnoBee.Licensing.Utilities.DeploymentCore.Exceptions;
using TechnoBee.Licensing.Utilities.DeploymentCore.Services;

namespace TechnoBee.Licensing.Utilities.DeploymentCore.Commands {
    public class InstallLicenseCommand : BaseCommandProvider, ICommand<InstallLicenseOptions> {
        private IFileService _fileService;
        private InstallLicenseOptions _options;

        public InstallLicenseCommand() {
            _fileService = AppKernel.Get<IFileService>();
        }

        public InstallLicenseCommand(IFileService fileService) {
            if (fileService == null)
                throw new ArgumentNullException(nameof(fileService));

            _fileService = fileService;
        }

        public InstallLicenseOptions InputData {
            get { return _options; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(InputData));

                if (String.IsNullOrEmpty(value.Path))
                    throw new ArgumentNullException(nameof(InputData.Path));

                _options = value;
            }
        }

        public CommandExecutionResult Execute() {
            if (String.IsNullOrEmpty(InputData?.Path))
                throw new ArgumentNullException(nameof(InputData.Path));

            try {
                if (!_fileService.IsFileExist(InputData.Path))
                    throw new LicenseNotFoundException("The argument corresponding to the license file path is not a valid!");

                string licenseStorageDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Technobee");
                _fileService.CreateDirectory(licenseStorageDirectory);

                string fileName = Path.GetFileName(InputData.Path);
                bool isLicenseAlreadyExist = _fileService.IsFileExist(Path.Combine(licenseStorageDirectory, fileName));
                if (isLicenseAlreadyExist && InputData.ForcedInstallation == false)
                    throw new LicenseAlreadyInstalledException(
                        "The another one license file for the Technobee application is already installed. " +
                        "Use -f (--force) flag to overwrite the current license file with new one.");

                _fileService.CopyFile(InputData.Path, Path.Combine(licenseStorageDirectory, fileName), true);

                return new CommandExecutionResult() { Success = true, Message = "The license was successfully installed" };
            }
            catch (Exception ex) {
                throw new LicenseInstallationException(ex.Message, ex);
            }
        }
    }
}
