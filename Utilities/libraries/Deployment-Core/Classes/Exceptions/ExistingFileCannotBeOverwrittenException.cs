using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Utilities.DeploymentCore.Exceptions
{
    public class ExistingFileCannotBeOverwrittenException
        : Exception
    {
        private const String MESSAGE_TEMPLATE = "File '{0}' already exists.";

        public ExistingFileCannotBeOverwrittenException(String fileName)
            : base(String.Format(MESSAGE_TEMPLATE, fileName))
        {
            FileName = fileName;
        }

        public readonly String FileName;
    }
}
