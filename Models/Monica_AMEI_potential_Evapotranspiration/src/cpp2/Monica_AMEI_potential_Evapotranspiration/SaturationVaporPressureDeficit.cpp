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
#include "SaturationVaporPressureDeficit.h"
using namespace Monica_AMEI_potential_Evapotranspiration;
SaturationVaporPressureDeficit::SaturationVaporPressureDeficit() {}
void SaturationVaporPressureDeficit::Calculate_Model(AMEIPotETState &s, AMEIPotETState &s1, AMEIPotETRate &r, AMEIPotETAuxiliary &a, AMEIPotETExogenous &ex)
{
    //- Name: SaturationVaporPressureDeficit -Version: 1, -Time step: 1
    //- Description:
    //            * Title: MONICA saturation vapor pressure deficit
    //            * Authors: Claas Nendel (transcription into Crop2ML by Michael Berg-Mohnicke)
    //            * Reference: 
    //        
    //            * Institution: ZALF e.V.
    //            * ExtendedDescription: 
    //        
    //            * ShortDescription: Calculates saturation vapor pressure deficit as in the MONICA model
    //- inputs:
    //            * name: vapor_pressure
    //                          ** description : vapor pressure
    //                          ** inputtype : variable
    //                          ** variablecategory : exogenous
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 0
    //                          ** default : 0
    //                          ** unit : kPa
    //            * name: saturated_vapor_pressure
    //                          ** description : saturated vapor pressure
    //                          ** inputtype : variable
    //                          ** variablecategory : auxiliary
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 0
    //                          ** unit : kPa
    //- outputs:
    //            * name: saturation_vapor_pressure_deficit
    //                          ** description : saturation vapor pressure deficit
    //                          ** variablecategory : auxiliary
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 0
    //                          ** unit : kPa
    a.saturation_vapor_pressure_deficit = a.saturated_vapor_pressure - ex.vapor_pressure;
}