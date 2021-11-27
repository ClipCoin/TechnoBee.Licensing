using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TechnoBee.Licensing.Framework;

namespace TechnoBee.Licensing.Clients
{
    public interface IProductFeatureLicenseQueryBuilder
    {
        IProductFeatureLicenseQueryBuilder ForProduct(Guid productGuid);
        IProductFeatureLicenseQueryBuilder ForProductVersion(Version version);
        IProductFeatureLicenseQueryBuilder ForProductEdition(String edition);
        IProductFeatureLicenseQueryBuilder ForApplication(String applicationName);
        IProductFeatureLicenseQueryBuilder ForEntryAssembly();
        IProductFeatureLicenseQueryBuilder ForExecutingAssembly();
        IProductFeatureLicenseQueryBuilder EffectiveOn(DateTime time);
        IProductFeatureLicenseQueryBuilder ForThisHost();
        IProductFeatureLicense YieldSingle();
        IProductFeatureLicense YieldSingleOrDefault();
        IEnumerable<IProductFeatureLicense> YieldAll();
        //IProductFeatureLicense YieldMerged();
    }
}
