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
#include "Radiation.h"
using namespace Monica_Evapotranspiration;
Radiation::Radiation() {}
double Radiation::getlatitude() { return this->latitude; }
void Radiation::setlatitude(double _latitude) { this->latitude = _latitude; }
void Radiation::Calculate_Model(ETState &s, ETState &s1, ETRate &r, ETAuxiliary &a, ETExogenous &ex)
{
    //- Name: Radiation -Version: 1, -Time step: 1
    //- Description:
    //            * Title: MONICA radiation calculations
    //            * Authors: Claas Nendel (transcription into Crop2ML by Michael Berg-Mohnicke)
    //            * Reference: 
    //            Taken from the original HERMES model, Kersebaum, K.C. and Richter J.
    //            (1991): Modelling nitrogen dynamics in a plant-soil system with a
    //            simple model for advisory purposes. Fert. Res. 27 (2-3), 273 - 281.
    //        
    //            * Institution: ZALF e.V.
    //            * ExtendedDescription: 
    //        
    //            * ShortDescription: Calculates the MONICA reference evapotranspiration
    //- inputs:
    //            * name: latitude
    //                          ** description : latitude
    //                          ** inputtype : parameter
    //                          ** parametercategory : constant
    //                          ** datatype : DOUBLE
    //                          ** max : 90
    //                          ** min : -90
    //                          ** default : 0
    //                          ** unit : degree
    //            * name: global_radiation
    //                          ** description : global radiation
    //                          ** inputtype : variable
    //                          ** variablecategory : exogenous
    //                          ** datatype : DOUBLE
    //                          ** max : 50
    //                          ** min : 0
    //                          ** default : 0
    //                          ** unit : MJ/m2
    //            * name: julian_day
    //                          ** description : day of year
    //                          ** inputtype : variable
    //                          ** variablecategory : exogenous
    //                          ** datatype : INT
    //                          ** max : 366
    //                          ** min : 1
    //                          ** default : 1
    //                          ** unit : day
    //            * name: sunshine_hours
    //                          ** description : number of sunshine hours
    //                          ** inputtype : variable
    //                          ** variablecategory : exogenous
    //                          ** datatype : DOUBLE
    //                          ** max : 24
    //                          ** min : 0
    //                          ** default : 0
    //                          ** unit : h
    //- outputs:
    //            * name: declination
    //                          ** description : declination
    //                          ** variablecategory : auxiliary
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 0
    //                          ** unit : 
    //            * name: astronomic_daylength
    //                          ** description : astronomic_daylength
    //                          ** variablecategory : auxiliary
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 0
    //                          ** unit : mm
    //            * name: effective_daylength
    //                          ** description : effective_daylength
    //                          ** variablecategory : auxiliary
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 0
    //                          ** unit : mm
    //            * name: photoperiodic_daylength
    //                          ** description : photoperiodic_daylength
    //                          ** variablecategory : auxiliary
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 0
    //                          ** unit : mm
    //            * name: sunshine_hours_global_radiation
    //                          ** description : global radiation calculated from sunshine hours
    //                          ** variablecategory : auxiliary
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 0
    //                          ** unit : MJ/m2
    //            * name: extraterrestrial_radiation
    //                          ** description : extraterrestrial_radiation
    //                          ** variablecategory : auxiliary
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 0
    //                          ** unit : MJ/m2
    //            * name: clear_day_radiation
    //                          ** description : clear_day_radiation
    //                          ** variablecategory : auxiliary
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 0
    //                          ** unit : MJ/m2
    //            * name: overcast_day_radiation
    //                          ** description : overcast_day_radiation
    //                          ** variablecategory : auxiliary
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 0
    //                          ** unit : MJ/m2
    //            * name: phot_act_radiation_mean
    //                          ** description : phot_act_radiation_mean
    //                          ** variablecategory : auxiliary
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 0
    //                          ** unit : J/m2
    double decl_sin;
    double decl_cos;
    std::tie(a.declination, decl_sin, decl_cos) = calc_declinations(latitude, ex.julian_day);
    a.astronomic_daylength = calc_astronomic_daylength(latitude, ex.julian_day);
    a.effective_daylength = calc_effective_daylength(latitude, ex.julian_day);
    a.photoperiodic_daylength = calc_photoperiodic_daylength(latitude, ex.julian_day);
    double arg_phot_act;
    arg_phot_act = std::min(1.0, decl_sin / decl_cos * (decl_sin / decl_cos));
    a.phot_act_radiation_mean = 3600.0 * (decl_sin * a.astronomic_daylength + (24.0 / M_PI * decl_cos * std::sqrt((1.0 - arg_phot_act))));
    if (a.phot_act_radiation_mean > 0.0 && a.astronomic_daylength > 0.0) {
        a.clear_day_radiation = 0.5 * 1300.0 * a.phot_act_radiation_mean * std::exp(-0.14 / (a.phot_act_radiation_mean / (a.astronomic_daylength * 3600.0)));
    }
    a.overcast_day_radiation = 0.2 * a.clear_day_radiation;
    double solar_constant;
    solar_constant = 0.082;
    double SC;
    SC = 24.0 * 60.0 / M_PI * solar_constant * (1.0 + (0.033 * std::cos(2.0 * M_PI * ex.julian_day / 365.0)));
    double solar_angle;
    solar_angle = -std::tan((latitude * M_PI / 180.0)) * std::tan(a.declination * M_PI / 180.0);
    solar_angle = bound(-1.0, solar_angle, 1.0);
    double sunset_solar_angle;
    sunset_solar_angle = std::acos(solar_angle);
    a.extraterrestrial_radiation = SC * (sunset_solar_angle * decl_sin + (decl_cos * std::sin(sunset_solar_angle)));
    if (ex.sunshine_hours > 0.0) {
        if (ex.global_radiation <= 0.0 && a.astronomic_daylength > 0.0) {
            a.sunshine_hours_global_radiation = a.extraterrestrial_radiation * (0.19 + (0.55 * ex.sunshine_hours / a.astronomic_daylength));
        }
        else {
            a.sunshine_hours_global_radiation = 0.0;
        }
    }
}
double Radiation::bound(double lower, double value, double upper)
{
    if (value < lower) {
        return lower;
    }
    if (value > upper) {
        return upper;
    }
    return value;
}
std::tuple<double,double,double> Radiation::calc_declinations(double latitude, int julian_day)
{
    double declination;
    declination = -23.4 * std::cos(2.0 * M_PI * ((julian_day + 10.0) / 365.0));
    double decl_sin;
    decl_sin = std::sin(declination * M_PI / 180.0) * std::sin(latitude * M_PI / 180.0);
    double decl_cos;
    decl_cos = std::cos(declination * M_PI / 180.0) * std::cos(latitude * M_PI / 180.0);
    return std::make_tuple(declination, decl_sin, decl_cos);
}
double Radiation::calc_astronomic_daylength(double latitude, int julian_day)
{
    double declination;
    double decl_sin;
    double decl_cos;
    std::tie(declination, decl_sin, decl_cos) = calc_declinations(latitude, julian_day);
    double astro_daylength;
    astro_daylength = decl_sin / decl_cos;
    astro_daylength = bound(-1.0, astro_daylength, 1.0);
    double astronomic_daylength;
    astronomic_daylength = 12.0 * (M_PI + (2.0 * std::asin(astronomic_daylength))) / M_PI;
    return astronomic_daylength;
}
double Radiation::calc_effective_daylength(double latitude, int julian_day)
{
    double declination;
    double decl_sin;
    double decl_cos;
    std::tie(declination, decl_sin, decl_cos) = calc_declinations(latitude, julian_day);
    double eff_daylength;
    eff_daylength = (-std::sin((8.0 * M_PI / 180.0)) + decl_sin) / decl_cos;
    eff_daylength = bound(-1.0, eff_daylength, 1.0);
    double effective_daylength;
    effective_daylength = 12.0 * (M_PI + (2.0 * std::asin(eff_daylength))) / M_PI;
    return effective_daylength;
}
double Radiation::calc_photoperiodic_daylength(double latitude, int julian_day)
{
    double declination;
    double decl_sin;
    double decl_cos;
    std::tie(declination, decl_sin, decl_cos) = calc_declinations(latitude, julian_day);
    double photo_daylength;
    photo_daylength = (-std::sin((-6.0 * M_PI / 180.0)) + decl_sin) / decl_cos;
    photo_daylength = bound(-1.0, photo_daylength, 1.0);
    double photoperiodic_daylength;
    photoperiodic_daylength = 12.0 * (M_PI + (2.0 * std::asin(photo_daylength))) / M_PI;
    return photoperiodic_daylength;
}