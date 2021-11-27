#include <stdio.h>
#include <list>
#include "ESLicense.h"
#pragma once

class ESLicenseFile
{
public:
	ESLicenseFile();

	std::string Guid;
	time_t IssueTime;

	std::list<ESLicense> Licenses;
};

