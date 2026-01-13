
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using CRA.ModelLayer.MetadataTypes;
using CRA.ModelLayer.Core;
using CRA.ModelLayer.Strategy;
using System.Reflection;
using VarInfo=CRA.ModelLayer.Core.VarInfo;
using Preconditions=CRA.ModelLayer.Core.Preconditions;
using CRA.AgroManagement;       

using SiriusQualityEvapotranspirationComp.DomainClass;
namespace SiriusQualityEvapotranspirationComp.Strategies
{
    public class EvapotranspirationCompComponent : IStrategySiriusQualityEvapotranspirationComp
    {
        public EvapotranspirationCompComponent()
        {
            ModellingOptions mo0_0 = new ModellingOptions();
            //Parameters
            List<VarInfo> _parameters0_0 = new List<VarInfo>();
            VarInfo v1 = new CompositeStrategyVarInfo(_{'modu': 'Evapotranspiration', 'var': 'evaporation_zeta'}, "evaporation_zeta");
            _parameters0_0.Add(v1);
            VarInfo v2 = new CompositeStrategyVarInfo(_{'modu': 'Evapotranspiration', 'var': 'maximum_evaporation_impact_depth'}, "maximum_evaporation_impact_depth");
            _parameters0_0.Add(v2);
            VarInfo v3 = new CompositeStrategyVarInfo(_{'modu': 'Evapotranspiration', 'var': 'no_of_soil_layers'}, "no_of_soil_layers");
            _parameters0_0.Add(v3);
            VarInfo v4 = new CompositeStrategyVarInfo(_{'modu': 'Evapotranspiration', 'var': 'layer_thickness'}, "layer_thickness");
            _parameters0_0.Add(v4);
            VarInfo v5 = new CompositeStrategyVarInfo(_{'modu': 'Evapotranspiration', 'var': 'reference_albedo'}, "reference_albedo");
            _parameters0_0.Add(v5);
            VarInfo v6 = new CompositeStrategyVarInfo(_{'modu': 'Evapotranspiration', 'var': 'stomata_resistance'}, "stomata_resistance");
            _parameters0_0.Add(v6);
            VarInfo v7 = new CompositeStrategyVarInfo(_{'modu': 'Evapotranspiration', 'var': 'evaporation_reduction_method'}, "evaporation_reduction_method");
            _parameters0_0.Add(v7);
            VarInfo v8 = new CompositeStrategyVarInfo(_{'modu': 'Evapotranspiration', 'var': 'xsa_critical_soil_moisture'}, "xsa_critical_soil_moisture");
            _parameters0_0.Add(v8);
            List<PropertyDescription> _inputs0_0 = new List<PropertyDescription>();
            PropertyDescription pd1 = new PropertyDescription();
            pd1.DomainClassType = typeof(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenous);
            pd1.PropertyName = "external_reference_evapotranspiration";
            pd1.PropertyType = (SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenousVarInfo.external_reference_evapotranspiration).ValueType.TypeForCurrentValue;
            pd1.PropertyVarInfo =(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenousVarInfo.external_reference_evapotranspiration);
            _inputs0_0.Add(pd1);
            PropertyDescription pd2 = new PropertyDescription();
            pd2.DomainClassType = typeof(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenous);
            pd2.PropertyName = "height_nn";
            pd2.PropertyType = (SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenousVarInfo.height_nn).ValueType.TypeForCurrentValue;
            pd2.PropertyVarInfo =(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenousVarInfo.height_nn);
            _inputs0_0.Add(pd2);
            PropertyDescription pd3 = new PropertyDescription();
            pd3.DomainClassType = typeof(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenous);
            pd3.PropertyName = "max_air_temperature";
            pd3.PropertyType = (SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenousVarInfo.max_air_temperature).ValueType.TypeForCurrentValue;
            pd3.PropertyVarInfo =(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenousVarInfo.max_air_temperature);
            _inputs0_0.Add(pd3);
            PropertyDescription pd4 = new PropertyDescription();
            pd4.DomainClassType = typeof(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenous);
            pd4.PropertyName = "min_air_temperature";
            pd4.PropertyType = (SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenousVarInfo.min_air_temperature).ValueType.TypeForCurrentValue;
            pd4.PropertyVarInfo =(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenousVarInfo.min_air_temperature);
            _inputs0_0.Add(pd4);
            PropertyDescription pd5 = new PropertyDescription();
            pd5.DomainClassType = typeof(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenous);
            pd5.PropertyName = "mean_air_temperature";
            pd5.PropertyType = (SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenousVarInfo.mean_air_temperature).ValueType.TypeForCurrentValue;
            pd5.PropertyVarInfo =(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenousVarInfo.mean_air_temperature);
            _inputs0_0.Add(pd5);
            PropertyDescription pd6 = new PropertyDescription();
            pd6.DomainClassType = typeof(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenous);
            pd6.PropertyName = "relative_humidity";
            pd6.PropertyType = (SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenousVarInfo.relative_humidity).ValueType.TypeForCurrentValue;
            pd6.PropertyVarInfo =(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenousVarInfo.relative_humidity);
            _inputs0_0.Add(pd6);
            PropertyDescription pd7 = new PropertyDescription();
            pd7.DomainClassType = typeof(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenous);
            pd7.PropertyName = "wind_speed";
            pd7.PropertyType = (SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenousVarInfo.wind_speed).ValueType.TypeForCurrentValue;
            pd7.PropertyVarInfo =(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenousVarInfo.wind_speed);
            _inputs0_0.Add(pd7);
            PropertyDescription pd8 = new PropertyDescription();
            pd8.DomainClassType = typeof(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenous);
            pd8.PropertyName = "wind_speed_height";
            pd8.PropertyType = (SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenousVarInfo.wind_speed_height).ValueType.TypeForCurrentValue;
            pd8.PropertyVarInfo =(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenousVarInfo.wind_speed_height);
            _inputs0_0.Add(pd8);
            PropertyDescription pd9 = new PropertyDescription();
            pd9.DomainClassType = typeof(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenous);
            pd9.PropertyName = "global_radiation";
            pd9.PropertyType = (SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenousVarInfo.global_radiation).ValueType.TypeForCurrentValue;
            pd9.PropertyVarInfo =(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenousVarInfo.global_radiation);
            _inputs0_0.Add(pd9);
            PropertyDescription pd10 = new PropertyDescription();
            pd10.DomainClassType = typeof(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenous);
            pd10.PropertyName = "julian_day";
            pd10.PropertyType = (SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenousVarInfo.julian_day).ValueType.TypeForCurrentValue;
            pd10.PropertyVarInfo =(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenousVarInfo.julian_day);
            _inputs0_0.Add(pd10);
            PropertyDescription pd11 = new PropertyDescription();
            pd11.DomainClassType = typeof(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenous);
            pd11.PropertyName = "latitude";
            pd11.PropertyType = (SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenousVarInfo.latitude).ValueType.TypeForCurrentValue;
            pd11.PropertyVarInfo =(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenousVarInfo.latitude);
            _inputs0_0.Add(pd11);
            PropertyDescription pd12 = new PropertyDescription();
            pd12.DomainClassType = typeof(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompState);
            pd12.PropertyName = "evaporated_from_surface";
            pd12.PropertyType = (SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.evaporated_from_surface).ValueType.TypeForCurrentValue;
            pd12.PropertyVarInfo =(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.evaporated_from_surface);
            _inputs0_0.Add(pd12);
            PropertyDescription pd13 = new PropertyDescription();
            pd13.DomainClassType = typeof(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompState);
            pd13.PropertyName = "surface_water_storage";
            pd13.PropertyType = (SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.surface_water_storage).ValueType.TypeForCurrentValue;
            pd13.PropertyVarInfo =(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.surface_water_storage);
            _inputs0_0.Add(pd13);
            PropertyDescription pd14 = new PropertyDescription();
            pd14.DomainClassType = typeof(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompState);
            pd14.PropertyName = "snow_depth";
            pd14.PropertyType = (SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.snow_depth).ValueType.TypeForCurrentValue;
            pd14.PropertyVarInfo =(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.snow_depth);
            _inputs0_0.Add(pd14);
            PropertyDescription pd15 = new PropertyDescription();
            pd15.DomainClassType = typeof(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompState);
            pd15.PropertyName = "developmental_stage";
            pd15.PropertyType = (SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.developmental_stage).ValueType.TypeForCurrentValue;
            pd15.PropertyVarInfo =(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.developmental_stage);
            _inputs0_0.Add(pd15);
            PropertyDescription pd16 = new PropertyDescription();
            pd16.DomainClassType = typeof(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompState);
            pd16.PropertyName = "crop_reference_evapotranspiration";
            pd16.PropertyType = (SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.crop_reference_evapotranspiration).ValueType.TypeForCurrentValue;
            pd16.PropertyVarInfo =(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.crop_reference_evapotranspiration);
            _inputs0_0.Add(pd16);
            PropertyDescription pd17 = new PropertyDescription();
            pd17.DomainClassType = typeof(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompState);
            pd17.PropertyName = "reference_evapotranspiration";
            pd17.PropertyType = (SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.reference_evapotranspiration).ValueType.TypeForCurrentValue;
            pd17.PropertyVarInfo =(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.reference_evapotranspiration);
            _inputs0_0.Add(pd17);
            PropertyDescription pd18 = new PropertyDescription();
            pd18.DomainClassType = typeof(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompState);
            pd18.PropertyName = "actual_evaporation";
            pd18.PropertyType = (SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.actual_evaporation).ValueType.TypeForCurrentValue;
            pd18.PropertyVarInfo =(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.actual_evaporation);
            _inputs0_0.Add(pd18);
            PropertyDescription pd19 = new PropertyDescription();
            pd19.DomainClassType = typeof(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompState);
            pd19.PropertyName = "actual_transpiration";
            pd19.PropertyType = (SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.actual_transpiration).ValueType.TypeForCurrentValue;
            pd19.PropertyVarInfo =(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.actual_transpiration);
            _inputs0_0.Add(pd19);
            PropertyDescription pd20 = new PropertyDescription();
            pd20.DomainClassType = typeof(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompState);
            pd20.PropertyName = "kc_factor";
            pd20.PropertyType = (SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.kc_factor).ValueType.TypeForCurrentValue;
            pd20.PropertyVarInfo =(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.kc_factor);
            _inputs0_0.Add(pd20);
            PropertyDescription pd21 = new PropertyDescription();
            pd21.DomainClassType = typeof(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompState);
            pd21.PropertyName = "percentage_soil_coverage";
            pd21.PropertyType = (SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.percentage_soil_coverage).ValueType.TypeForCurrentValue;
            pd21.PropertyVarInfo =(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.percentage_soil_coverage);
            _inputs0_0.Add(pd21);
            PropertyDescription pd22 = new PropertyDescription();
            pd22.DomainClassType = typeof(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompState);
            pd22.PropertyName = "soil_moisture";
            pd22.PropertyType = (SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.soil_moisture).ValueType.TypeForCurrentValue;
            pd22.PropertyVarInfo =(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.soil_moisture);
            _inputs0_0.Add(pd22);
            PropertyDescription pd23 = new PropertyDescription();
            pd23.DomainClassType = typeof(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompState);
            pd23.PropertyName = "permanent_wilting_point";
            pd23.PropertyType = (SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.permanent_wilting_point).ValueType.TypeForCurrentValue;
            pd23.PropertyVarInfo =(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.permanent_wilting_point);
            _inputs0_0.Add(pd23);
            PropertyDescription pd24 = new PropertyDescription();
            pd24.DomainClassType = typeof(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompState);
            pd24.PropertyName = "field_capacity";
            pd24.PropertyType = (SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.field_capacity).ValueType.TypeForCurrentValue;
            pd24.PropertyVarInfo =(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.field_capacity);
            _inputs0_0.Add(pd24);
            PropertyDescription pd25 = new PropertyDescription();
            pd25.DomainClassType = typeof(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompState);
            pd25.PropertyName = "evaporation";
            pd25.PropertyType = (SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.evaporation).ValueType.TypeForCurrentValue;
            pd25.PropertyVarInfo =(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.evaporation);
            _inputs0_0.Add(pd25);
            PropertyDescription pd26 = new PropertyDescription();
            pd26.DomainClassType = typeof(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompState);
            pd26.PropertyName = "transpiration";
            pd26.PropertyType = (SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.transpiration).ValueType.TypeForCurrentValue;
            pd26.PropertyVarInfo =(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.transpiration);
            _inputs0_0.Add(pd26);
            PropertyDescription pd27 = new PropertyDescription();
            pd27.DomainClassType = typeof(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompState);
            pd27.PropertyName = "crop_transpiration";
            pd27.PropertyType = (SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.crop_transpiration).ValueType.TypeForCurrentValue;
            pd27.PropertyVarInfo =(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.crop_transpiration);
            _inputs0_0.Add(pd27);
            PropertyDescription pd28 = new PropertyDescription();
            pd28.DomainClassType = typeof(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompState);
            pd28.PropertyName = "crop_remaining_evapotranspiration";
            pd28.PropertyType = (SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.crop_remaining_evapotranspiration).ValueType.TypeForCurrentValue;
            pd28.PropertyVarInfo =(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.crop_remaining_evapotranspiration);
            _inputs0_0.Add(pd28);
            PropertyDescription pd29 = new PropertyDescription();
            pd29.DomainClassType = typeof(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompState);
            pd29.PropertyName = "crop_evaporated_from_intercepted";
            pd29.PropertyType = (SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.crop_evaporated_from_intercepted).ValueType.TypeForCurrentValue;
            pd29.PropertyVarInfo =(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.crop_evaporated_from_intercepted);
            _inputs0_0.Add(pd29);
            PropertyDescription pd30 = new PropertyDescription();
            pd30.DomainClassType = typeof(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompState);
            pd30.PropertyName = "evapotranspiration";
            pd30.PropertyType = (SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.evapotranspiration).ValueType.TypeForCurrentValue;
            pd30.PropertyVarInfo =(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.evapotranspiration);
            _inputs0_0.Add(pd30);
            PropertyDescription pd31 = new PropertyDescription();
            pd31.DomainClassType = typeof(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompState);
            pd31.PropertyName = "actual_evapotranspiration";
            pd31.PropertyType = (SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.actual_evapotranspiration).ValueType.TypeForCurrentValue;
            pd31.PropertyVarInfo =(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.actual_evapotranspiration);
            _inputs0_0.Add(pd31);
            PropertyDescription pd32 = new PropertyDescription();
            pd32.DomainClassType = typeof(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompState);
            pd32.PropertyName = "vapor_pressure";
            pd32.PropertyType = (SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.vapor_pressure).ValueType.TypeForCurrentValue;
            pd32.PropertyVarInfo =(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.vapor_pressure);
            _inputs0_0.Add(pd32);
            mo0_0.Inputs=_inputs0_0;
            List<PropertyDescription> _outputs0_0 = new List<PropertyDescription>();
            PropertyDescription pd33 = new PropertyDescription();
            pd33.DomainClassType = typeof(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompState);
            pd33.PropertyName = "evaporated_from_surface";
            pd33.PropertyType = (SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.evaporated_from_surface).ValueType.TypeForCurrentValue;
            pd33.PropertyVarInfo =(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.evaporated_from_surface);
            _outputs0_0.Add(pd33);
            PropertyDescription pd34 = new PropertyDescription();
            pd34.DomainClassType = typeof(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompState);
            pd34.PropertyName = "actual_evapotranspiration";
            pd34.PropertyType = (SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.actual_evapotranspiration).ValueType.TypeForCurrentValue;
            pd34.PropertyVarInfo =(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.actual_evapotranspiration);
            _outputs0_0.Add(pd34);
            mo0_0.Outputs=_outputs0_0;
            List<string> lAssStrat0_0 = new List<string>();
            lAssStrat0_0.Add(typeof(SiriusQualityEvapotranspirationComp.Strategies.Evapotranspiration).FullName);
            mo0_0.AssociatedStrategies = lAssStrat0_0;
            _modellingOptionsManager = new ModellingOptionsManager(mo0_0);
            SetStaticParametersVarInfoDefinitions();
            SetPublisherData();
        }

