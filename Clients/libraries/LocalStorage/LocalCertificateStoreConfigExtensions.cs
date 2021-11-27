using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TechnoBee.Counters.Storage;

namespace TechnoBee.Licensing.Clients
{
    public static class LocalCertificateStoreConfigExtensions
    {
        public static ILocalCertificateStoreConfig AddSystemLicenseFolder(this ILocalCertificateStoreConfig cfg)
        {
            String hostLicenseFileStorageFolder = Path.Combine(Environment.SpecialFolder.CommonApplicationData.ToString(), "TechnoBee", "Licenses");

            cfg.AddDirectory(hostLicenseFileStorageFolder);

            return cfg;
        }
    }
}
