using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Utilities.Common
{
    public interface IDiskDrive
    {
        String DeviceId { get; }
        String SerialNumber { get; }
    }
}
