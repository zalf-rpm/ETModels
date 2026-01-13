using System;
using CRA.AgroManagement;
using CRA.ModelLayer.Strategy;
namespace SiriusQualityEvapotranspirationComp.DomainClass
{
    public interface IStrategySiriusQualityEvapotranspirationComp : IStrategy
    {
        void Estimate( EvapotranspirationCompState s, EvapotranspirationCompState s1, EvapotranspirationCompRate r, EvapotranspirationCompAuxiliary a, EvapotranspirationCompExogenous ex);

        string TestPreConditions( EvapotranspirationCompState s, EvapotranspirationCompState s1, EvapotranspirationCompRate r, EvapotranspirationCompAuxiliary a, EvapotranspirationCompExogenous ex, string callID);

        string TestPostConditions( EvapotranspirationCompState s, EvapotranspirationCompState s1, EvapotranspirationCompRate r, EvapotranspirationCompAuxiliary a, EvapotranspirationCompExogenous ex, string callID);

        void SetParametersDefaultValue();
    }
}