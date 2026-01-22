#include <cmath>
#include <iostream>
#include <vector>
#include <string>
#include <numeric>
#include <algorithm>
#include <array>
#include <map>
#include <set>
#include <tuple>
#include "StomataResistance.h"
using namespace Monica_Evapotranspiration;
StomataResistance::StomataResistance() {}
double StomataResistance::getsaturation_beta() { return this->saturation_beta; }
double StomataResistance::getstomata_conductance_alpha() { return this->stomata_conductance_alpha; }
int StomataResistance::getcarboxylation_pathway() { return this->carboxylation_pathway; }
void StomataResistance::setsaturation_beta(double _saturation_beta) { this->saturation_beta = _saturation_beta; }
void StomataResistance::setstomata_conductance_alpha(double _stomata_conductance_alpha) { this->stomata_conductance_alpha = _stomata_conductance_alpha; }
void StomataResistance::setcarboxylation_pathway(int _carboxylation_pathway) { this->carboxylation_pathway = _carboxylation_pathway; }
void StomataResistance::Calculate_Model(ETState &s, ETState &s1, ETRate &r, ETAuxiliary &a, ETExogenous &ex)
{
    //- Name: StomataResistance -Version: 1, -Time step: 1
    //- Description:
    //            * Title: MONICA stomata resistance
    //            * Authors: Claas Nendel (transcription into Crop2ML by Michael Berg-Mohnicke)
    //            * Reference: 
    //        
    //            * Institution: ZALF e.V.
    //            * ExtendedDescription: 
    //        
    //            * ShortDescription: Calculates the MONICA reference evapotranspiration
    //- inputs:
    //            * name: saturation_beta
    //                          ** description : Original: Yu et al. 2001; beta = 3.5
    //                          ** inputtype : parameter
    //                          ** parametercategory : constant
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 
    //                          ** default : 2.5
    //                          ** unit : 
    //            * name: stomata_conductance_alpha
    //                          ** description : Original: Yu et al. 2001; alpha = 0.06 mol/m2/s
    //                          ** inputtype : parameter
    //                          ** parametercategory : constant
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 
    //                          ** default : 40
    //                          ** unit : mmol/m2/s
    //            * name: carboxylation_pathway
    //                          ** description : Is it a C3 (=1) or C4 (=2) crop. (0 = no crop)
    //                          ** inputtype : parameter
    //                          ** parametercategory : constant
    //                          ** datatype : INT
    //                          ** max : 2
    //                          ** min : 0
    //                          ** default : 0
    //                          ** unit : dimensionless
    //            * name: atmospheric_co2_concentration
    //                          ** description : CO2 concentration in the atmosphere. Optional if no crop.
    //                          ** inputtype : variable
    //                          ** variablecategory : exogenous
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 0
    //                          ** default : 430
    //                          ** unit : ppm
    //            * name: saturation_vapor_pressure_deficit
    //                          ** description : saturation_vapor_pressure_deficit
    //                          ** inputtype : variable
    //                          ** variablecategory : auxiliary
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 0
    //                          ** unit : kPa
    //            * name: gross_photosynthesis_reference_mol
    //                          ** description : gross_photosynthesis_reference_mol (-1 = not supplied = no crop)
    //                          ** inputtype : variable
    //                          ** variablecategory : auxiliary
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 0
    //                          ** default : -1
    //                          ** unit : [mol m-2 s-1] or [cm3 cm-2 s-1]
    //- outputs:
    //            * name: stomata_resistance
    //                          ** description : stomata resistance (FAO default value is 100)
    //                          ** variablecategory : auxiliary
    //                          ** datatype : DOUBLE
    //                          ** max : 10000
    //                          ** min : 0
    //                          ** unit : s/m
    if (carboxylation_pathway > 0) {
        if (a.gross_photosynthesis_reference_mol <= 0.0) {
            a.stomata_resistance = 999999.9;
        }
        else if (carboxylation_pathway == 1) {
            a.stomata_resistance = ex.atmospheric_co2_concentration * (1.0 + (a.saturation_vapor_pressure_deficit / saturation_beta)) / (stomata_conductance_alpha * a.gross_photosynthesis_reference_mol);
        }
        else {
            a.stomata_resistance = ex.atmospheric_co2_concentration * (1.0 + (a.saturation_vapor_pressure_deficit / saturation_beta)) / (stomata_conductance_alpha * a.gross_photosynthesis_reference_mol);
        }
    }
}