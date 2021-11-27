#pragma once
#include <stdio.h>
#include <list>
#include "ESApplication.h"
#include "ESComponent.h"

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

