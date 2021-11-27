using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using YamlDeserializerBuilder = YamlDotNet.Serialization.DeserializerBuilder;

namespace TechnoBee.Licensing.Helpers
{
    public static class TextStreamParserFlowExtensions
    {
        public static YamlStreamParserFlow AsYaml(this TextStreamParserFlow flow)
        {
            return new YamlStreamParserFlow(flow);
        }

        public static YamlStreamParserFlow AsYaml(this TextStreamParserFlow flow, Action<YamlDeserializerBuilder> builderConfiguration)
        {
            return new YamlStreamParserFlow(flow, builderConfiguration);
        }
    }
}
