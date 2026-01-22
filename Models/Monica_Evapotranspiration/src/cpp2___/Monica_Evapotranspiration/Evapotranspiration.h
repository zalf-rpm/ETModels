
#pragma once
#define _USE_MATH_DEFINES
#include <cmath>
#include <iostream>
#include <vector>
#include <string>
#include "ETState.h"
#include "ETRate.h"
#include "ETAuxiliary.h"
#include "ETExogenous.h"
namespace Monica_Evapotranspiration {
class Evapotranspiration
{
private:
    double evaporation_zeta{40};
    double maximum_evaporation_impact_depth{5};
    int evaporation_reduction_method{1};
    double xsa_critical_soil_moisture{0.1};
    int no_of_soil_layers{20};
    int no_of_soil_moisture_layers{21};
    std::vector<double> layer_thickness;
    std::vector<double> permanent_wilting_point;
    std::vector<double> field_capacity;
public:
    Evapotranspiration();

    void Calculate_Model(ETState &s, ETState &s1, ETRate &r, ETAuxiliary &a, ETExogenous &ex);

    void Init(ETState &s, ETState &s1, ETRate &r, ETAuxiliary &a, ETExogenous &ex);

    double bound(double lower, double value, double upper);

    double get_deprivation_factor(int layer_no, double deprivation_depth, double zeta, double layer_thickness);

    double e_reducer_1(double pwp, double fc, double sm, double percentage_soil_coverage, double reference_evapotranspiration, int evaporation_reduction_method, double xsa_critical_soil_moisture);

    double getevaporation_zeta();
    void setevaporation_zeta(double _evaporation_zeta);

    double getmaximum_evaporation_impact_depth();
    void setmaximum_evaporation_impact_depth(double _maximum_evaporation_impact_depth);

    int getevaporation_reduction_method();
    void setevaporation_reduction_method(int _evaporation_reduction_method);

    double getxsa_critical_soil_moisture();
    void setxsa_critical_soil_moisture(double _xsa_critical_soil_moisture);

    int getno_of_soil_layers();
    void setno_of_soil_layers(int _no_of_soil_layers);

    int getno_of_soil_moisture_layers();
    void setno_of_soil_moisture_layers(int _no_of_soil_moisture_layers);

    std::vector<double> & getlayer_thickness();
    void setlayer_thickness(const std::vector<double> &  _layer_thickness);

    std::vector<double> & getpermanent_wilting_point();
    void setpermanent_wilting_point(const std::vector<double> &  _permanent_wilting_point);

    std::vector<double> & getfield_capacity();
    void setfield_capacity(const std::vector<double> &  _field_capacity);
};
}