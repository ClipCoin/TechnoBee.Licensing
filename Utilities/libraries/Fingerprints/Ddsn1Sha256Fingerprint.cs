using System;
using System.Security.Cryptography;
using System.Linq;
using System.Text;

using TechnoBee.Licensing.Utilities.Common;

namespace TechnoBee.Licensing.Utilities.Fingerprints
{
    [FingerprintAlgorithm("Ddsn1:Sha256")]
    public sealed class Ddsn1Sha256Fingerprint
        : IFingerprintAlgorithm
    {
        public Byte[] GenerateFingerprint(IHostInformationDigest digest)
        {
            if (digest.DiskDrives.Count() > 0)
            {
                String serialNumber = digest
                    .DiskDrives
                    .OrderBy(dd => dd.DeviceId)
                    .First()
                    .SerialNumber;

                Byte[] bytes = Encoding.UTF8.GetBytes(serialNumber);

                return HashAlgorithm
                    .Create("SHA256")
                    .ComputeHash(bytes);
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
