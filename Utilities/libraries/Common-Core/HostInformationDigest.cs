using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Utilities.Common
{
    public class HostInformationDigest
        : IHostInformationDigest
    {
        public Version Version { get; set; }
        public DateTime Timestamp { get; set; }
        public String HostName { get; set; }
        public DiskDrive[] DiskDrives { get; set; }
        public NetworkInterface[] NetworkInterfaces { get; set; }

        IEnumerable<IDiskDrive> IHostInformationDigest.DiskDrives => DiskDrives;
        IEnumerable<INetworkInterface> IHostInformationDigest.NetworkInterfaces => NetworkInterfaces;
    }
}
