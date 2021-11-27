#pragma once
#include "stdafx.h"
#include <stdio.h>
#include <ctime>
#include <string>


using namespace System;
using System::Runtime::InteropServices::Marshal;

public ref class Conversions {
public:
	static DateTime time_t2DateTime(std::time_t date) {
		double sec = static_cast<double>(date);
		return DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind::Utc).AddSeconds(sec);
	}
	static std::time_t DateTime2time_t(DateTime date) {
		TimeSpan diff = date.ToUniversalTime() - DateTime(1970, 1, 1);
		return static_cast<std::time_t>(diff.TotalSeconds);
	}

	static std::string UnmandgStrToMandg(System::String^ value)
	{
		return (const char *)Marshal::StringToHGlobalAnsi(value).ToPointer();
	}
};