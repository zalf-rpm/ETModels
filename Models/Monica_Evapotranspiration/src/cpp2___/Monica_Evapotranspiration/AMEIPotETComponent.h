#include "SaturationVaporPressureDeficit.h"
#include "ReferenceEvapotranspiration.h"
#include "PotentialEvapotranspiration.h"

namespace Monica_Evapotranspiration {
class AMEIPotETComponent
{
private:
    double height_nn{0};
public:
    AMEIPotETComponent();

    AMEIPotETComponent(AMEIPotETComponent& copy);

    void Calculate_Model(AMEIPotETState &s, AMEIPotETState &s1, AMEIPotETRate &r, AMEIPotETAuxiliary &a, AMEIPotETExogenous &ex);

    void Init(AMEIPotETState &s, AMEIPotETState &s1, AMEIPotETRate &r, AMEIPotETAuxiliary &a, AMEIPotETExogenous &ex);

    double getheight_nn();
    void setheight_nn(double _height_nn);

    SaturationVaporPressureDeficit _SaturationVaporPressureDeficit;
    ReferenceEvapotranspiration _ReferenceEvapotranspiration;
    PotentialEvapotranspiration _PotentialEvapotranspiration;

};
}