        public string Description
        {
            get { return "" ;}
        }

        public string URL
        {
            get { return "" ;}
        }

        public string Domain
        {
            get { return "";}
        }

        public string ModelType
        {
            get { return "";}
        }

        public bool IsContext
        {
            get { return false;}
        }

        public IList<int> TimeStep
        {
            get
            {
                IList<int> ts = new List<int>();
                return ts;
            }
        }

        private  PublisherData _pd;
        public PublisherData PublisherData
        {
            get { return _pd;} 
        }

        private  void SetPublisherData()
        {
            _pd = new CRA.ModelLayer.MetadataTypes.PublisherData();
            _pd.Add("Creator", "Michael Berg-Mohnicke");
            _pd.Add("Date", "");
            _pd.Add("Publisher", "ZALF e.V. "); 
        }

        private ModellingOptionsManager _modellingOptionsManager;
        public ModellingOptionsManager ModellingOptionsManager
        {
            get { return _modellingOptionsManager; } 
        }

        public IEnumerable<Type> GetStrategyDomainClassesTypes()
        {
            return new List<Type>() {  typeof(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompState), typeof(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompState), typeof(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompRate), typeof(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompAuxiliary), typeof(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenous)};
        }

        public double evaporation_zeta
        {
            get
            {
                 return _Evapotranspiration.evaporation_zeta; 
            }
            set
            {
                _Evapotranspiration.evaporation_zeta = value;
            }
        }
        public double maximum_evaporation_impact_depth
        {
            get
            {
                 return _Evapotranspiration.maximum_evaporation_impact_depth; 
            }
            set
            {
                _Evapotranspiration.maximum_evaporation_impact_depth = value;
            }
        }
        public int no_of_soil_layers
        {
            get
            {
                 return _Evapotranspiration.no_of_soil_layers; 
            }
            set
            {
                _Evapotranspiration.no_of_soil_layers = value;
            }
        }
        public double[] layer_thickness
        {
            get
            {
                 return _Evapotranspiration.layer_thickness; 
            }
            set
            {
                _Evapotranspiration.layer_thickness = value;
            }
        }
        public double reference_albedo
        {
            get
            {
                 return _Evapotranspiration.reference_albedo; 
            }
            set
            {
                _Evapotranspiration.reference_albedo = value;
            }
        }
        public double stomata_resistance
        {
            get
            {
                 return _Evapotranspiration.stomata_resistance; 
            }
            set
            {
                _Evapotranspiration.stomata_resistance = value;
            }
        }
        public int evaporation_reduction_method
        {
            get
            {
                 return _Evapotranspiration.evaporation_reduction_method; 
            }
            set
            {
                _Evapotranspiration.evaporation_reduction_method = value;
            }
        }
        public double xsa_critical_soil_moisture
        {
            get
            {
                 return _Evapotranspiration.xsa_critical_soil_moisture; 
            }
            set
            {
                _Evapotranspiration.xsa_critical_soil_moisture = value;
            }
        }

