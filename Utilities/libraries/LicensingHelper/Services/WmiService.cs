using System;
using System.Linq;
using System.Collections.Generic;
using System.Management;
using TechnoBee.Licensing.Utilities.Common;

namespace TechnoBee.Licensing.Utilities.LicensingHelper.Services {
    public class WmiService : IWmiService {

        public IEnumerable<IDiskDrive> GetDiskDrives() {
            return new ManagementObjectSearcher($"SELECT * FROM Win32_DiskDrive")
                .Get()
                .Cast<ManagementObject>()
                .Select(mo => {
                    return new DiskDrive()
                    {
                        DeviceId = mo["DeviceID"].ToString(),
                        SerialNumber = mo["SerialNumber"].ToString()
                    };
                });
        }

        public string GetHostName() {
            return Environment.GetEnvironmentVariable("COMPUTERNAME");
        }

        public IEnumerable<INetworkInterface> GetNetworkInterfaces() {
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * FROM Win32_NetworkAdapterConfiguration"))
            {
                return searcher
                    .Get()
                    .Cast<ManagementObject>()
                    .Select(mo => {
                        return new NetworkInterface()
                        {
                            MacAddress = mo["MacAddress"]?.ToString()
                        };
                    })
                    .Where(ni => ni.MacAddress != null);
            }
        }
    }
}
