using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace TechnoBee.Licensing.Facades
{
    public enum ProductFeatureLicenseState
    {
        Unknown = 0x00,
        Granted = 0x01,
        Denied = 0x02,
    }

    [Guid("352DBF21-AF8C-49EF-AE51-DCFCDA738B14")]
    public interface IProductFeatureLicense
    {
        ProductFeatureLicenseState GetState();
        IProductLicenseProperties GetProperties();
    }
}
