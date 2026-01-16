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
void PotentialEvapotranspiration::Init(ETState &s, ETState &s1, ETRate &r, ETAuxiliary &a, ETExogenous &ex)
{
    s.potential_evapotranspiration = 0.0;
}
PotentialEvapotranspiration::PotentialEvapotranspiration() {}
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
    //            * name: reference_evapotranspiration
    //                          ** description : ET0
    //                          ** inputtype : variable
    //                          ** variablecategory : exogenous
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 0
    //                          ** default : 0
    //                          ** unit : mm
    //            * name: external_reference_evapotranspiration
    //                          ** description : externally supplied ET0
    //                          ** inputtype : variable
    //                          ** variablecategory : exogenous
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 0
    //                          ** default : -1
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
    //            * name: crop_reference_evapotranspiration
    //                          ** description : the crop specific ET0, if no external ET0 and crop is planted
    //                          ** inputtype : variable
    //                          ** variablecategory : exogenous
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 0
    //                          ** default : -1
    //                          ** unit : mm
    //            * name: crop_remaining_evapotranspiration
    //                          ** description : crop remaining evapotranspiration
    //                          ** inputtype : variable
    //                          ** variablecategory : exogenous
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 
    //                          ** default : 
    //                          ** unit : mm
    //            * name: potential_evapotranspiration
    //                          ** description : the potential evapotranspiration
    //                          ** inputtype : variable
    //                          ** variablecategory : state
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 0
    //                          ** default : 0
    //                          ** unit : mm
    //- outputs:
    //            * name: potential_evapotranspiration
    //                          ** description : the potential evapotranspiration
    //                          ** variablecategory : state
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 0
    //                          ** unit : mm
    //            * name: reference_evapotranspiration
    //                          ** description : ET0
    //                          ** variablecategory : exogenous
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 0
    //                          ** unit : mm
    if (ex.developmental_stage > 0) {
        if (ex.external_reference_evapotranspiration < 0.0) {
            ex.reference_evapotranspiration = ex.crop_reference_evapotranspiration;
        }
        else {
            ex.reference_evapotranspiration = ex.external_reference_evapotranspiration;
        }
        s.potential_evapotranspiration = ex.crop_remaining_evapotranspiration;
    }
    else {
        if (ex.external_reference_evapotranspiration >= 0.0) {
            ex.reference_evapotranspiration = ex.external_reference_evapotranspiration;
        }
        s.potential_evapotranspiration = ex.reference_evapotranspiration * ex.kc_factor;
    }
    if (s.potential_evapotranspiration > 6.5) {
        s.potential_evapotranspiration = 6.5;
    }
}