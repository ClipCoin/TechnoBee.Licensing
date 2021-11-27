using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Framework
{
    public interface IProductLicense
    {
        Guid Guid { get; }
        DateTime IssueTime { get; }
        DateTime EffectiveTime { get; }
        TimeSpan ValidityPeriod { get; }
        ILicensingAuthority Authority { get; }
        ILicenseHolder Holder { get; }
        ILicenseCoverage Coverage { get; }
        ILicensePrerequisites Prerequisites { get; }
        IEnumerable<ILicenseScript> Scripts { get; }
    }
}
