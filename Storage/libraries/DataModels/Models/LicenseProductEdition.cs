using System;
using System.Collections.Generic;
using System.Text;

namespace Technobee.Licensing.Storage.DataModels.Models
{
    public class LicenseProductEdition
    {
        public int LicenseProductEditionID { get; set; }
        public int LicenseProductID { get; set; }
        public int EditionID { get; set; }
    }
}
