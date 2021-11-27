using System;
using System.Collections.Generic;
using System.Text;

namespace LicenseUtil.Classes.License.Builders
{
    public class ValLicenseBuilder
    {
        private string _guid = "";
        private DateTime IssueTime;
        private DateTime EffectiveTime;
        private ValCoverage Coverage;
        private ValPrerequisites Prerequisites;


        public void SetGuid(string guid)
        {
            _guid = guid;
        }

        public void SetCoverage(ValCoverage coverage)
        {
            Coverage = coverage;
        }

        public void SetPrerequisites(ValPrerequisites prerequisites)
        {
            Prerequisites = prerequisites;
        }

        public void SetPeriod(DateTime[] period)
        {
            IssueTime = period[0];
            EffectiveTime = period[1];
        }

        public ValLicense Build()
        {
            return new ValLicense(_guid, IssueTime, EffectiveTime, Coverage, Prerequisites);
        }
    }
}
