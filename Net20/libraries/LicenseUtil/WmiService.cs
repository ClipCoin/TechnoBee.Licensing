using System;
using System.Collections.Generic;
using System.Management;
using System.Text;

namespace LicenseUtil
{
    public class WmiService : IWmiService
    {
        public IEnumerable<IDiskDrive> GetDiskDrives()
        {
               var objects = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive")
                .Get();
            ICollection<IDiskDrive> result = new List<IDiskDrive>();
            foreach (var mo in objects)
            {
                if (mo["DeviceID"] == null)
                    continue;
                result.Add(new DiskDrive()
                    {
                        DeviceId = mo["DeviceID"].ToString(),
                        SerialNumber = mo["SerialNumber"] == null ? "no_serial" : mo["SerialNumber"].ToString()
                    });
            }

            return result;
        }

        public string GetHostName()
        {
            return Environment.GetEnvironmentVariable("COMPUTERNAME");
        }

        public IEnumerable<INetworkInterface> GetNetworkInterfaces()
        {
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * FROM Win32_NetworkAdapterConfiguration"))
            {
                var objects = searcher
                    .Get();

                ICollection<INetworkInterface> result = new List<INetworkInterface>();

                foreach (var mo in objects)
                {
                    var address = new NetworkInterface()
                    {
                        MacAddress = mo["MacAddress"]?.ToString()
                    };

                    if (address.MacAddress != null)
                        result.Add(address);
                }

                return result;
            }
        }
    }
}
