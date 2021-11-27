using System;
using System.Collections.Generic;
using System.Text;

namespace Technobee.Licensing.Storage.DataModels.Models
{
    public class LicenseProductVersion
    {
        public int LicenseProductVersionID { get; set; }
        public int LicenseProductID { get; set; }
        public int VersionID { get; set; }
    }
}
