using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using YamlSerializerBuilder = YamlDotNet.Serialization.SerializerBuilder;
using YamlDotNet.Serialization;

namespace TechnoBee.Licensing.Helpers
{
    public class YamlStreamEmitterFlow
    {
        public YamlStreamEmitterFlow(TextStreamEmitterFlow textStreamEmitterFlow)
            : this(textStreamEmitterFlow, cfg => { })
        {
            
        }

        public YamlStreamEmitterFlow(TextStreamEmitterFlow textStreamEmitterFlow
            , Action<YamlSerializerBuilder> serializerBuilderConfiguration)
        {
            _textStreamEmitterFlow = textStreamEmitterFlow;
            _lazyWriter = new Lazy<TextWriter>(() => _textStreamEmitterFlow.Writer);
            _lazyStream = new Lazy<Stream>(() => _textStreamEmitterFlow.StreamEmitterFlow.Stream);
            _serializerBuilderConfiguration = serializerBuilderConfiguration;
        }

        public TextWriter Writer => _lazyWriter.Value;
        public Stream Stream => _lazyStream.Value;
        public Action<YamlSerializerBuilder> SerializerBuilderConfiguration => _serializerBuilderConfiguration;

        private TextStreamEmitterFlow _textStreamEmitterFlow;
        private Lazy<TextWriter> _lazyWriter;
        private Lazy<Stream> _lazyStream;
        private Action<YamlSerializerBuilder> _serializerBuilderConfiguration;
    }
}