        public void SetParametersDefaultValue()
        {
            _modellingOptionsManager.SetParametersDefaultValue();
            _Evapotranspiration.SetParametersDefaultValue();
        }

        private static void SetStaticParametersVarInfoDefinitions()
        {

            evaporation_zetaVarInfo.Name = "evaporation_zeta";
            evaporation_zetaVarInfo.Description = "shape factor";
            evaporation_zetaVarInfo.MaxValue = 40;
            evaporation_zetaVarInfo.MinValue = 0;
            evaporation_zetaVarInfo.DefaultValue = 40;
            evaporation_zetaVarInfo.Units = "dimensionless";
            evaporation_zetaVarInfo.ValueType = VarInfoValueTypes.GetInstanceForName("Double");

            maximum_evaporation_impact_depthVarInfo.Name = "maximum_evaporation_impact_depth";
            maximum_evaporation_impact_depthVarInfo.Description = "maximumEvaporationImpactDepth";
            maximum_evaporation_impact_depthVarInfo.MaxValue = -1D;
            maximum_evaporation_impact_depthVarInfo.MinValue = 0;
            maximum_evaporation_impact_depthVarInfo.DefaultValue = 5;
            maximum_evaporation_impact_depthVarInfo.Units = "dm";
            maximum_evaporation_impact_depthVarInfo.ValueType = VarInfoValueTypes.GetInstanceForName("Double");

            no_of_soil_layersVarInfo.Name = "no_of_soil_layers";
            no_of_soil_layersVarInfo.Description = "number of soil layers";
            no_of_soil_layersVarInfo.MaxValue = -1D;
            no_of_soil_layersVarInfo.MinValue = 0;
            no_of_soil_layersVarInfo.DefaultValue = 20;
            no_of_soil_layersVarInfo.Units = "dimensionless";
            no_of_soil_layersVarInfo.ValueType = VarInfoValueTypes.GetInstanceForName("Integer");

            layer_thicknessVarInfo.Name = "layer_thickness";
            layer_thicknessVarInfo.Description = "layer thickness array";
            layer_thicknessVarInfo.MaxValue = -1D;
            layer_thicknessVarInfo.MinValue = -1D;
            layer_thicknessVarInfo.DefaultValue = -1D;
            layer_thicknessVarInfo.Units = "m";
            layer_thicknessVarInfo.ValueType = VarInfoValueTypes.GetInstanceForName("ArrayDouble");

            reference_albedoVarInfo.Name = "reference_albedo";
            reference_albedoVarInfo.Description = "reference albedo";
            reference_albedoVarInfo.MaxValue = 1;
            reference_albedoVarInfo.MinValue = 0;
            reference_albedoVarInfo.DefaultValue = 0;
            reference_albedoVarInfo.Units = "dimensionless";
            reference_albedoVarInfo.ValueType = VarInfoValueTypes.GetInstanceForName("Double");

            stomata_resistanceVarInfo.Name = "stomata_resistance";
            stomata_resistanceVarInfo.Description = "stomata resistance";
            stomata_resistanceVarInfo.MaxValue = 10000;
            stomata_resistanceVarInfo.MinValue = 0;
            stomata_resistanceVarInfo.DefaultValue = 100;
            stomata_resistanceVarInfo.Units = "s/m";
            stomata_resistanceVarInfo.ValueType = VarInfoValueTypes.GetInstanceForName("Double");

            evaporation_reduction_methodVarInfo.Name = "evaporation_reduction_method";
            evaporation_reduction_methodVarInfo.Description = "THESEUS (0) or HERMES (1) evaporation reduction method";
            evaporation_reduction_methodVarInfo.MaxValue = 1;
            evaporation_reduction_methodVarInfo.MinValue = 0;
            evaporation_reduction_methodVarInfo.DefaultValue = 1;
            evaporation_reduction_methodVarInfo.Units = "dimensionless";
            evaporation_reduction_methodVarInfo.ValueType = VarInfoValueTypes.GetInstanceForName("Integer");

            xsa_critical_soil_moistureVarInfo.Name = "xsa_critical_soil_moisture";
            xsa_critical_soil_moistureVarInfo.Description = "XSACriticalSoilMoisture";
            xsa_critical_soil_moistureVarInfo.MaxValue = 1.5;
            xsa_critical_soil_moistureVarInfo.MinValue = 0;
            xsa_critical_soil_moistureVarInfo.DefaultValue = 0.1;
            xsa_critical_soil_moistureVarInfo.Units = "m3/m3";
            xsa_critical_soil_moistureVarInfo.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
        }

