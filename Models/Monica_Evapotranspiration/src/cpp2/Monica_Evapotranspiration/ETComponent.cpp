#include "ETComponent.h"
using namespace Monica_Evapotranspiration;
ETComponent::ETComponent()
{
       
}


double ETComponent::getlatitude(){ return this->latitude; }
double ETComponent::getreference_albedo(){ return this->reference_albedo; }
double ETComponent::getheight_nn(){ return this->height_nn; }
double ETComponent::getsaturation_beta(){ return this->saturation_beta; }
double ETComponent::getstomata_conductance_alpha(){ return this->stomata_conductance_alpha; }
int ETComponent::getcarboxylation_pathway(){ return this->carboxylation_pathway; }
double ETComponent::getevaporation_zeta(){ return this->evaporation_zeta; }
double ETComponent::getmaximum_evaporation_impact_depth(){ return this->maximum_evaporation_impact_depth; }
int ETComponent::getevaporation_reduction_method(){ return this->evaporation_reduction_method; }
double ETComponent::getxsa_critical_soil_moisture(){ return this->xsa_critical_soil_moisture; }
int ETComponent::getno_of_soil_layers(){ return this->no_of_soil_layers; }
int ETComponent::getno_of_soil_moisture_layers(){ return this->no_of_soil_moisture_layers; }
std::vector<double> & ETComponent::getlayer_thickness(){ return this->layer_thickness; }
std::vector<double> & ETComponent::getpermanent_wilting_point(){ return this->permanent_wilting_point; }
std::vector<double> & ETComponent::getfield_capacity(){ return this->field_capacity; }

void ETComponent::setlatitude(double _latitude)
{
    _Radiation.setlatitude(_latitude);
}
void ETComponent::setreference_albedo(double _reference_albedo)
{
    _NetRadiation.setreference_albedo(_reference_albedo);
}
void ETComponent::setheight_nn(double _height_nn)
{
    _NetRadiation.setheight_nn(_height_nn);
    _ReferenceEvapotranspiration.setheight_nn(_height_nn);
}
void ETComponent::setsaturation_beta(double _saturation_beta)
{
    _StomataResistance.setsaturation_beta(_saturation_beta);
}
void ETComponent::setstomata_conductance_alpha(double _stomata_conductance_alpha)
{
    _StomataResistance.setstomata_conductance_alpha(_stomata_conductance_alpha);
}
void ETComponent::setcarboxylation_pathway(int _carboxylation_pathway)
{
    _StomataResistance.setcarboxylation_pathway(_carboxylation_pathway);
}
void ETComponent::setevaporation_zeta(double _evaporation_zeta)
{
    _Evapotranspiration.setevaporation_zeta(_evaporation_zeta);
}
void ETComponent::setmaximum_evaporation_impact_depth(double _maximum_evaporation_impact_depth)
{
    _Evapotranspiration.setmaximum_evaporation_impact_depth(_maximum_evaporation_impact_depth);
}
void ETComponent::setevaporation_reduction_method(int _evaporation_reduction_method)
{
    _Evapotranspiration.setevaporation_reduction_method(_evaporation_reduction_method);
}
void ETComponent::setxsa_critical_soil_moisture(double _xsa_critical_soil_moisture)
{
    _Evapotranspiration.setxsa_critical_soil_moisture(_xsa_critical_soil_moisture);
}
void ETComponent::setno_of_soil_layers(int _no_of_soil_layers)
{
    _Evapotranspiration.setno_of_soil_layers(_no_of_soil_layers);
}
void ETComponent::setno_of_soil_moisture_layers(int _no_of_soil_moisture_layers)
{
    _Evapotranspiration.setno_of_soil_moisture_layers(_no_of_soil_moisture_layers);
}
void ETComponent::setlayer_thickness(const std::vector<double> & _layer_thickness)
{
    _Evapotranspiration.setlayer_thickness(_layer_thickness);
}
void ETComponent::setpermanent_wilting_point(const std::vector<double> & _permanent_wilting_point)
{
    _Evapotranspiration.setpermanent_wilting_point(_permanent_wilting_point);
}
void ETComponent::setfield_capacity(const std::vector<double> & _field_capacity)
{
    _Evapotranspiration.setfield_capacity(_field_capacity);
}
void ETComponent::Calculate_Model(ETState &s, ETState &s1, ETRate &r, ETAuxiliary &a, ETExogenous &ex)
{
    _Radiation.Calculate_Model(s, s1, r, a, ex);
    _SaturatedVaporPressure.Calculate_Model(s, s1, r, a, ex);
    _NetRadiation.Calculate_Model(s, s1, r, a, ex);
    _SaturationVaporPressureDeficit.Calculate_Model(s, s1, r, a, ex);
    _StomataResistance.Calculate_Model(s, s1, r, a, ex);
    _ReferenceEvapotranspiration.Calculate_Model(s, s1, r, a, ex);
    _PotentialEvapotranspiration.Calculate_Model(s, s1, r, a, ex);
    _Evapotranspiration.Calculate_Model(s, s1, r, a, ex);
}
ETComponent::ETComponent(ETComponent& toCopy)
{
    latitude = toCopy.getlatitude();
    reference_albedo = toCopy.getreference_albedo();
    height_nn = toCopy.getheight_nn();
    saturation_beta = toCopy.getsaturation_beta();
    stomata_conductance_alpha = toCopy.getstomata_conductance_alpha();
    carboxylation_pathway = toCopy.getcarboxylation_pathway();
    evaporation_zeta = toCopy.getevaporation_zeta();
    maximum_evaporation_impact_depth = toCopy.getmaximum_evaporation_impact_depth();
    evaporation_reduction_method = toCopy.getevaporation_reduction_method();
    xsa_critical_soil_moisture = toCopy.getxsa_critical_soil_moisture();
    no_of_soil_layers = toCopy.getno_of_soil_layers();
    no_of_soil_moisture_layers = toCopy.getno_of_soil_moisture_layers();
    for (int i = 0; i < no_of_soil_moisture_layers; i++)
{
    layer_thickness[i] = toCopy.getlayer_thickness()[i];
}

    for (int i = 0; i < no_of_soil_moisture_layers; i++)
{
    permanent_wilting_point[i] = toCopy.getpermanent_wilting_point()[i];
}

    for (int i = 0; i < no_of_soil_moisture_layers; i++)
{
    field_capacity[i] = toCopy.getfield_capacity()[i];
}

}


void ETComponent::Init(ETState &s, ETState &s1, ETRate &r, ETAuxiliary &a, ETExogenous &ex)
{
    _Evapotranspiration.Init(s, s1, r, a, ex);
}
