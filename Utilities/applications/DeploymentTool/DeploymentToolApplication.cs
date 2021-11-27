using System;
using Microsoft.Extensions.CommandLineUtils;
using TechnoBee.Licensing.Utilities.Deployment_Sdk;
using TechnoBee.Licensing.Utilities.DeploymentCore;
using TechnoBee.Licensing.Utilities.DeploymentCore.Commands;
using TechnoBee.Licensing.Utilities.DeploymentCore.Exceptions;
using TechnoBee.Licensing.Utilities.DeploymentTool.Helpers;

using Microsoft.Extensions.DependencyInjection;

namespace TechnoBee.Licensing.Utilities.DeploymentTool
{
    internal class DeploymentToolApplication
    {
        public DeploymentToolApplication
            ( IServiceProvider serviceProvider
            , GenerateFingerprintCommand generateFingerprintCommand
            , DeploymentLogger logger)
        {
            _serviceProvider = serviceProvider
                ?? throw new ArgumentNullException(nameof(serviceProvider));

            _logger = logger
                ?? throw new ArgumentNullException(nameof(logger));
        }

        public int Execute(string[] args)
        {
            int statusCode = 0;
            var commandLineApplication = new CommandLineApplication(throwOnUnexpectedArg: true);

            try
            {
                var installOption = commandLineApplication.Option(
                  "-$|-i |--install", "The command used to install license file into the license storage directiory",
                  CommandOptionType.SingleValue);

                var forcedInstallation = commandLineApplication.Option("-$|-f|--force", "Force new license installation", CommandOptionType.NoValue);

                var outputOption = commandLineApplication.Option(
                  "-$|-o |--output", "The command used to create a fingerprint info needed to get a new license file",
                  CommandOptionType.SingleValue);

                var logOption = commandLineApplication.Option(
                  "-$|-l |--log", "The command used to log the events",
                  CommandOptionType.SingleValue);

                commandLineApplication.HelpOption("-? | -h | --help");

                commandLineApplication.OnExecute(() =>
                {
                    var is_loggerNeeded = false;

                    string logFilePath;

                    if (logOption.HasValue())
                    {
                        is_loggerNeeded = true;
                        logFilePath = logOption.Value();
                    }

                    if (installOption.HasValue())
                    {
                        var installCommand = _serviceProvider.GetRequiredService<InstallLicenseCommand>();

                        installCommand.InputData = new InstallLicenseOptions()
                        {
                            Path = installOption.Value(),
                            ForcedInstallation = forcedInstallation.HasValue()
                        };

                        var result = installCommand.Execute();
                    }

                    else
                    {
                        var command2 = _serviceProvider.GetRequiredService<GenerateFingerprintCommand>();

                        command2.InputData = new GenerateFingerprintOptions()
                        {
                            Path = outputOption.Value(),
                            OverwriteExistingFile = forcedInstallation.Value() != null
                        };

                        var succeeded = command2.Execute().Success;
                        if (succeeded)
                        {
                            Console.WriteLine(command2.OutputData);
                        }
                    }

                    return 0;
                });

                statusCode = commandLineApplication.Execute(args);
            }
            catch (CommandParsingException ex)
            {
                MessageHelper.DisplayMessage(MessageType.Warning, "Unknown command detected. See the possible commands bellow.");
                _logger.Log(ex, Deployment_Sdk.MessageType.Warning);
                commandLineApplication.ShowHelp();
            }
            catch (LicenseAlreadyInstalledException ex)
            {
                MessageHelper.DisplayMessage(MessageType.Warning, ex);
                _logger.Log(ex, Deployment_Sdk.MessageType.Warning);
            }
            catch (Exception ex)
            {
                MessageHelper.DisplayMessage(MessageType.Error, ex);
                _logger.Log(ex, Deployment_Sdk.MessageType.Error);
                statusCode = -1;
            }

            return statusCode;
        }

        private readonly IServiceProvider _serviceProvider;
        private readonly DeploymentLogger _logger;
    }
}
