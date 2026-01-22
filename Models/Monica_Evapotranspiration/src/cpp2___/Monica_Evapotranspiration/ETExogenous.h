#pragma once
#define _USE_MATH_DEFINES
#include <cmath>
#include <iostream>
#include <vector>
#include <string>

namespace Monica_Evapotranspiration {
struct ETExogenous
{
    double max_air_temperature{0};
    double min_air_temperature{0};
    double mean_air_temperature{0};
    double relative_humidity{0};
    double wind_speed{0};
    double wind_speed_height{2};
    double global_radiation{0};
    int julian_day{1};
    double vapor_pressure{0};
    double atmospheric_co2_concentration{430};
    double kc_factor{0.75};
    int developmental_stage{0};
    double crop_remaining_evapotranspiration{0.0};
    bool has_snow_cover{false};
    std::vector<double> crop_transpiration;
    double crop_evaporated_from_intercepted{0.0};
    double percentage_soil_coverage{0};
};
}