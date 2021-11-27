using System;
using System.Collections.Generic;
using System.Xml;

namespace LicenseUtil.Classes
{
    public  struct LicenseState
    {
        public bool Valid
        {
            get { return Errors.Count == 0; }
        }

        public List<LicenseErrorCodes> Errors;
        public DateTime? Expiration;

        public LicenseState(DateTime? expiration, List<LicenseErrorCodes> errors = null)
        {
            Errors = errors ?? new List<LicenseErrorCodes>();
            Expiration = expiration;
        }

        public LicenseState(LicenseErrorCodes error, DateTime? expiration = null)
        {
            Errors = new List<LicenseErrorCodes>();
            Errors.Add(error);
            Expiration = expiration;
        }

        //public override string ToString()
        //{
        //    XmlDocument doc = new XmlDocument();
        //    XmlElement root = doc.CreateElement("State");
        //    root.();
        //}
    }
}
