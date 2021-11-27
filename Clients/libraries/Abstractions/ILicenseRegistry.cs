using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TechnoBee.Licensing.Framework;

namespace TechnoBee.Licensing.Clients
{
    public interface ILicenseRegistry
    {
        IProductFeatureLicense[] QueryLicenses(String productGuid, String productVersion, String productEdition, String applicationName, String effectiveDate, Predicate<IProductLicense> productLicenceSelector );
    }
}
