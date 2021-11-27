using System.Collections.Generic;

namespace LicenseUtil.Classes.License.Builders
{
    public class ValProductBuilder
    {
        private string _guid = "";
        private List<string> Versions = new List<string>();
        private List<string> Editions = new List<string>();
        private List<ValApplication> Applications = new List<ValApplication>();
        private List<ValComponent> Components = new List<ValComponent>();

        public void SetGuid(string guid)
        {
            _guid = guid;
        }

        public void AddVersion(string version)
        {
            Versions.Add(version);
        }

        public void AddEdition(string edition)
        {
            Editions.Add(edition);
        }

        public void AddApplication(ValApplication application)
        {
            Applications.Add(application);
        }

        public void AddComponent(ValComponent component)
        {
            Components.Add(component);
        }
        
        public ValProduct Build()
        {
            ValProduct result = new ValProduct() { Guid = _guid };
            result.Applications.AddRange(Applications);
            result.Versions.AddRange(Versions);
            result.Editions.AddRange(Editions);
            result.Components.AddRange(Components);

            return result;
        }
    }
}
