using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TechnoBee.Licensing.Framework;

using Microsoft.Extensions.DependencyInjection;
using TechnoBee.Licensing.Utilities.Fingerprints;
using TechnoBee.Licensing.Utilities.Common;
using TechnoBee.Licensing.Utilities.Deployment;

namespace TechnoBee.Licensing.Clients
{
    internal class LicensingService
        : ILicensing
    {
        public LicensingService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            _lazyFingerprintAlgorithmProvider = new Lazy<IFingerprintAlgorithmProvider>(() => _serviceProvider.GetRequiredService<IFingerprintAlgorithmProvider>());
            _lazyHostInformationDigest = new Lazy<IHostInformationDigest>(() => _serviceProvider.GetRequiredService<IHostInformationDigestProvider>().GenerateHostInformationDigest());
        }

        public void AttachRegistry(ILicenseRegistry registry)
        {
            _registries.Add(registry);
        }

        public IProductFeatureLicense[] QueryLicenses(String productGuid, String productVersion, String productEdition, String applicationName, String effectiveDate, bool verifyHost )
        {
            return _registries
                .SelectMany(r => r.QueryLicenses(productGuid, productVersion, productEdition, applicationName, effectiveDate, l => verifyHost ? VerifyHost(l, _lazyHostInformationDigest.Value) : DontVerifyHost(l)))
                .ToArray();
        }

        private bool VerifyHost( IProductLicense license, IHostInformationDigest hostInformationDigest )
        {
            return license.Prerequisites.Host.Fingerprints.All(f => f.Value == Convert.ToBase64String(_lazyFingerprintAlgorithmProvider.Value.GetAlgorithm(f.Algorithm).GenerateFingerprint(hostInformationDigest)));
        }

        private bool DontVerifyHost( IProductLicense license )
        {
            return true;
        }

        private readonly Lazy<IFingerprintAlgorithmProvider> _lazyFingerprintAlgorithmProvider;
        private readonly Lazy<IHostInformationDigest> _lazyHostInformationDigest;
        private readonly IServiceProvider _serviceProvider;
        private readonly List<ILicenseRegistry> _registries = new List<ILicenseRegistry>();
    }
}
