using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Framework
{
    public class LicensingAuthority
        : ILicensingAuthority
    {
        public Guid Guid { get; set; }
        public String Name { get; set; }
        public Guid FacilityGuid { get; set; }
    }
}
