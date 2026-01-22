
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
class PotentialEvapotranspiration
{
private:
    double potential_evapotranspiration_cap{6.5};
public:
    PotentialEvapotranspiration();

    void Calculate_Model(ETState &s, ETState &s1, ETRate &r, ETAuxiliary &a, ETExogenous &ex);

    double getpotential_evapotranspiration_cap();
    void setpotential_evapotranspiration_cap(double _potential_evapotranspiration_cap);
};
}