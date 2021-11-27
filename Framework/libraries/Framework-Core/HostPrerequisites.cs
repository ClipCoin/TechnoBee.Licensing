using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Framework
{
    public class HostPrerequisites
        : IHostPrerequisites
    {
        public HostPrerequisites()
        {
            Fingerprints = new List<HostFingerprint>();
        }

        public List<HostFingerprint> Fingerprints { get; set; }

        IEnumerable<IHostFingerprint> IHostPrerequisites.Fingerprints => Fingerprints;
    }
}
