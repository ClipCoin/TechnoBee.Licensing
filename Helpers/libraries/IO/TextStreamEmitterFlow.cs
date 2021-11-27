using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Helpers
{
    public class TextStreamEmitterFlow
    {
        public TextStreamEmitterFlow(StreamEmitterFlow streamEmitterFlow)
        {
            _streamEmitterFlow = streamEmitterFlow;
            _lazyWriter = new Lazy<TextWriter>(() => new StreamWriter(_streamEmitterFlow.Stream));
        }

        public StreamEmitterFlow StreamEmitterFlow => _streamEmitterFlow;
        public TextWriter Writer => _lazyWriter.Value;

        private Lazy<TextWriter> _lazyWriter;
        private StreamEmitterFlow _streamEmitterFlow;
    }
}
