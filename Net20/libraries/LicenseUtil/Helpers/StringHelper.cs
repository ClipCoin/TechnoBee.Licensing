using System;
using System.Collections.Generic;
using System.Text;

namespace LicenseUtil.Helpers
{
    public class StringHelper
    {
        public static bool Equals(string A, string B)
        {
            return String.Equals(A.Trim(), B.Trim(), StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
