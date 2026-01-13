
using System;
using System.Collections.Generic;
using CRA.ModelLayer.Core;
using System.Reflection;
using CRA.ModelLayer.ParametersManagement;   

namespace SiriusQualityEvapotranspirationComp.DomainClass
{
    public class EvapotranspirationCompState : ICloneable, IDomainClass
    {
        private double _evaporated_from_surface;
        private double _surface_water_storage;
        private double _snow_depth;
        private int _developmental_stage;
        private double _crop_reference_evapotranspiration;
        private double _reference_evapotranspiration;
        private double _actual_evaporation;
        private double _actual_transpiration;
        private double _kc_factor;
        private double _percentage_soil_coverage;
        private double[] _soil_moisture;
        private double[] _permanent_wilting_point;
        private double[] _field_capacity;
        private double[] _evaporation;
        private double[] _transpiration;
        private double[] _crop_transpiration;
        private double _crop_remaining_evapotranspiration;
        private double _crop_evaporated_from_intercepted;
        private double[] _evapotranspiration;
        private double _actual_evapotranspiration;
        private double _vapor_pressure;
        private ParametersIO _parametersIO;

        public EvapotranspirationCompState()
        {
            _parametersIO = new ParametersIO(this);
        }

        public EvapotranspirationCompState(EvapotranspirationCompState toCopy, bool copyAll) // copy constructor 
        {
            if (copyAll)
            {
                        evaporated_from_surface = toCopy.evaporated_from_surface;
                        surface_water_storage = toCopy.surface_water_storage;
                        snow_depth = toCopy.snow_depth;
                        developmental_stage = toCopy.developmental_stage;
                        crop_reference_evapotranspiration = toCopy.crop_reference_evapotranspiration;
                        reference_evapotranspiration = toCopy.reference_evapotranspiration;
                        actual_evaporation = toCopy.actual_evaporation;
                        actual_transpiration = toCopy.actual_transpiration;
                        kc_factor = toCopy.kc_factor;
                        percentage_soil_coverage = toCopy.percentage_soil_coverage;
                        soil_moisture = new double[toCopy.soil_moisture.Length];
        for (int i = 0; i < toCopy.soil_moisture.Length; i++)
            { soil_moisture[i] = toCopy.soil_moisture[i]; }
    
                        permanent_wilting_point = new double[toCopy.permanent_wilting_point.Length];
        for (int i = 0; i < toCopy.permanent_wilting_point.Length; i++)
            { permanent_wilting_point[i] = toCopy.permanent_wilting_point[i]; }
    
                        field_capacity = new double[toCopy.field_capacity.Length];
        for (int i = 0; i < toCopy.field_capacity.Length; i++)
            { field_capacity[i] = toCopy.field_capacity[i]; }
    
                        evaporation = new double[toCopy.evaporation.Length];
        for (int i = 0; i < toCopy.evaporation.Length; i++)
            { evaporation[i] = toCopy.evaporation[i]; }
    
                        transpiration = new double[toCopy.transpiration.Length];
        for (int i = 0; i < toCopy.transpiration.Length; i++)
            { transpiration[i] = toCopy.transpiration[i]; }
    
                        crop_transpiration = new double[toCopy.crop_transpiration.Length];
        for (int i = 0; i < toCopy.crop_transpiration.Length; i++)
            { crop_transpiration[i] = toCopy.crop_transpiration[i]; }
    
                        crop_remaining_evapotranspiration = toCopy.crop_remaining_evapotranspiration;
                        crop_evaporated_from_intercepted = toCopy.crop_evaporated_from_intercepted;
                        evapotranspiration = new double[toCopy.evapotranspiration.Length];
        for (int i = 0; i < toCopy.evapotranspiration.Length; i++)
            { evapotranspiration[i] = toCopy.evapotranspiration[i]; }
    
                        actual_evapotranspiration = toCopy.actual_evapotranspiration;
                        vapor_pressure = toCopy.vapor_pressure;
                    }
                }

