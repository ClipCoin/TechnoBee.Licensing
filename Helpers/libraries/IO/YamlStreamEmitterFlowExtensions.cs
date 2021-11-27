using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using YamlDotNetSerializerBuilder = YamlDotNet.Serialization.SerializerBuilder;

namespace TechnoBee.Licensing.Helpers
{
    public static class YamlStreamEmitterFlowExtensions
    {
        public static Stream From<T>(this YamlStreamEmitterFlow emitterFlow, T obj)
        {
            YamlDotNetSerializerBuilder servializerBuilder = new YamlDotNetSerializerBuilder();

            emitterFlow.SerializerBuilderConfiguration(servializerBuilder);

            emitterFlow
                .Writer
                .Write(servializerBuilder.Build().Serialize(obj));

            emitterFlow
                .Writer
                .Flush();

            return emitterFlow.Stream;
        }
    }
}
