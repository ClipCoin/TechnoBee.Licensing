// CppPoc.cpp : Defines the entry point for the console application.
//

#include <iostream>
#include "stdafx.h"
#include "CppClrWrapper.h"
#include "ESLicenseFile.h"
#include "stdio.h"
#include "conio.h"
#include <Windows.h>
#include <string>


int main() 
{
	CppClrWrapper wrapper;

#pragma region Get Fingerprint
	const char *fprintFile = "AppFolder\\hostinfo.tbhid";
	bool generateFingerprintResult = wrapper.GetFingerprint(fprintFile, true);

	if (generateFingerprintResult)
		printf("Fingerprint saved to '%s'\n", fprintFile);
	else printf("An error occured while generating fingerprint\n");
#pragma endregion

#pragma region Check correct License
	const char *certificateFile = "AppFolder\\Elementstore.cer";
	const char *licenseFile = "AppFolder\\sample01.tblcd";
	const char *licenseAppName = "NetApplication01";
	const char *licenseProdGuid = "385d940f-a5e5-46e3-a479-317779d8482f";
	const char *licenseProdVersion = "1.1.1230.34";
	const char *licenseProdEdition = "standard";
	const char *error = "";

	try
	{
		bool checkLicenseResult = wrapper.GetLicenseExpirationDate(certificateFile, licenseFile, licenseAppName, licenseProdGuid, licenseProdVersion, licenseProdEdition);
		if (checkLicenseResult)
			printf("+OK: Active license approved\n");
		else printf("-ERR: Active license rejected!\n");
	}
	catch (const std::exception& e)
	{
		printf("%s\n", e.what());
	}
#pragma endregion


#pragma region Check incorrect License
	try
	{
		bool checkLicenseResult = wrapper.GetLicenseExpirationDate(certificateFile, licenseFile, "foo", "bar", "baz", "foobar");
		if (checkLicenseResult)
			printf("-ERR: Invalid license approved!\n");
		else printf("+OK: Invalid license rejected\n");
	}
	catch (const std::exception& e)
	{
		printf("%s\n", e.what());
	}
#pragma endregion


#pragma region Check incorrect License
	try
	{
		ESLicenseFile file = wrapper.LicenceFile(certificateFile, licenseFile);
		printf("+OK: License builded!\n");
	}
	catch (const std::exception& e)
	{
		printf("%s\n", e.what());
	}
#pragma endregion 

	printf("Press any key to exit...\n");
	_getch();

	return 0;
}