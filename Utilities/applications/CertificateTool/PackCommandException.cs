using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Utilities.CertificateTool
{
    public class PackCommandException
        : CommandException
    {
        public PackCommandException(Exception innerException)
            : base("pack", "", innerException)
        {

        }
    }
}