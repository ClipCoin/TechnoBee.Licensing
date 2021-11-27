using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Framework
{
    public interface ILicenseRestrictions
    {
        Object this[String key] { get; }
    }
}
