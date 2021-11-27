using System;
using System.Collections.Generic;
using System.Text;

namespace LicenseUtil
{
    public interface IWmiService
    {
        String GetHostName();
        IEnumerable<IDiskDrive> GetDiskDrives();
        IEnumerable<INetworkInterface> GetNetworkInterfaces();
        //FingerprintData GetFingerprint();
    }
}
