#include "ESLicenseFile.h"

class CppClrWrapperPrivate;

class __declspec(dllexport) CppClrWrapper
{
private: CppClrWrapperPrivate * _private;

public:
	CppClrWrapper();
	~CppClrWrapper();
	const char* GetLicenseExpirationDate(const char * certificateLocation, const char * licenseFileName, const char * applicationName, const char * productGuid, const char * productVersion, const char * productEdition);
	bool GetFingerprint(const char *outputfile, bool overwrite);
	ESLicenseFile LicenceFile(const char * certificateLocation, const char * licenseFileName);
};