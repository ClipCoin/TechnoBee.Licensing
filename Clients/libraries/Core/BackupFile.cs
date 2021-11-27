using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Client
{
    public class BackupFile
        : IDisposable
    {
        public BackupFile(String originalFilePath, String backupFilePath)
        {
            if (!File.Exists(originalFilePath))
                throw new FileNotFoundException($"File '{originalFilePath}' not found.", originalFilePath);

            if (File.Exists(backupFilePath))
                throw new Exception("File already exists at backup location.");

            _originalFilePath = originalFilePath;
            _backupFilePath = backupFilePath;

            File.Move(originalFilePath, backupFilePath);
        }

        public void Restore()
        {
            if (_backupFilePath != null)
            {
                File.Move(_backupFilePath, _originalFilePath);

                _originalFilePath = null;
            }
        }

        public void Discard()
        {
            File.Delete(_backupFilePath);
        }

        public void Dispose()
        {
            if (_backupFilePath != null)
                Restore();
        }

        private String _originalFilePath;
        private String _backupFilePath;
    }
}
