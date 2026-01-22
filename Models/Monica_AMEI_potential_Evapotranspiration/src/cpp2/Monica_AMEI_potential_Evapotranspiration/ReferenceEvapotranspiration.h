
#pragma once
#define _USE_MATH_DEFINES
#include <cmath>
#include <iostream>
#include <vector>
#include <string>
#include "AMEIPotETState.h"
#include "AMEIPotETRate.h"
#include "AMEIPotETAuxiliary.h"
#include "AMEIPotETExogenous.h"
namespace Monica_AMEI_potential_Evapotranspiration {
class ReferenceEvapotranspiration
{
private:
    double height_nn{0};
public:
    ReferenceEvapotranspiration();

    void Calculate_Model(AMEIPotETState &s, AMEIPotETState &s1, AMEIPotETRate &r, AMEIPotETAuxiliary &a, AMEIPotETExogenous &ex);

    double getheight_nn();
    void setheight_nn(double _height_nn);
};
}