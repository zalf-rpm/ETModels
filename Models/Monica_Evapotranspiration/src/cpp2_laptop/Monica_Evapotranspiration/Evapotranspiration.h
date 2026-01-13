
#pragma once
#define _USE_MATH_DEFINES
#include <cmath>
#include <iostream>
#include <vector>
#include <string>
#include "EvapotranspirationCompState.h"
#include "EvapotranspirationCompRate.h"
#include "EvapotranspirationCompAuxiliary.h"
#include "EvapotranspirationCompExogenous.h"
namespace Monica_Evapotranspiration {
class Evapotranspiration
{
private:
    double evaporation_zeta{40};
    double maximum_evaporation_impact_depth{5};
    int no_of_soil_layers{20};
    std::vector<double> layer_thickness;
    double reference_albedo{0};
    double stomata_resistance{100};
    int evaporation_reduction_method{1};
    double xsa_critical_soil_moisture{0.1};
public:
    Evapotranspiration();

    void Calculate_Model(EvapotranspirationCompState &s, EvapotranspirationCompState &s1, EvapotranspirationCompRate &r, EvapotranspirationCompAuxiliary &a, EvapotranspirationCompExogenous &ex);

    double get_deprivation_factor(int layer_no, double deprivation_depth, double zeta, double layer_thickness);

    double bound(double lower, double value, double upper);

    std::tuple<double,double> calc_reference_evapotranspiration(double height_nn, double max_air_temperature, double min_air_temperature, double relative_humidity, double mean_air_temperature, double wind_speed, double wind_speed_height, double global_radiation, int julian_day, double latitude, double reference_albedo, double vapor_pressure, double stomata_resistance);

    double e_reducer_1(double pwp, double fc, double sm, double percentage_soil_coverage, double reference_evapotranspiration, int evaporation_reduction_method, double xsa_critical_soil_moisture);

    double getevaporation_zeta();
    void setevaporation_zeta(double _evaporation_zeta);

    double getmaximum_evaporation_impact_depth();
    void setmaximum_evaporation_impact_depth(double _maximum_evaporation_impact_depth);

    int getno_of_soil_layers();
    void setno_of_soil_layers(int _no_of_soil_layers);

    std::vector<double> & getlayer_thickness();
    void setlayer_thickness(const std::vector<double> &  _layer_thickness);

    double getreference_albedo();
    void setreference_albedo(double _reference_albedo);

    double getstomata_resistance();
    void setstomata_resistance(double _stomata_resistance);

    int getevaporation_reduction_method();
    void setevaporation_reduction_method(int _evaporation_reduction_method);

    double getxsa_critical_soil_moisture();
    void setxsa_critical_soil_moisture(double _xsa_critical_soil_moisture);
};
}