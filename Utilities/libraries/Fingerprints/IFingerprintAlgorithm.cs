using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TechnoBee.Licensing.Utilities.Common;

namespace TechnoBee.Licensing.Utilities.Fingerprints
{
    public interface IFingerprintAlgorithm
    {
        Byte[] GenerateFingerprint(IHostInformationDigest digest);
    }
}
