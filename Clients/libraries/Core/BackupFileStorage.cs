using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Client
{
    public class BackupFileStorage
    {
        public BackupFileStorage(String location)
        {
            _location = location ?? throw new ArgumentNullException(nameof(location));
        }

        public BackupFile Save(String filePath)
        {
            if (filePath == null)
                throw new ArgumentNullException(nameof(filePath));

            if (!File.Exists(filePath))
                throw new FileNotFoundException($"File '{filePath}' not found.", filePath);

            return new BackupFile(filePath, PickBackupFilePath(_location));
        }

        private static String PickBackupFilePath(String location)
        {
            String result;

            do
            {
                result = Path.Combine(location, Path.GetRandomFileName());
            } while (File.Exists(result));

            return result;
        }

        private readonly String _location;
    }
}
