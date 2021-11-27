using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Framework
{
    public class LicenseCoverage
        : ILicenseCoverage
    {
        public LicenseCoverage()
        {
            Products = new List<ProductLicenseCoverage>();
        }

        public List<ProductLicenseCoverage> Products { get; set; }
        IEnumerable<IProductLicenseCoverage> ILicenseCoverage.Products => Products;
    }
}
