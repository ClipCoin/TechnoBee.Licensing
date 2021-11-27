using System;
using System.Collections.Generic;
using System.Text;

namespace LicenseUtil.Classes.License
{
    public class ValLicenseFile : List<ValLicense>
    {
        public string id { get; set; }
        public DateTime IssueTime { get; set; }


    }
}
