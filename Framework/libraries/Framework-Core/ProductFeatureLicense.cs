using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Framework
{
    public class ProductFeatureLicense
        : IProductFeatureLicense
    {
        public ProductFeatureLicense()
        {
            Features = new Dictionary<String, ProductFeatureLicense>();
            Settings = new Dictionary<String, String>();
            Restrictions = new Dictionary<String, String>();
        }

        public FeatureLicenseState LicenseState { get; set; }
        public Dictionary<String, ProductFeatureLicense> Features { get; set; }
        public Dictionary<String, String> Settings { get; set; }
        public Dictionary<String, String> Restrictions { get; set; }

        IReadOnlyDictionary<String, IProductFeatureLicense> IProductFeatureLicense.Features => Features
            .Select( kv => new KeyValuePair<String, IProductFeatureLicense>(kv.Key, kv.Value))
            .ToDictionary(kv => kv.Key, kv => kv.Value);

        IReadOnlyDictionary<String, String> IProductFeatureLicense.Restrictions => Restrictions;
        IReadOnlyDictionary<String, String> IProductFeatureLicense.Settings => Settings;
    }
}
