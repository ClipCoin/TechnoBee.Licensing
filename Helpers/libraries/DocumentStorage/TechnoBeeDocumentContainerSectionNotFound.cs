using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Helpers
{
    public class TechnoBeeDocumentContainerSectionNotFoundException
        : Exception
    {
        private const String MESSAGE_TEMPLATE = "Section '{0}' not found.";

        public TechnoBeeDocumentContainerSectionNotFoundException(String sectionName)
            : base(String.Format(MESSAGE_TEMPLATE, sectionName))
        {
            SectionName = sectionName;
        }

        public readonly String SectionName;
    }
}
