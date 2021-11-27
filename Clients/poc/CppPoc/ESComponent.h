#pragma once
#include "stdafx.h"
#include <stdio.h>
#include <string>
#include <list>
#include "ESProductRestriction.h"


class ESComponent
{
public:
	ESComponent();

	std::string Name;
	std::list<ESProductRestriction> Restrictions;
};

