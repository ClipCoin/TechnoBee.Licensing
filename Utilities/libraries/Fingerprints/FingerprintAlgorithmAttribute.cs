using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Utilities.Fingerprints
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class FingerprintAlgorithmAttribute
        : Attribute
    {
        public FingerprintAlgorithmAttribute(String moniker)
        {
            Moniker = moniker;
        }

        public readonly String Moniker;
    }
}
