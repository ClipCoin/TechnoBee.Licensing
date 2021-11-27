using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Helpers
{
    public static class BinaryStreamEmitterFlowExtensions
    {
        public static void From(this BinaryStreamEmitterFlow emitterFlow, Byte[] bytes)
        {
            emitterFlow
                .Writer
                .Write(bytes);
        }
    }
}
