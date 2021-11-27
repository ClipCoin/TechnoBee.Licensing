using System;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;

namespace LicenseUtil
{
    public sealed class Ddsn1Sha256Fingerprint
    {
        public Byte[] GenerateFingerprint(IHostInformationDigest digest)
        {
            List<IDiskDrive> drives = new List<IDiskDrive>();
            IEnumerator<IDiskDrive> enumerator = digest.DiskDrives.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Current != null)
                    drives.Add(enumerator.Current);
            }

            if (drives.Count > 0)
            {
                drives.Sort((x, y) => x.DeviceId.CompareTo(y.DeviceId));

                String serialNumber = drives[0].SerialNumber;

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
