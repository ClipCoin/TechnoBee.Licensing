using System;
using System.Collections.Generic;
using System.Text;

namespace LicenseUtil.Classes.License
{
    public class ValProduct
    {
        public string Guid { get; set; }

        public List<string> Versions { get; set; } = new List<string>();
        public List<ValApplication> Applications { get; set; } = new List<ValApplication>();
        public List<string> Editions { get; set; } = new List<string>();
        public List<ValComponent> Components { get; set; } = new List<ValComponent>();
    }
}
