
using System;
using System.Collections.Generic;
using CRA.ModelLayer.Core;
using System.Reflection;
using CRA.ModelLayer.ParametersManagement;   

namespace SiriusQualityEvapotranspirationComp.DomainClass
                        {
                            public class EvapotranspirationCompExogenous : ICloneable, IDomainClass
                            {
                                private double _external_reference_evapotranspiration;
                                private double _height_nn;
                                private double _max_air_temperature;
                                private double _min_air_temperature;
                                private double _mean_air_temperature;
                                private double _relative_humidity;
                                private double _wind_speed;
                                private double _wind_speed_height;
                                private double _global_radiation;
                                private int _julian_day;
                                private double _latitude;
                                private ParametersIO _parametersIO;

                                public EvapotranspirationCompExogenous()
                                {
                                    _parametersIO = new ParametersIO(this);
                                }

                                public EvapotranspirationCompExogenous(EvapotranspirationCompExogenous toCopy, bool copyAll) // copy constructor 
                                {
                                    if (copyAll)
                                    {
                                                external_reference_evapotranspiration = toCopy.external_reference_evapotranspiration;
                                                height_nn = toCopy.height_nn;
                                                max_air_temperature = toCopy.max_air_temperature;
                                                min_air_temperature = toCopy.min_air_temperature;
                                                mean_air_temperature = toCopy.mean_air_temperature;
                                                relative_humidity = toCopy.relative_humidity;
                                                wind_speed = toCopy.wind_speed;
                                                wind_speed_height = toCopy.wind_speed_height;
                                                global_radiation = toCopy.global_radiation;
                                                julian_day = toCopy.julian_day;
                                                latitude = toCopy.latitude;
                                            }
                                        }

                                        public double external_reference_evapotranspiration
    {
        get { return this._external_reference_evapotranspiration; }
        set { this._external_reference_evapotranspiration= value; } 
    }
                                        public double height_nn
    {
        get { return this._height_nn; }
        set { this._height_nn= value; } 
    }
                                        public double max_air_temperature
    {
        get { return this._max_air_temperature; }
        set { this._max_air_temperature= value; } 
    }
                                        public double min_air_temperature
    {
        get { return this._min_air_temperature; }
        set { this._min_air_temperature= value; } 
    }
                                        public double mean_air_temperature
    {
        get { return this._mean_air_temperature; }
        set { this._mean_air_temperature= value; } 
    }
                                        public double relative_humidity
    {
        get { return this._relative_humidity; }
        set { this._relative_humidity= value; } 
    }
                                        public double wind_speed
    {
        get { return this._wind_speed; }
        set { this._wind_speed= value; } 
    }
                                        public double wind_speed_height
    {
        get { return this._wind_speed_height; }
        set { this._wind_speed_height= value; } 
    }
                                        public double global_radiation
    {
        get { return this._global_radiation; }
        set { this._global_radiation= value; } 
    }
                                        public int julian_day
    {
        get { return this._julian_day; }
        set { this._julian_day= value; } 
    }
                                        public double latitude
    {
        get { return this._latitude; }
        set { this._latitude= value; } 
    }

                                        public string Description
                                        {
                                            get { return "EvapotranspirationCompExogenous of the component";}
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
                                             _external_reference_evapotranspiration = default(double);
                                             _height_nn = default(double);
                                             _max_air_temperature = default(double);
                                             _min_air_temperature = default(double);
                                             _mean_air_temperature = default(double);
                                             _relative_humidity = default(double);
                                             _wind_speed = default(double);
                                             _wind_speed_height = default(double);
                                             _global_radiation = default(double);
                                             _julian_day = default(int);
                                             _latitude = default(double);
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