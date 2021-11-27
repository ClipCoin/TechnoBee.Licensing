using System.Collections.Generic;

namespace LicenseUtil.Classes.License
{
    public class ValHost : List<ValFingerprint>
    {
        public override string ToString()
        {
            return $"{Count} fingerprints";
        }
    }
}