using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Framework
{
    public interface IProductFeatureLicense
    {
        FeatureLicenseState LicenseState { get; }
        IReadOnlyDictionary<String, IProductFeatureLicense> Features { get; }
        IReadOnlyDictionary<String, String> Settings { get; }
        IReadOnlyDictionary<String, String> Restrictions { get; }
        //IProductFeatureLicense ResolveLicenseForSubDomain(String domainPath);
    }
}
