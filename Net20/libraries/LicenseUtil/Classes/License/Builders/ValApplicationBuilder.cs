using System;
using System.Collections.Generic;
using System.Text;

namespace LicenseUtil.Classes.License.Builders
{
    public class ValApplicationBuilder
    {
        private string Name = "";
        private bool Enabled = false;

        public void SetName(string name)
        {
            Name = name;
        }
        public void SetState(bool enabled)
        {
            Enabled = enabled;
        }

        public ValApplication Build()
        {
            return new ValApplication(Name, Enabled);
        }
    }
}
