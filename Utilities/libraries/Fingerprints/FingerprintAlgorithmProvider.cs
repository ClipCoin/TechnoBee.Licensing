using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TechnoBee.Licensing.Helpers;

namespace TechnoBee.Licensing.Utilities.Fingerprints
{
    internal class FingerprintAlgorithmProvider
        : IFingerprintAlgorithmProvider
    {
        public IFingerprintAlgorithm GetAlgorithm(string moniker)
        {
            return Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(t => typeof(IFingerprintAlgorithm).IsAssignableFrom(t))
                .Where(t => t.GetCustomAttribute<FingerprintAlgorithmAttribute>()?.Moniker == moniker)
                .SingleOrDefault()
                .InvokeDefaultConstructor<IFingerprintAlgorithm>();
        }
    }
}
