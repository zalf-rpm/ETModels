
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
public:
    PotentialEvapotranspiration();

    void Calculate_Model(ETState &s, ETState &s1, ETRate &r, ETAuxiliary &a, ETExogenous &ex);

    void Init(ETState &s, ETState &s1, ETRate &r, ETAuxiliary &a, ETExogenous &ex);

};
}