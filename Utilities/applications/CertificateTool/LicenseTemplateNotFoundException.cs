using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Utilities.CertificateTool
{
    public class LicenseTemplateNotFoundException
        : UserFriendlyException
    {
        private const String MESSAGE_TEMPLATE = "License template '{0}' not found.";

        public LicenseTemplateNotFoundException(String templateName)
            : base(String.Format(MESSAGE_TEMPLATE, templateName))
        {
            TemplateName = templateName;
        }

        public readonly String TemplateName;
    }
}
