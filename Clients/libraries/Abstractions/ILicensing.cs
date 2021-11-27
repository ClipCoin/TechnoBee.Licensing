using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TechnoBee.Licensing.Framework;

namespace TechnoBee.Licensing.Clients
{
    public interface ILicensing
    {
        IProductFeatureLicense[] QueryLicenses(String productGuid, String productVersion, String productEdition, String applicationName, String effectiveDate, bool verifyHost );
        void AttachRegistry(ILicenseRegistry registry);
    }
}