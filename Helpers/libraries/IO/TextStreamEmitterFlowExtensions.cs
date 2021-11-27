using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlSerializerBuilder = YamlDotNet.Serialization.SerializerBuilder;

namespace TechnoBee.Licensing.Helpers
{
    public static class TextStreamEmitterFlowExtensions
    {
        public static YamlStreamEmitterFlow AsYaml(this TextStreamEmitterFlow textStreamEmitterFlow)
        {
            return new YamlStreamEmitterFlow(textStreamEmitterFlow);
        }

        public static YamlStreamEmitterFlow AsYaml(this TextStreamEmitterFlow textStreamEmitterFlow
            , Action<YamlSerializerBuilder> serializerBuilderConfiguration)
        {
            return new YamlStreamEmitterFlow(textStreamEmitterFlow, serializerBuilderConfiguration);
        }
    }
}
