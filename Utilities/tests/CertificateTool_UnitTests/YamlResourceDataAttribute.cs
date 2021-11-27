using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using TechnoBee.Licensing.Helpers;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization.NamingConventions;

using IYamlParser = YamlDotNet.Core.IParser;
using ParsingEvent = YamlDotNet.Core.Events.ParsingEvent;
using YamlDeserializer = YamlDotNet.Serialization.Deserializer;
using YamlDeserializerBuilder = YamlDotNet.Serialization.DeserializerBuilder;

namespace TechnoBee.Licensing.Utilities.CertificateTool_UnitTests
{
    internal class ParameterParser
        : IYamlParser
    {
        public ParsingEvent Current => _events.Count > 0 ? _events.Peek() : null;

        public void AddEvent(ParsingEvent evt)
        {
            _events.Enqueue(evt);
        }

        public bool MoveNext()
        {
            if (_events.Count > 0)
            {
                _events.Dequeue();
            }

            return _events.Count > 0;
        }

        private Queue<ParsingEvent> _events = new Queue<ParsingEvent>();
    }

    internal class ParameterParserConverter
        : YamlDotNet.Serialization.IYamlTypeConverter
    {
        public bool Accepts(Type type)
        {
            return typeof(ParameterParser) == type;
        }

        public object ReadYaml(IYamlParser parser, Type type)
        {
            ParameterParser parameterParser = new ParameterParser();

            Int32 level = 1;

            if (parser.Current is Scalar)
            {
                parameterParser.AddEvent(parser.Current);
            }
            else if (parser.Current is SequenceStart)
            {
                parameterParser.AddEvent(parser.Current);

                while (parser.MoveNext())
                {
                    parameterParser.AddEvent(parser.Current);

                    level += parser.Current.NestingIncrease;

                    if (level == 0)
                        break;
                }
            }
            else
            {
                throw new Exception();
            }

            parser.MoveNext();

            return parameterParser;

            //return new MemberSerializationToken();
        }

        public void WriteYaml(IEmitter emitter, object value, Type type)
        {
            throw new NotImplementedException();
        }
    }

    public class YamlResourceDataAttribute
        : Xunit.Sdk.DataAttribute
    {
        public YamlResourceDataAttribute(String resourceName)
        {
            ResourceName = resourceName;
        }

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            YamlDeserializer deserializer = new YamlDeserializerBuilder()
                .Build();

            return testMethod
                .DeclaringType
                .Assembly
                .GetManifestResourceStream(ResourceName)
                .Parse()
                .AsText()
                .AsYaml(builder =>
                {
                    builder.WithNamingConvention(new CamelCaseNamingConvention());
                    builder.WithTypeConverter(new ParameterParserConverter());
                })
                .To<List<Dictionary<String, ParameterParser>>>()
                .Select(d =>
                {
                    return testMethod
                        .GetParameters()
                        .Select(p => deserializer.Deserialize(d[p.Name], p.ParameterType))
                        .ToArray();
                });
            //            throw new NotImplementedException();
        }

        public readonly String ResourceName;
    }
}
