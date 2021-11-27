using System;
using System.Collections.Generic;
using System.Text;

namespace LicenseUtil
{
    public class DiskDrive
        : IDiskDrive
    {
        public String DeviceId { get; set; }
        public String SerialNumber { get; set; }
    }
}
