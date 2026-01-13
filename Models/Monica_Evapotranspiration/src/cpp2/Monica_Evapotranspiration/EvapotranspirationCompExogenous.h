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
};
}