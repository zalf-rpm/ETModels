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
#include "Evapotranspiration.h"
using namespace Monica_Evapotranspiration;
void Evapotranspiration::Init(EvapotranspirationCompState &s, EvapotranspirationCompState &s1, EvapotranspirationCompRate &r, EvapotranspirationCompAuxiliary &a, EvapotranspirationCompExogenous &ex)
{
    s.surface_water_storage = 0.0;
    s.evaporated_from_surface = 0.0;
    s.reference_evapotranspiration = 0.0;
    s.actual_evaporation = 0.0;
    s.actual_transpiration = 0.0;
    s.soil_moisture = std::vector<double>(no_of_soil_moisture_layers);
    s.evaporation = std::vector<double>(no_of_soil_moisture_layers);
    s.transpiration = std::vector<double>(no_of_soil_moisture_layers);
    s.evapotranspiration = std::vector<double>(no_of_soil_moisture_layers);
    s.actual_evapotranspiration = 0.0;
    s.net_radiation = 0.0;
    s.soil_moisture = std::move(std::vector<double>(no_of_soil_moisture_layers, 0.0));
    s.evaporation = std::move(std::vector<double>(no_of_soil_moisture_layers, 0.0));
    s.transpiration = std::move(std::vector<double>(no_of_soil_moisture_layers, 0.0));
    s.evapotranspiration = std::move(std::vector<double>(no_of_soil_moisture_layers, 0.0));
}
Evapotranspiration::Evapotranspiration() {}
double Evapotranspiration::getevaporation_zeta() { return this->evaporation_zeta; }
double Evapotranspiration::getmaximum_evaporation_impact_depth() { return this->maximum_evaporation_impact_depth; }
double Evapotranspiration::getreference_albedo() { return this->reference_albedo; }
double Evapotranspiration::getstomata_resistance() { return this->stomata_resistance; }
int Evapotranspiration::getevaporation_reduction_method() { return this->evaporation_reduction_method; }
double Evapotranspiration::getxsa_critical_soil_moisture() { return this->xsa_critical_soil_moisture; }
double Evapotranspiration::getlatitude() { return this->latitude; }
double Evapotranspiration::getheight_nn() { return this->height_nn; }
int Evapotranspiration::getno_of_soil_layers() { return this->no_of_soil_layers; }
int Evapotranspiration::getno_of_soil_moisture_layers() { return this->no_of_soil_moisture_layers; }
std::vector<double> & Evapotranspiration::getlayer_thickness() { return this->layer_thickness; }
std::vector<double> & Evapotranspiration::getpermanent_wilting_point() { return this->permanent_wilting_point; }
std::vector<double> & Evapotranspiration::getfield_capacity() { return this->field_capacity; }
void Evapotranspiration::setevaporation_zeta(double _evaporation_zeta) { this->evaporation_zeta = _evaporation_zeta; }
void Evapotranspiration::setmaximum_evaporation_impact_depth(double _maximum_evaporation_impact_depth) { this->maximum_evaporation_impact_depth = _maximum_evaporation_impact_depth; }
void Evapotranspiration::setreference_albedo(double _reference_albedo) { this->reference_albedo = _reference_albedo; }
void Evapotranspiration::setstomata_resistance(double _stomata_resistance) { this->stomata_resistance = _stomata_resistance; }
void Evapotranspiration::setevaporation_reduction_method(int _evaporation_reduction_method) { this->evaporation_reduction_method = _evaporation_reduction_method; }
void Evapotranspiration::setxsa_critical_soil_moisture(double _xsa_critical_soil_moisture) { this->xsa_critical_soil_moisture = _xsa_critical_soil_moisture; }
void Evapotranspiration::setlatitude(double _latitude) { this->latitude = _latitude; }
void Evapotranspiration::setheight_nn(double _height_nn) { this->height_nn = _height_nn; }
void Evapotranspiration::setno_of_soil_layers(int _no_of_soil_layers) { this->no_of_soil_layers = _no_of_soil_layers; }
void Evapotranspiration::setno_of_soil_moisture_layers(int _no_of_soil_moisture_layers) { this->no_of_soil_moisture_layers = _no_of_soil_moisture_layers; }
void Evapotranspiration::setlayer_thickness(std::vector<double> const &_layer_thickness){
    this->layer_thickness = _layer_thickness;
}
void Evapotranspiration::setpermanent_wilting_point(std::vector<double> const &_permanent_wilting_point){
    this->permanent_wilting_point = _permanent_wilting_point;
}
void Evapotranspiration::setfield_capacity(std::vector<double> const &_field_capacity){
    this->field_capacity = _field_capacity;
}
void Evapotranspiration::Calculate_Model(EvapotranspirationCompState &s, EvapotranspirationCompState &s1, EvapotranspirationCompRate &r, EvapotranspirationCompAuxiliary &a, EvapotranspirationCompExogenous &ex)
{
    //- Name: Evapotranspiration -Version: 1, -Time step: 1
    //- Description:
    //            * Title: Model of evapotranspiration
    //            * Authors: Claas Nendel (transcription into Crop2ML by Michael Berg-Mohnicke)
    //            * Reference: None
    //            * Institution: ZALF e.V.
    //            * ExtendedDescription: None
    //            * ShortDescription: Calculates the evapotranspiration
    //- inputs:
    //            * name: evaporation_zeta
    //                          ** description : shape factor
    //                          ** inputtype : parameter
    //                          ** parametercategory : constant
    //                          ** datatype : DOUBLE
    //                          ** max : 40
    //                          ** min : 0
    //                          ** default : 40
    //                          ** unit : dimensionless
    //            * name: maximum_evaporation_impact_depth
    //                          ** description : maximumEvaporationImpactDepth
    //                          ** inputtype : parameter
    //                          ** parametercategory : constant
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 0
    //                          ** default : 5
    //                          ** unit : dm
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
    //                          ** description : stomata resistance
    //                          ** inputtype : parameter
    //                          ** parametercategory : constant
    //                          ** datatype : DOUBLE
    //                          ** max : 10000
    //                          ** min : 0
    //                          ** default : 100
    //                          ** unit : s/m
    //            * name: evaporation_reduction_method
    //                          ** description : THESEUS (0) or HERMES (1) evaporation reduction method
    //                          ** inputtype : parameter
    //                          ** parametercategory : constant
    //                          ** datatype : INT
    //                          ** max : 1
    //                          ** min : 0
    //                          ** default : 1
    //                          ** unit : dimensionless
    //            * name: xsa_critical_soil_moisture
    //                          ** description : XSACriticalSoilMoisture
    //                          ** inputtype : parameter
    //                          ** parametercategory : constant
    //                          ** datatype : DOUBLE
    //                          ** max : 1.5
    //                          ** min : 0
    //                          ** default : 0.1
    //                          ** unit : m3/m3
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
    //                          ** description : height above sea leavel
    //                          ** inputtype : parameter
    //                          ** parametercategory : constant
    //                          ** datatype : DOUBLE
    //                          ** max : 9999
    //                          ** min : -9999
    //                          ** default : 0
    //                          ** unit : m
    //            * name: no_of_soil_layers
    //                          ** description : number of soil layers
    //                          ** inputtype : parameter
    //                          ** parametercategory : constant
    //                          ** datatype : INT
    //                          ** max : 
    //                          ** min : 0
    //                          ** default : 20
    //                          ** unit : dimensionless
    //            * name: no_of_soil_moisture_layers
    //                          ** description : number of soil layers + 1
    //                          ** inputtype : parameter
    //                          ** parametercategory : constant
    //                          ** datatype : INT
    //                          ** max : 
    //                          ** min : 0
    //                          ** default : 21
    //                          ** unit : dimensionless
    //            * name: layer_thickness
    //                          ** description : layer thickness array
    //                          ** inputtype : parameter
    //                          ** parametercategory : constant
    //                          ** datatype : DOUBLEARRAY
    //                          ** len : no_of_soil_moisture_layers
    //                          ** max : 
    //                          ** min : 
    //                          ** default : 
    //                          ** unit : m
    //            * name: permanent_wilting_point
    //                          ** description : permanent wilting point array
    //                          ** inputtype : parameter
    //                          ** parametercategory : constant
    //                          ** datatype : DOUBLEARRAY
    //                          ** len : no_of_soil_moisture_layers
    //                          ** max : 
    //                          ** min : 
    //                          ** default : 
    //                          ** unit : m3/m3
    //            * name: field_capacity
    //                          ** description : field capacity array
    //                          ** inputtype : parameter
    //                          ** parametercategory : constant
    //                          ** datatype : DOUBLEARRAY
    //                          ** len : no_of_soil_moisture_layers
    //                          ** max : 
    //                          ** min : 
    //                          ** default : 
    //                          ** unit : m3/m3
    //            * name: external_reference_evapotranspiration
    //                          ** description : externally supplied ET0
    //                          ** inputtype : variable
    //                          ** variablecategory : exogenous
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 0
    //                          ** default : -1
    //                          ** unit : mm
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
    //            * name: kc_factor
    //                          ** description : crop coefficient ETc/ET0
    //                          ** inputtype : variable
    //                          ** variablecategory : exogenous
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 0
    //                          ** default : 0.75
    //                          ** unit : dimensionless
    //            * name: has_snow_cover
    //                          ** description : is the soil covered by snow
    //                          ** inputtype : variable
    //                          ** variablecategory : exogenous
    //                          ** datatype : BOOLEAN
    //                          ** default : false
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
    //            * name: crop_transpiration
    //                          ** description : crop transpiration array
    //                          ** inputtype : variable
    //                          ** variablecategory : exogenous
    //                          ** datatype : DOUBLEARRAY
    //                          ** len : no_of_soil_moisture_layers
    //                          ** max : 
    //                          ** min : 
    //                          ** default : 
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
    //            * name: crop_evaporated_from_intercepted
    //                          ** description : crop evaporated water from intercepted water
    //                          ** inputtype : variable
    //                          ** variablecategory : exogenous
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 
    //                          ** default : 
    //                          ** unit : mm
    //            * name: percentage_soil_coverage
    //                          ** description : fraction of soil covered by crop
    //                          ** inputtype : variable
    //                          ** variablecategory : exogenous
    //                          ** datatype : DOUBLE
    //                          ** max : 1
    //                          ** min : 0
    //                          ** default : 0
    //                          ** unit : m2/m2
    //            * name: vapor_pressure
    //                          ** description : vapor pressure
    //                          ** inputtype : variable
    //                          ** variablecategory : exogenous
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 0
    //                          ** default : 0
    //                          ** unit : kPa
    //            * name: surface_water_storage
    //                          ** description : Simulates a virtual layer that contains the surface water
    //                          ** inputtype : variable
    //                          ** variablecategory : state
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 0
    //                          ** default : 0
    //                          ** unit : mm
    //            * name: evaporated_from_surface
    //                          ** description : evaporated_from_surface
    //                          ** inputtype : variable
    //                          ** variablecategory : state
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 0
    //                          ** default : 0
    //                          ** unit : mm
    //            * name: reference_evapotranspiration
    //                          ** description : reference evapotranspiration (ET0)
    //                          ** inputtype : variable
    //                          ** variablecategory : state
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 0
    //                          ** default : 0
    //                          ** unit : mm
    //            * name: actual_evaporation
    //                          ** description : actual evaporation
    //                          ** inputtype : variable
    //                          ** variablecategory : state
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 0
    //                          ** default : 0
    //                          ** unit : mm
    //            * name: actual_transpiration
    //                          ** description : actual transpiration
    //                          ** inputtype : variable
    //                          ** variablecategory : state
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 0
    //                          ** default : 0
    //                          ** unit : mm
    //            * name: soil_moisture
    //                          ** description : soil moisture array
    //                          ** inputtype : variable
    //                          ** variablecategory : state
    //                          ** datatype : DOUBLEARRAY
    //                          ** len : no_of_soil_moisture_layers
    //                          ** max : 
    //                          ** min : 
    //                          ** default : 
    //                          ** unit : m3/m3
    //            * name: evaporation
    //                          ** description : evaporation array
    //                          ** inputtype : variable
    //                          ** variablecategory : state
    //                          ** datatype : DOUBLEARRAY
    //                          ** len : no_of_soil_moisture_layers
    //                          ** max : 
    //                          ** min : 
    //                          ** default : 
    //                          ** unit : mm
    //            * name: transpiration
    //                          ** description : transpiration array
    //                          ** inputtype : variable
    //                          ** variablecategory : state
    //                          ** datatype : DOUBLEARRAY
    //                          ** len : no_of_soil_moisture_layers
    //                          ** max : 
    //                          ** min : 
    //                          ** default : 
    //                          ** unit : mm
    //            * name: evapotranspiration
    //                          ** description : evapotranspiration array
    //                          ** inputtype : variable
    //                          ** variablecategory : state
    //                          ** datatype : DOUBLEARRAY
    //                          ** len : no_of_soil_moisture_layers
    //                          ** max : 
    //                          ** min : 
    //                          ** default : 
    //                          ** unit : mm
    //            * name: actual_evapotranspiration
    //                          ** description : actual evapotranspiration
    //                          ** inputtype : variable
    //                          ** variablecategory : state
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 0
    //                          ** default : 0
    //                          ** unit : mm
    //            * name: net_radiation
    //                          ** description : net radiation
    //                          ** inputtype : variable
    //                          ** variablecategory : state
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 0
    //                          ** default : 0
    //                          ** unit : MJ/m2
    //- outputs:
    //            * name: evaporated_from_surface
    //                          ** description : 
    //                          ** variablecategory : state
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 0
    //                          ** unit : mm
    //            * name: actual_evapotranspiration
    //                          ** description : actual evapotranspiration
    //                          ** variablecategory : state
    //                          ** datatype : DOUBLE
    //                          ** max : 200
    //                          ** min : 0
    //                          ** unit : mm
    //            * name: reference_evapotranspiration
    //                          ** description : reference evapotranspiration (ET0)
    //                          ** variablecategory : state
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 0
    //                          ** unit : mm
    //            * name: actual_evaporation
    //                          ** description : actual evaporation
    //                          ** variablecategory : state
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 0
    //                          ** unit : mm
    //            * name: actual_transpiration
    //                          ** description : actual transpiration
    //                          ** variablecategory : state
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 0
    //                          ** unit : mm
    //            * name: surface_water_storage
    //                          ** description : Simulates a virtual layer that contains the surface water
    //                          ** variablecategory : state
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 0
    //                          ** unit : mm
    //            * name: soil_moisture
    //                          ** description : soil moisture array
    //                          ** variablecategory : state
    //                          ** datatype : DOUBLEARRAY
    //                          ** len : no_of_soil_moisture_layers
    //                          ** max : 
    //                          ** min : 
    //                          ** unit : m3/m3
    //            * name: net_radiation
    //                          ** description : net radiation
    //                          ** variablecategory : state
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 0
    //                          ** unit : MJ/m2
    s.evaporated_from_surface = 0.0;
    double potential_evapotranspiration = 0.0;
    double evaporated_from_intercept = 0.0;
    if (ex.developmental_stage > 0) {
        if (ex.external_reference_evapotranspiration < 0.0) {
            s.reference_evapotranspiration = ex.crop_reference_evapotranspiration;
        }
        else {
            s.reference_evapotranspiration = ex.external_reference_evapotranspiration;
        }
        potential_evapotranspiration = ex.crop_remaining_evapotranspiration;
        evaporated_from_intercept = ex.crop_evaporated_from_intercepted;
    }
    else {
        if (ex.external_reference_evapotranspiration < 0.0) {
            std::tie(s.reference_evapotranspiration, s.net_radiation) = calc_reference_evapotranspiration(height_nn, ex.max_air_temperature, ex.min_air_temperature, ex.relative_humidity, ex.mean_air_temperature, ex.wind_speed, ex.wind_speed_height, ex.global_radiation, ex.julian_day, latitude, reference_albedo, ex.vapor_pressure, stomata_resistance);
        }
        else {
            s.reference_evapotranspiration = ex.external_reference_evapotranspiration;
        }
        potential_evapotranspiration = s.reference_evapotranspiration * ex.kc_factor;
    }
    s.actual_evaporation = 0.0;
    s.actual_transpiration = 0.0;
    if (potential_evapotranspiration > 6.5) {
        potential_evapotranspiration = 6.5;
    }
    bool evaporation_from_surface = false;
    double eRed1;
    double eRed2;
    double eRed3;
    double eReducer;
    int i;
    if (potential_evapotranspiration > 0.0) {
        evaporation_from_surface = false;
        if (s.surface_water_storage > 0.0) {
            evaporation_from_surface = true;
            potential_evapotranspiration = potential_evapotranspiration * 1.1 / ex.kc_factor;
            if (ex.has_snow_cover) {
                s.evaporated_from_surface = 0.0;
            }
            else if (s.surface_water_storage < potential_evapotranspiration) {
                potential_evapotranspiration = potential_evapotranspiration - s.surface_water_storage;
                s.evaporated_from_surface = s.surface_water_storage;
                s.surface_water_storage = 0.0;
            }
            else {
                s.surface_water_storage = s.surface_water_storage - potential_evapotranspiration;
                s.evaporated_from_surface = potential_evapotranspiration;
                potential_evapotranspiration = 0.0;
            }
            potential_evapotranspiration = potential_evapotranspiration * ex.kc_factor / 1.1;
        }
        if (potential_evapotranspiration > 0.0) {
            for (i=0; i!=no_of_soil_layers; i+=1) {
                eRed1 = e_reducer_1(permanent_wilting_point[i], field_capacity[i], s.soil_moisture[i], ex.percentage_soil_coverage, potential_evapotranspiration, evaporation_reduction_method, xsa_critical_soil_moisture);
                eRed2 = 0.0;
                if (float(i) >= maximum_evaporation_impact_depth) {
                    eRed2 = 0.0;
                }
                else {
                    eRed2 = get_deprivation_factor(i + 1, maximum_evaporation_impact_depth, evaporation_zeta, layer_thickness[i]);
                }
                eRed3 = 0.0;
                if (i > 0 && s.soil_moisture[i] < s.soil_moisture[i - 1]) {
                    eRed3 = 0.1;
                }
                else {
                    eRed3 = 1.0;
                }
                eReducer = eRed1 * eRed2 * eRed3;
                if (ex.developmental_stage > 0) {
                    if (ex.percentage_soil_coverage >= 0.0 && ex.percentage_soil_coverage < 1.0) {
                        s.evaporation[i] = (1.0 - ex.percentage_soil_coverage) * eReducer * potential_evapotranspiration;
                    }
                    else if (ex.percentage_soil_coverage >= 1.0) {
                        s.evaporation[i] = 0.0;
                    }
                    if (ex.has_snow_cover) {
                        s.evaporation[i] = 0.0;
                    }
                    s.transpiration[i] = ex.crop_transpiration[i];
                    if (evaporation_from_surface) {
                        s.transpiration[i] = ex.percentage_soil_coverage * eReducer * potential_evapotranspiration;
                    }
                }
                else {
                    if (ex.has_snow_cover) {
                        s.evaporation[i] = 0.0;
                    }
                    else {
                        s.evaporation[i] = potential_evapotranspiration * eReducer;
                        s.transpiration[i] = 0.0;
                    }
                }
                s.evapotranspiration[i] = s.evaporation[i] + s.transpiration[i];
                s.soil_moisture[i] = s.soil_moisture[i] - (s.evapotranspiration[i] / 1000.0 / layer_thickness[i]);
                if (s.soil_moisture[i] < 0.01) {
                    s.soil_moisture[i] = 0.01;
                }
                s.actual_transpiration = s.actual_transpiration + s.transpiration[i];
                s.actual_evaporation = s.actual_evaporation + s.evaporation[i];
            }
        }
    }
    s.actual_evapotranspiration = s.actual_transpiration + s.actual_evaporation + evaporated_from_intercept + s.evaporated_from_surface;
}
double Evapotranspiration::get_deprivation_factor(int layer_no, double deprivation_depth, double zeta, double layer_thickness)
{
    double ltf;
    ltf = deprivation_depth / (layer_thickness * 10.0);
    double deprivation_factor;
    double c2;
    double c3;
    if (std::abs(zeta) < 0.0003) {
        deprivation_factor = 2.0 / ltf - (1.0 / (ltf * ltf) * (2 * layer_no - 1));
    }
    else {
        c2 = std::log((ltf + (zeta * layer_no)) / (ltf + (zeta * (layer_no - 1))));
        c3 = zeta / (ltf * (zeta + 1.0));
        deprivation_factor = (c2 - c3) / (std::log(zeta + 1.0) - (zeta / (zeta + 1.0)));
    }
    return deprivation_factor;
}
double Evapotranspiration::bound(double lower, double value, double upper)
{
    if (value < lower) {
        return lower;
    }
    if (value > upper) {
        return upper;
    }
    return value;
}
std::tuple<double,double> Evapotranspiration::calc_reference_evapotranspiration(double height_nn, double max_air_temperature, double min_air_temperature, double relative_humidity, double mean_air_temperature, double wind_speed, double wind_speed_height, double global_radiation, int julian_day, double latitude, double reference_albedo, double vapor_pressure, double stomata_resistance)
{
    double declination;
    declination = -23.4 * std::cos(2.0 * M_PI * ((julian_day + 10.0) / 365.0));
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
    SC = 24.0 * 60.0 / M_PI * 8.20 * (1.0 + (0.033 * std::cos(2.0 * M_PI * julian_day / 365.0)));
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
    saturated_vapor_pressure_max = 0.6108 * std::exp(17.27 * max_air_temperature / (237.3 + max_air_temperature));
    double saturated_vapor_pressure_min;
    saturated_vapor_pressure_min = 0.6108 * std::exp(17.27 * min_air_temperature / (237.3 + min_air_temperature));
    double saturated_vapor_pressure;
    saturated_vapor_pressure = (saturated_vapor_pressure_max + saturated_vapor_pressure_min) / 2.0;
    if (vapor_pressure < 0.0) {
        if (relative_humidity <= 0.0) {
            vapor_pressure = saturated_vapor_pressure_min;
        }
        else {
            vapor_pressure = relative_humidity * saturated_vapor_pressure;
        }
    }
    double saturation_deficit;
    saturation_deficit = saturated_vapor_pressure - vapor_pressure;
    double saturated_vapour_pressure_slope;
    saturated_vapour_pressure_slope = 4098.0 * (0.6108 * std::exp(17.27 * mean_air_temperature / (mean_air_temperature + 237.3))) / ((mean_air_temperature + 237.3) * (mean_air_temperature + 237.3));
    double wind_speed_2m;
    wind_speed_2m = std::max(0.5, wind_speed * (4.87 / std::log((67.8 * wind_speed_height - 5.42))));
    double surface_resistance;
    surface_resistance = stomata_resistance / 1.44;
    double clear_sky_solar_radiation;
    clear_sky_solar_radiation = (0.75 + (0.00002 * height_nn)) * extraterrestrial_radiation;
    double relative_shortwave_radiation;
    relative_shortwave_radiation = clear_sky_solar_radiation > 0.0 ? std::min(global_radiation / clear_sky_solar_radiation, 1.0) : 1.0;
    double bolzmann_constant = 0.0000000049;
    double shortwave_radiation;
    shortwave_radiation = (1.0 - reference_albedo) * global_radiation;
    double longwave_radiation;
    longwave_radiation = bolzmann_constant * ((std::pow(min_air_temperature + 273.16, 4.0) + std::pow(max_air_temperature + 273.16, 4.0)) / 2.0) * (1.35 * relative_shortwave_radiation - 0.35) * (0.34 - (0.14 * std::sqrt(vapor_pressure)));
    double net_radiation;
    net_radiation = shortwave_radiation - longwave_radiation;
    double reference_evapotranspiration;
    reference_evapotranspiration = (0.408 * saturated_vapour_pressure_slope * net_radiation + (psycrometer_constant * (900.0 / (mean_air_temperature + 273.0)) * wind_speed_2m * saturation_deficit)) / (saturated_vapour_pressure_slope + (psycrometer_constant * (1.0 + (surface_resistance / 208.0 * wind_speed_2m))));
    if (reference_evapotranspiration < 0.0) {
        reference_evapotranspiration = 0.0;
    }
    return std::make_tuple(reference_evapotranspiration, net_radiation);
}
double Evapotranspiration::e_reducer_1(double pwp, double fc, double sm, double percentage_soil_coverage, double reference_evapotranspiration, int evaporation_reduction_method, double xsa_critical_soil_moisture)
{
    sm = std::max(0.33 * pwp, sm);
    double relative_evaporable_water;
    relative_evaporable_water = std::min(1.0, (sm - (0.33 * pwp)) / (fc - (0.33 * pwp)));
    double e_reduction_factor = 0.0;
    double critical_soil_moisture;
    double reducer;
    double xsa;
    if (evaporation_reduction_method == 0) {
        critical_soil_moisture = 0.65 * fc;
        if (percentage_soil_coverage > 0.0) {
            reducer = 1.0;
            if (reference_evapotranspiration > 2.5) {
                xsa = (0.65 * fc - pwp) * (fc - pwp);
                reducer = xsa + ((1 - xsa) / 17.5 * (reference_evapotranspiration - 2.5));
            }
            else {
                reducer = xsa_critical_soil_moisture / 2.5 * reference_evapotranspiration;
            }
            critical_soil_moisture = fc * reducer;
        }
        if (sm > critical_soil_moisture) {
            e_reduction_factor = 1.0;
        }
        else if (sm > (0.33 * pwp)) {
            e_reduction_factor = relative_evaporable_water;
        }
        else {
            e_reduction_factor = 0.0;
        }
    }
    else {
        if (relative_evaporable_water > 0.33) {
            e_reduction_factor = 1.0 - (0.1 * (1.0 - relative_evaporable_water) / (1.0 - 0.33));
        }
        else if (relative_evaporable_water > 0.22) {
            e_reduction_factor = 0.9 - (0.625 * (0.33 - relative_evaporable_water) / (0.33 - 0.22));
        }
        else if (relative_evaporable_water > 0.2) {
            e_reduction_factor = 0.275 - (0.225 * (0.22 - relative_evaporable_water) / (0.22 - 0.2));
        }
        else {
            e_reduction_factor = 0.05 - (0.05 * (0.2 - relative_evaporable_water) / 0.2);
        }
    }
    return e_reduction_factor;
}