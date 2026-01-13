
using System;
using System.Collections.Generic;
using CRA.ModelLayer.Core;
using System.Reflection;
using CRA.ModelLayer.ParametersManagement;   

namespace SiriusQualityEvapotranspirationComp.DomainClass
                                {
                                    public class EvapotranspirationCompStateVarInfo : IVarInfoClass
                                    {
                                        static VarInfo _evaporated_from_surface = new VarInfo();
                                        static VarInfo _surface_water_storage = new VarInfo();
                                        static VarInfo _snow_depth = new VarInfo();
                                        static VarInfo _developmental_stage = new VarInfo();
                                        static VarInfo _crop_reference_evapotranspiration = new VarInfo();
                                        static VarInfo _reference_evapotranspiration = new VarInfo();
                                        static VarInfo _actual_evaporation = new VarInfo();
                                        static VarInfo _actual_transpiration = new VarInfo();
                                        static VarInfo _kc_factor = new VarInfo();
                                        static VarInfo _percentage_soil_coverage = new VarInfo();
                                        static VarInfo _soil_moisture = new VarInfo();
                                        static VarInfo _permanent_wilting_point = new VarInfo();
                                        static VarInfo _field_capacity = new VarInfo();
                                        static VarInfo _evaporation = new VarInfo();
                                        static VarInfo _transpiration = new VarInfo();
                                        static VarInfo _crop_transpiration = new VarInfo();
                                        static VarInfo _crop_remaining_evapotranspiration = new VarInfo();
                                        static VarInfo _crop_evaporated_from_intercepted = new VarInfo();
                                        static VarInfo _evapotranspiration = new VarInfo();
                                        static VarInfo _actual_evapotranspiration = new VarInfo();
                                        static VarInfo _vapor_pressure = new VarInfo();

                                        static EvapotranspirationCompStateVarInfo()
                                        {
                                            EvapotranspirationCompStateVarInfo.DescribeVariables();
                                        }

                                        public virtual string Description
                                        {
                                            get { return "EvapotranspirationCompState Domain class of the component";}
                                        }

                                        public string URL
                                        {
                                            get { return "http://" ;}
                                        }

                                        public string DomainClassOfReference
                                        {
                                            get { return "EvapotranspirationCompState";}
                                        }

                                        public static  VarInfo evaporated_from_surface
                                        {
                                            get { return _evaporated_from_surface;}
                                        }

                                        public static  VarInfo surface_water_storage
                                        {
                                            get { return _surface_water_storage;}
                                        }

                                        public static  VarInfo snow_depth
                                        {
                                            get { return _snow_depth;}
                                        }

                                        public static  VarInfo developmental_stage
                                        {
                                            get { return _developmental_stage;}
                                        }

                                        public static  VarInfo crop_reference_evapotranspiration
                                        {
                                            get { return _crop_reference_evapotranspiration;}
                                        }

                                        public static  VarInfo reference_evapotranspiration
                                        {
                                            get { return _reference_evapotranspiration;}
                                        }

                                        public static  VarInfo actual_evaporation
                                        {
                                            get { return _actual_evaporation;}
                                        }

                                        public static  VarInfo actual_transpiration
                                        {
                                            get { return _actual_transpiration;}
                                        }

                                        public static  VarInfo kc_factor
                                        {
                                            get { return _kc_factor;}
                                        }

                                        public static  VarInfo percentage_soil_coverage
                                        {
                                            get { return _percentage_soil_coverage;}
                                        }

                                        public static  VarInfo soil_moisture
                                        {
                                            get { return _soil_moisture;}
                                        }

                                        public static  VarInfo permanent_wilting_point
                                        {
                                            get { return _permanent_wilting_point;}
                                        }

                                        public static  VarInfo field_capacity
                                        {
                                            get { return _field_capacity;}
                                        }

                                        public static  VarInfo evaporation
                                        {
                                            get { return _evaporation;}
                                        }

                                        public static  VarInfo transpiration
                                        {
                                            get { return _transpiration;}
                                        }

                                        public static  VarInfo crop_transpiration
                                        {
                                            get { return _crop_transpiration;}
                                        }

                                        public static  VarInfo crop_remaining_evapotranspiration
                                        {
                                            get { return _crop_remaining_evapotranspiration;}
                                        }

                                        public static  VarInfo crop_evaporated_from_intercepted
                                        {
                                            get { return _crop_evaporated_from_intercepted;}
                                        }

                                        public static  VarInfo evapotranspiration
                                        {
                                            get { return _evapotranspiration;}
                                        }

                                        public static  VarInfo actual_evapotranspiration
                                        {
                                            get { return _actual_evapotranspiration;}
                                        }

                                        public static  VarInfo vapor_pressure
                                        {
                                            get { return _vapor_pressure;}
                                        }

                                        static void DescribeVariables()
                                        {
                                            _evaporated_from_surface.Name = "evaporated_from_surface";
                                            _evaporated_from_surface.Description = "evaporated_from_surface";
                                            _evaporated_from_surface.MaxValue = -1D;
                                            _evaporated_from_surface.MinValue = 0;
                                            _evaporated_from_surface.DefaultValue = 0;
                                            _evaporated_from_surface.Units = "mm";
                                            _evaporated_from_surface.ValueType = VarInfoValueTypes.GetInstanceForName("Double");

                                            _surface_water_storage.Name = "surface_water_storage";
                                            _surface_water_storage.Description = "Simulates a virtual layer that contains the surface water";
                                            _surface_water_storage.MaxValue = -1D;
                                            _surface_water_storage.MinValue = 0;
                                            _surface_water_storage.DefaultValue = 0;
                                            _surface_water_storage.Units = "mm";
                                            _surface_water_storage.ValueType = VarInfoValueTypes.GetInstanceForName("Double");

                                            _snow_depth.Name = "snow_depth";
                                            _snow_depth.Description = "depth of snow layer";
                                            _snow_depth.MaxValue = -1D;
                                            _snow_depth.MinValue = 0;
                                            _snow_depth.DefaultValue = 0;
                                            _snow_depth.Units = "mm";
                                            _snow_depth.ValueType = VarInfoValueTypes.GetInstanceForName("Double");

                                            _developmental_stage.Name = "developmental_stage";
                                            _developmental_stage.Description = "MONICA crop developmental stage";
                                            _developmental_stage.MaxValue = 6;
                                            _developmental_stage.MinValue = 0;
                                            _developmental_stage.DefaultValue = 0;
                                            _developmental_stage.Units = "dimensionless";
                                            _developmental_stage.ValueType = VarInfoValueTypes.GetInstanceForName("Integer");

                                            _crop_reference_evapotranspiration.Name = "crop_reference_evapotranspiration";
                                            _crop_reference_evapotranspiration.Description = "the crop specific ET0, if no external ET0 and crop is planted";
                                            _crop_reference_evapotranspiration.MaxValue = -1D;
                                            _crop_reference_evapotranspiration.MinValue = 0;
                                            _crop_reference_evapotranspiration.DefaultValue = -1;
                                            _crop_reference_evapotranspiration.Units = "mm";
                                            _crop_reference_evapotranspiration.ValueType = VarInfoValueTypes.GetInstanceForName("Double");

                                            _reference_evapotranspiration.Name = "reference_evapotranspiration";
                                            _reference_evapotranspiration.Description = "reference evapotranspiration (ET0)";
                                            _reference_evapotranspiration.MaxValue = -1D;
                                            _reference_evapotranspiration.MinValue = 0;
                                            _reference_evapotranspiration.DefaultValue = 0;
                                            _reference_evapotranspiration.Units = "mm";
                                            _reference_evapotranspiration.ValueType = VarInfoValueTypes.GetInstanceForName("Double");

                                            _actual_evaporation.Name = "actual_evaporation";
                                            _actual_evaporation.Description = "actual evaporation";
                                            _actual_evaporation.MaxValue = -1D;
                                            _actual_evaporation.MinValue = 0;
                                            _actual_evaporation.DefaultValue = 0;
                                            _actual_evaporation.Units = "mm";
                                            _actual_evaporation.ValueType = VarInfoValueTypes.GetInstanceForName("Double");

                                            _actual_transpiration.Name = "actual_transpiration";
                                            _actual_transpiration.Description = "actual transpiration";
                                            _actual_transpiration.MaxValue = -1D;
                                            _actual_transpiration.MinValue = 0;
                                            _actual_transpiration.DefaultValue = 0;
                                            _actual_transpiration.Units = "mm";
                                            _actual_transpiration.ValueType = VarInfoValueTypes.GetInstanceForName("Double");

                                            _kc_factor.Name = "kc_factor";
                                            _kc_factor.Description = "crop coefficient ETc/ET0";
                                            _kc_factor.MaxValue = -1D;
                                            _kc_factor.MinValue = 0;
                                            _kc_factor.DefaultValue = 0.75;
                                            _kc_factor.Units = "dimensionless";
                                            _kc_factor.ValueType = VarInfoValueTypes.GetInstanceForName("Double");

                                            _percentage_soil_coverage.Name = "percentage_soil_coverage";
                                            _percentage_soil_coverage.Description = "fraction of soil covered by crop";
                                            _percentage_soil_coverage.MaxValue = 1;
                                            _percentage_soil_coverage.MinValue = 0;
                                            _percentage_soil_coverage.DefaultValue = 0;
                                            _percentage_soil_coverage.Units = "m2/m2";
                                            _percentage_soil_coverage.ValueType = VarInfoValueTypes.GetInstanceForName("Double");

                                            _soil_moisture.Name = "soil_moisture";
                                            _soil_moisture.Description = "soil moisture array";
                                            _soil_moisture.MaxValue = -1D;
                                            _soil_moisture.MinValue = -1D;
                                            _soil_moisture.DefaultValue = -1D;
                                            _soil_moisture.Units = "m3/m3";
                                            _soil_moisture.ValueType = VarInfoValueTypes.GetInstanceForName("ArrayDouble");

                                            _permanent_wilting_point.Name = "permanent_wilting_point";
                                            _permanent_wilting_point.Description = "permanent wilting point array";
                                            _permanent_wilting_point.MaxValue = -1D;
                                            _permanent_wilting_point.MinValue = -1D;
                                            _permanent_wilting_point.DefaultValue = -1D;
                                            _permanent_wilting_point.Units = "m3/m3";
                                            _permanent_wilting_point.ValueType = VarInfoValueTypes.GetInstanceForName("ArrayDouble");

                                            _field_capacity.Name = "field_capacity";
                                            _field_capacity.Description = "field capacity array";
                                            _field_capacity.MaxValue = -1D;
                                            _field_capacity.MinValue = -1D;
                                            _field_capacity.DefaultValue = -1D;
                                            _field_capacity.Units = "m3/m3";
                                            _field_capacity.ValueType = VarInfoValueTypes.GetInstanceForName("ArrayDouble");

                                            _evaporation.Name = "evaporation";
                                            _evaporation.Description = "evaporation array";
                                            _evaporation.MaxValue = -1D;
                                            _evaporation.MinValue = -1D;
                                            _evaporation.DefaultValue = -1D;
                                            _evaporation.Units = "mm";
                                            _evaporation.ValueType = VarInfoValueTypes.GetInstanceForName("ArrayDouble");

                                            _transpiration.Name = "transpiration";
                                            _transpiration.Description = "transpiration array";
                                            _transpiration.MaxValue = -1D;
                                            _transpiration.MinValue = -1D;
                                            _transpiration.DefaultValue = -1D;
                                            _transpiration.Units = "mm";
                                            _transpiration.ValueType = VarInfoValueTypes.GetInstanceForName("ArrayDouble");

                                            _crop_transpiration.Name = "crop_transpiration";
                                            _crop_transpiration.Description = "crop transpiration array";
                                            _crop_transpiration.MaxValue = -1D;
                                            _crop_transpiration.MinValue = -1D;
                                            _crop_transpiration.DefaultValue = -1D;
                                            _crop_transpiration.Units = "mm";
                                            _crop_transpiration.ValueType = VarInfoValueTypes.GetInstanceForName("ArrayDouble");

                                            _crop_remaining_evapotranspiration.Name = "crop_remaining_evapotranspiration";
                                            _crop_remaining_evapotranspiration.Description = "crop remaining evapotranspiration";
                                            _crop_remaining_evapotranspiration.MaxValue = -1D;
                                            _crop_remaining_evapotranspiration.MinValue = -1D;
                                            _crop_remaining_evapotranspiration.DefaultValue = -1D;
                                            _crop_remaining_evapotranspiration.Units = "mm";
                                            _crop_remaining_evapotranspiration.ValueType = VarInfoValueTypes.GetInstanceForName("Double");

                                            _crop_evaporated_from_intercepted.Name = "crop_evaporated_from_intercepted";
                                            _crop_evaporated_from_intercepted.Description = "crop evaporated water from intercepted water";
                                            _crop_evaporated_from_intercepted.MaxValue = -1D;
                                            _crop_evaporated_from_intercepted.MinValue = -1D;
                                            _crop_evaporated_from_intercepted.DefaultValue = -1D;
                                            _crop_evaporated_from_intercepted.Units = "mm";
                                            _crop_evaporated_from_intercepted.ValueType = VarInfoValueTypes.GetInstanceForName("Double");

                                            _evapotranspiration.Name = "evapotranspiration";
                                            _evapotranspiration.Description = "evapotranspiration array";
                                            _evapotranspiration.MaxValue = -1D;
                                            _evapotranspiration.MinValue = -1D;
                                            _evapotranspiration.DefaultValue = -1D;
                                            _evapotranspiration.Units = "mm";
                                            _evapotranspiration.ValueType = VarInfoValueTypes.GetInstanceForName("ArrayDouble");

                                            _actual_evapotranspiration.Name = "actual_evapotranspiration";
                                            _actual_evapotranspiration.Description = "actual evapotranspiration";
                                            _actual_evapotranspiration.MaxValue = -1D;
                                            _actual_evapotranspiration.MinValue = 0;
                                            _actual_evapotranspiration.DefaultValue = 0;
                                            _actual_evapotranspiration.Units = "mm";
                                            _actual_evapotranspiration.ValueType = VarInfoValueTypes.GetInstanceForName("Double");

                                            _vapor_pressure.Name = "vapor_pressure";
                                            _vapor_pressure.Description = "vapor pressure";
                                            _vapor_pressure.MaxValue = -1D;
                                            _vapor_pressure.MinValue = 0;
                                            _vapor_pressure.DefaultValue = 0;
                                            _vapor_pressure.Units = "kPa";
                                            _vapor_pressure.ValueType = VarInfoValueTypes.GetInstanceForName("Double");

                                        }

                                    }
                                }