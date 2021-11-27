using System;
using System.Collections.Generic;
using System.Text;

namespace LicenseUtil.Classes.License
{
    public class ValCoverage : List<ValProduct>
    {
        public override string ToString()
        {
            return $"{Count} products";
        }
    }
}
