using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using YamlDotNet;

using YamlDeserializer = YamlDotNet.Serialization.Deserializer;


namespace TechnoBee.Licensing.Helpers
{
    public static class StreamExtensions
    {
        public static StreamParserFlow Parse(this Stream stream)
        {
            return new StreamParserFlow(stream);
        }

        public static StreamEmitterFlow Emit(this Stream stream)
        {
            return new StreamEmitterFlow(stream);
        }

        public static MemoryStream ToMemory(this Stream stream)
        {
            MemoryStream memoryStream = new MemoryStream();

            stream.CopyTo(memoryStream);

            return memoryStream;
        }
    }
}
