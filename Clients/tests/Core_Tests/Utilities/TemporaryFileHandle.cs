using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Clients.Utilities
{
    public class TemporaryFileHandle
        : IDisposable
    {
        public Stream Stream => _stream;
        public String FilePath => _filePath;

        public void Open(String filePath, FileAccess fileAccess = FileAccess.ReadWrite)
        {
            if (_stream != null)
                throw new Exception();

            _stream = new FileStream(filePath, FileMode.OpenOrCreate, fileAccess);

            _filePath = filePath;
        }

        public void Close()
        {
            if (_stream != null)
            {
                _stream.Dispose();
                _stream = null;
            }
        }

        public void Dispose()
        {
            Close();

            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
        }

        private String _filePath;
        private Stream _stream;
    }
}
