using System;
using System.Collections.Generic;
using System.Text;

namespace LicenseUtil.Classes.License
{
    public class ValPrerequisites
    {
        public List<ValHost> Hosts = new List<ValHost>();

        public override string ToString()
        {
            return $"Hosts: {Hosts.Count}"; 
        }
    }
}
