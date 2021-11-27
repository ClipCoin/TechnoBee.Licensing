using System;
using System.IO;

namespace TechnoBee.Licensing.Utilities.DeploymentCore.Services {
    public class FileService : IFileService {

        public bool IsFileExist(string path) {
            return File.Exists(path);
        }

        public void CopyFile(string copyFrom, string copyTo, bool overwrite = false) {
            File.Copy(copyFrom, copyTo, overwrite);
        }

        public void CreateDirectory(string path) {
            Directory.CreateDirectory(path);
        }

        public void SaveFingerprintInfo(string directory, string fingerprint) {
            if (String.IsNullOrEmpty(fingerprint)) {
                throw new ArgumentNullException(nameof(fingerprint));
            }

            string filePath = Path.Combine(directory, "fingerprint.tbhid");
            File.WriteAllText(filePath, fingerprint);
        }
    }
}
