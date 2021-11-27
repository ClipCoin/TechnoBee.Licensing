using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TechnoBee.Counters.Storage;

namespace TechnoBee.Licensing.Clients
{
    public interface ILicenseRegistryBuilder
    {
        ILicenseRegistryBuilder AddStore(ILicenseCertificateStore store);
        ILicenseRegistry BuildLicenseRegistry();
    }
}
