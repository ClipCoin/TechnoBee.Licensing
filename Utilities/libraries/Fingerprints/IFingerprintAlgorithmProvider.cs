using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Utilities.Fingerprints
{
    public interface IFingerprintAlgorithmProvider
    {
        IFingerprintAlgorithm GetAlgorithm(String moniker);
    }
}
