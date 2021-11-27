using System;
using System.Collections.Generic;
using System.Text;

namespace Technobee.Licensing.Storage.DataModels.Models
{
    public class LicenseProduct
    {
        public int LicenseProductID { get; set; }
        public int LicenseID { get; set; }
        public int ProductID { get; set; }
    }
}
