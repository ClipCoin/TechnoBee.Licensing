using System;
using System.IO;
using System.Reflection;
using static System.Reflection.Assembly;
using static System.IO.Path;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.CommandLineUtils;

using TechnoBee.Licensing.Framework;
using TechnoBee.Licensing.Clients;
using TechnoBee.Licensing.Utilities.Deployment.Classes;
using TechnoBee.Licensing.Utilities.LicensingHelper;
using TechnoBee.Licensing.Utilities.Fingerprints;

namespace NetApplication01
{
    class Program
    {
        public const String README_FILE_NAME = "readme.txt";
        public const String LICENSE_FILE_NAME = @"C:\Temp\sample01.tblcd";
        public const String LICENSE_APPLICATION_NAME = "NetApplication01";
        public const String LICENSE_PRODUCT_GUID_STRING = "385D940F-A5E5-46E3-A479-317779D8482F";
        public const String LICENSE_PRODUCT_VERSION = "1.0.2";

        static Int32 Main(string[] args)
        {
            return new ServiceCollection()
                .AddLicensing()
                .AddHostInformationDigestProvider()
                .AddLicensingHelpers()
                .AddFileService()
                .AddFingerprints()
                .AddSingleton(sp => {
                    CommandLineApplication application = new CommandLineApplication();

                    application.OnExecute(() =>
                    {
                        Int32 exitCode = 0;

                        try
                        {
                            PrintReadme(Combine(GetDirectoryName(GetExecutingAssembly().Location), README_FILE_NAME));

                            IProductFeatureLicense license = sp.GetRequiredService<ILicensing>()
                                .AttachRegistry(builder => {
                                    builder.AddLocalStore(cfg =>
                                    {
                                        cfg.AddFile(Combine(GetDirectoryName(GetExecutingAssembly().Location), LICENSE_FILE_NAME));
                                    });
                                })
                                .ConstructApplicationLicenseQuery()
                                .ForProduct(LICENSE_PRODUCT_GUID_STRING)
                                .ForProductVersion(LICENSE_PRODUCT_VERSION)
                                .ForApplication(LICENSE_APPLICATION_NAME)
                                .ForThisHost()
                                .EffectiveOn(DateTime.Now)
                                .YieldSingle();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.ToString()}");

                            exitCode = 1;
                        }

                        return exitCode;
                    });

                    return application;
                })
                .BuildServiceProvider()
                .GetRequiredService<CommandLineApplication>()
                .Execute(args);
        }

        private static void PrintReadme(String filePath)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                Console.WriteLine(reader.ReadToEnd());
            }
        }
    }
}
