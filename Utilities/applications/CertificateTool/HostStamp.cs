#region
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using qval = System.DateTime;
#endregion

namespace TechnoBee.Licensing.Utilities.CertificateTool
{
    public class HostStamp
    {
        const long StampQ = 636689376000000000;

        public static bool IsValid
            => qval.Now.Ticks <= StampQ;
    }
}
