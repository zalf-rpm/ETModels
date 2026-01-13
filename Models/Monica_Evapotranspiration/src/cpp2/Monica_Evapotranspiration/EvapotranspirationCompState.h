#pragma once
#define _USE_MATH_DEFINES
#include <cmath>
#include <iostream>
#include<vector>
#include<string>
namespace Monica_Evapotranspiration {
struct EvapotranspirationCompState
{
    double evaporated_from_surface{0};
    double surface_water_storage{0};
    double snow_depth{0};
    int developmental_stage{0};
    double crop_reference_evapotranspiration{-1};
    double reference_evapotranspiration{0};
    double actual_evaporation{0};
    double actual_transpiration{0};
    double kc_factor{0.75};
    double percentage_soil_coverage{0};
    std::vector<double> soil_moisture;
    std::vector<double> evaporation;
    std::vector<double> transpiration;
    std::vector<double> crop_transpiration;
    double crop_remaining_evapotranspiration{0.0};
    double crop_evaporated_from_intercepted{0.0};
    std::vector<double> evapotranspiration;
    double actual_evapotranspiration{0};
    double vapor_pressure{0};
    double net_radiation{0};
};
}