using System;
using System.Collections.Generic;
using System.Text;

namespace LicenseUtil.Classes.License.Builders
{
    public class ValCoverageBuilder
    {
        private List<ValProduct> Products = new List<ValProduct>();

        public void AddProduct(ValProduct product)
        {
            Products.Add(product);
        }

        public ValCoverage Build()
        {
            ValCoverage result = new ValCoverage();
            result.AddRange(Products);

            return result;
        }
    }
}
