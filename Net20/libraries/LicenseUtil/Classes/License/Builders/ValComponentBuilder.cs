using System.Collections.Generic;

namespace LicenseUtil.Classes.License.Builders
{
    public class ValComponentBuilder
    {
        private string _name = "";
        private Dictionary<string, string> Restrictions = new Dictionary<string, string>();

        public void SetComponentName(string name)
        {
            _name = name;
        }

        public void AddRestriction(string name, object value)
        {
            Restrictions[name] = value.ToString();
        }
        
        public ValComponent Build()
        {
            ValComponent result = new ValComponent() { Name = _name };
            result.Restrictions = Restrictions;

            return result;
        }
    }
}
