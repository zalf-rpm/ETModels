
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
class PotentialEvapotranspiration
{
private:
    double potential_evapotranspiration_cap{6.5};
public:
    PotentialEvapotranspiration();

    void Calculate_Model(AMEIPotETState &s, AMEIPotETState &s1, AMEIPotETRate &r, AMEIPotETAuxiliary &a, AMEIPotETExogenous &ex);

    double getpotential_evapotranspiration_cap();
    void setpotential_evapotranspiration_cap(double _potential_evapotranspiration_cap);
};
}