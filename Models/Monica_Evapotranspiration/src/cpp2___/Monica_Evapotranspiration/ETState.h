#pragma once
#define _USE_MATH_DEFINES
#include <cmath>
#include <iostream>
#include<vector>
#include<string>
namespace Monica_Evapotranspiration {
struct ETState
{
    double surface_water_storage{0};
    double evaporated_from_surface{0};
    double actual_evaporation{0};
    double actual_transpiration{0};
    std::vector<double> soil_moisture;
    std::vector<double> evaporation;
    std::vector<double> transpiration;
    std::vector<double> evapotranspiration;
    double actual_evapotranspiration{0};
};
}