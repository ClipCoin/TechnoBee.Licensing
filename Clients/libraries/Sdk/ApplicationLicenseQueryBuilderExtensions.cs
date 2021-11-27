using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Clients
{
    public static class ApplicationLicenseQueryBuilderExtensions
    {
        public static IProductFeatureLicenseQueryBuilder ForProduct(this IProductFeatureLicenseQueryBuilder builder, String guidString)
        {
            return builder.ForProduct(Guid.Parse(guidString));
        }
        public static IProductFeatureLicenseQueryBuilder ForProductVersion(this IProductFeatureLicenseQueryBuilder builder, String versionString)
        {
            return builder.ForProductVersion(Version.Parse(versionString));
        }
    }
}
