using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Framework
{
    public interface ILicenseHolder
    {
        Int32 Id { get; }
        String IdentityProvider { get; }
        String Name { get; }
        String Contacts { get; }
    }
}
