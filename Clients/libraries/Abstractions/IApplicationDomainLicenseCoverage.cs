using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Clients
{
    public interface IApplicationDomainLicenseCoverage
    {
        Boolean IsEnabled { get; }
        IReadOnlyDictionary<String, Object> Restrictions { get; }
    }
}
