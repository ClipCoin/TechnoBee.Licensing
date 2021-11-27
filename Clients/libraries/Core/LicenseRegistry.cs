using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoBee.Licensing.Framework;
using TechnoBee.Counters.Storage;

namespace TechnoBee.Licensing.Clients
{
    internal sealed class LicenseRegistry
        : ILicenseRegistry
    {
        public LicenseRegistry( IEnumerable<ILicenseCertificateStore> stores )
        {
            _stores = stores
                ?? throw new ArgumentNullException(nameof(stores));
        }
         
        public IProductFeatureLicense[] QueryLicenses( string productGuidStr, string productVersionStr, string productEdition, string applicationName, string effectiveDateStr, Predicate<IProductLicense> productLicenceSelector )
        {
            if (!Guid.TryParse(productGuidStr, out Guid productGuid))
            {
                throw new Exception($"Invalid guid string '{productGuid}'.");
            }

            if (!DateTime.TryParse(effectiveDateStr, out DateTime effectiveDate))
            {
                throw new Exception($"Invalid date string '{effectiveDateStr}'.");
            }

            return QueryLicensesInternal(productGuid, productVersionStr, productEdition, applicationName, effectiveDate, productLicenceSelector);
        }

        private IProductFeatureLicense[] QueryLicensesInternal( Guid productGuid, string productVersion, string productEdition, string applicationName, DateTime effectiveTime, Predicate<IProductLicense> productLicenceSelector )
        {
            return _stores.SelectMany(s => s.LoadFiles())
                .Select(f => f.Content)
                .Where(c => c.Licenses != null)
                .SelectMany(c => c.Licenses)
                .Where(l => l.EffectiveTime >= effectiveTime)
                .Where(l => productLicenceSelector(l))
                .SelectMany(l => l.Coverage.Products)
                .Where(p => p.ProductGuid == productGuid && (p.Editions == null || !p.Editions.Any() || p.Editions.Contains(productEdition)) && (productVersion == null || p.Versions == null || !p.Versions.Any() || p.Versions.Any(v => productVersion.StartsWith(v))))
                .Select(p => p.Applications)
                .Where(c => c.ContainsKey(applicationName))
                .SelectMany(c => c.Values)
                .ToArray();
        }

        private readonly IEnumerable<ILicenseCertificateStore> _stores;
    }
}
