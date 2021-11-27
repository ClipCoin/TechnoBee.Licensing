using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace TechnoBee.Licensing.Facades
{
    [Guid("8A4DB8AF-04EB-4E8C-ADAE-B1FF78BCC07C")]
    public interface IProductLicencingFacadeBroker
    {
        IProductLicensingFacade GetProductLicensingFacade(String productGuid);
    }
}
