using System;
using System.Collections.Generic;
using System.Text;

namespace LicenseUtil
{
    public interface IDiskDrive
    {
        String DeviceId { get; }
        String SerialNumber { get; }
    }
}
