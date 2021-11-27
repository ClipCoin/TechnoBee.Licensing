using System.Collections.Generic;

namespace LicenseUtil.Classes.License.Builders
{
    public class ValPrerequisitesBuilder
    {
        private List<ValHost> Hosts = new List<ValHost>();

        public void AddHost(ValHost host)
        {
            Hosts.Add(host);
        }

        public ValPrerequisites Build()
        {
            ValPrerequisites result = new ValPrerequisites();
            result.Hosts.AddRange(Hosts);
            return result;
        }
    }
}
