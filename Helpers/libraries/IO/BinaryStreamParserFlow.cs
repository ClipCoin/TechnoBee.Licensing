using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Helpers
{
    public class BinaryStreamParserFlow
    {
        public BinaryStreamParserFlow(BinaryReader reader)
        {
            reader = _reader;
        }

        private BinaryReader _reader;
    }
}
