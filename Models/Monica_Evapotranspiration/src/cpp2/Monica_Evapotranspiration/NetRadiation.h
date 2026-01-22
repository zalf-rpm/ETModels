
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
class NetRadiation
{
private:
    double height_nn{0};
    double reference_albedo{0};
public:
    NetRadiation();

    void Calculate_Model(ETState &s, ETState &s1, ETRate &r, ETAuxiliary &a, ETExogenous &ex);

    double getheight_nn();
    void setheight_nn(double _height_nn);

    double getreference_albedo();
    void setreference_albedo(double _reference_albedo);
};
}