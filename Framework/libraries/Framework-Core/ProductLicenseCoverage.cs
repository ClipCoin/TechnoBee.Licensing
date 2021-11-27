using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Framework
{
    public class ProductLicenseCoverage
        : IProductLicenseCoverage
    {
        public ProductLicenseCoverage()
        {
            Versions = new List<String>();
            Editions = new List<String>();
            Applications = new Dictionary<String, ProductFeatureLicense>();
            Components = new Dictionary<String, ProductFeatureLicense>();
        }

        public Guid ProductGuid { get; set; }
        public List<String> Versions { get; set; }
        public List<String> Editions { get; set; }
        public Dictionary<String, ProductFeatureLicense> Applications { get; set; }
        public Dictionary<String, ProductFeatureLicense> Components { get; set; }

        IEnumerable<String> IProductLicenseCoverage.Versions => Versions;
        IEnumerable<String> IProductLicenseCoverage.Editions => Editions;
        IReadOnlyDictionary<String, IProductFeatureLicense> IProductLicenseCoverage.Applications => Applications
            .Select(kv => new KeyValuePair<String, IProductFeatureLicense>(kv.Key, kv.Value))
            .ToDictionary(kv => kv.Key, kv => kv.Value);

        IReadOnlyDictionary<String, IProductFeatureLicense> IProductLicenseCoverage.Components => Components
            .Select(kv => new KeyValuePair<String, IProductFeatureLicense>(kv.Key, kv.Value))
            .ToDictionary(kv => kv.Key, kv => kv.Value);
    }
}