        public static VarInfo evaporation_zetaVarInfo
        {
            get { return SiriusQualityEvapotranspirationComp.Strategies.{'modu': 'Evapotranspiration', 'var': 'evaporation_zeta'}.evaporation_zetaVarInfo;} 
        }

        public static VarInfo maximum_evaporation_impact_depthVarInfo
        {
            get { return SiriusQualityEvapotranspirationComp.Strategies.{'modu': 'Evapotranspiration', 'var': 'maximum_evaporation_impact_depth'}.maximum_evaporation_impact_depthVarInfo;} 
        }

        public static VarInfo no_of_soil_layersVarInfo
        {
            get { return SiriusQualityEvapotranspirationComp.Strategies.{'modu': 'Evapotranspiration', 'var': 'no_of_soil_layers'}.no_of_soil_layersVarInfo;} 
        }

        public static VarInfo layer_thicknessVarInfo
        {
            get { return SiriusQualityEvapotranspirationComp.Strategies.{'modu': 'Evapotranspiration', 'var': 'layer_thickness'}.layer_thicknessVarInfo;} 
        }

        public static VarInfo reference_albedoVarInfo
        {
            get { return SiriusQualityEvapotranspirationComp.Strategies.{'modu': 'Evapotranspiration', 'var': 'reference_albedo'}.reference_albedoVarInfo;} 
        }

