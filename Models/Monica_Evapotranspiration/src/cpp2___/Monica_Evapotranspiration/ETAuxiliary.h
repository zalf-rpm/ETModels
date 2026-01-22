#pragma once
#define _USE_MATH_DEFINES
#include <cmath>
#include <iostream>
#include <vector>
#include <string>

namespace Monica_Evapotranspiration {
struct ETAuxiliary
{
    double stomata_resistance{100};
    double gross_photosynthesis_reference_mol{-1};
    double potential_evapotranspiration{0.0};
    double reference_evapotranspiration{0.0};
};
}