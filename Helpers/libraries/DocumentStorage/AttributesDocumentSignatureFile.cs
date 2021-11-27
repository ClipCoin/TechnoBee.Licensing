using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TechnoBee.Licensing.Helpers;

namespace TechnoBee.Licensing.Helpers
{
    public class AttributesDocumentSignatureFile
        : AttributesDocumentSignatureFileBase
    {
        public Stream DocumentStream => _documentStream;

        protected override Stream InternalDocumentStream
        {
            get => _documentStream.ToMemory();
            set
            {
                MemoryStream currentStreamBackup = _documentStream;

                _documentStream = value.ToMemory();

                currentStreamBackup.Dispose();
            }
        }

        private MemoryStream _documentStream = new MemoryStream();
    }

    public abstract class AttributesDocumentSignatureFileBase<T>
        : AttributesDocumentSignatureFileBase
        where T : class
    {
        public T Document
        {
            get => _document;
            set => _document = value;
        }

        protected override Stream InternalDocumentStream
        {
            get 
            {
                Stream intermediateStream = new MemoryStream();

                EmitDocumentStream(_document, intermediateStream);

                return intermediateStream;
            }
            set
            {
                _document = ParseDocumentStream(value);
            }
        }

        protected abstract T ParseDocumentStream(Stream stream);
        protected abstract void EmitDocumentStream(T document, Stream stream);

        private T _document;
    }
}
