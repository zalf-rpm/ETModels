
#pragma once
#define _USE_MATH_DEFINES
#include <cmath>
#include <iostream>
#include <vector>
#include <string>
#include "ETState.h"
#include "ETRate.h"
#include "ETAuxiliary.h"
#include "ETExogenous.h"
namespace Monica_Evapotranspiration {
class Radiation
{
private:
    double latitude{0};
public:
    Radiation();

    void Calculate_Model(ETState &s, ETState &s1, ETRate &r, ETAuxiliary &a, ETExogenous &ex);

    double bound(double lower, double value, double upper);

    std::tuple<double,double,double> calc_declinations(double latitude, int julian_day);

    double calc_astronomic_daylength(double latitude, int julian_day);

    double calc_effective_daylength(double latitude, int julian_day);

    double calc_photoperiodic_daylength(double latitude, int julian_day);

    double getlatitude();
    void setlatitude(double _latitude);
};
}