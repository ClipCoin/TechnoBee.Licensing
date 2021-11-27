using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Framework
{
    public interface IProductLicenseCoverage
    {
        Guid ProductGuid { get; }
        IEnumerable<String> Versions { get; }
        IEnumerable<String> Editions { get; }
        IReadOnlyDictionary<String, IProductFeatureLicense> Applications { get; }
        IReadOnlyDictionary<String, IProductFeatureLicense> Components { get; }
    }
}
