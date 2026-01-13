using System;
using System.Collections.Generic;
using System.Linq;
using SQCrop2ML_EvapotranspirationComp.DomainClass;
using SQCrop2ML_EvapotranspirationComp.Strategies;

namespace SiriusModel.Model.EvapotranspirationComp
{
    class EvapotranspirationCompWrapper :  UniverseLink
    {
        private EvapotranspirationCompState s;
        private EvapotranspirationCompState s1;
        private EvapotranspirationCompRate r;
        private EvapotranspirationCompAuxiliary a;
        private EvapotranspirationCompExogenous ex;
        private EvapotranspirationCompComponent evapotranspirationcompComponent;

        public EvapotranspirationCompWrapper(Universe universe) : base(universe)
        {
            s = new EvapotranspirationCompState();
            r = new EvapotranspirationCompRate();
            a = new EvapotranspirationCompAuxiliary();
            ex = new EvapotranspirationCompExogenous();
            evapotranspirationcompComponent = new EvapotranspirationComp();
            loadParameters();
        }

        public double evaporated_from_surface{ get { return s.evaporated_from_surface;}} 
     
        public double actual_evapotranspiration{ get { return s.actual_evapotranspiration;}} 
     

        public EvapotranspirationCompWrapper(Universe universe, EvapotranspirationCompWrapper toCopy, bool copyAll) : base(universe)
        {
            s = (toCopy.s != null) ? new EvapotranspirationCompState(toCopy.s, copyAll) : null;
            r = (toCopy.r != null) ? new EvapotranspirationCompRate(toCopy.r, copyAll) : null;
            a = (toCopy.a != null) ? new EvapotranspirationCompAuxiliary(toCopy.a, copyAll) : null;
            ex = (toCopy.ex != null) ? new EvapotranspirationCompExogenous(toCopy.ex, copyAll) : null;
            if (copyAll)
            {
                evapotranspirationcompComponent = (toCopy.evapotranspirationcompComponent != null) ? new EvapotranspirationComp(toCopy.evapotranspirationcompComponent) : null;
            }
        }

        public void Init(){
            setExogenous();
            loadParameters();
            evapotranspirationcompComponent.Init(s, s1, r, a, ex);
        }

        private void loadParameters()
        {
            evapotranspirationcompComponent.evaporation_zeta = 40; 
            evapotranspirationcompComponent.maximum_evaporation_impact_depth = 5; 
            evapotranspirationcompComponent.no_of_soil_layers = 20; 
            evapotranspirationcompComponent.layer_thickness = null; // To be modified
            evapotranspirationcompComponent.reference_albedo = 0; 
            evapotranspirationcompComponent.stomata_resistance = 100; 
            evapotranspirationcompComponent.evaporation_reduction_method = 1; 
            evapotranspirationcompComponent.xsa_critical_soil_moisture = 0.1; 
        }

        public void EstimateEvapotranspirationComp(double external_reference_evapotranspiration, double height_nn, double max_air_temperature, double min_air_temperature, double mean_air_temperature, double relative_humidity, double wind_speed, double wind_speed_height, double global_radiation, int julian_day, double latitude)
        {
            ex.external_reference_evapotranspiration = external_reference_evapotranspiration;
            ex.height_nn = height_nn;
            ex.max_air_temperature = max_air_temperature;
            ex.min_air_temperature = min_air_temperature;
            ex.mean_air_temperature = mean_air_temperature;
            ex.relative_humidity = relative_humidity;
            ex.wind_speed = wind_speed;
            ex.wind_speed_height = wind_speed_height;
            ex.global_radiation = global_radiation;
            ex.julian_day = julian_day;
            ex.latitude = latitude;
            evapotranspirationcompComponent.CalculateModel(s,s1, r, a, ex);
        }

    }

}