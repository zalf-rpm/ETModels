#include "AMEIPotETComponent.h"
using namespace Monica_AMEI_potential_Evapotranspiration;
AMEIPotETComponent::AMEIPotETComponent()
{
       
}


double AMEIPotETComponent::getheight_nn(){ return this->height_nn; }

void AMEIPotETComponent::setheight_nn(double _height_nn)
{
    _ReferenceEvapotranspiration.setheight_nn(_height_nn);
}
void AMEIPotETComponent::Calculate_Model(AMEIPotETState &s, AMEIPotETState &s1, AMEIPotETRate &r, AMEIPotETAuxiliary &a, AMEIPotETExogenous &ex)
{
    _SaturationVaporPressureDeficit.Calculate_Model(s, s1, r, a, ex);
    _ReferenceEvapotranspiration.Calculate_Model(s, s1, r, a, ex);
    _PotentialEvapotranspiration.Calculate_Model(s, s1, r, a, ex);
}
AMEIPotETComponent::AMEIPotETComponent(AMEIPotETComponent& toCopy)
{
    height_nn = toCopy.getheight_nn();
}