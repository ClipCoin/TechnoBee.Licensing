using System;
using System.Collections.Generic;
using System.Text;

namespace LicenseUtil.Classes.License
{
    public class ValApplication
    {
        public string Name { get; private set; }
        public bool Enabled { get; private set; }

        public ValApplication(string name , bool enabled)
        {
            Name = name;
            Enabled = enabled;
        }
    }
}
