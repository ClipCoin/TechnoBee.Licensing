using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Glob;

using TechnoBee.Counters.Storage;

namespace TechnoBee.Licensing.Clients
{
    public static class LicenseRegistryBuilderExtensions
    {
        public static ILicenseRegistryBuilder AddLocalStore(this ILicenseRegistryBuilder builder, Action<ILocalCertificateStoreConfig> cfgRoutine)
        {
            LocalCertificateStoreConfig cfg = new LocalCertificateStoreConfig();

            cfgRoutine(cfg);

            return builder.AddStore(new LocalCertificateStore(cfg));
        }
    }
}
