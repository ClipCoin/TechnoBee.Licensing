#pragma once
#include <stdio.h>
#include "ESCoverage.h"

class ESLicense
{
public:
	ESLicense();

	std::string guid;
	time_t from;
	time_t to;

	ESCoverage Coverage;
};

