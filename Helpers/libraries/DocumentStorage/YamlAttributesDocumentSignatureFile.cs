using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using YamlSerializer = YamlDotNet.Serialization.Serializer;
using YamlDeserializer = YamlDotNet.Serialization.Deserializer;
using TechnoBee.Licensing.Utilities.Common;
using YamlDotNet.Serialization.NamingConventions;

namespace TechnoBee.Licensing.Helpers
{
    public class YamlAttributesDocumentSignatureFile<T>
        : AttributesDocumentSignatureFileBase<T>
        where T : class
    {
        protected override void EmitDocumentStream(T document, Stream stream)
        {
            stream
                .Emit()
                .AsText()
                .AsYaml(cfg =>
                {
                    cfg.EmitDefaults();
                    cfg.WithNamingConvention(new CamelCaseNamingConvention());
                    cfg.WithTypeConverter(new YamlVersionConverter());
                })
                .From(document);

            stream.Seek(0, SeekOrigin.Begin);
        }

        protected override T ParseDocumentStream(Stream stream)
        {
            return stream
                .Parse()
                .AsText()
                .AsYaml(cfg =>
                {
                    cfg.WithNamingConvention(new CamelCaseNamingConvention());
                    cfg.WithTypeConverter(new YamlVersionConverter());
                })
                .To<T>();
        }
    }
}
