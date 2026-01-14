#pragma once
#define _USE_MATH_DEFINES
#include <cmath>
#include <iostream>
#include <vector>
#include <string>

namespace Monica_Evapotranspiration {
struct EvapotranspirationCompExogenous
{
    double external_reference_evapotranspiration{-1};
    double max_air_temperature{0};
    double min_air_temperature{0};
    double mean_air_temperature{0};
    double relative_humidity{0};
    double wind_speed{0};
    double wind_speed_height{2};
    double global_radiation{0};
    int julian_day{1};
    bool has_snow_cover{false};
    int developmental_stage{0};
    double crop_reference_evapotranspiration{-1};
    double kc_factor{0.75};
    double percentage_soil_coverage{0};
    std::vector<double> crop_transpiration;
    double crop_remaining_evapotranspiration{0.0};
    double crop_evaporated_from_intercepted{0.0};
    double vapor_pressure{0};
};
}