#include "EvapotranspirationCompComponent.h"
using namespace Monica_Evapotranspiration;
EvapotranspirationCompComponent::EvapotranspirationCompComponent()
{
       
}


double EvapotranspirationCompComponent::getevaporation_zeta(){ return this->evaporation_zeta; }
double EvapotranspirationCompComponent::getmaximum_evaporation_impact_depth(){ return this->maximum_evaporation_impact_depth; }
int EvapotranspirationCompComponent::getno_of_soil_layers(){ return this->no_of_soil_layers; }
std::vector<double> & EvapotranspirationCompComponent::getlayer_thickness(){ return this->layer_thickness; }
double EvapotranspirationCompComponent::getreference_albedo(){ return this->reference_albedo; }
double EvapotranspirationCompComponent::getstomata_resistance(){ return this->stomata_resistance; }
int EvapotranspirationCompComponent::getevaporation_reduction_method(){ return this->evaporation_reduction_method; }
double EvapotranspirationCompComponent::getxsa_critical_soil_moisture(){ return this->xsa_critical_soil_moisture; }

void EvapotranspirationCompComponent::setevaporation_zeta(double _evaporation_zeta)
{
    _Evapotranspiration.setevaporation_zeta(_evaporation_zeta);
}
void EvapotranspirationCompComponent::setmaximum_evaporation_impact_depth(double _maximum_evaporation_impact_depth)
{
    _Evapotranspiration.setmaximum_evaporation_impact_depth(_maximum_evaporation_impact_depth);
}
void EvapotranspirationCompComponent::setno_of_soil_layers(int _no_of_soil_layers)
{
    _Evapotranspiration.setno_of_soil_layers(_no_of_soil_layers);
}
void EvapotranspirationCompComponent::setlayer_thickness(const std::vector<double> & _layer_thickness)
{
    _Evapotranspiration.setlayer_thickness(_layer_thickness);
}
void EvapotranspirationCompComponent::setreference_albedo(double _reference_albedo)
{
    _Evapotranspiration.setreference_albedo(_reference_albedo);
}
void EvapotranspirationCompComponent::setstomata_resistance(double _stomata_resistance)
{
    _Evapotranspiration.setstomata_resistance(_stomata_resistance);
}
void EvapotranspirationCompComponent::setevaporation_reduction_method(int _evaporation_reduction_method)
{
    _Evapotranspiration.setevaporation_reduction_method(_evaporation_reduction_method);
}
void EvapotranspirationCompComponent::setxsa_critical_soil_moisture(double _xsa_critical_soil_moisture)
{
    _Evapotranspiration.setxsa_critical_soil_moisture(_xsa_critical_soil_moisture);
}
void EvapotranspirationCompComponent::Calculate_Model(EvapotranspirationCompState &s, EvapotranspirationCompState &s1, EvapotranspirationCompRate &r, EvapotranspirationCompAuxiliary &a, EvapotranspirationCompExogenous &ex)
{
    _Evapotranspiration.Calculate_Model(s, s1, r, a, ex);
}
EvapotranspirationCompComponent::EvapotranspirationCompComponent(EvapotranspirationCompComponent& toCopy)
{
    evaporation_zeta = toCopy.getevaporation_zeta();
    maximum_evaporation_impact_depth = toCopy.getmaximum_evaporation_impact_depth();
    no_of_soil_layers = toCopy.getno_of_soil_layers();
    for (int i = 0; i < no_of_soil_layers; i++)
{
    layer_thickness[i] = toCopy.getlayer_thickness()[i];
}

    reference_albedo = toCopy.getreference_albedo();
    stomata_resistance = toCopy.getstomata_resistance();
    evaporation_reduction_method = toCopy.getevaporation_reduction_method();
    xsa_critical_soil_moisture = toCopy.getxsa_critical_soil_moisture();
}