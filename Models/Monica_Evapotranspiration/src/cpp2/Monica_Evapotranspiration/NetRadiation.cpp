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
#include "NetRadiation.h"
using namespace Monica_Evapotranspiration;
NetRadiation::NetRadiation() {}
double NetRadiation::getheight_nn() { return this->height_nn; }
double NetRadiation::getreference_albedo() { return this->reference_albedo; }
void NetRadiation::setheight_nn(double _height_nn) { this->height_nn = _height_nn; }
void NetRadiation::setreference_albedo(double _reference_albedo) { this->reference_albedo = _reference_albedo; }
void NetRadiation::Calculate_Model(ETState &s, ETState &s1, ETRate &r, ETAuxiliary &a, ETExogenous &ex)
{
    //- Name: NetRadiation -Version: 1, -Time step: 1
    //- Description:
    //            * Title: MONICA net radiation calculation
    //            * Authors: Claas Nendel (transcription into Crop2ML by Michael Berg-Mohnicke)
    //            * Reference: 
    //        
    //            * Institution: ZALF e.V.
    //            * ExtendedDescription: 
    //        
    //            * ShortDescription: None
    //- inputs:
    //            * name: height_nn
    //                          ** description : height above sea level
    //                          ** inputtype : parameter
    //                          ** parametercategory : constant
    //                          ** datatype : DOUBLE
    //                          ** max : 9999
    //                          ** min : -9999
    //                          ** default : 0
    //                          ** unit : m
    //            * name: reference_albedo
    //                          ** description : reference albedo
    //                          ** inputtype : parameter
    //                          ** parametercategory : constant
    //                          ** datatype : DOUBLE
    //                          ** max : 1
    //                          ** min : 0
    //                          ** default : 0
    //                          ** unit : dimensionless
    //            * name: global_radiation
    //                          ** description : global radiation
    //                          ** inputtype : variable
    //                          ** variablecategory : exogenous
    //                          ** datatype : DOUBLE
    //                          ** max : 50
    //                          ** min : 0
    //                          ** default : 0
    //                          ** unit : MJ/m2
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
    //            * name: vapor_pressure
    //                          ** description : vapor pressure
    //                          ** inputtype : variable
    //                          ** variablecategory : exogenous
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 0
    //                          ** default : 0
    //                          ** unit : kPa
    //            * name: extraterrestrial_radiation
    //                          ** description : extraterrestrial_radiation
    //                          ** inputtype : variable
    //                          ** variablecategory : exogenous
    //                          ** datatype : DOUBLE
    //                          ** max : 50
    //                          ** min : 0
    //                          ** default : 0
    //                          ** unit : MJ/m2
    //- outputs:
    //            * name: net_radiation
    //                          ** description : net_radiation
    //                          ** variablecategory : auxiliary
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 0
    //                          ** unit : MJ/m2
    double clear_sky_solar_radiation;
    clear_sky_solar_radiation = (0.75 + (0.00002 * height_nn)) * ex.extraterrestrial_radiation;
    double relative_shortwave_radiation;
    relative_shortwave_radiation = clear_sky_solar_radiation > 0.0 ? std::min(ex.global_radiation / clear_sky_solar_radiation, 1.0) : 1.0;
    double bolzmann_constant = 0.0000000049;
    double shortwave_radiation;
    shortwave_radiation = (1.0 - reference_albedo) * ex.global_radiation;
    double longwave_radiation;
    longwave_radiation = bolzmann_constant * ((std::pow(ex.min_air_temperature + 273.16, 4.0) + std::pow(ex.max_air_temperature + 273.16, 4.0)) / 2.0) * (1.35 * relative_shortwave_radiation - 0.35) * (0.34 - (0.14 * std::sqrt(ex.vapor_pressure)));
    a.net_radiation = shortwave_radiation - longwave_radiation;
}