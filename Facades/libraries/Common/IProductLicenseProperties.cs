using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Facades
{
    public interface IProductLicenseProperties
    {
        Object GetPropertyValue(String propertyName);
       //  void SetPropertyValue(String propertyName, Object propertyValue); // Set не нужен. Properties должны быть readonly    
    }
}
