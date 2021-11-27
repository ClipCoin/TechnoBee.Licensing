using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Framework
{
    public class ProductLicense
        : IProductLicense
    {
        public ProductLicense()
        {
            Authority = new LicensingAuthority();
            Holder = new LicenseHolder();
            Coverage = new LicenseCoverage();
            Prerequisites = new LicensePrerequisites();
            Scripts = new List<LicenseScript>();
        }

        public Guid Guid { get; set; }
        public DateTime IssueTime { get; set; }
        public DateTime EffectiveTime { get; set; }
        public TimeSpan ValidityPeriod { get; set; }
        public LicensingAuthority Authority { get; set; }
        public LicenseHolder Holder { get; set; }
        public LicenseCoverage Coverage { get; set; }
        public LicensePrerequisites Prerequisites { get; set; }
        public List<LicenseScript> Scripts { get; set; }

        ILicensingAuthority IProductLicense.Authority => Authority;
        ILicenseHolder IProductLicense.Holder => Holder;
        ILicenseCoverage IProductLicense.Coverage => Coverage;
        ILicensePrerequisites IProductLicense.Prerequisites => Prerequisites;
        IEnumerable<ILicenseScript> IProductLicense.Scripts => Scripts;
    }
}