                public double evaporated_from_surface
    {
        get { return this._evaporated_from_surface; }
        set { this._evaporated_from_surface= value; } 
    }
                public double surface_water_storage
    {
        get { return this._surface_water_storage; }
        set { this._surface_water_storage= value; } 
    }
                public double snow_depth
    {
        get { return this._snow_depth; }
        set { this._snow_depth= value; } 
    }
                public int developmental_stage
    {
        get { return this._developmental_stage; }
        set { this._developmental_stage= value; } 
    }
                public double crop_reference_evapotranspiration
    {
        get { return this._crop_reference_evapotranspiration; }
        set { this._crop_reference_evapotranspiration= value; } 
    }
                public double reference_evapotranspiration
    {
        get { return this._reference_evapotranspiration; }
        set { this._reference_evapotranspiration= value; } 
    }
                public double actual_evaporation
    {
        get { return this._actual_evaporation; }
        set { this._actual_evaporation= value; } 
    }
                public double actual_transpiration
    {
        get { return this._actual_transpiration; }
        set { this._actual_transpiration= value; } 
    }
                public double kc_factor
    {
        get { return this._kc_factor; }
        set { this._kc_factor= value; } 
    }
                public double percentage_soil_coverage
    {
        get { return this._percentage_soil_coverage; }
        set { this._percentage_soil_coverage= value; } 
    }
                public double[] soil_moisture
    {
        get { return this._soil_moisture; }
        set { this._soil_moisture= value; } 
    }
                public double[] permanent_wilting_point
    {
        get { return this._permanent_wilting_point; }
        set { this._permanent_wilting_point= value; } 
    }
                public double[] field_capacity
    {
        get { return this._field_capacity; }
        set { this._field_capacity= value; } 
    }
                public double[] evaporation
    {
        get { return this._evaporation; }
        set { this._evaporation= value; } 
    }
                public double[] transpiration
    {
        get { return this._transpiration; }
        set { this._transpiration= value; } 
    }
                public double[] crop_transpiration
    {
        get { return this._crop_transpiration; }
        set { this._crop_transpiration= value; } 
    }
                public double crop_remaining_evapotranspiration
    {
        get { return this._crop_remaining_evapotranspiration; }
        set { this._crop_remaining_evapotranspiration= value; } 
    }
                public double crop_evaporated_from_intercepted
    {
        get { return this._crop_evaporated_from_intercepted; }
        set { this._crop_evaporated_from_intercepted= value; } 
    }
                public double[] evapotranspiration
    {
        get { return this._evapotranspiration; }
        set { this._evapotranspiration= value; } 
    }
                public double actual_evapotranspiration
    {
        get { return this._actual_evapotranspiration; }
        set { this._actual_evapotranspiration= value; } 
    }
                public double vapor_pressure
    {
        get { return this._vapor_pressure; }
        set { this._vapor_pressure= value; } 
    }

                public string Description
                {
                    get { return "EvapotranspirationCompState of the component";}
                }

                public string URL
                {
                    get { return "http://" ;}
                }

                public virtual IDictionary<string, PropertyInfo> PropertiesDescription
                {
                    get { return _parametersIO.GetCachedProperties(typeof(IDomainClass));}
                }

                public virtual Boolean ClearValues()
                {
                     _evaporated_from_surface = default(double);
                     _surface_water_storage = default(double);
                     _snow_depth = default(double);
                     _developmental_stage = default(int);
                     _crop_reference_evapotranspiration = default(double);
                     _reference_evapotranspiration = default(double);
                     _actual_evaporation = default(double);
                     _actual_transpiration = default(double);
                     _kc_factor = default(double);
                     _percentage_soil_coverage = default(double);
                     _soil_moisture = new double[no_of_soil_layers];
                     _permanent_wilting_point = new double[no_of_soil_layers];
                     _field_capacity = new double[no_of_soil_layers];
                     _evaporation = new double[no_of_soil_layers];
                     _transpiration = new double[no_of_soil_layers];
                     _crop_transpiration = new double[no_of_soil_layers];
                     _crop_remaining_evapotranspiration = default(double);
                     _crop_evaporated_from_intercepted = default(double);
                     _evapotranspiration = new double[no_of_soil_layers];
                     _actual_evapotranspiration = default(double);
                     _vapor_pressure = default(double);
                    return true;
                }

                public virtual Object Clone()
                {
                    IDomainClass myclass = (IDomainClass) this.MemberwiseClone();
                    _parametersIO.PopulateClonedCopy(myclass);
                    return myclass;
                }
            }
        }