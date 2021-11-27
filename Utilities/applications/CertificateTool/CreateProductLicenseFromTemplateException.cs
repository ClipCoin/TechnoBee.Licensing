using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Utilities.CertificateTool
{
    public class CreateProductLicenseFromTemplateException
        : CommandException
    {
        private const String DEFAULT_MESSAGE = "Failed to create a new product license file from template.";

        public CreateProductLicenseFromTemplateException(Exception innerException)
            : base("new", DEFAULT_MESSAGE, innerException)
        {

        }
    }
}
