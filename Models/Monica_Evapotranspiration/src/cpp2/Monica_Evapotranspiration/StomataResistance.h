
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
class StomataResistance
{
private:
    double saturation_beta{2.5};
    double stomata_conductance_alpha{40};
    int carboxylation_pathway{0};
public:
    StomataResistance();

    void Calculate_Model(ETState &s, ETState &s1, ETRate &r, ETAuxiliary &a, ETExogenous &ex);

    double getsaturation_beta();
    void setsaturation_beta(double _saturation_beta);

    double getstomata_conductance_alpha();
    void setstomata_conductance_alpha(double _stomata_conductance_alpha);

    int getcarboxylation_pathway();
    void setcarboxylation_pathway(int _carboxylation_pathway);
};
}