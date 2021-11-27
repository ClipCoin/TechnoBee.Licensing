using System;
using System.Collections.Generic;
using System.Text;

namespace Technobee.Licensing.Storage.DataModels.Models
{
    public class License
    {
        public int LicenseID { get; private set; }
        public int ExternalOwnerID { get; private set; }
        public DateTime DateFrom { get; private set; }
        public DateTime DateTo { get; private set; }
        public DateTime Timestamp { get; private set; }
    }
}
