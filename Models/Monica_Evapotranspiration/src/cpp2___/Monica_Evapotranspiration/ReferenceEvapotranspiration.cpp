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
#include "ReferenceEvapotranspiration.h"
using namespace Monica_Evapotranspiration;
ReferenceEvapotranspiration::ReferenceEvapotranspiration() {}
double ReferenceEvapotranspiration::getreference_albedo() { return this->reference_albedo; }
double ReferenceEvapotranspiration::getstomata_resistance() { return this->stomata_resistance; }
double ReferenceEvapotranspiration::getsaturation_beta() { return this->saturation_beta; }
double ReferenceEvapotranspiration::getstomata_conductance_alpha() { return this->stomata_conductance_alpha; }
double ReferenceEvapotranspiration::getlatitude() { return this->latitude; }
double ReferenceEvapotranspiration::getheight_nn() { return this->height_nn; }
int ReferenceEvapotranspiration::getcarboxylation_pathway() { return this->carboxylation_pathway; }
void ReferenceEvapotranspiration::setreference_albedo(double _reference_albedo) { this->reference_albedo = _reference_albedo; }
void ReferenceEvapotranspiration::setstomata_resistance(double _stomata_resistance) { this->stomata_resistance = _stomata_resistance; }
void ReferenceEvapotranspiration::setsaturation_beta(double _saturation_beta) { this->saturation_beta = _saturation_beta; }
void ReferenceEvapotranspiration::setstomata_conductance_alpha(double _stomata_conductance_alpha) { this->stomata_conductance_alpha = _stomata_conductance_alpha; }
void ReferenceEvapotranspiration::setlatitude(double _latitude) { this->latitude = _latitude; }
void ReferenceEvapotranspiration::setheight_nn(double _height_nn) { this->height_nn = _height_nn; }
void ReferenceEvapotranspiration::setcarboxylation_pathway(int _carboxylation_pathway) { this->carboxylation_pathway = _carboxylation_pathway; }
void ReferenceEvapotranspiration::Calculate_Model(ETState &s, ETState &s1, ETRate &r, ETAuxiliary &a, ETExogenous &ex)
{
    //- Name: ReferenceEvapotranspiration -Version: 1, -Time step: 1
    //- Description:
    //            * Title: MONICA reference evapotranspiration
    //            * Authors: Claas Nendel (transcription into Crop2ML by Michael Berg-Mohnicke)
    //            * Reference: 
    //            Allen RG, Pereira LS, Raes D, Smith M. (1998) Crop evapotranspiration. Guidelines for computing crop water requirements. FAO Irrigation and
    //            Drainage Paper 56, FAO, Roma
    //        
    //            * Institution: ZALF e.V.
    //            * ExtendedDescription: 
    //            A method following Penman-Monteith as described by the FAO in Allen
    //            RG, Pereira LS, Raes D, Smith M. (1998) Crop evapotranspiration.
    //            Guidelines for computing crop water requirements. FAO Irrigation and
    //            Drainage Paper 56, FAO, Roma
    //        
    //            * ShortDescription: Calculates the MONICA reference evapotranspiration
    //- inputs:
    //            * name: reference_albedo
    //                          ** description : reference albedo
    //                          ** inputtype : parameter
    //                          ** parametercategory : constant
    //                          ** datatype : DOUBLE
    //                          ** max : 1
    //                          ** min : 0
    //                          ** default : 0
    //                          ** unit : dimensionless
    //            * name: stomata_resistance
    //                          ** description : stomata resistance (FAO default value is 100)
    //                          ** inputtype : parameter
    //                          ** parametercategory : constant
    //                          ** datatype : DOUBLE
    //                          ** max : 10000
    //                          ** min : 0
    //                          ** default : 100
    //                          ** unit : s/m
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
    //            * name: latitude
    //                          ** description : latitude
    //                          ** inputtype : parameter
    //                          ** parametercategory : constant
    //                          ** datatype : DOUBLE
    //                          ** max : 90
    //                          ** min : -90
    //                          ** default : 0
    //                          ** unit : degree
    //            * name: height_nn
    //                          ** description : height above sea level
    //                          ** inputtype : parameter
    //                          ** parametercategory : constant
    //                          ** datatype : DOUBLE
    //                          ** max : 9999
    //                          ** min : -9999
    //                          ** default : 0
    //                          ** unit : m
    //            * name: carboxylation_pathway
    //                          ** description : Is it a C3 (=1) or C4 (=2) crop. (0 = no crop)
    //                          ** inputtype : parameter
    //                          ** parametercategory : constant
    //                          ** datatype : INT
    //                          ** max : 2
    //                          ** min : 0
    //                          ** default : 0
    //                          ** unit : dimensionless
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
    //            * name: mean_air_temperature
    //                          ** description : daily average air temperature
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
    //            * name: wind_speed
    //                          ** description : wind speed measured at wind speed height
    //                          ** inputtype : variable
    //                          ** variablecategory : exogenous
    //                          ** datatype : DOUBLE
    //                          ** max : 9999
    //                          ** min : 0
    //                          ** default : 0
    //                          ** unit : m/s
    //            * name: wind_speed_height
    //                          ** description : height at which the wind speed has been measured
    //                          ** inputtype : variable
    //                          ** variablecategory : exogenous
    //                          ** datatype : DOUBLE
    //                          ** max : 9999
    //                          ** min : -9999
    //                          ** default : 2
    //                          ** unit : m
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
    //            * name: vapor_pressure
    //                          ** description : vapor pressure
    //                          ** inputtype : variable
    //                          ** variablecategory : exogenous
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 0
    //                          ** default : 0
    //                          ** unit : kPa
    //            * name: atmospheric_co2_concentration
    //                          ** description : CO2 concentration in the atmosphere. Optional if no crop.
    //                          ** inputtype : variable
    //                          ** variablecategory : exogenous
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 0
    //                          ** default : 430
    //                          ** unit : ppm
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
    //            * name: reference_evapotranspiration
    //                          ** description : reference evapotranspiration (ET0)
    //                          ** variablecategory : auxiliary
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 0
    //                          ** unit : mm
    //            * name: net_radiation
    //                          ** description : net radiation
    //                          ** variablecategory : state
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 0
    //                          ** unit : MJ/m2
    double declination;
    declination = -23.4 * std::cos(2.0 * M_PI * ((ex.julian_day + 10.0) / 365.0));
    double declination_sinus;
    declination_sinus = std::sin(declination * M_PI / 180.0) * std::sin(latitude * M_PI / 180.0);
    double declination_cosinus;
    declination_cosinus = std::cos(declination * M_PI / 180.0) * std::cos(latitude * M_PI / 180.0);
    double arg_astro_day_length;
    arg_astro_day_length = declination_sinus / declination_cosinus;
    arg_astro_day_length = bound(-1.0, arg_astro_day_length, 1.0);
    double astronomic_day_length;
    astronomic_day_length = 12.0 * (M_PI + (2.0 * std::asin(arg_astro_day_length))) / M_PI;
    double arg_effective_day_length;
    arg_effective_day_length = (-std::sin((8.0 * M_PI / 180.0)) + declination_sinus) / declination_cosinus;
    arg_effective_day_length = bound(-1.0, arg_effective_day_length, 1.0);
    double arg_photo_day_length;
    arg_photo_day_length = (-std::sin((-6.0 * M_PI / 180.0)) + declination_sinus) / declination_cosinus;
    arg_photo_day_length = bound(-1.0, arg_photo_day_length, 1.0);
    double arg_phot_act;
    arg_phot_act = std::min(1.0, declination_sinus / declination_cosinus * (declination_sinus / declination_cosinus));
    double phot_act_radiation_mean;
    phot_act_radiation_mean = 3600.0 * (declination_sinus * astronomic_day_length + (24.0 / M_PI * declination_cosinus * std::sqrt((1.0 - arg_phot_act))));
    double clear_day_radiation = 0.0;
    if (phot_act_radiation_mean > 0.0 && astronomic_day_length > 0.0) {
        clear_day_radiation = 0.5 * 1300.0 * phot_act_radiation_mean * std::exp(-0.14 / (phot_act_radiation_mean / (astronomic_day_length * 3600.0)));
    }
    double SC;
    SC = 24.0 * 60.0 / M_PI * 8.20 * (1.0 + (0.033 * std::cos(2.0 * M_PI * ex.julian_day / 365.0)));
    double arg_SHA;
    arg_SHA = bound(-1.0, -std::tan((latitude * M_PI / 180.0)) * std::tan(declination * M_PI / 180.0), 1.0);
    double SHA;
    SHA = std::acos(arg_SHA);
    double extraterrestrial_radiation;
    extraterrestrial_radiation = SC * (SHA * declination_sinus + (declination_cosinus * std::sin(SHA))) / 100.0;
    double atmospheric_pressure;
    atmospheric_pressure = 101.3 * std::pow((293.0 - (0.0065 * height_nn)) / 293.0, 5.26);
    double psycrometer_constant;
    psycrometer_constant = 0.000665 * atmospheric_pressure;
    double saturated_vapor_pressure_max;
    saturated_vapor_pressure_max = 0.6108 * std::exp(17.27 * ex.max_air_temperature / (237.3 + ex.max_air_temperature));
    double saturated_vapor_pressure_min;
    saturated_vapor_pressure_min = 0.6108 * std::exp(17.27 * ex.min_air_temperature / (237.3 + ex.min_air_temperature));
    double saturated_vapor_pressure;
    saturated_vapor_pressure = (saturated_vapor_pressure_max + saturated_vapor_pressure_min) / 2.0;
    if (ex.vapor_pressure < 0.0) {
        if (ex.relative_humidity <= 0.0) {
            ex.vapor_pressure = saturated_vapor_pressure_min;
        }
        else {
            ex.vapor_pressure = ex.relative_humidity * saturated_vapor_pressure;
        }
    }
    double saturation_deficit;
    saturation_deficit = saturated_vapor_pressure - ex.vapor_pressure;
    double saturated_vapour_pressure_slope;
    saturated_vapour_pressure_slope = 4098.0 * (0.6108 * std::exp(17.27 * ex.mean_air_temperature / (ex.mean_air_temperature + 237.3))) / ((ex.mean_air_temperature + 237.3) * (ex.mean_air_temperature + 237.3));
    double wind_speed_2m;
    wind_speed_2m = std::max(0.5, ex.wind_speed * (4.87 / std::log((67.8 * ex.wind_speed_height - 5.42))));
    double aerodynamic_resistance;
    aerodynamic_resistance = 208.0 / wind_speed_2m;
    if (carboxylation_pathway > 0) {
        if (a.gross_photosynthesis_reference_mol <= 0.0) {
            stomata_resistance = 999999.9;
        }
        else if (carboxylation_pathway == 1) {
            stomata_resistance = ex.atmospheric_co2_concentration * (1.0 + (saturation_deficit / saturation_beta)) / (stomata_conductance_alpha * a.gross_photosynthesis_reference_mol);
        }
        else {
            stomata_resistance = ex.atmospheric_co2_concentration * (1.0 + (saturation_deficit / saturation_beta)) / (stomata_conductance_alpha * a.gross_photosynthesis_reference_mol);
        }
    }
    double surface_resistance;
    surface_resistance = stomata_resistance / 1.44;
    double clear_sky_solar_radiation;
    clear_sky_solar_radiation = (0.75 + (0.00002 * height_nn)) * extraterrestrial_radiation;
    double relative_shortwave_radiation;
    relative_shortwave_radiation = clear_sky_solar_radiation > 0.0 ? std::min(ex.global_radiation / clear_sky_solar_radiation, 1.0) : 1.0;
    double bolzmann_constant = 0.0000000049;
    double shortwave_radiation;
    shortwave_radiation = (1.0 - reference_albedo) * ex.global_radiation;
    double longwave_radiation;
    longwave_radiation = bolzmann_constant * ((std::pow(ex.min_air_temperature + 273.16, 4.0) + std::pow(ex.max_air_temperature + 273.16, 4.0)) / 2.0) * (1.35 * relative_shortwave_radiation - 0.35) * (0.34 - (0.14 * std::sqrt(ex.vapor_pressure)));
    s.net_radiation = shortwave_radiation - longwave_radiation;
    a.reference_evapotranspiration = (0.408 * saturated_vapour_pressure_slope * s.net_radiation + (psycrometer_constant * (900.0 / (ex.mean_air_temperature + 273.0)) * wind_speed_2m * saturation_deficit)) / (saturated_vapour_pressure_slope + (psycrometer_constant * (1.0 + (surface_resistance / aerodynamic_resistance))));
    if (a.reference_evapotranspiration < 0.0) {
        a.reference_evapotranspiration = 0.0;
    }
}
double ReferenceEvapotranspiration::bound(double lower, double value, double upper)
{
    if (value < lower) {
        return lower;
    }
    if (value > upper) {
        return upper;
    }
    return value;
}