        public static VarInfo stomata_resistanceVarInfo
        {
            get { return SiriusQualityEvapotranspirationComp.Strategies.{'modu': 'Evapotranspiration', 'var': 'stomata_resistance'}.stomata_resistanceVarInfo;} 
        }

        public static VarInfo evaporation_reduction_methodVarInfo
        {
            get { return SiriusQualityEvapotranspirationComp.Strategies.{'modu': 'Evapotranspiration', 'var': 'evaporation_reduction_method'}.evaporation_reduction_methodVarInfo;} 
        }

        public static VarInfo xsa_critical_soil_moistureVarInfo
        {
            get { return SiriusQualityEvapotranspirationComp.Strategies.{'modu': 'Evapotranspiration', 'var': 'xsa_critical_soil_moisture'}.xsa_critical_soil_moistureVarInfo;} 
        }

        public string TestPostConditions(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompState s,SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompState s1,SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompRate r,SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompAuxiliary a,SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenous ex,string callID)
        {
            try
            {
                //Set current values of the outputs to the static VarInfo representing the output properties of the domain classes
                SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.evaporated_from_surface.CurrentValue=s.evaporated_from_surface;
                SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.actual_evapotranspiration.CurrentValue=s.actual_evapotranspiration;

                ConditionsCollection prc = new ConditionsCollection();
                Preconditions pre = new Preconditions(); 

                RangeBasedCondition r41 = new RangeBasedCondition(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.evaporated_from_surface);
                if(r41.ApplicableVarInfoValueTypes.Contains( SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.evaporated_from_surface.ValueType)){prc.AddCondition(r41);}
                RangeBasedCondition r42 = new RangeBasedCondition(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.actual_evapotranspiration);
                if(r42.ApplicableVarInfoValueTypes.Contains( SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.actual_evapotranspiration.ValueType)){prc.AddCondition(r42);}

                string ret = "";
                ret += _Evapotranspiration.TestPostConditions(s, s1, r, a, ex, " strategy SiriusQualityEvapotranspirationComp.Strategies.EvapotranspirationComp");
                if (ret != "") { pre.TestsOut(ret, true, "   postconditions tests of associated classes"); }

                string postConditionsResult = pre.VerifyPostconditions(prc, callID); if (!string.IsNullOrEmpty(postConditionsResult)) { pre.TestsOut(postConditionsResult, true, "PostConditions errors in strategy " + this.GetType().Name); } return postConditionsResult;
            }
            catch (Exception exception)
            {
                string msg = "Component SiriusQuality.EvapotranspirationComp, " + this.GetType().Name + ": Unhandled exception running post-condition test. ";
                throw new Exception(msg, exception);
            }
        }

