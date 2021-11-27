using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Clients
{
    public interface IProductFeatureLicenseQuery
    {
        Guid? ProductGuid { get; }
        Version ProductVersion { get; }
        String ProductEdition { get; }
        String ApplicationName { get; }
        DateTime? EffectiveDate { get; }
        bool VerifyHost { get; }
    }
}
