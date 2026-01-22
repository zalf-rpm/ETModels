#pragma once
#define _USE_MATH_DEFINES
#include <cmath>
#include <iostream>
#include <vector>
#include <string>

namespace Monica_AMEI_potential_Evapotranspiration {
struct AMEIPotETAuxiliary
{
    double saturated_vapor_pressure{0.0};
    double stomata_resistance{100};
    double net_radiation{0.0};
    double reference_evapotranspiration{0.0};
    double potential_evapotranspiration{0.0};
    double saturation_vapor_pressure_deficit{0.0};
};
}