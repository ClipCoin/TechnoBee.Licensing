using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Helpers
{
    public static class StreamEmitterFlowExtensions
    {
        public static TextStreamEmitterFlow AsText(this StreamEmitterFlow streamEmitterFlow)
        {
            return new TextStreamEmitterFlow(streamEmitterFlow);
        }

        public static BinaryStreamEmitterFlow AsBinary(this StreamEmitterFlow streamEmitter)
        {
            return new BinaryStreamEmitterFlow(new BinaryWriter(streamEmitter.Stream));
        }
    }
}
