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
#include "SaturatedVaporPressure.h"
using namespace Monica_Evapotranspiration;
SaturatedVaporPressure::SaturatedVaporPressure() {}
void SaturatedVaporPressure::Calculate_Model(ETState &s, ETState &s1, ETRate &r, ETAuxiliary &a, ETExogenous &ex)
{
    //- Name: SaturatedVaporPressure -Version: 1, -Time step: 1
    //- Description:
    //            * Title: MONICA saturated vapor pressure
    //            * Authors: Claas Nendel (transcription into Crop2ML by Michael Berg-Mohnicke)
    //            * Reference: 
    //        
    //            * Institution: ZALF e.V.
    //            * ExtendedDescription: 
    //        
    //            * ShortDescription: Calculates saturated vapor pressure as in the MONICA model
    //- inputs:
    //            * name: max_air_temperature
    //                          ** description : daily maximum air temperature
    //                          ** inputtype : variable
    //                          ** variablecategory : exogenous
    //                          ** datatype : DOUBLE
    //                          ** max : 100
    //                          ** min : -100
    //                          ** default : 0
    //                          ** unit : °C
    //            * name: min_air_temperature
    //                          ** description : daily minimum air temperature
    //                          ** inputtype : variable
    //                          ** variablecategory : exogenous
    //                          ** datatype : DOUBLE
    //                          ** max : 100
    //                          ** min : -100
    //                          ** default : 0
    //                          ** unit : °C
    //            * name: relative_humidity
    //                          ** description : relative humidity
    //                          ** inputtype : variable
    //                          ** variablecategory : exogenous
    //                          ** datatype : DOUBLE
    //                          ** max : 1
    //                          ** min : 0
    //                          ** default : 0
    //                          ** unit : fraction
    //- outputs:
    //            * name: saturated_vapor_pressure
    //                          ** description : saturated vapor pressure
    //                          ** variablecategory : auxiliary
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 0
    //                          ** unit : kPa
    //            * name: vapor_pressure
    //                          ** description : vapor pressure from relative humidity and saturated vapor pressure
    //                          ** variablecategory : auxiliary
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 0
    //                          ** unit : kPa
    double saturated_vapor_pressure_max;
    saturated_vapor_pressure_max = 0.6108 * std::exp(17.27 * ex.max_air_temperature / (237.3 + ex.max_air_temperature));
    double saturated_vapor_pressure_min;
    saturated_vapor_pressure_min = 0.6108 * std::exp(17.27 * ex.min_air_temperature / (237.3 + ex.min_air_temperature));
    a.saturated_vapor_pressure = (saturated_vapor_pressure_max + saturated_vapor_pressure_min) / 2.0;
    if (ex.relative_humidity <= 0.0) {
        a.vapor_pressure = saturated_vapor_pressure_min;
    }
    else {
        a.vapor_pressure = ex.relative_humidity * a.saturated_vapor_pressure;
    }
}