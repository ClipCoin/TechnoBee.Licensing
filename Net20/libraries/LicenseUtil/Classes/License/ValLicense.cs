using System;

namespace LicenseUtil.Classes.License
{
    public class ValLicense
    {
        public string Guid { get; private set; }
        public DateTime From { get; private set; }
        public DateTime To { get; private set; }

        public ValLicense(string guid, DateTime from, DateTime to, ValCoverage coverage, ValPrerequisites prerequisites)
        {
            Guid = guid;
            From = from;
            To = to;
            Coverage = coverage;
            Prerequisites = prerequisites;
        }

        public ValCoverage Coverage { get; private set; }
        public ValPrerequisites Prerequisites { get; private set; }

        public bool IsCurrent
        {
            get { return DateTime.Now > From && DateTime.Now.Date <= To; }
        }

        public override string ToString()
        {
            return this.Guid;
        }
    }
}
