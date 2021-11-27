#include <stdio.h>
#include <list>

#pragma once

class ESProductRestriction
{
public:
	ESProductRestriction();

	std::string name;
	std::string value;
};

class ESComponent
{
public:
	ESComponent();

	std::string Name;
	std::list<ESProductRestriction> Restrictions;
};

class ESApplication
{
public:
	ESApplication();

	std::string name;
	bool enabled;
};

class ESProduct
{
public:
	ESProduct();

	std::string guid;

	std::list<std::string> Versions;
	std::list<std::string> Editions;
	std::list<ESApplication> Applications;
	std::list<ESComponent> Components;
};



class ESCoverage
{
public:
	ESCoverage();

	std::list<ESProduct> Products;
};


class ESLicense
{
public:
	ESLicense();

	std::string guid;
	time_t from;
	time_t to;

	ESCoverage Coverage;
};

class ESLicenseFile
{
public:
	ESLicenseFile();

	std::string Guid;
	time_t IssueTime;

	std::list<ESLicense> Licenses;
};



