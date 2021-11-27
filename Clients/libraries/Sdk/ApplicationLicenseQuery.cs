using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Clients
{
    public class ApplicationLicenseQuery
        : IProductFeatureLicenseQuery
    {
        public Guid? ProductGuid { get; set; }
        public Version ProductVersion { get; set; }
        public string ProductEdition { get; set; }
        public string ApplicationName { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public bool VerifyHost { get; set; }

        public override string ToString()
        {
            return $"{ApplicationName} ({ProductEdition} edition) ver. {this.ProductVersion}"; 
        }
    }
}
