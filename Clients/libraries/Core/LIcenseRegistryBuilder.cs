using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TechnoBee.Counters.Storage;

namespace TechnoBee.Licensing.Clients
{
    public class LicenseRegistryBuilder
        : ILicenseRegistryBuilder
    {
        public ILicenseRegistryBuilder AddStore(ILicenseCertificateStore store)
        {
            if (store == null)
                throw new ArgumentNullException(nameof(store));

            _stores.Add(store);

            return this;
        }

        public ILicenseRegistry BuildLicenseRegistry()
        {
            return new LicenseRegistry(_stores);
        }

        private readonly List<ILicenseCertificateStore> _stores = new List<ILicenseCertificateStore>();
    }
}
