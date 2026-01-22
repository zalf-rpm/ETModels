
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
class ReferenceEvapotranspiration
{
private:
    double reference_albedo{0};
    double stomata_resistance{100};
    double latitude{0};
    double height_nn{0};
public:
    ReferenceEvapotranspiration();

    void Calculate_Model(ETState &s, ETState &s1, ETRate &r, ETAuxiliary &a, ETExogenous &ex);

    void Init(ETState &s, ETState &s1, ETRate &r, ETAuxiliary &a, ETExogenous &ex);

    double bound(double lower, double value, double upper);

    double getreference_albedo();
    void setreference_albedo(double _reference_albedo);

    double getstomata_resistance();
    void setstomata_resistance(double _stomata_resistance);

    double getlatitude();
    void setlatitude(double _latitude);

    double getheight_nn();
    void setheight_nn(double _height_nn);
};
}