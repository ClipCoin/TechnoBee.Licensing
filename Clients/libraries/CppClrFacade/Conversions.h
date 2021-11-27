#pragma once
#include <ctime>

using namespace System;

public ref class Conversions {
public:
	static DateTime time_t2DateTime(std::time_t date);
	static std::time_t DateTime2time_t(DateTime date);
	static std::string UnmandgStrToMandg(System::String^ value);
};