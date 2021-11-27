using System;
using System.Collections.Generic;
using System.Text;

namespace LicenseUtil
{
    internal sealed class HostInformationDigestProvider
    {
        public HostInformationDigestProvider()
        {
            _wmiService = new WmiService();
        }

        public IHostInformationDigest GenerateHostInformationDigest()
        {
            HostInformationDigest result = new HostInformationDigest()
            {
                Version = new Version("1.0"),
                Timestamp = DateTime.Now,
                HostName = _wmiService.GetHostName()
            };

            if (_wmiService == null)
                throw new Exception("WmiService service is null");

            var _drives = _wmiService.GetDiskDrives();
            List<DiskDrive> drives = new List<DiskDrive>();
            foreach (var d in _drives)
                drives.Add((DiskDrive)d);

            result.DiskDrives = drives.ToArray();

            var _interfaces = _wmiService.GetNetworkInterfaces();
            List<NetworkInterface> interfaces = new List<NetworkInterface>();
            foreach (var ni in _interfaces)
                interfaces.Add((NetworkInterface)ni);

            result.NetworkInterfaces = interfaces.ToArray();

            return result;
        }

        private readonly IWmiService _wmiService;
    }
}