#include "stdafx.h"
#include <msclr\auto_gcroot.h>
#include <string>
#include <iostream>
#include "CppClrWrapper.h"
#include "ESLicenseFile.h"
#include "Conversions.h"
//#include <msclr\marshal_cppstd.h>

using System::IntPtr;
using System::Runtime::InteropServices::Marshal;
using namespace System;
using namespace System::Runtime::InteropServices;
using namespace std;
using namespace LicenseUtil;
using namespace LicenseUtil::Classes;
using namespace LicenseUtil::Classes::License;

class CppClrWrapperPrivate
{
public: msclr::auto_gcroot<LicenseFacade^> facade;
};

CppClrWrapperPrivate * _private;

bool CppClrWrapper::GetFingerprint(const char *outputfile, bool overwrite)
{
	System::String^ filePath = gcnew System::String(outputfile);

	return _private->facade->GenerateFingerprint(filePath, overwrite);
}

ESLicenseFile CppClrWrapper::LicenceFile(const char * certificateLocation, const char * licenseFileName)
{
	System::String^ _certificateLocation = gcnew System::String(certificateLocation);
	System::String^ _licenseFileName = gcnew System::String(licenseFileName);

	ValLicenseFile^ unm = _private->facade->License(_certificateLocation, _licenseFileName);

	ESLicenseFile result;
	result.Guid = Conversions::UnmandgStrToMandg(unm->id);

	for (int i = 0; i < unm->Count; i++)
	{
		ValLicense^ orig = unm[i];
		ESLicense license;

		license.guid = Conversions::UnmandgStrToMandg(orig->Guid);
		license.from = Conversions::DateTime2time_t(orig->From);
		license.to = Conversions::DateTime2time_t(orig->To);

		ESCoverage coverage;
		for (int j = 0; j < orig->Coverage->Count; j++)
		{
			ValProduct^ prod = orig->Coverage[j];
			ESProduct product;

			product.guid = Conversions::UnmandgStrToMandg(prod->Guid);

			// Append versions
			for (int k = 0; k < prod->Versions->Count; k++)
				product.Versions.push_back(Conversions::UnmandgStrToMandg(prod->Versions[k]->ToString()));

			// Append editions
			for (int k = 0; k < prod->Editions->Count; k++)
				product.Editions.push_back(Conversions::UnmandgStrToMandg(prod->Editions[k]->ToString()));

			// Append applications
			for (int k = 0; k < prod->Applications->Count; k++)
			{
				ValApplication^ app = prod->Applications[k];
				ESApplication application;
				application.name = Conversions::UnmandgStrToMandg(app->Name);
				application.enabled = app->Enabled;

				product.Applications.push_back(application);
			}
			// Append components
			//for (int k = 0; k < prod->Components->Count; k++)
			//{
			//	ValComponent^ cmp = prod->Components[k];
			//	ESComponent component;
				//component.Name = Conversions::UnmandgStrToMandg(cmp->Name);

				//System::Collections::Generic::Dictionary<System::String^, System::String^>::Enumerator enumerator
				//	= cmp->Restrictions->GetEnumerator();
				//while (enumerator.MoveNext())
				//{
				//	ESProductRestriction restriction;
				//	restriction.name = Conversions::UnmandgStrToMandg(enumerator.Current.Key);
				//	restriction.value = Conversions::UnmandgStrToMandg(enumerator.Current.Value);
				//	component.Restrictions.push_back(restriction);
				//}

			//	product.Components.push_back(component);
		//	}

			coverage.Products.push_back(product);
		}
		license.Coverage = coverage;

		result.Licenses.push_back(license);
	}

	return result;
}


const char * CppClrWrapper::GetLicenseExpirationDate(const char * certificateLocation, const char * licenseFileName,
	const char * applicationName,
	const char * productGuid,
	const char * productVersion,
	const char * productEdition)
{
	System::String^ _certificateLocation = gcnew System::String(certificateLocation);
	System::String^ _licenseFileName = gcnew System::String(licenseFileName);
	System::String^ _licenseApplicationName = gcnew System::String(applicationName);
	System::String^ _licenseProductGuid = gcnew System::String(productGuid);
	System::String^ _licenseProductVersion = gcnew System::String(productVersion);
	System::String^ _licenseProductEdition = gcnew System::String(productEdition);

	//const char * errorMsg = "";
	//System::String^ _error = gcnew System::String(errorMsg);

	try
	{
		System::String^ result =
			_private->facade->GetLicenseExpirationDate(_certificateLocation, _licenseFileName,
			_licenseApplicationName,
			_licenseProductGuid,
			_licenseProductVersion,
			_licenseProductEdition);

		return (const char*)(Marshal::StringToHGlobalAnsi(result)).ToPointer();
	//	msclr::interop::marshal_context oMarshalContext;
	//	return oMarshalContext.marshal_as<const char*>(result);
	}
	catch (System::Exception^ ex)
	{
		throw std::exception((char*)Marshal::StringToHGlobalAnsi(ex->Message).ToPointer());
	}
}

CppClrWrapper::CppClrWrapper()
{
	_private = new CppClrWrapperPrivate();
	_private->facade = gcnew LicenseFacade();
}

CppClrWrapper::~CppClrWrapper()
{
	delete _private;
}