using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TechnoBee.Licensing.Framework;
using TechnoBee.Licensing.Utilities.DeploymentCore.Exceptions;

namespace TechnoBee.Licensing.Clients
{
    public static class LicensingExtensions
    {
        public static IProductFeatureLicense QuerySingleApplicationLicense(this ILicensing registry, IProductFeatureLicenseQuery query)
        {
            IProductFeatureLicense result = registry
                .QueryLicenses
                (query.ProductGuid.ToString()
                , query.ProductVersion.ToString()
                , query.ProductEdition
                , query.ApplicationName
                , query.EffectiveDate.ToString()
                , query.VerifyHost).FirstOrDefault();

            return result == null ? throw new LicenseNotFoundException($"No license detected for product {query}") : result;
        }

        public static IProductFeatureLicense QuerySingleOrDefaultApplicationLicense(this ILicensing registry, IProductFeatureLicenseQuery query)
        {
            return registry
                .QueryLicenses
                (query.ProductGuid.ToString()
                , query.ProductVersion.ToString()
                , query.ProductEdition
                , query.ApplicationName
                , query.EffectiveDate.ToString()
                , query.VerifyHost)
                .SingleOrDefault();
        }

        public static IEnumerable<IProductFeatureLicense> QueryApplicationLicenses(this ILicensing registry, IProductFeatureLicenseQuery query)
        {
            return registry
                .QueryLicenses
                (query.ProductGuid.ToString()
                , query.ProductVersion.ToString()
                , query.ProductEdition
                , query.ApplicationName
                , query.EffectiveDate.ToString()
                , query.VerifyHost);
        }

        public static ILicensing AttachRegistry(this ILicensing licensing, Action<ILicenseRegistryBuilder> configuration)
        {
            LicenseRegistryBuilder builder = new LicenseRegistryBuilder();

            configuration(builder);

            licensing.AttachRegistry(builder.BuildLicenseRegistry());

            return licensing;
        }

        public static IProductFeatureLicenseQueryBuilder ConstructApplicationLicenseQuery(this ILicensing licensing)
        {
            return new ApplicationLicenseQueryBuilder(licensing);
        }
    }
}
