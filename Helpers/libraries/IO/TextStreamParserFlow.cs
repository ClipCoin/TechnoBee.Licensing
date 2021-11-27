using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Helpers
{
    public class TextStreamParserFlow
    {
        public TextStreamParserFlow(TextReader reader)
        {
            _reader = reader;
        }

        public TextReader Reader => _reader;

        private TextReader _reader;
    }
}
