using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Framework
{
    public class HostFingerprint
        : IHostFingerprint
    {
        public String Value { get; set; }
        public String Algorithm { get; set; }
    }
}
