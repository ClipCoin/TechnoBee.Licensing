using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Clients
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public sealed class EnsureLicenseAttribute
        : Attribute
    {
        public EnsureLicenseAttribute(String productIdString = null
            , String productEdition = null
            , String productVersionString = null
            , String applicationName = null)
        {
            ProductId = Guid.Parse(productIdString);
            ProductEdition = productEdition;
            ProductVersion = Version.Parse(productVersionString);
            ApplicationName = applicationName;
    }

        public readonly Guid? ProductId;
        public readonly String ProductEdition;
        public readonly Version ProductVersion;
        public readonly String ApplicationName;
    }
}
