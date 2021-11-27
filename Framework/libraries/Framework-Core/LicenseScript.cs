using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Framework
{
    public class LicenseScript
        : ILicenseScript
    {
        public String Hook { get; set; }
        public String Language { get; set; }
        public String Code { get; set; }
    }
}
