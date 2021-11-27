using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Helpers
{
    public class BinaryStreamEmitterFlow
    {
        public BinaryStreamEmitterFlow(BinaryWriter writer)
        {
            _writer = writer;
        }

        public BinaryWriter Writer => _writer;

        private BinaryWriter _writer;
    }
}
