using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TechnoBee.Licensing.Utilities.Common;

namespace TechnoBee.Licensing.Utilities.Fingerprints
{
    [FingerprintAlgorithm("DdsnA:Sha256")]
    public class DdsnASha256Fingerprint
        : IFingerprintAlgorithm
    {
        public Byte[] GenerateFingerprint(IHostInformationDigest digest)
        {
            if (digest.DiskDrives.Count() > 0)
            {
                String[] serialNumbers = digest
                    .DiskDrives
                    .OrderBy(dd => dd.DeviceId)
                    .Select(dd => dd.SerialNumber)
                    .ToArray();

                Byte[] bytes = Encoding
                    .UTF8
                    .GetBytes(String.Join("|", serialNumbers));

                HashAlgorithm hashAlgorithm = HashAlgorithm.Create("SHA256");

                return hashAlgorithm.ComputeHash(bytes);
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
