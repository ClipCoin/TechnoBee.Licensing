using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Utilities.CertificateTool
{
    public class ExceptionBase
        : Exception
    {
        public ExceptionBase()
            : base()
        {

        }

        public ExceptionBase(String message)
            : base(message)
        {

        }

        public ExceptionBase(String message, Exception innerException)
            : base(message, innerException)
        {

        }

        //protected static String ComposeMessage(params String[] messageParts)
        //{
        //    return String.Concat(messageParts);
        //}
    }
}
