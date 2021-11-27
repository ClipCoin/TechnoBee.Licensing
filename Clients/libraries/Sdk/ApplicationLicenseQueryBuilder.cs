using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TechnoBee.Licensing.Framework;

namespace TechnoBee.Licensing.Clients
{
    internal class ApplicationLicenseQueryBuilder
        : IProductFeatureLicenseQueryBuilder
    {
        public ApplicationLicenseQueryBuilder(ILicensing licensing)
        {
            _licensing = licensing
                ?? throw new ArgumentNullException(nameof(licensing));
        }

        public IProductFeatureLicenseQueryBuilder ForApplication(string applicationName)
        {
            _query.ApplicationName = applicationName;

            return this;
        }

        public IProductFeatureLicenseQueryBuilder ForProductEdition(string edition)
        {
            _query.ProductEdition = edition;

            return this;
        }

        public IProductFeatureLicenseQueryBuilder ForProduct(Guid productGuid)
        {
            _query.ProductGuid = productGuid;

            return this;
        }

        public IProductFeatureLicenseQueryBuilder ForProductVersion(Version version)
        {
            _query.ProductVersion = version;

            return this;
        }

        public IProductFeatureLicenseQueryBuilder ForExecutingAssembly()
        {
            EnsureLicenseAttribute attribute = Assembly
                .GetExecutingAssembly()
                .GetCustomAttribute<EnsureLicenseAttribute>();

            UpdateQueryFromAttribute(attribute, _query);

            return this;
        }

        public IProductFeatureLicenseQueryBuilder ForEntryAssembly()
        {
            EnsureLicenseAttribute attribute = Assembly
                .GetEntryAssembly()
                .GetCustomAttribute<EnsureLicenseAttribute>();

            UpdateQueryFromAttribute(attribute, _query);

            return this;
        }

        public IEnumerable<IProductFeatureLicense> YieldAll()
        {
            return _licensing.QueryApplicationLicenses(_query);
        }

        public IProductFeatureLicense YieldSingle()
        {
            return _licensing.QuerySingleApplicationLicense(_query);
        }

        public IProductFeatureLicense YieldSingleOrDefault()
        {
            return _licensing.QuerySingleOrDefaultApplicationLicense(_query);
        }

        private void UpdateQueryFromAttribute(EnsureLicenseAttribute attribute, ApplicationLicenseQuery query)
        {
            if (attribute != null)
            {
                _query.ProductGuid = attribute.ProductId ?? _query.ProductGuid;
                _query.ProductEdition = attribute.ProductEdition ?? _query.ProductEdition;
                _query.ProductVersion = attribute.ProductVersion ?? _query.ProductVersion;
                _query.ApplicationName = attribute.ApplicationName ?? _query.ApplicationName;
            }
        }

        public IProductFeatureLicenseQueryBuilder EffectiveOn( DateTime time )
        {
            _query.EffectiveDate = time;

            return this;
        }

        public IProductFeatureLicenseQueryBuilder ForThisHost()
        {
            _query.VerifyHost = true;

            return this;
        }

        private readonly ApplicationLicenseQuery _query = new ApplicationLicenseQuery();
        private readonly ILicensing _licensing;
    }
}
