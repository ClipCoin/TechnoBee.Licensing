using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Framework
{
    public class LicensePrerequisites
        : ILicensePrerequisites
    {
        public LicensePrerequisites()
        {
            Host = new HostPrerequisites();
        }
        public HostPrerequisites Host { get; set; }

        IHostPrerequisites ILicensePrerequisites.Host => Host;
    }
}
