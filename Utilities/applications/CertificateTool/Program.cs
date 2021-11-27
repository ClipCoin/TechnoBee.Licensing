using System;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.CommandLineUtils;
using System.Runtime.CompilerServices;
using TechnoBee.Licensing.Utilities.Fingerprints;

[assembly: InternalsVisibleTo(@"TechnoBee.Licensing.Utilities.CertificateTool_UnitTests,"
                                + "PublicKey=0024000004800000940000000602000000240000525341310004000001000100110ae02ec83d3f"
                                + "716baa0820ad038b854677aeb4ec84b0cb5dc7826d83cc5d6fd97a64c1eea0cc1839959c1e967a"
                                + "021f8d0e39cf3e6d4028baf91ff77df6f5cc6411c1dcc372fe661c8486e9b2b825c89c3a0aa795"
                                + "5d9db710e3fd8846eea084565b960515ac90f24c3bf7bd581df7c0fa3aa14038ca89e50da3295c"
                                + "94e35eed")]


namespace TechnoBee.Licensing.Utilities.CertificateTool
{
    internal static class Program
    {
        private const Int32 EXIT_CODE_UNEXPECTED_ERROR = 1;
        private const Int32 EXIT_CODE_SUCCESS = 0;

        private const String DEFAULT_TEMPLATE_NAME = "default";

        static Int32 Main(string[] args)
        {
            Int32 exitCode = EXIT_CODE_UNEXPECTED_ERROR;

            try
            {
                //PrintConsoleHeader();

                exitCode = new ServiceCollection()
                    .AddFingerprints()
                    .AddSingleton<CommandLineApplicationEx>(sp =>
                    {
                        CertificateToolLogic logic = new CertificateToolLogic(sp);

                        application = new CommandLineApplicationEx(sp);

                        application.Command("new", app => ConfigureCommandNew(app, logic));
                        application.Command("verify", app => ConfigureCommandVerify(app, logic));
                        application.Command("pack", app => ConfigureCommandPack(app, logic));

                        application.OnExecute(() => {
                            //PrintConsoleHeader();

                            application.ShowHelp();

                            return 0;
                        });

                        return application;
                    })
                    .BuildServiceProvider()
                    .GetRequiredService<CommandLineApplicationEx>()
                    .Execute(args);
            }
            catch (CommandParameterException ex)
            {
                Console.WriteLine($"ERROR: {ex.ComposeMessage(" ")}");

                application?.ShowHelp((ex as CommandException)?.Command);
            }
            catch (UserFriendlyException ex)
            {
                Console.WriteLine($"ERROR: {ex.ComposeMessage(" ")}");
            }
            //catch (CommandException ex){
            //    Console.WriteLine($"ERROR: {ex.Message}");

            //    application?.ShowHelp(ex.Command);
            //}
            catch (Exception)
            {
                throw;
            }
            //catch (OutOfMemoryException ex)
            //{
            //    Environment.FailFast(ex.Message);
            //}
            //catch (UserFriendlyException ex) when (ex.Message != null)
            //{
            //    exitCode = LogException(ex).ErrorCode;
            //}
            //catch (Exception ex)
            //{
            //    WriteErrorMessage(ex.ToString());
            //}
            Console.WriteLine("Press ENTER to exit...");
            Console.ReadLine();
            return exitCode;
        }

        private static TException LogException<TException>(TException exception)
            where TException : Exception
        {
            Console.WriteLine(exception.Message);

            return exception;
        }

        internal static void ConfigureCommandNew(CommandLineApplication app, CertificateToolLogic logic)
        {
            CommandArgument fileNameArgument = app.Argument("file name", "A name of the file to be created. Shall the name represent a relative path, it is considered relative to the processes working directory.");

            CommandOption templateOption = app.Option("-t|--template"
                , "A name of a predefined license template or a path to a file to be used as template."
                , CommandOptionType.SingleValue);

            app.OnExecute(() =>
            {
                try
                {
                    String fileName = fileNameArgument.Value
                        ?? throw new RequiredParameterNotFoundException("new", fileNameArgument);

                    logic.ExecuteCommandNew(fileNameArgument.Value, templateOption.Value(), false);
                }
                catch (Exception ex)
                {
                    throw new CreateProductLicenseFromTemplateException(ex);
                }

                return EXIT_CODE_SUCCESS;
            });
        }

        internal static void ConfigureCommandVerify(CommandLineApplication app, CertificateToolLogic logic)
        {
            app.Argument("file", "A name of the file to be verified. Shall the name represent a relative path, it is considered relative to the processes working directory.");

            app.Option
                ( "-c|--certificate"
                , "A path to a certificate file to test the signature against."
                , CommandOptionType.SingleValue );

            app.OnExecute(() =>
            {
                try
                {
                    logic.ExecuteCommandVerify();
                }
                catch (Exception ex)
                {
                    throw new CertificateValidationException(ex);
                }

                return EXIT_CODE_SUCCESS;
            });
        }

        internal static void ConfigureCommandPack(CommandLineApplication app, CertificateToolLogic logic)
        {
            CommandArgument licensesArgument = app.Argument
                ( "licenses"
                , "A name of the license file to pack. Shall the name represent a relative path, it is considered relative to the processes working directory."
                , true );

            CommandOption hostOption = app.Option
                ( template: "-h|--host"
                , description: "Path to a host information digest file."
                , optionType: CommandOptionType.SingleValue );

            CommandOption fingerprintOption = app.Option
                ( "-f|--fingerprint"
                , "Fingerprint generation algorithm (multiple values are allowed)."
                , CommandOptionType.MultipleValue );

            CommandOption certificateOption = app.Option
                ( "-c|--certificate"
                , "A path to a certificate file to sign the license."
                , CommandOptionType.SingleValue );

            CommandOption outputOption = app.Option
                ( "-o|--output"
                , "A path to save packed file to."
                , CommandOptionType.SingleValue );

            app.OnExecute(() =>
            {
                try
                {
                    logic.ExecuteCommandPack
                    ( licensesArgument.Values
                    , hostOption.Value()
                    , fingerprintOption.Values
                    , outputOption.Value() 
                    , false );
                }
                catch (CommandException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    throw new PackCommandException(ex);
                }

                return EXIT_CODE_SUCCESS;
            });
        }

        private static CommandLineApplicationEx application;
    }
}
