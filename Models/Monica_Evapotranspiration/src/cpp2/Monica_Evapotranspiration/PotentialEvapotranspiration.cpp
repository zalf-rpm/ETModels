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
#include "PotentialEvapotranspiration.h"
using namespace Monica_Evapotranspiration;
PotentialEvapotranspiration::PotentialEvapotranspiration() {}
double PotentialEvapotranspiration::getpotential_evapotranspiration_cap() { return this->potential_evapotranspiration_cap; }
void PotentialEvapotranspiration::setpotential_evapotranspiration_cap(double _potential_evapotranspiration_cap) { this->potential_evapotranspiration_cap = _potential_evapotranspiration_cap; }
void PotentialEvapotranspiration::Calculate_Model(ETState &s, ETState &s1, ETRate &r, ETAuxiliary &a, ETExogenous &ex)
{
    //- Name: PotentialEvapotranspiration -Version: 1, -Time step: 1
    //- Description:
    //            * Title: MONICA potential evapotranspiration calculation
    //            * Authors: Claas Nendel (transcription into Crop2ML by Michael Berg-Mohnicke)
    //            * Reference: None
    //            * Institution: ZALF e.V.
    //            * ExtendedDescription: None
    //            * ShortDescription: Calculates the MONICA potential evapotranspiration
    //- inputs:
    //            * name: potential_evapotranspiration_cap
    //                          ** description : cap ET0 to this value
    //                          ** inputtype : parameter
    //                          ** parametercategory : constant
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 0
    //                          ** default : 6.5
    //                          ** unit : mm
    //            * name: kc_factor
    //                          ** description : crop coefficient ETc/ET0
    //                          ** inputtype : variable
    //                          ** variablecategory : exogenous
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 0
    //                          ** default : 0.75
    //                          ** unit : dimensionless
    //            * name: developmental_stage
    //                          ** description : MONICA crop developmental stage
    //                          ** inputtype : variable
    //                          ** variablecategory : exogenous
    //                          ** datatype : INT
    //                          ** max : 6
    //                          ** min : 0
    //                          ** default : 0
    //                          ** unit : dimensionless
    //            * name: crop_remaining_evapotranspiration
    //                          ** description : crop remaining evapotranspiration
    //                          ** inputtype : variable
    //                          ** variablecategory : exogenous
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 
    //                          ** default : 
    //                          ** unit : mm
    //            * name: reference_evapotranspiration
    //                          ** description : ET0 either from external ET0 or crop specific ET0
    //                          ** inputtype : variable
    //                          ** variablecategory : auxiliary
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 0
    //                          ** default : 0
    //                          ** unit : mm
    //- outputs:
    //            * name: potential_evapotranspiration
    //                          ** description : the potential evapotranspiration
    //                          ** variablecategory : auxiliary
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 0
    //                          ** unit : mm
    if (ex.developmental_stage > 0) {
        a.potential_evapotranspiration = ex.crop_remaining_evapotranspiration;
    }
    else {
        a.potential_evapotranspiration = a.reference_evapotranspiration * ex.kc_factor;
    }
    if (a.potential_evapotranspiration > potential_evapotranspiration_cap) {
        a.potential_evapotranspiration = potential_evapotranspiration_cap;
    }
}