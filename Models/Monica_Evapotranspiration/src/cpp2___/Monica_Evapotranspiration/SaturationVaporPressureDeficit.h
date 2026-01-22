
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
namespace Monica_Evapotranspiration {
class SaturationVaporPressureDeficit
{
private:
public:
    SaturationVaporPressureDeficit();

    void Calculate_Model(AMEIPotETState &s, AMEIPotETState &s1, AMEIPotETRate &r, AMEIPotETAuxiliary &a, AMEIPotETExogenous &ex);

};
}