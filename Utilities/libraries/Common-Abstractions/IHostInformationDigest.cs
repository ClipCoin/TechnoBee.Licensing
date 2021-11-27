using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Utilities.Common
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
