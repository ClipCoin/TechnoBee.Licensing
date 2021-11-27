using System;

using Microsoft.Extensions.DependencyInjection;

using TechnoBee.Licensing.Clients;

[assembly:EnsureLicense(productIdString: "EF0A530C-DAED-4C24-952C-9378D0EB7809"
    , productEdition: "ultimate"
    , productVersionString: "2.0.1.*"
    , applicationName: "NetPoc")]

namespace NetPoc
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceProvider serviceProvider = new ServiceCollection()
                .AddLicensing()
                .AddPocRoutines()
                .BuildServiceProvider();

            PocRoutines routines = serviceProvider
                .GetRequiredService<PocRoutines>();

            routines.Routine1();
        }
    }
}