        public string TestPreConditions(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompState s,SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompState s1,SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompRate r,SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompAuxiliary a,SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenous ex,string callID)
        {
            try
            {
                //Set current values of the inputs to the static VarInfo representing the inputs properties of the domain classes
                SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenousVarInfo.external_reference_evapotranspiration.CurrentValue=ex.external_reference_evapotranspiration;
                SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenousVarInfo.height_nn.CurrentValue=ex.height_nn;
                SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenousVarInfo.max_air_temperature.CurrentValue=ex.max_air_temperature;
                SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenousVarInfo.min_air_temperature.CurrentValue=ex.min_air_temperature;
                SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenousVarInfo.mean_air_temperature.CurrentValue=ex.mean_air_temperature;
                SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenousVarInfo.relative_humidity.CurrentValue=ex.relative_humidity;
                SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenousVarInfo.wind_speed.CurrentValue=ex.wind_speed;
                SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenousVarInfo.wind_speed_height.CurrentValue=ex.wind_speed_height;
                SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenousVarInfo.global_radiation.CurrentValue=ex.global_radiation;
                SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenousVarInfo.julian_day.CurrentValue=ex.julian_day;
                SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenousVarInfo.latitude.CurrentValue=ex.latitude;
                SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.evaporated_from_surface.CurrentValue=s.evaporated_from_surface;
                SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.surface_water_storage.CurrentValue=s.surface_water_storage;
                SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.snow_depth.CurrentValue=s.snow_depth;
                SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.developmental_stage.CurrentValue=s.developmental_stage;
                SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.crop_reference_evapotranspiration.CurrentValue=s.crop_reference_evapotranspiration;
                SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.reference_evapotranspiration.CurrentValue=s.reference_evapotranspiration;
                SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.actual_evaporation.CurrentValue=s.actual_evaporation;
                SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.actual_transpiration.CurrentValue=s.actual_transpiration;
                SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.kc_factor.CurrentValue=s.kc_factor;
                SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.percentage_soil_coverage.CurrentValue=s.percentage_soil_coverage;
                SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.soil_moisture.CurrentValue=s.soil_moisture;
                SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.permanent_wilting_point.CurrentValue=s.permanent_wilting_point;
                SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.field_capacity.CurrentValue=s.field_capacity;
                SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.evaporation.CurrentValue=s.evaporation;
                SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.transpiration.CurrentValue=s.transpiration;
                SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.crop_transpiration.CurrentValue=s.crop_transpiration;
                SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.crop_remaining_evapotranspiration.CurrentValue=s.crop_remaining_evapotranspiration;
                SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.crop_evaporated_from_intercepted.CurrentValue=s.crop_evaporated_from_intercepted;
                SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.evapotranspiration.CurrentValue=s.evapotranspiration;
                SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.actual_evapotranspiration.CurrentValue=s.actual_evapotranspiration;
                SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.vapor_pressure.CurrentValue=s.vapor_pressure;
                ConditionsCollection prc = new ConditionsCollection();
                Preconditions pre = new Preconditions(); 
                RangeBasedCondition r1 = new RangeBasedCondition(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenousVarInfo.external_reference_evapotranspiration);
                if(r1.ApplicableVarInfoValueTypes.Contains( SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenousVarInfo.external_reference_evapotranspiration.ValueType)){prc.AddCondition(r1);}
                RangeBasedCondition r2 = new RangeBasedCondition(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenousVarInfo.height_nn);
                if(r2.ApplicableVarInfoValueTypes.Contains( SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenousVarInfo.height_nn.ValueType)){prc.AddCondition(r2);}
                RangeBasedCondition r3 = new RangeBasedCondition(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenousVarInfo.max_air_temperature);
                if(r3.ApplicableVarInfoValueTypes.Contains( SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenousVarInfo.max_air_temperature.ValueType)){prc.AddCondition(r3);}
                RangeBasedCondition r4 = new RangeBasedCondition(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenousVarInfo.min_air_temperature);
                if(r4.ApplicableVarInfoValueTypes.Contains( SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenousVarInfo.min_air_temperature.ValueType)){prc.AddCondition(r4);}
                RangeBasedCondition r5 = new RangeBasedCondition(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenousVarInfo.mean_air_temperature);
                if(r5.ApplicableVarInfoValueTypes.Contains( SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenousVarInfo.mean_air_temperature.ValueType)){prc.AddCondition(r5);}
                RangeBasedCondition r6 = new RangeBasedCondition(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenousVarInfo.relative_humidity);
                if(r6.ApplicableVarInfoValueTypes.Contains( SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenousVarInfo.relative_humidity.ValueType)){prc.AddCondition(r6);}
                RangeBasedCondition r7 = new RangeBasedCondition(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenousVarInfo.wind_speed);
                if(r7.ApplicableVarInfoValueTypes.Contains( SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenousVarInfo.wind_speed.ValueType)){prc.AddCondition(r7);}
                RangeBasedCondition r8 = new RangeBasedCondition(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenousVarInfo.wind_speed_height);
                if(r8.ApplicableVarInfoValueTypes.Contains( SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenousVarInfo.wind_speed_height.ValueType)){prc.AddCondition(r8);}
                RangeBasedCondition r9 = new RangeBasedCondition(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenousVarInfo.global_radiation);
                if(r9.ApplicableVarInfoValueTypes.Contains( SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenousVarInfo.global_radiation.ValueType)){prc.AddCondition(r9);}
                RangeBasedCondition r10 = new RangeBasedCondition(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenousVarInfo.julian_day);
                if(r10.ApplicableVarInfoValueTypes.Contains( SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenousVarInfo.julian_day.ValueType)){prc.AddCondition(r10);}
                RangeBasedCondition r11 = new RangeBasedCondition(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenousVarInfo.latitude);
                if(r11.ApplicableVarInfoValueTypes.Contains( SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenousVarInfo.latitude.ValueType)){prc.AddCondition(r11);}
                RangeBasedCondition r12 = new RangeBasedCondition(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.evaporated_from_surface);
                if(r12.ApplicableVarInfoValueTypes.Contains( SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.evaporated_from_surface.ValueType)){prc.AddCondition(r12);}
                RangeBasedCondition r13 = new RangeBasedCondition(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.surface_water_storage);
                if(r13.ApplicableVarInfoValueTypes.Contains( SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.surface_water_storage.ValueType)){prc.AddCondition(r13);}
                RangeBasedCondition r14 = new RangeBasedCondition(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.snow_depth);
                if(r14.ApplicableVarInfoValueTypes.Contains( SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.snow_depth.ValueType)){prc.AddCondition(r14);}
                RangeBasedCondition r15 = new RangeBasedCondition(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.developmental_stage);
                if(r15.ApplicableVarInfoValueTypes.Contains( SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.developmental_stage.ValueType)){prc.AddCondition(r15);}
                RangeBasedCondition r16 = new RangeBasedCondition(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.crop_reference_evapotranspiration);
                if(r16.ApplicableVarInfoValueTypes.Contains( SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.crop_reference_evapotranspiration.ValueType)){prc.AddCondition(r16);}
                RangeBasedCondition r17 = new RangeBasedCondition(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.reference_evapotranspiration);
                if(r17.ApplicableVarInfoValueTypes.Contains( SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.reference_evapotranspiration.ValueType)){prc.AddCondition(r17);}
                RangeBasedCondition r18 = new RangeBasedCondition(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.actual_evaporation);
                if(r18.ApplicableVarInfoValueTypes.Contains( SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.actual_evaporation.ValueType)){prc.AddCondition(r18);}
                RangeBasedCondition r19 = new RangeBasedCondition(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.actual_transpiration);
                if(r19.ApplicableVarInfoValueTypes.Contains( SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.actual_transpiration.ValueType)){prc.AddCondition(r19);}
                RangeBasedCondition r20 = new RangeBasedCondition(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.kc_factor);
                if(r20.ApplicableVarInfoValueTypes.Contains( SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.kc_factor.ValueType)){prc.AddCondition(r20);}
                RangeBasedCondition r21 = new RangeBasedCondition(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.percentage_soil_coverage);
                if(r21.ApplicableVarInfoValueTypes.Contains( SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.percentage_soil_coverage.ValueType)){prc.AddCondition(r21);}
                RangeBasedCondition r22 = new RangeBasedCondition(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.soil_moisture);
                if(r22.ApplicableVarInfoValueTypes.Contains( SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.soil_moisture.ValueType)){prc.AddCondition(r22);}
                RangeBasedCondition r23 = new RangeBasedCondition(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.permanent_wilting_point);
                if(r23.ApplicableVarInfoValueTypes.Contains( SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.permanent_wilting_point.ValueType)){prc.AddCondition(r23);}
                RangeBasedCondition r24 = new RangeBasedCondition(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.field_capacity);
                if(r24.ApplicableVarInfoValueTypes.Contains( SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.field_capacity.ValueType)){prc.AddCondition(r24);}
                RangeBasedCondition r25 = new RangeBasedCondition(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.evaporation);
                if(r25.ApplicableVarInfoValueTypes.Contains( SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.evaporation.ValueType)){prc.AddCondition(r25);}
                RangeBasedCondition r26 = new RangeBasedCondition(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.transpiration);
                if(r26.ApplicableVarInfoValueTypes.Contains( SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.transpiration.ValueType)){prc.AddCondition(r26);}
                RangeBasedCondition r27 = new RangeBasedCondition(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.crop_transpiration);
                if(r27.ApplicableVarInfoValueTypes.Contains( SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.crop_transpiration.ValueType)){prc.AddCondition(r27);}
                RangeBasedCondition r28 = new RangeBasedCondition(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.crop_remaining_evapotranspiration);
                if(r28.ApplicableVarInfoValueTypes.Contains( SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.crop_remaining_evapotranspiration.ValueType)){prc.AddCondition(r28);}
                RangeBasedCondition r29 = new RangeBasedCondition(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.crop_evaporated_from_intercepted);
                if(r29.ApplicableVarInfoValueTypes.Contains( SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.crop_evaporated_from_intercepted.ValueType)){prc.AddCondition(r29);}
                RangeBasedCondition r30 = new RangeBasedCondition(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.evapotranspiration);
                if(r30.ApplicableVarInfoValueTypes.Contains( SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.evapotranspiration.ValueType)){prc.AddCondition(r30);}
                RangeBasedCondition r31 = new RangeBasedCondition(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.actual_evapotranspiration);
                if(r31.ApplicableVarInfoValueTypes.Contains( SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.actual_evapotranspiration.ValueType)){prc.AddCondition(r31);}
                RangeBasedCondition r32 = new RangeBasedCondition(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.vapor_pressure);
                if(r32.ApplicableVarInfoValueTypes.Contains( SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.vapor_pressure.ValueType)){prc.AddCondition(r32);}

                prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("evaporation_zeta")));
                prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("maximum_evaporation_impact_depth")));
                prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("no_of_soil_layers")));
                prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("layer_thickness")));
                prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("reference_albedo")));
                prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("stomata_resistance")));
                prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("evaporation_reduction_method")));
                prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("xsa_critical_soil_moisture")));
                string ret = "";
                ret += _Evapotranspiration.TestPreConditions(s, s1, r, a, ex, " strategy SiriusQualityEvapotranspirationComp.Strategies.EvapotranspirationComp");
                if (ret != "") { pre.TestsOut(ret, true, "   preconditions tests of associated classes"); }

                string preConditionsResult = pre.VerifyPreconditions(prc, callID); if (!string.IsNullOrEmpty(preConditionsResult)) { pre.TestsOut(preConditionsResult, true, "PreConditions errors in component " + this.GetType().Name); } return preConditionsResult;
            }
            catch (Exception exception)
            {
                string msg = "Component SiriusQuality.EvapotranspirationComp, " + this.GetType().Name + ": Unhandled exception running pre-condition test. ";
                throw new Exception(msg, exception);
            }
        }

        public void Estimate(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompState s,SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompState s1,SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompRate r,SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompAuxiliary a,SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenous ex)
        {
            try
            {
                CalculateModel(s, s1, r, a, ex);
            }
            catch (Exception exception)
            {
                string msg = "Error in component SiriusQualityEvapotranspirationComp, strategy: " + this.GetType().Name + ": Unhandled exception running model. "+exception.GetType().FullName+" - "+exception.Message;
                throw new Exception(msg, exception);
            }
        }

        private void CalculateModel(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompState s,SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompState s1,SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompRate r,SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompAuxiliary a,SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenous ex)
        {
            EstimateOfAssociatedClasses(s, s1, r, a, ex);
        }

        //Declaration of the associated strategies
        Evapotranspiration _Evapotranspiration = new Evapotranspiration();

        private void EstimateOfAssociatedClasses(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompState s,SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompState s1,SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompRate r,SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompAuxiliary a,SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenous ex)
        {
            _Evapotranspiration.Estimate(s,s1, r, a, ex);
        }

        public EvapotranspirationCompComponent(EvapotranspirationCompComponent toCopy): this() // copy constructor 
        {
                evaporation_zeta = toCopy.evaporation_zeta;
                maximum_evaporation_impact_depth = toCopy.maximum_evaporation_impact_depth;
                no_of_soil_layers = toCopy.no_of_soil_layers;
                
        for (int i = 0; i < no_of_soil_layers; i++)
            { layer_thickness[i] = toCopy.layer_thickness[i]; }
    
                reference_albedo = toCopy.reference_albedo;
                stomata_resistance = toCopy.stomata_resistance;
                evaporation_reduction_method = toCopy.evaporation_reduction_method;
                xsa_critical_soil_moisture = toCopy.xsa_critical_soil_moisture;
            }
        }
    }