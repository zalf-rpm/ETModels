#pragma once
#define _USE_MATH_DEFINES
#include <cmath>
#include <iostream>
#include <vector>
#include <string>

namespace Monica_Evapotranspiration {
struct AMEIPotETExogenous
{
    double vapor_pressure{0};
    double mean_air_temperature{0};
    double wind_speed{0};
    double wind_speed_height{2};
    double kc_factor{0.75};
    int developmental_stage{0};
    double crop_remaining_evapotranspiration{0.0};
};
}