using System;
using System.Collections.Generic;
using System.Text;

namespace Technobee.Licensing.Storage.DataModels.Models
{
    public class Fingerprint
    {
        public int FingerprintID { get; private set; }
        public int ExternalOwnerID { get; private set; }
        public string Comment { get; private set; }
        public DateTime Timestamp { get; private set; }
    }
}
