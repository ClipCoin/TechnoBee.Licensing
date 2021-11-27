using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Core;
using YamlDotNet.Serialization;
using YamlDotNet.Core.Events;

namespace TechnoBee.Licensing.Utilities.Common
{
    public class YamlVersionConverter
        : IYamlTypeConverter
    {
        public bool Accepts(Type type)
        {
            return type == typeof(System.Version);
        }

        public object ReadYaml(IParser parser, Type type)
        {

            string value = ((Scalar)parser.Current).Value;

            object version = value == "*" 
                ? null
                : System.Version.Parse(value);

            parser.MoveNext();

            return version;
        }

        public void WriteYaml(IEmitter emitter, object value, Type type)
        {
            if (value != null)
            {
                emitter.Emit((ParsingEvent)new Scalar((string)null, (string)null, value?.ToString(), ScalarStyle.Any, true, false));
            }
            else
            {
                emitter.Emit((ParsingEvent)new Scalar((string)null, (string)null, "*", ScalarStyle.Any, true, false));
            }

            
        }
    }
}
