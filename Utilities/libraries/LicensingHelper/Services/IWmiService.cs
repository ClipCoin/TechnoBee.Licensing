using System;
using System.Collections.Generic;
using System.Text;
using TechnoBee.Licensing.Utilities.Common;

namespace TechnoBee.Licensing.Utilities.LicensingHelper.Services {
    public interface IWmiService {
        String GetHostName();
        IEnumerable<IDiskDrive> GetDiskDrives();
        IEnumerable<INetworkInterface> GetNetworkInterfaces();
        //FingerprintData GetFingerprint();
    }
}
