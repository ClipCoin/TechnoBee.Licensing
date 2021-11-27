using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Helpers
{
    public class StreamParserFlow
    {
        public StreamParserFlow(Stream stream)
        {
            _stream = stream;
        }

        public Stream Stream => _stream;

        private Stream _stream;
    }
}
