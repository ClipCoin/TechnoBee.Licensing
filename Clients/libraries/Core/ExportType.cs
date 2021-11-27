using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Clients
{
    public class ExportType
    {
        public ExportType()
        {

        }

        public ExportType(ExportType export)
        {
            StringValue = export.StringValue;
        }

        public String StringValue { get; set; }
    }
}
