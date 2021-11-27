using System;
using System.Collections.Generic;
using System.Text;

namespace LicenseUtil.Classes.License
{
    public class ValComponent
    {
        public string Name { get; set; }
        public Dictionary<string, string> Restrictions { get; set; } = new Dictionary<string, string>();
    }
}
