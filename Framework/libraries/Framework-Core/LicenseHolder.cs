using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Framework
{
    public class LicenseHolder
        : ILicenseHolder
    {
        public int Id { get; set; }
        public String IdentityProvider { get; set; }
        public String Name { get; set; }
        public String Contacts { get; set; }
    }
}
