using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Helpers
{
    public static class StreamParserFlowExtensions
    {
        public static Byte[] AsBytes(this StreamParserFlow flow)
        {
            Byte[] buffer = new Byte[flow.Stream.Length];

            flow.Stream.Read(buffer, 0, (Int32)flow.Stream.Length);

            return buffer;
        }

        public static BinaryStreamParserFlow AsBinary(this StreamParserFlow flow)
        {
            return new BinaryStreamParserFlow(new BinaryReader(flow.Stream));
        }

        public static TextStreamParserFlow AsText(this StreamParserFlow flow)
        {
            return new TextStreamParserFlow(new StreamReader(flow.Stream));
        }

    }
}
