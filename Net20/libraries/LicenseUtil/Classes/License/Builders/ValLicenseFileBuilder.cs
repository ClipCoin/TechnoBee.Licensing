using System;
using System.Collections.Generic;
using System.Text;

namespace LicenseUtil.Classes.License.Builders
{
    public class ValLicenseFileBuilder
    {
        private List<ValLicense> Licenses = new List<ValLicense>();
        private string Id = "";
        private DateTime? IssueTime = null;

        public void AddLicense(ValLicense license)
        {
            Licenses.Add(license);
        }

        public void SetId(string id)
        {
            Id = id;
        }

        public void SetIssueTime(DateTime issueTime)
        {
            IssueTime = issueTime;
        }

        public ValLicenseFile Build()
        {
            ValLicenseFile result = new ValLicenseFile();
            result.id = Id;
            result.IssueTime = IssueTime.Value;
            result.AddRange(Licenses);

            return result;
        }
    }
}
