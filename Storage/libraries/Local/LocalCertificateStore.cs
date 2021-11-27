using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TechnoBee.Licensing.Framework;
using TechnoBee.Licensing.Helpers;

namespace TechnoBee.Counters.Storage
{
    public class LocalCertificateStore
        : ILicenseCertificateStore
    {
        public LocalCertificateStore(LocalCertificateStoreConfig cfg)
        {
            _cfg = cfg
                ?? throw new ArgumentNullException(nameof(cfg));
        }

        public IEnumerable<ILicenseCertificateFile> LoadFiles()
        {
            return _cfg.Files
                .Select(f => SelectLoadRoutine(f.FullName)(f.FullName));
        }

        private static Func<String, ILicenseCertificateFile> SelectLoadRoutine(String filePath)
        {
            switch (Path.GetExtension(filePath).ToUpperInvariant())
            {
                case ".TBLCD":
                    return LoadCertificateFromAdsFile;

                default:
                    throw new FormatException("Expected file extension (.tblcd)");
            }
        }

        private static ILicenseCertificateFile LoadCertificateFromAdsFile(String fileName)
        {
            LicenseCertificateFile file = new LicenseCertificateFile();

            file.Load(fileName);

            return file;
        }

        private readonly LocalCertificateStoreConfig _cfg;
    }
}
