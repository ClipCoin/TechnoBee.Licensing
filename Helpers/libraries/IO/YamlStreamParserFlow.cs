using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using YamlDotNet;
using YamlDotNet.Serialization;

using YamlDeserializerBuilder = YamlDotNet.Serialization.DeserializerBuilder;
using YamlDeserializer = YamlDotNet.Serialization.Deserializer;

namespace TechnoBee.Licensing.Helpers
{
    public class YamlStreamParserFlow
    {
        public YamlStreamParserFlow(TextStreamParserFlow parserFlow)
            : this(parserFlow, cfg => { })
        {
            
        }

        public YamlStreamParserFlow(TextStreamParserFlow parserFlow, Action<YamlDeserializerBuilder> builderConfiguration)
        {
            _reader = parserFlow.Reader;
            _builderConfiguration = builderConfiguration;
        }

        public T To<T>()
            where T : class

        {
            YamlDeserializerBuilder deserializerBuilder = new YamlDeserializerBuilder();

            _builderConfiguration(deserializerBuilder);

             return deserializerBuilder.Build().Deserialize<T>(_reader);
        }

        public Object To(Type type)
        {
            YamlDeserializerBuilder deserializerBuilder = new YamlDeserializerBuilder();

            _builderConfiguration(deserializerBuilder);

            return deserializerBuilder.Build().Deserialize(_reader, type);
        }

        public Array ToArray(Type type)
        {
            YamlDeserializerBuilder deserializerBuilder = new YamlDeserializerBuilder();

            _builderConfiguration(deserializerBuilder);

            return deserializerBuilder.Build().Deserialize(_reader, type.MakeArrayType()) as Array;
        }

        private TextReader _reader;
        private Action<YamlDeserializerBuilder> _builderConfiguration;
    }
}
