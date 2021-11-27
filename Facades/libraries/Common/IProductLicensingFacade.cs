using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace TechnoBee.Licensing.Facades
{
    [Guid("7523F37A-419E-4CF6-9DC0-EA8CE46D0ECE")]
    public interface IProductLicensingFacade
    {
        IProductFeatureLicense GetProductFeatureLicense(String featureName);
    }
}
