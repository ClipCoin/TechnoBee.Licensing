using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Framework
{
    public class LicenseCertificate
        : ILicenseCertificate
    {
        public Guid Id { get; set; }

        public DateTime IssueTime { get; set; }
      
        public Version Version { get; set; }

        public IEnumerable<ProductLicense> Licenses { get; set; }

        IEnumerable<IProductLicense> ILicenseCertificate.Licenses => Licenses;
    }
}
