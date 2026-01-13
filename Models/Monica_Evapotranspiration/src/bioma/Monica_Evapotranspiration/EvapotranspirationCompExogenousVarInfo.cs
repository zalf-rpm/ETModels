
using System;
using System.Collections.Generic;
using CRA.ModelLayer.Core;
using System.Reflection;
using CRA.ModelLayer.ParametersManagement;   

namespace EvapotranspirationComp.DomainClass
                                {
                                    public class EvapotranspirationCompExogenousVarInfo : IVarInfoClass
                                    {
                                        static VarInfo _external_reference_evapotranspiration = new VarInfo();
                                        static VarInfo _height_nn = new VarInfo();
                                        static VarInfo _max_air_temperature = new VarInfo();
                                        static VarInfo _min_air_temperature = new VarInfo();
                                        static VarInfo _mean_air_temperature = new VarInfo();
                                        static VarInfo _relative_humidity = new VarInfo();
                                        static VarInfo _wind_speed = new VarInfo();
                                        static VarInfo _wind_speed_height = new VarInfo();
                                        static VarInfo _global_radiation = new VarInfo();
                                        static VarInfo _julian_day = new VarInfo();
                                        static VarInfo _latitude = new VarInfo();

                                        static EvapotranspirationCompExogenousVarInfo()
                                        {
                                            EvapotranspirationCompExogenousVarInfo.DescribeVariables();
                                        }

                                        public virtual string Description
                                        {
                                            get { return "EvapotranspirationCompExogenous Domain class of the component";}
                                        }

                                        public string URL
                                        {
                                            get { return "http://" ;}
                                        }

                                        public string DomainClassOfReference
                                        {
                                            get { return "EvapotranspirationCompExogenous";}
                                        }

                                        public static  VarInfo external_reference_evapotranspiration
                                        {
                                            get { return _external_reference_evapotranspiration;}
                                        }

                                        public static  VarInfo height_nn
                                        {
                                            get { return _height_nn;}
                                        }

                                        public static  VarInfo max_air_temperature
                                        {
                                            get { return _max_air_temperature;}
                                        }

                                        public static  VarInfo min_air_temperature
                                        {
                                            get { return _min_air_temperature;}
                                        }

                                        public static  VarInfo mean_air_temperature
                                        {
                                            get { return _mean_air_temperature;}
                                        }

                                        public static  VarInfo relative_humidity
                                        {
                                            get { return _relative_humidity;}
                                        }

                                        public static  VarInfo wind_speed
                                        {
                                            get { return _wind_speed;}
                                        }

                                        public static  VarInfo wind_speed_height
                                        {
                                            get { return _wind_speed_height;}
                                        }

                                        public static  VarInfo global_radiation
                                        {
                                            get { return _global_radiation;}
                                        }

                                        public static  VarInfo julian_day
                                        {
                                            get { return _julian_day;}
                                        }

                                        public static  VarInfo latitude
                                        {
                                            get { return _latitude;}
                                        }

                                        static void DescribeVariables()
                                        {
                                            _external_reference_evapotranspiration.Name = "external_reference_evapotranspiration";
                                            _external_reference_evapotranspiration.Description = "externally supplied ET0";
                                            _external_reference_evapotranspiration.MaxValue = -1D;
                                            _external_reference_evapotranspiration.MinValue = 0;
                                            _external_reference_evapotranspiration.DefaultValue = -1;
                                            _external_reference_evapotranspiration.Units = "mm";
                                            _external_reference_evapotranspiration.ValueType = VarInfoValueTypes.GetInstanceForName("Double");

                                            _height_nn.Name = "height_nn";
                                            _height_nn.Description = "height above sea leavel";
                                            _height_nn.MaxValue = 9999;
                                            _height_nn.MinValue = -9999;
                                            _height_nn.DefaultValue = 0;
                                            _height_nn.Units = "m";
                                            _height_nn.ValueType = VarInfoValueTypes.GetInstanceForName("Double");

                                            _max_air_temperature.Name = "max_air_temperature";
                                            _max_air_temperature.Description = "daily maximum air temperature";
                                            _max_air_temperature.MaxValue = 100;
                                            _max_air_temperature.MinValue = -100;
                                            _max_air_temperature.DefaultValue = 0;
                                            _max_air_temperature.Units = "°C";
                                            _max_air_temperature.ValueType = VarInfoValueTypes.GetInstanceForName("Double");

                                            _min_air_temperature.Name = "min_air_temperature";
                                            _min_air_temperature.Description = "daily minimum air temperature";
                                            _min_air_temperature.MaxValue = 100;
                                            _min_air_temperature.MinValue = -100;
                                            _min_air_temperature.DefaultValue = 0;
                                            _min_air_temperature.Units = "°C";
                                            _min_air_temperature.ValueType = VarInfoValueTypes.GetInstanceForName("Double");

                                            _mean_air_temperature.Name = "mean_air_temperature";
                                            _mean_air_temperature.Description = "daily average air temperature";
                                            _mean_air_temperature.MaxValue = 100;
                                            _mean_air_temperature.MinValue = -100;
                                            _mean_air_temperature.DefaultValue = 0;
                                            _mean_air_temperature.Units = "°C";
                                            _mean_air_temperature.ValueType = VarInfoValueTypes.GetInstanceForName("Double");

                                            _relative_humidity.Name = "relative_humidity";
                                            _relative_humidity.Description = "relative humidity";
                                            _relative_humidity.MaxValue = 1;
                                            _relative_humidity.MinValue = 0;
                                            _relative_humidity.DefaultValue = 0;
                                            _relative_humidity.Units = "fraction";
                                            _relative_humidity.ValueType = VarInfoValueTypes.GetInstanceForName("Double");

                                            _wind_speed.Name = "wind_speed";
                                            _wind_speed.Description = "wind speed measured at wind speed height";
                                            _wind_speed.MaxValue = 9999;
                                            _wind_speed.MinValue = 0;
                                            _wind_speed.DefaultValue = 0;
                                            _wind_speed.Units = "m/s";
                                            _wind_speed.ValueType = VarInfoValueTypes.GetInstanceForName("Double");

                                            _wind_speed_height.Name = "wind_speed_height";
                                            _wind_speed_height.Description = "height at which the wind speed has been measured";
                                            _wind_speed_height.MaxValue = 9999;
                                            _wind_speed_height.MinValue = -9999;
                                            _wind_speed_height.DefaultValue = 2;
                                            _wind_speed_height.Units = "m";
                                            _wind_speed_height.ValueType = VarInfoValueTypes.GetInstanceForName("Double");

                                            _global_radiation.Name = "global_radiation";
                                            _global_radiation.Description = "global radiation";
                                            _global_radiation.MaxValue = 50;
                                            _global_radiation.MinValue = 0;
                                            _global_radiation.DefaultValue = 0;
                                            _global_radiation.Units = "MJ/m2";
                                            _global_radiation.ValueType = VarInfoValueTypes.GetInstanceForName("Double");

                                            _julian_day.Name = "julian_day";
                                            _julian_day.Description = "day of year";
                                            _julian_day.MaxValue = 366;
                                            _julian_day.MinValue = 1;
                                            _julian_day.DefaultValue = 1;
                                            _julian_day.Units = "day";
                                            _julian_day.ValueType = VarInfoValueTypes.GetInstanceForName("Integer");

                                            _latitude.Name = "latitude";
                                            _latitude.Description = "latitude";
                                            _latitude.MaxValue = 90;
                                            _latitude.MinValue = -90;
                                            _latitude.DefaultValue = 0;
                                            _latitude.Units = "degree";
                                            _latitude.ValueType = VarInfoValueTypes.GetInstanceForName("Double");

                                        }

                                    }
                                }