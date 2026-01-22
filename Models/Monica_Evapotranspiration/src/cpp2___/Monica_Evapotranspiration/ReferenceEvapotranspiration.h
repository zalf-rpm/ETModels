
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
class ReferenceEvapotranspiration
{
private:
    double reference_albedo{0};
    double stomata_resistance{100};
    double saturation_beta{2.5};
    double stomata_conductance_alpha{40};
    double latitude{0};
    double height_nn{0};
    int carboxylation_pathway{0};
public:
    ReferenceEvapotranspiration();

    void Calculate_Model(AMEIPotETState &s, AMEIPotETState &s1, AMEIPotETRate &r, AMEIPotETAuxiliary &a, AMEIPotETExogenous &ex);

    double bound(double lower, double value, double upper);

    double getreference_albedo();
    void setreference_albedo(double _reference_albedo);

    double getstomata_resistance();
    void setstomata_resistance(double _stomata_resistance);

    double getsaturation_beta();
    void setsaturation_beta(double _saturation_beta);

    double getstomata_conductance_alpha();
    void setstomata_conductance_alpha(double _stomata_conductance_alpha);

    double getlatitude();
    void setlatitude(double _latitude);

    double getheight_nn();
    void setheight_nn(double _height_nn);

    int getcarboxylation_pathway();
    void setcarboxylation_pathway(int _carboxylation_pathway);
};
}