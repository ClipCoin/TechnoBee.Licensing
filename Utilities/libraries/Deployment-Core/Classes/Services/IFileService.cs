
namespace TechnoBee.Licensing.Utilities.DeploymentCore.Services
{
    public interface IFileService {
        bool IsFileExist(string path);
        void CopyFile(string path, string licenseStoragePath, bool overwrite = false);
        void CreateDirectory(string path);
        void SaveFingerprintInfo(string destinationPath, string fingerprintInfo);
    }
}
