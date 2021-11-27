using System;
using System.IO;
using TechnoBee.Licensing.Helpers;
using TechnoBee.Licensing.Utilities.Common;
using TechnoBee.Licensing.Utilities.Deployment;
using TechnoBee.Licensing.Utilities.DeploymentAbstractions;
using TechnoBee.Licensing.Utilities.DeploymentAbstractions.Commands;
using TechnoBee.Licensing.Utilities.DeploymentCore.Exceptions;
using TechnoBee.Licensing.Utilities.DeploymentCore.Services;
using YamlDotNet.Serialization.NamingConventions;

namespace TechnoBee.Licensing.Utilities.DeploymentCore.Commands
{
    public class GenerateFingerprintCommand : ICommand<GenerateFingerprintOptions>, ICommandResult<string>
    {
        public GenerateFingerprintCommand( IHostInformationDigestProvider hostInformationDigestGenerator, IFileService fileService )
        {
            _hostInformationDigestGenerator = hostInformationDigestGenerator;
            _fileService = fileService;
        }

        public GenerateFingerprintOptions InputData { get; set; }

        public string OutputData { get; private set; }

        public CommandExecutionResult Execute()
        {
            try
            {
                IHostInformationDigest hostInformationDigest = _hostInformationDigestGenerator
                    .GenerateHostInformationDigest();

                if (File.Exists(InputData?.Path) && !InputData.OverwriteExistingFile)
                {
                    throw new ExistingFileCannotBeOverwrittenException(InputData?.Path);
                }

                String tempFileName = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

                if (!String.IsNullOrEmpty(InputData?.Path))
                {
                    var directory = Path.GetDirectoryName(InputData.Path);
                    if (!String.IsNullOrEmpty(directory))
                        _fileService.CreateDirectory(directory);
                }

                using (Stream stream = File.Open(tempFileName, FileMode.Create, FileAccess.Write))
                {
                    stream
                        .Emit()
                        .AsText()
                        .AsYaml(cfg =>
                        {
                            cfg.EmitDefaults();
                            cfg.WithNamingConvention(new CamelCaseNamingConvention());
                            cfg.WithTypeConverter(new YamlVersionConverter());
                        })
                        .From(hostInformationDigest);
                }

                if (File.Exists(InputData?.Path))
                {
                    File.Replace(tempFileName, InputData?.Path, Path.Combine(Path.GetTempPath(), Path.GetRandomFileName()));
                }
                else
                {
                    File.Copy(tempFileName, InputData?.Path);
                }

                return new CommandExecutionResult() { Message = "Operation complete", Success = true };
            }
            catch (Exception ex)
            {
                throw new GenerateFingerprintException(ex.Message, ex);
            }
        }

        private readonly IFileService _fileService;
        private readonly IHostInformationDigestProvider _hostInformationDigestGenerator;
    }
}