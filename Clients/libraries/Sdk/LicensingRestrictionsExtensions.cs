using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TechnoBee.Licensing.Framework;

namespace TechnoBee.Licensing.Clients
{
    public static class LicensingRestrictionsExtensions
    {
        public static Object GetValue(this ILicenseRestrictions restrictions, String key)
        {
            return restrictions[key];
        }

        public static T GetValue<T>(this ILicenseRestrictions restrictions, String key)
            where T: class
        {
            return restrictions[key] as T;
        }
    }
}
