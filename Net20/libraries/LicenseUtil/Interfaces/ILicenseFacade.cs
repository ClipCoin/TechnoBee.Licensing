using LicenseUtil.Classes.License;

namespace LicenseUtil
{
    public interface ILicenseFacade
    {
        bool GenerateFingerprint(string filepath, bool overwrite);
        string GetLicenseExpirationDate(string signerCertificateLocation, string LicenseFileName, string LicenseApplicationName, string LicenseProductGuid, string LicenseProductVersion, string LicenseProductEdition);

        ValLicenseFile License(string signerCertificateLocation, string LicenseFileName);
    }
}
