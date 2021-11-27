using System;
using System.Collections.Generic;
using System.Text;

namespace LicenseUtil
{
    public interface IHostInformationDigest
    {
        Version Version { get; }
        DateTime Timestamp { get; }
        String HostName { get; }
        IEnumerable<IDiskDrive> DiskDrives { get; }
        IEnumerable<INetworkInterface> NetworkInterfaces { get; }
    }
}
