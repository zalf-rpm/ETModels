
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
    public class Evapotranspiration : IStrategySiriusQualityEvapotranspirationComp
    {
        public Evapotranspiration()
        {
            ModellingOptions mo0_0 = new ModellingOptions();
            //Parameters
            List<VarInfo> _parameters0_0 = new List<VarInfo>();
            VarInfo v1 = new VarInfo();
            v1.DefaultValue = 40;
            v1.Description = "shape factor";
            v1.Id = 0;
            v1.MaxValue = 40;
            v1.MinValue = 0;
            v1.Name = "evaporation_zeta";
            v1.Size = 1;
            v1.Units = "dimensionless";
            v1.URL = "";
            v1.VarType = CRA.ModelLayer.Core.VarInfo.Type.PARAMETER;
            v1.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
            _parameters0_0.Add(v1);
            VarInfo v2 = new VarInfo();
            v2.DefaultValue = 5;
            v2.Description = "maximumEvaporationImpactDepth";
            v2.Id = 0;
            v2.MaxValue = -1D;
            v2.MinValue = 0;
            v2.Name = "maximum_evaporation_impact_depth";
            v2.Size = 1;
            v2.Units = "dm";
            v2.URL = "";
            v2.VarType = CRA.ModelLayer.Core.VarInfo.Type.PARAMETER;
            v2.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
            _parameters0_0.Add(v2);
            VarInfo v3 = new VarInfo();
            v3.DefaultValue = 20;
            v3.Description = "number of soil layers";
            v3.Id = 0;
            v3.MaxValue = -1D;
            v3.MinValue = 0;
            v3.Name = "no_of_soil_layers";
            v3.Size = 1;
            v3.Units = "dimensionless";
            v3.URL = "";
            v3.VarType = CRA.ModelLayer.Core.VarInfo.Type.PARAMETER;
            v3.ValueType = VarInfoValueTypes.GetInstanceForName("Integer");
            _parameters0_0.Add(v3);
            VarInfo v4 = new VarInfo();
            v4.DefaultValue = -1D;
            v4.Description = "layer thickness array";
            v4.Id = 0;
            v4.MaxValue = -1D;
            v4.MinValue = -1D;
            v4.Name = "layer_thickness";
            v4.Size = 1;
            v4.Units = "m";
            v4.URL = "";
            v4.VarType = CRA.ModelLayer.Core.VarInfo.Type.PARAMETER;
            v4.ValueType = VarInfoValueTypes.GetInstanceForName("ArrayDouble");
            _parameters0_0.Add(v4);
            VarInfo v5 = new VarInfo();
            v5.DefaultValue = 0;
            v5.Description = "reference albedo";
            v5.Id = 0;
            v5.MaxValue = 1;
            v5.MinValue = 0;
            v5.Name = "reference_albedo";
            v5.Size = 1;
            v5.Units = "dimensionless";
            v5.URL = "";
            v5.VarType = CRA.ModelLayer.Core.VarInfo.Type.PARAMETER;
            v5.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
            _parameters0_0.Add(v5);
            VarInfo v6 = new VarInfo();
            v6.DefaultValue = 100;
            v6.Description = "stomata resistance";
            v6.Id = 0;
            v6.MaxValue = 10000;
            v6.MinValue = 0;
            v6.Name = "stomata_resistance";
            v6.Size = 1;
            v6.Units = "s/m";
            v6.URL = "";
            v6.VarType = CRA.ModelLayer.Core.VarInfo.Type.PARAMETER;
            v6.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
            _parameters0_0.Add(v6);
            VarInfo v7 = new VarInfo();
            v7.DefaultValue = 1;
            v7.Description = "THESEUS (0) or HERMES (1) evaporation reduction method";
            v7.Id = 0;
            v7.MaxValue = 1;
            v7.MinValue = 0;
            v7.Name = "evaporation_reduction_method";
            v7.Size = 1;
            v7.Units = "dimensionless";
            v7.URL = "";
            v7.VarType = CRA.ModelLayer.Core.VarInfo.Type.PARAMETER;
            v7.ValueType = VarInfoValueTypes.GetInstanceForName("Integer");
            _parameters0_0.Add(v7);
            VarInfo v8 = new VarInfo();
            v8.DefaultValue = 0.1;
            v8.Description = "XSACriticalSoilMoisture";
            v8.Id = 0;
            v8.MaxValue = 1.5;
            v8.MinValue = 0;
            v8.Name = "xsa_critical_soil_moisture";
            v8.Size = 1;
            v8.Units = "m3/m3";
            v8.URL = "";
            v8.VarType = CRA.ModelLayer.Core.VarInfo.Type.PARAMETER;
            v8.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
            _parameters0_0.Add(v8);
            mo0_0.Parameters=_parameters0_0;

            //Inputs
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
            pd13.PropertyName = "snow_depth";
            pd13.PropertyType = (SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.snow_depth).ValueType.TypeForCurrentValue;
            pd13.PropertyVarInfo =(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.snow_depth);
            _inputs0_0.Add(pd13);
            PropertyDescription pd14 = new PropertyDescription();
            pd14.DomainClassType = typeof(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompState);
            pd14.PropertyName = "developmental_stage";
            pd14.PropertyType = (SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.developmental_stage).ValueType.TypeForCurrentValue;
            pd14.PropertyVarInfo =(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.developmental_stage);
            _inputs0_0.Add(pd14);
            PropertyDescription pd15 = new PropertyDescription();
            pd15.DomainClassType = typeof(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompState);
            pd15.PropertyName = "crop_reference_evapotranspiration";
            pd15.PropertyType = (SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.crop_reference_evapotranspiration).ValueType.TypeForCurrentValue;
            pd15.PropertyVarInfo =(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.crop_reference_evapotranspiration);
            _inputs0_0.Add(pd15);
            PropertyDescription pd16 = new PropertyDescription();
            pd16.DomainClassType = typeof(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompState);
            pd16.PropertyName = "reference_evapotranspiration";
            pd16.PropertyType = (SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.reference_evapotranspiration).ValueType.TypeForCurrentValue;
            pd16.PropertyVarInfo =(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.reference_evapotranspiration);
            _inputs0_0.Add(pd16);
            PropertyDescription pd17 = new PropertyDescription();
            pd17.DomainClassType = typeof(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompState);
            pd17.PropertyName = "actual_evaporation";
            pd17.PropertyType = (SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.actual_evaporation).ValueType.TypeForCurrentValue;
            pd17.PropertyVarInfo =(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.actual_evaporation);
            _inputs0_0.Add(pd17);
            PropertyDescription pd18 = new PropertyDescription();
            pd18.DomainClassType = typeof(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompState);
            pd18.PropertyName = "actual_transpiration";
            pd18.PropertyType = (SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.actual_transpiration).ValueType.TypeForCurrentValue;
            pd18.PropertyVarInfo =(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.actual_transpiration);
            _inputs0_0.Add(pd18);
            PropertyDescription pd19 = new PropertyDescription();
            pd19.DomainClassType = typeof(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompState);
            pd19.PropertyName = "surface_water_storage";
            pd19.PropertyType = (SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.surface_water_storage).ValueType.TypeForCurrentValue;
            pd19.PropertyVarInfo =(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.surface_water_storage);
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

            //Outputs
            List<PropertyDescription> _outputs0_0 = new List<PropertyDescription>();
            PropertyDescription pd33 = new PropertyDescription();
            pd33.DomainClassType = typeof(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompState);
            pd33.PropertyName = "evaporated_from_surface";
            pd33.PropertyType = (SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.evaporated_from_surface).ValueType.TypeForCurrentValue;
            pd33.PropertyVarInfo =(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.evaporated_from_surface);
            _outputs0_0.Add(pd33);
            mo0_0.Outputs=_outputs0_0;PropertyDescription pd34 = new PropertyDescription();
            pd34.DomainClassType = typeof(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompState);
            pd34.PropertyName = "actual_evapotranspiration";
            pd34.PropertyType = (SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.actual_evapotranspiration).ValueType.TypeForCurrentValue;
            pd34.PropertyVarInfo =(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.actual_evapotranspiration);
            _outputs0_0.Add(pd34);
            mo0_0.Outputs=_outputs0_0;
            //Associated strategies
            List<string> lAssStrat0_0 = new List<string>();
            mo0_0.AssociatedStrategies = lAssStrat0_0;
            //Adding the modeling options to the modeling options manager
            _modellingOptionsManager = new ModellingOptionsManager(mo0_0);
            SetStaticParametersVarInfoDefinitions();
            SetPublisherData();

        }

        public string Description
        {
            
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
            _pd.Add("Creator", "Claas Nendel (transcription into Crop2ML by Michael Berg-Mohnicke)");
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
            return new List<Type>() {  typeof(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompState),  typeof(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompState), typeof(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompRate), typeof(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompAuxiliary), typeof(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompExogenous)};
        }

        // Getter and setters for the value of the parameters of the strategy. The actual parameters are stored into the ModelingOptionsManager of the strategy.

        public double evaporation_zeta
        {
            get { 
                VarInfo vi= _modellingOptionsManager.GetParameterByName("evaporation_zeta");
                if (vi != null && vi.CurrentValue!=null) return (double)vi.CurrentValue ;
                else throw new Exception("Parameter 'evaporation_zeta' not found (or found null) in strategy 'Evapotranspiration'");
            } set {
                VarInfo vi = _modellingOptionsManager.GetParameterByName("evaporation_zeta");
                if (vi != null)  vi.CurrentValue=value;
                else throw new Exception("Parameter 'evaporation_zeta' not found in strategy 'Evapotranspiration'");
            }
        }
        public double maximum_evaporation_impact_depth
        {
            get { 
                VarInfo vi= _modellingOptionsManager.GetParameterByName("maximum_evaporation_impact_depth");
                if (vi != null && vi.CurrentValue!=null) return (double)vi.CurrentValue ;
                else throw new Exception("Parameter 'maximum_evaporation_impact_depth' not found (or found null) in strategy 'Evapotranspiration'");
            } set {
                VarInfo vi = _modellingOptionsManager.GetParameterByName("maximum_evaporation_impact_depth");
                if (vi != null)  vi.CurrentValue=value;
                else throw new Exception("Parameter 'maximum_evaporation_impact_depth' not found in strategy 'Evapotranspiration'");
            }
        }
        public int no_of_soil_layers
        {
            get { 
                VarInfo vi= _modellingOptionsManager.GetParameterByName("no_of_soil_layers");
                if (vi != null && vi.CurrentValue!=null) return (int)vi.CurrentValue ;
                else throw new Exception("Parameter 'no_of_soil_layers' not found (or found null) in strategy 'Evapotranspiration'");
            } set {
                VarInfo vi = _modellingOptionsManager.GetParameterByName("no_of_soil_layers");
                if (vi != null)  vi.CurrentValue=value;
                else throw new Exception("Parameter 'no_of_soil_layers' not found in strategy 'Evapotranspiration'");
            }
        }
        public double[] layer_thickness
        {
            get { 
                VarInfo vi= _modellingOptionsManager.GetParameterByName("layer_thickness");
                if (vi != null && vi.CurrentValue!=null) return (double[])vi.CurrentValue ;
                else throw new Exception("Parameter 'layer_thickness' not found (or found null) in strategy 'Evapotranspiration'");
            } set {
                VarInfo vi = _modellingOptionsManager.GetParameterByName("layer_thickness");
                if (vi != null)  vi.CurrentValue=value;
                else throw new Exception("Parameter 'layer_thickness' not found in strategy 'Evapotranspiration'");
            }
        }
        public double reference_albedo
        {
            get { 
                VarInfo vi= _modellingOptionsManager.GetParameterByName("reference_albedo");
                if (vi != null && vi.CurrentValue!=null) return (double)vi.CurrentValue ;
                else throw new Exception("Parameter 'reference_albedo' not found (or found null) in strategy 'Evapotranspiration'");
            } set {
                VarInfo vi = _modellingOptionsManager.GetParameterByName("reference_albedo");
                if (vi != null)  vi.CurrentValue=value;
                else throw new Exception("Parameter 'reference_albedo' not found in strategy 'Evapotranspiration'");
            }
        }
        public double stomata_resistance
        {
            get { 
                VarInfo vi= _modellingOptionsManager.GetParameterByName("stomata_resistance");
                if (vi != null && vi.CurrentValue!=null) return (double)vi.CurrentValue ;
                else throw new Exception("Parameter 'stomata_resistance' not found (or found null) in strategy 'Evapotranspiration'");
            } set {
                VarInfo vi = _modellingOptionsManager.GetParameterByName("stomata_resistance");
                if (vi != null)  vi.CurrentValue=value;
                else throw new Exception("Parameter 'stomata_resistance' not found in strategy 'Evapotranspiration'");
            }
        }
        public int evaporation_reduction_method
        {
            get { 
                VarInfo vi= _modellingOptionsManager.GetParameterByName("evaporation_reduction_method");
                if (vi != null && vi.CurrentValue!=null) return (int)vi.CurrentValue ;
                else throw new Exception("Parameter 'evaporation_reduction_method' not found (or found null) in strategy 'Evapotranspiration'");
            } set {
                VarInfo vi = _modellingOptionsManager.GetParameterByName("evaporation_reduction_method");
                if (vi != null)  vi.CurrentValue=value;
                else throw new Exception("Parameter 'evaporation_reduction_method' not found in strategy 'Evapotranspiration'");
            }
        }
        public double xsa_critical_soil_moisture
        {
            get { 
                VarInfo vi= _modellingOptionsManager.GetParameterByName("xsa_critical_soil_moisture");
                if (vi != null && vi.CurrentValue!=null) return (double)vi.CurrentValue ;
                else throw new Exception("Parameter 'xsa_critical_soil_moisture' not found (or found null) in strategy 'Evapotranspiration'");
            } set {
                VarInfo vi = _modellingOptionsManager.GetParameterByName("xsa_critical_soil_moisture");
                if (vi != null)  vi.CurrentValue=value;
                else throw new Exception("Parameter 'xsa_critical_soil_moisture' not found in strategy 'Evapotranspiration'");
            }
        }

        public void SetParametersDefaultValue()
        {
            _modellingOptionsManager.SetParametersDefaultValue();
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

        private static VarInfo _evaporation_zetaVarInfo = new VarInfo();
        public static VarInfo evaporation_zetaVarInfo
        {
            get { return _evaporation_zetaVarInfo;} 
        }

        private static VarInfo _maximum_evaporation_impact_depthVarInfo = new VarInfo();
        public static VarInfo maximum_evaporation_impact_depthVarInfo
        {
            get { return _maximum_evaporation_impact_depthVarInfo;} 
        }

        private static VarInfo _no_of_soil_layersVarInfo = new VarInfo();
        public static VarInfo no_of_soil_layersVarInfo
        {
            get { return _no_of_soil_layersVarInfo;} 
        }

        private static VarInfo _layer_thicknessVarInfo = new VarInfo();
        public static VarInfo layer_thicknessVarInfo
        {
            get { return _layer_thicknessVarInfo;} 
        }

        private static VarInfo _reference_albedoVarInfo = new VarInfo();
        public static VarInfo reference_albedoVarInfo
        {
            get { return _reference_albedoVarInfo;} 
        }

        private static VarInfo _stomata_resistanceVarInfo = new VarInfo();
        public static VarInfo stomata_resistanceVarInfo
        {
            get { return _stomata_resistanceVarInfo;} 
        }

        private static VarInfo _evaporation_reduction_methodVarInfo = new VarInfo();
        public static VarInfo evaporation_reduction_methodVarInfo
        {
            get { return _evaporation_reduction_methodVarInfo;} 
        }

        private static VarInfo _xsa_critical_soil_moistureVarInfo = new VarInfo();
        public static VarInfo xsa_critical_soil_moistureVarInfo
        {
            get { return _xsa_critical_soil_moistureVarInfo;} 
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
                string postConditionsResult = pre.VerifyPostconditions(prc, callID); if (!string.IsNullOrEmpty(postConditionsResult)) { pre.TestsOut(postConditionsResult, true, "PostConditions errors in strategy " + this.GetType().Name); } return postConditionsResult;
            }
            catch (Exception exception)
            {
                string msg = "SiriusQuality.EvapotranspirationComp, " + this.GetType().Name + ": Unhandled exception running post-condition test. ";
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
                SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.snow_depth.CurrentValue=s.snow_depth;
                SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.developmental_stage.CurrentValue=s.developmental_stage;
                SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.crop_reference_evapotranspiration.CurrentValue=s.crop_reference_evapotranspiration;
                SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.reference_evapotranspiration.CurrentValue=s.reference_evapotranspiration;
                SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.actual_evaporation.CurrentValue=s.actual_evaporation;
                SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.actual_transpiration.CurrentValue=s.actual_transpiration;
                SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.surface_water_storage.CurrentValue=s.surface_water_storage;
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
                RangeBasedCondition r13 = new RangeBasedCondition(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.snow_depth);
                if(r13.ApplicableVarInfoValueTypes.Contains( SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.snow_depth.ValueType)){prc.AddCondition(r13);}
                RangeBasedCondition r14 = new RangeBasedCondition(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.developmental_stage);
                if(r14.ApplicableVarInfoValueTypes.Contains( SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.developmental_stage.ValueType)){prc.AddCondition(r14);}
                RangeBasedCondition r15 = new RangeBasedCondition(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.crop_reference_evapotranspiration);
                if(r15.ApplicableVarInfoValueTypes.Contains( SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.crop_reference_evapotranspiration.ValueType)){prc.AddCondition(r15);}
                RangeBasedCondition r16 = new RangeBasedCondition(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.reference_evapotranspiration);
                if(r16.ApplicableVarInfoValueTypes.Contains( SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.reference_evapotranspiration.ValueType)){prc.AddCondition(r16);}
                RangeBasedCondition r17 = new RangeBasedCondition(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.actual_evaporation);
                if(r17.ApplicableVarInfoValueTypes.Contains( SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.actual_evaporation.ValueType)){prc.AddCondition(r17);}
                RangeBasedCondition r18 = new RangeBasedCondition(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.actual_transpiration);
                if(r18.ApplicableVarInfoValueTypes.Contains( SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.actual_transpiration.ValueType)){prc.AddCondition(r18);}
                RangeBasedCondition r19 = new RangeBasedCondition(SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.surface_water_storage);
                if(r19.ApplicableVarInfoValueTypes.Contains( SiriusQualityEvapotranspirationComp.DomainClass.EvapotranspirationCompStateVarInfo.surface_water_storage.ValueType)){prc.AddCondition(r19);}
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
                string preConditionsResult = pre.VerifyPreconditions(prc, callID); if (!string.IsNullOrEmpty(preConditionsResult)) { pre.TestsOut(preConditionsResult, true, "PreConditions errors in strategy " + this.GetType().Name); } return preConditionsResult;
            }
            catch (Exception exception)
            {
                string msg = "SiriusQuality.EvapotranspirationComp, " + this.GetType().Name + ": Unhandled exception running pre-condition test. ";
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

        private double _evaporation_zeta;
        public double evaporation_zeta
    {
        get { return this._evaporation_zeta; }
        set { this._evaporation_zeta= value; } 
    }
        private double _maximum_evaporation_impact_depth;
        public double maximum_evaporation_impact_depth
    {
        get { return this._maximum_evaporation_impact_depth; }
        set { this._maximum_evaporation_impact_depth= value; } 
    }
        private int _no_of_soil_layers;
        public int no_of_soil_layers
    {
        get { return this._no_of_soil_layers; }
        set { this._no_of_soil_layers= value; } 
    }
        private double[] _layer_thickness;
        public double[] layer_thickness
    {
        get { return this._layer_thickness; }
        set { this._layer_thickness= value; } 
    }
        private double _reference_albedo;
        public double reference_albedo
    {
        get { return this._reference_albedo; }
        set { this._reference_albedo= value; } 
    }
        private double _stomata_resistance;
        public double stomata_resistance
    {
        get { return this._stomata_resistance; }
        set { this._stomata_resistance= value; } 
    }
        private int _evaporation_reduction_method;
        public int evaporation_reduction_method
    {
        get { return this._evaporation_reduction_method; }
        set { this._evaporation_reduction_method= value; } 
    }
        private double _xsa_critical_soil_moisture;
        public double xsa_critical_soil_moisture
    {
        get { return this._xsa_critical_soil_moisture; }
        set { this._xsa_critical_soil_moisture= value; } 
    }
    /// <summary>
    /// Constructor of the Evapotranspiration component")
    /// </summary>  
    public Evapotranspiration() { }
    
        public void  CalculateModel(EvapotranspirationCompState s, EvapotranspirationCompState s1, EvapotranspirationCompRate r, EvapotranspirationCompAuxiliary a, EvapotranspirationCompExogenous ex)
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
    //            * name: no_of_soil_layers
    //                          ** description : number of soil layers
    //                          ** inputtype : parameter
    //                          ** parametercategory : constant
    //                          ** datatype : INT
    //                          ** max : 
    //                          ** min : 0
    //                          ** default : 20
    //                          ** unit : dimensionless
    //            * name: layer_thickness
    //                          ** description : layer thickness array
    //                          ** inputtype : parameter
    //                          ** parametercategory : constant
    //                          ** datatype : DOUBLEARRAY
    //                          ** len : no_of_soil_layers
    //                          ** max : 
    //                          ** min : 
    //                          ** default : 
    //                          ** unit : m
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
    //            * name: external_reference_evapotranspiration
    //                          ** description : externally supplied ET0
    //                          ** inputtype : variable
    //                          ** variablecategory : exogenous
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 0
    //                          ** default : -1
    //                          ** unit : mm
    //            * name: height_nn
    //                          ** description : height above sea leavel
    //                          ** inputtype : variable
    //                          ** variablecategory : exogenous
    //                          ** datatype : DOUBLE
    //                          ** max : 9999
    //                          ** min : -9999
    //                          ** default : 0
    //                          ** unit : m
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
    //            * name: latitude
    //                          ** description : latitude
    //                          ** inputtype : variable
    //                          ** variablecategory : exogenous
    //                          ** datatype : DOUBLE
    //                          ** max : 90
    //                          ** min : -90
    //                          ** default : 0
    //                          ** unit : degree
    //            * name: evaporated_from_surface
    //                          ** description : evaporated_from_surface
    //                          ** inputtype : variable
    //                          ** variablecategory : state
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 0
    //                          ** default : 0
    //                          ** unit : mm
    //            * name: snow_depth
    //                          ** description : depth of snow layer
    //                          ** inputtype : variable
    //                          ** variablecategory : state
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 0
    //                          ** default : 0
    //                          ** unit : mm
    //            * name: developmental_stage
    //                          ** description : MONICA crop developmental stage
    //                          ** inputtype : variable
    //                          ** variablecategory : state
    //                          ** datatype : INT
    //                          ** max : 6
    //                          ** min : 0
    //                          ** default : 0
    //                          ** unit : dimensionless
    //            * name: crop_reference_evapotranspiration
    //                          ** description : the crop specific ET0, if no external ET0 and crop is planted
    //                          ** inputtype : variable
    //                          ** variablecategory : state
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 0
    //                          ** default : -1
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
    //            * name: surface_water_storage
    //                          ** description : Simulates a virtual layer that contains the surface water
    //                          ** inputtype : variable
    //                          ** variablecategory : state
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 0
    //                          ** default : 0
    //                          ** unit : mm
    //            * name: kc_factor
    //                          ** description : crop coefficient ETc/ET0
    //                          ** inputtype : variable
    //                          ** variablecategory : state
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 0
    //                          ** default : 0.75
    //                          ** unit : dimensionless
    //            * name: percentage_soil_coverage
    //                          ** description : fraction of soil covered by crop
    //                          ** inputtype : variable
    //                          ** variablecategory : state
    //                          ** datatype : DOUBLE
    //                          ** max : 1
    //                          ** min : 0
    //                          ** default : 0
    //                          ** unit : m2/m2
    //            * name: soil_moisture
    //                          ** description : soil moisture array
    //                          ** inputtype : variable
    //                          ** variablecategory : state
    //                          ** datatype : DOUBLEARRAY
    //                          ** len : no_of_soil_layers
    //                          ** max : 
    //                          ** min : 
    //                          ** default : 
    //                          ** unit : m3/m3
    //            * name: permanent_wilting_point
    //                          ** description : permanent wilting point array
    //                          ** inputtype : variable
    //                          ** variablecategory : state
    //                          ** datatype : DOUBLEARRAY
    //                          ** len : no_of_soil_layers
    //                          ** max : 2
    //                          ** min : 0
    //                          ** default : 0
    //                          ** unit : m3/m3
    //            * name: field_capacity
    //                          ** description : field capacity array
    //                          ** inputtype : variable
    //                          ** variablecategory : state
    //                          ** datatype : DOUBLEARRAY
    //                          ** len : no_of_soil_layers
    //                          ** max : 1
    //                          ** min : 0
    //                          ** default : 0
    //                          ** unit : m3/m3
    //            * name: evaporation
    //                          ** description : evaporation array
    //                          ** inputtype : variable
    //                          ** variablecategory : state
    //                          ** datatype : DOUBLEARRAY
    //                          ** len : no_of_soil_layers
    //                          ** max : 1
    //                          ** min : 0
    //                          ** default : 0
    //                          ** unit : mm
    //            * name: transpiration
    //                          ** description : transpiration array
    //                          ** inputtype : variable
    //                          ** variablecategory : state
    //                          ** datatype : DOUBLEARRAY
    //                          ** len : no_of_soil_layers
    //                          ** max : 
    //                          ** min : 
    //                          ** default : 
    //                          ** unit : mm
    //            * name: crop_transpiration
    //                          ** description : crop transpiration array
    //                          ** inputtype : variable
    //                          ** variablecategory : state
    //                          ** datatype : DOUBLEARRAY
    //                          ** len : no_of_soil_layers
    //                          ** max : 
    //                          ** min : 
    //                          ** default : 
    //                          ** unit : mm
    //            * name: crop_remaining_evapotranspiration
    //                          ** description : crop remaining evapotranspiration
    //                          ** inputtype : variable
    //                          ** variablecategory : state
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 
    //                          ** default : 
    //                          ** unit : mm
    //            * name: crop_evaporated_from_intercepted
    //                          ** description : crop evaporated water from intercepted water
    //                          ** inputtype : variable
    //                          ** variablecategory : state
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 
    //                          ** default : 
    //                          ** unit : mm
    //            * name: evapotranspiration
    //                          ** description : evapotranspiration array
    //                          ** inputtype : variable
    //                          ** variablecategory : state
    //                          ** datatype : DOUBLEARRAY
    //                          ** len : no_of_soil_layers
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
    //            * name: vapor_pressure
    //                          ** description : vapor pressure
    //                          ** inputtype : variable
    //                          ** variablecategory : state
    //                          ** datatype : DOUBLE
    //                          ** max : 
    //                          ** min : 0
    //                          ** default : 0
    //                          ** unit : kPa
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
            double external_reference_evapotranspiration = ex.external_reference_evapotranspiration;
            double height_nn = ex.height_nn;
            double max_air_temperature = ex.max_air_temperature;
            double min_air_temperature = ex.min_air_temperature;
            double mean_air_temperature = ex.mean_air_temperature;
            double relative_humidity = ex.relative_humidity;
            double wind_speed = ex.wind_speed;
            double wind_speed_height = ex.wind_speed_height;
            double global_radiation = ex.global_radiation;
            int julian_day = ex.julian_day;
            double latitude = ex.latitude;
            double evaporated_from_surface = s.evaporated_from_surface;
            double snow_depth = s.snow_depth;
            int developmental_stage = s.developmental_stage;
            double crop_reference_evapotranspiration = s.crop_reference_evapotranspiration;
            double reference_evapotranspiration = s.reference_evapotranspiration;
            double actual_evaporation = s.actual_evaporation;
            double actual_transpiration = s.actual_transpiration;
            double surface_water_storage = s.surface_water_storage;
            double kc_factor = s.kc_factor;
            double percentage_soil_coverage = s.percentage_soil_coverage;
            double[] soil_moisture = s.soil_moisture;
            double[] permanent_wilting_point = s.permanent_wilting_point;
            double[] field_capacity = s.field_capacity;
            double[] evaporation = s.evaporation;
            double[] transpiration = s.transpiration;
            double[] crop_transpiration = s.crop_transpiration;
            double crop_remaining_evapotranspiration = s.crop_remaining_evapotranspiration;
            double crop_evaporated_from_intercepted = s.crop_evaporated_from_intercepted;
            double[] evapotranspiration = s.evapotranspiration;
            double actual_evapotranspiration = s.actual_evapotranspiration;
            double vapor_pressure = s.vapor_pressure;
            evaporated_from_surface = 0.0;
            double potential_evapotranspiration = 0.0;
            double evaporated_from_intercept = 0.0;
            if (developmental_stage > 0)
            {
                if (external_reference_evapotranspiration < 0.0)
                {
                    reference_evapotranspiration = reference_evapotranspiration;
                }
                else
                {
                    reference_evapotranspiration = external_reference_evapotranspiration;
                }
                potential_evapotranspiration = crop_remaining_evapotranspiration;
                evaporated_from_intercept = crop_evaporated_from_intercepted;
            }
            else
            {
                if (external_reference_evapotranspiration < 0.0)
                {
                    calc_reference_evapotranspiration(height_nn, max_air_temperature, min_air_temperature, relative_humidity, mean_air_temperature, wind_speed, wind_speed_height, global_radiation, julian_day, latitude, reference_albedo, ref vapor_pressure, stomata_resistance, out reference_evapotranspiration);
                }
                else
                {
                    reference_evapotranspiration = external_reference_evapotranspiration;
                }
                potential_evapotranspiration = reference_evapotranspiration * kc_factor;
            }
            actual_evaporation = 0.0;
            actual_transpiration = 0.0;
            if (potential_evapotranspiration > 6.5)
            {
                potential_evapotranspiration = 6.5;
            }
            bool evaporation_from_surface = false;
            double eRed1;
            double eRed2;
            double eRed3;
            double eReducer;
            int i;
            if (potential_evapotranspiration > 0.0)
            {
                evaporation_from_surface = false;
                if (surface_water_storage > 0.0)
                {
                    evaporation_from_surface = true;
                    potential_evapotranspiration = potential_evapotranspiration * 1.1 / kc_factor;
                    if (snow_depth > 0.0)
                    {
                        evaporated_from_surface = 0.0;
                    }
                    else if ( surface_water_storage < potential_evapotranspiration)
                    {
                        potential_evapotranspiration = potential_evapotranspiration - surface_water_storage;
                        evaporated_from_surface = surface_water_storage;
                        surface_water_storage = 0.0;
                    }
                    else
                    {
                        surface_water_storage = surface_water_storage - potential_evapotranspiration;
                        evaporated_from_surface = potential_evapotranspiration;
                        potential_evapotranspiration = 0.0;
                    }
                    potential_evapotranspiration = potential_evapotranspiration * kc_factor / 1.1;
                }
                if (potential_evapotranspiration > 0.0)
                {
                    for (i=0 ; i!=no_of_soil_layers ; i+=1)
                    {
                        eRed1 = e_reducer_1(permanent_wilting_point[i], field_capacity[i], soil_moisture[i], percentage_soil_coverage, potential_evapotranspiration, evaporation_reduction_method, xsa_critical_soil_moisture);
                        eRed2 = 0.0;
                        if ((double)(i) >= maximum_evaporation_impact_depth)
                        {
                            eRed2 = 0.0;
                        }
                        else
                        {
                            eRed2 = get_deprivation_factor(i + 1, maximum_evaporation_impact_depth, evaporation_zeta, layer_thickness[i]);
                        }
                        eRed3 = 0.0;
                        if (i > 0 && soil_moisture[i] < soil_moisture[i - 1])
                        {
                            eRed3 = 0.1;
                        }
                        else
                        {
                            eRed3 = 1.0;
                        }
                        eReducer = eRed1 * eRed2 * eRed3;
                        if (developmental_stage > 0)
                        {
                            if (percentage_soil_coverage >= 0.0 && percentage_soil_coverage < 1.0)
                            {
                                evaporation[i] = (1.0 - percentage_soil_coverage) * eReducer * potential_evapotranspiration;
                            }
                            else if ( percentage_soil_coverage >= 1.0)
                            {
                                evaporation[i] = 0.0;
                            }
                            if (snow_depth > 0.0)
                            {
                                evaporation[i] = 0.0;
                            }
                            transpiration[i] = crop_transpiration[i];
                            if (evaporation_from_surface)
                            {
                                transpiration[i] = percentage_soil_coverage * eReducer * potential_evapotranspiration;
                            }
                        }
                        else
                        {
                            if (snow_depth > 0.0)
                            {
                                evaporation[i] = 0.0;
                            }
                            else
                            {
                                evaporation[i] = potential_evapotranspiration * eReducer;
                                transpiration[i] = 0.0;
                            }
                        }
                        evapotranspiration[i] = evaporation[i] + transpiration[i];
                        soil_moisture[i] = soil_moisture[i] - (evapotranspiration[i] / 1000.0 / layer_thickness[i]);
                        if (soil_moisture[i] < 0.01)
                        {
                            soil_moisture[i] = 0.01;
                        }
                        actual_transpiration = actual_transpiration + transpiration[i];
                        actual_evaporation = actual_evaporation + evaporation[i];
                    }
                }
                actual_evapotranspiration = actual_transpiration + actual_evaporation + evaporated_from_intercept + evaporated_from_surface;
            }
            s.evaporated_from_surface= evaporated_from_surface;
            s.actual_evapotranspiration= actual_evapotranspiration;
        }
        public static double get_deprivation_factor(int layer_no, double deprivation_depth, double zeta, double layer_thickness)
        {
            double ltf;
            ltf = deprivation_depth / (layer_thickness * 10.0);
            double deprivation_factor;
            double c2;
            double c3;
            if (Math.Abs(zeta) < 0.0003)
            {
                deprivation_factor = 2.0 / ltf - (1.0 / (ltf * ltf) * (2 * layer_no - 1));
            }
            else
            {
                c2 = Math.Log((ltf + (zeta * layer_no)) / (ltf + (zeta * (layer_no - 1))));
                c3 = zeta / (ltf * (zeta + 1.0));
                deprivation_factor = (c2 - c3) / (Math.Log(zeta + 1.0) - (zeta / (zeta + 1.0)));
            }
            return deprivation_factor;
        }
        public static double bound(double lower, double value, double upper)
        {
            if (value < lower)
            {
                return lower;
            }
            if (value > upper)
            {
                return upper;
            }
            return value;
        }
        public static double calc_reference_evapotranspiration(double height_nn, double max_air_temperature, double min_air_temperature, double relative_humidity, double mean_air_temperature, double wind_speed, double wind_speed_height, double global_radiation, int julian_day, double latitude, double reference_albedo, ref double vapor_pressure, double stomata_resistance)
        {
            double declination;
            declination = -23.4 * Math.Cos(2.0 * Math.PI * ((julian_day + 10.0) / 365.0));
            double declination_sinus;
            declination_sinus = Math.Sin(declination * Math.PI / 180.0) * Math.Sin(latitude * Math.PI / 180.0);
            double declination_cosinus;
            declination_cosinus = Math.Cos(declination * Math.PI / 180.0) * Math.Cos(latitude * Math.PI / 180.0);
            double arg_astro_day_length;
            arg_astro_day_length = declination_sinus / declination_cosinus;
            arg_astro_day_length = bound(-1.0, arg_astro_day_length, 1.0);
            double astronomic_day_length;
            astronomic_day_length = 12.0 * (Math.PI + (2.0 * Math.Asin(arg_astro_day_length))) / Math.PI;
            double arg_effective_day_length;
            arg_effective_day_length = (-Math.Sin((8.0 * Math.PI / 180.0)) + declination_sinus) / declination_cosinus;
            arg_effective_day_length = bound(-1.0, arg_effective_day_length, 1.0);
            double arg_photo_day_length;
            arg_photo_day_length = (-Math.Sin((-6.0 * Math.PI / 180.0)) + declination_sinus) / declination_cosinus;
            arg_photo_day_length = bound(-1.0, arg_photo_day_length, 1.0);
            double arg_phot_act;
            arg_phot_act = Math.Min(1.0, declination_sinus / declination_cosinus * (declination_sinus / declination_cosinus));
            double phot_act_radiation_mean;
            phot_act_radiation_mean = 3600.0 * (declination_sinus * astronomic_day_length + (24.0 / Math.PI * declination_cosinus * Math.Sqrt((1.0 - arg_phot_act))));
            double clear_day_radiation = 0.0;
            if (phot_act_radiation_mean > 0.0 && astronomic_day_length > 0.0)
            {
                clear_day_radiation = 0.5 * 1300.0 * phot_act_radiation_mean * Math.Exp(-0.14 / (phot_act_radiation_mean / (astronomic_day_length * 3600.0)));
            }
            double SC;
            SC = 24.0 * 60.0 / Math.PI * 8.20 * (1.0 + (0.033 * Math.Cos(2.0 * Math.PI * julian_day / 365.0)));
            double arg_SHA;
            arg_SHA = bound(-1.0, -Math.Tan((latitude * Math.PI / 180.0)) * Math.Tan(declination * Math.PI / 180.0), 1.0);
            double SHA;
            SHA = Math.Acos(arg_SHA);
            double extraterrestrial_radiation;
            extraterrestrial_radiation = SC * (SHA * declination_sinus + (declination_cosinus * Math.Sin(SHA))) / 100.0;
            double atmospheric_pressure;
            atmospheric_pressure = 101.3 * Math.Pow((293.0 - (0.0065 * height_nn)) / 293.0, 5.26);
            double psycrometer_constant;
            psycrometer_constant = 0.000665 * atmospheric_pressure;
            double saturated_vapor_pressure_max;
            saturated_vapor_pressure_max = 0.6108 * Math.Exp(17.27 * max_air_temperature / (237.3 + max_air_temperature));
            double saturated_vapor_pressure_min;
            saturated_vapor_pressure_min = 0.6108 * Math.Exp(17.27 * min_air_temperature / (237.3 + min_air_temperature));
            double saturated_vapor_pressure;
            saturated_vapor_pressure = (saturated_vapor_pressure_max + saturated_vapor_pressure_min) / 2.0;
            if (vapor_pressure < 0.0)
            {
                if (relative_humidity <= 0.0)
                {
                    vapor_pressure = saturated_vapor_pressure_min;
                }
                else
                {
                    vapor_pressure = relative_humidity * saturated_vapor_pressure;
                }
            }
            double saturation_deficit;
            saturation_deficit = saturated_vapor_pressure - vapor_pressure;
            double saturated_vapour_pressure_slope;
            saturated_vapour_pressure_slope = 4098.0 * (0.6108 * Math.Exp(17.27 * mean_air_temperature / (mean_air_temperature + 237.3))) / ((mean_air_temperature + 237.3) * (mean_air_temperature + 237.3));
            double wind_speed_2m;
            wind_speed_2m = Math.Max(0.5, wind_speed * (4.87 / Math.Log((67.8 * wind_speed_height - 5.42))));
            double surface_resistance;
            surface_resistance = stomata_resistance / 1.44;
            double clear_sky_solar_radiation;
            clear_sky_solar_radiation = (0.75 + (0.00002 * height_nn)) * extraterrestrial_radiation;
            double relative_shortwave_radiation;
            relative_shortwave_radiation = clear_sky_solar_radiation > 0.0 ? Math.Min(global_radiation / clear_sky_solar_radiation, 1.0) : 1.0;
            double bolzmann_constant = 0.0000000049;
            double shortwave_radiation;
            shortwave_radiation = (1.0 - reference_albedo) * global_radiation;
            double longwave_radiation;
            longwave_radiation = bolzmann_constant * ((Math.Pow(min_air_temperature + 273.16, 4.0) + Math.Pow(max_air_temperature + 273.16, 4.0)) / 2.0) * (1.35 * relative_shortwave_radiation - 0.35) * (0.34 - (0.14 * Math.Sqrt(vapor_pressure)));
            double net_radiation;
            net_radiation = shortwave_radiation - longwave_radiation;
            double reference_evapotranspiration;
            reference_evapotranspiration = (0.408 * saturated_vapour_pressure_slope * net_radiation + (psycrometer_constant * (900.0 / (mean_air_temperature + 273.0)) * wind_speed_2m * saturation_deficit)) / (saturated_vapour_pressure_slope + (psycrometer_constant * (1.0 + (surface_resistance / 208.0 * wind_speed_2m))));
            if (reference_evapotranspiration < 0.0)
            {
                reference_evapotranspiration = 0.0;
            }
            return reference_evapotranspiration;
        }
        public static double e_reducer_1(double pwp, double fc, double sm, double percentage_soil_coverage, double reference_evapotranspiration, int evaporation_reduction_method, double xsa_critical_soil_moisture)
        {
            sm = Math.Max(0.33 * pwp, sm);
            double relative_evaporable_water;
            relative_evaporable_water = Math.Min(1.0, (sm - (0.33 * pwp)) / (fc - (0.33 * pwp)));
            double e_reduction_factor = 0.0;
            double critical_soil_moisture;
            double reducer;
            double xsa;
            if (evaporation_reduction_method == 0)
            {
                critical_soil_moisture = 0.65 * fc;
                if (percentage_soil_coverage > 0.0)
                {
                    reducer = 1.0;
                    if (reference_evapotranspiration > 2.5)
                    {
                        xsa = (0.65 * fc - pwp) * (fc - pwp);
                        reducer = xsa + ((1 - xsa) / 17.5 * (reference_evapotranspiration - 2.5));
                    }
                    else
                    {
                        reducer = xsa_critical_soil_moisture / 2.5 * reference_evapotranspiration;
                    }
                    critical_soil_moisture = fc * reducer;
                }
                if (sm > critical_soil_moisture)
                {
                    e_reduction_factor = 1.0;
                }
                else if ( sm > (0.33 * pwp))
                {
                    e_reduction_factor = relative_evaporable_water;
                }
                else
                {
                    e_reduction_factor = 0.0;
                }
            }
            else
            {
                if (relative_evaporable_water > 0.33)
                {
                    e_reduction_factor = 1.0 - (0.1 * (1.0 - relative_evaporable_water) / (1.0 - 0.33));
                }
                else if ( relative_evaporable_water > 0.22)
                {
                    e_reduction_factor = 0.9 - (0.625 * (0.33 - relative_evaporable_water) / (0.33 - 0.22));
                }
                else if ( relative_evaporable_water > 0.2)
                {
                    e_reduction_factor = 0.275 - (0.225 * (0.22 - relative_evaporable_water) / (0.22 - 0.2));
                }
                else
                {
                    e_reduction_factor = 0.05 - (0.05 * (0.2 - relative_evaporable_water) / 0.2);
                }
            }
            return e_reduction_factor;
        }
    }
}