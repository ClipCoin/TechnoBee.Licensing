using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Utilities.Common
{
    public class DiskDrive
        : IDiskDrive
    {
        public String DeviceId { get; set; }
        public String SerialNumber { get; set; }
    }
}
