using System;
using System.Collections.Generic;
using System.Linq;    
using Models.Core;   
namespace Models.Crop2ML;

/// <summary>
///- Name: Evapotranspiration -Version: 1, -Time step: 1
///- Description:
///            * Title: Model of evapotranspiration
///            * Authors: Claas Nendel (transcription into Crop2ML by Michael Berg-Mohnicke)
///            * Reference: None
///            * Institution: ZALF e.V.
///            * ExtendedDescription: None
///            * ShortDescription: Calculates the evapotranspiration
///- inputs:
///            * name: evaporation_zeta
///                          ** description : shape factor
///                          ** inputtype : parameter
///                          ** parametercategory : constant
///                          ** datatype : DOUBLE
///                          ** max : 40
///                          ** min : 0
///                          ** default : 40
///                          ** unit : dimensionless
///            * name: maximum_evaporation_impact_depth
///                          ** description : maximumEvaporationImpactDepth
///                          ** inputtype : parameter
///                          ** parametercategory : constant
///                          ** datatype : DOUBLE
///                          ** max : 
///                          ** min : 0
///                          ** default : 5
///                          ** unit : dm
///            * name: no_of_soil_layers
///                          ** description : number of soil layers
///                          ** inputtype : parameter
///                          ** parametercategory : constant
///                          ** datatype : INT
///                          ** max : 
///                          ** min : 0
///                          ** default : 20
///                          ** unit : dimensionless
///            * name: layer_thickness
///                          ** description : layer thickness array
///                          ** inputtype : parameter
///                          ** parametercategory : constant
///                          ** datatype : DOUBLEARRAY
///                          ** len : no_of_soil_layers
///                          ** max : 
///                          ** min : 
///                          ** default : 
///                          ** unit : m
///            * name: reference_albedo
///                          ** description : reference albedo
///                          ** inputtype : parameter
///                          ** parametercategory : constant
///                          ** datatype : DOUBLE
///                          ** max : 1
///                          ** min : 0
///                          ** default : 0
///                          ** unit : dimensionless
///            * name: stomata_resistance
///                          ** description : stomata resistance
///                          ** inputtype : parameter
///                          ** parametercategory : constant
///                          ** datatype : DOUBLE
///                          ** max : 10000
///                          ** min : 0
///                          ** default : 100
///                          ** unit : s/m
///            * name: evaporation_reduction_method
///                          ** description : THESEUS (0) or HERMES (1) evaporation reduction method
///                          ** inputtype : parameter
///                          ** parametercategory : constant
///                          ** datatype : INT
///                          ** max : 1
///                          ** min : 0
///                          ** default : 1
///                          ** unit : dimensionless
///            * name: xsa_critical_soil_moisture
///                          ** description : XSACriticalSoilMoisture
///                          ** inputtype : parameter
///                          ** parametercategory : constant
///                          ** datatype : DOUBLE
///                          ** max : 1.5
///                          ** min : 0
///                          ** default : 0.1
///                          ** unit : m3/m3
///            * name: external_reference_evapotranspiration
///                          ** description : externally supplied ET0
///                          ** inputtype : variable
///                          ** variablecategory : exogenous
///                          ** datatype : DOUBLE
///                          ** max : 
///                          ** min : 0
///                          ** default : -1
///                          ** unit : mm
///            * name: height_nn
///                          ** description : height above sea leavel
///                          ** inputtype : variable
///                          ** variablecategory : exogenous
///                          ** datatype : DOUBLE
///                          ** max : 9999
///                          ** min : -9999
///                          ** default : 0
///                          ** unit : m
///            * name: max_air_temperature
///                          ** description : daily maximum air temperature
///                          ** inputtype : variable
///                          ** variablecategory : exogenous
///                          ** datatype : DOUBLE
///                          ** max : 100
///                          ** min : -100
///                          ** default : 0
///                          ** unit : °C
///            * name: min_air_temperature
///                          ** description : daily minimum air temperature
///                          ** inputtype : variable
///                          ** variablecategory : exogenous
///                          ** datatype : DOUBLE
///                          ** max : 100
///                          ** min : -100
///                          ** default : 0
///                          ** unit : °C
///            * name: mean_air_temperature
///                          ** description : daily average air temperature
///                          ** inputtype : variable
///                          ** variablecategory : exogenous
///                          ** datatype : DOUBLE
///                          ** max : 100
///                          ** min : -100
///                          ** default : 0
///                          ** unit : °C
///            * name: relative_humidity
///                          ** description : relative humidity
///                          ** inputtype : variable
///                          ** variablecategory : exogenous
///                          ** datatype : DOUBLE
///                          ** max : 1
///                          ** min : 0
///                          ** default : 0
///                          ** unit : fraction
///            * name: wind_speed
///                          ** description : wind speed measured at wind speed height
///                          ** inputtype : variable
///                          ** variablecategory : exogenous
///                          ** datatype : DOUBLE
///                          ** max : 9999
///                          ** min : 0
///                          ** default : 0
///                          ** unit : m/s
///            * name: wind_speed_height
///                          ** description : height at which the wind speed has been measured
///                          ** inputtype : variable
///                          ** variablecategory : exogenous
///                          ** datatype : DOUBLE
///                          ** max : 9999
///                          ** min : -9999
///                          ** default : 2
///                          ** unit : m
///            * name: global_radiation
///                          ** description : global radiation
///                          ** inputtype : variable
///                          ** variablecategory : exogenous
///                          ** datatype : DOUBLE
///                          ** max : 50
///                          ** min : 0
///                          ** default : 0
///                          ** unit : MJ/m2
///            * name: julian_day
///                          ** description : day of year
///                          ** inputtype : variable
///                          ** variablecategory : exogenous
///                          ** datatype : INT
///                          ** max : 366
///                          ** min : 1
///                          ** default : 1
///                          ** unit : day
///            * name: latitude
///                          ** description : latitude
///                          ** inputtype : variable
///                          ** variablecategory : exogenous
///                          ** datatype : DOUBLE
///                          ** max : 90
///                          ** min : -90
///                          ** default : 0
///                          ** unit : degree
///            * name: evaporated_from_surface
///                          ** description : evaporated_from_surface
///                          ** inputtype : variable
///                          ** variablecategory : state
///                          ** datatype : DOUBLE
///                          ** max : 
///                          ** min : 0
///                          ** default : 0
///                          ** unit : mm
///            * name: snow_depth
///                          ** description : depth of snow layer
///                          ** inputtype : variable
///                          ** variablecategory : state
///                          ** datatype : DOUBLE
///                          ** max : 
///                          ** min : 0
///                          ** default : 0
///                          ** unit : mm
///            * name: developmental_stage
///                          ** description : MONICA crop developmental stage
///                          ** inputtype : variable
///                          ** variablecategory : state
///                          ** datatype : INT
///                          ** max : 6
///                          ** min : 0
///                          ** default : 0
///                          ** unit : dimensionless
///            * name: crop_reference_evapotranspiration
///                          ** description : the crop specific ET0, if no external ET0 and crop is planted
///                          ** inputtype : variable
///                          ** variablecategory : state
///                          ** datatype : DOUBLE
///                          ** max : 
///                          ** min : 0
///                          ** default : -1
///                          ** unit : mm
///            * name: reference_evapotranspiration
///                          ** description : reference evapotranspiration (ET0)
///                          ** inputtype : variable
///                          ** variablecategory : state
///                          ** datatype : DOUBLE
///                          ** max : 
///                          ** min : 0
///                          ** default : 0
///                          ** unit : mm
///            * name: actual_evaporation
///                          ** description : actual evaporation
///                          ** inputtype : variable
///                          ** variablecategory : state
///                          ** datatype : DOUBLE
///                          ** max : 
///                          ** min : 0
///                          ** default : 0
///                          ** unit : mm
///            * name: actual_transpiration
///                          ** description : actual transpiration
///                          ** inputtype : variable
///                          ** variablecategory : state
///                          ** datatype : DOUBLE
///                          ** max : 
///                          ** min : 0
///                          ** default : 0
///                          ** unit : mm
///            * name: surface_water_storage
///                          ** description : Simulates a virtual layer that contains the surface water
///                          ** inputtype : variable
///                          ** variablecategory : state
///                          ** datatype : DOUBLE
///                          ** max : 
///                          ** min : 0
///                          ** default : 0
///                          ** unit : mm
///            * name: kc_factor
///                          ** description : crop coefficient ETc/ET0
///                          ** inputtype : variable
///                          ** variablecategory : state
///                          ** datatype : DOUBLE
///                          ** max : 
///                          ** min : 0
///                          ** default : 0.75
///                          ** unit : dimensionless
///            * name: percentage_soil_coverage
///                          ** description : fraction of soil covered by crop
///                          ** inputtype : variable
///                          ** variablecategory : state
///                          ** datatype : DOUBLE
///                          ** max : 1
///                          ** min : 0
///                          ** default : 0
///                          ** unit : m2/m2
///            * name: soil_moisture
///                          ** description : soil moisture array
///                          ** inputtype : variable
///                          ** variablecategory : state
///                          ** datatype : DOUBLEARRAY
///                          ** len : no_of_soil_layers
///                          ** max : 
///                          ** min : 
///                          ** default : 
///                          ** unit : m3/m3
///            * name: permanent_wilting_point
///                          ** description : permanent wilting point array
///                          ** inputtype : variable
///                          ** variablecategory : state
///                          ** datatype : DOUBLEARRAY
///                          ** len : no_of_soil_layers
///                          ** max : 2
///                          ** min : 0
///                          ** default : 0
///                          ** unit : m3/m3
///            * name: field_capacity
///                          ** description : field capacity array
///                          ** inputtype : variable
///                          ** variablecategory : state
///                          ** datatype : DOUBLEARRAY
///                          ** len : no_of_soil_layers
///                          ** max : 1
///                          ** min : 0
///                          ** default : 0
///                          ** unit : m3/m3
///            * name: evaporation
///                          ** description : evaporation array
///                          ** inputtype : variable
///                          ** variablecategory : state
///                          ** datatype : DOUBLEARRAY
///                          ** len : no_of_soil_layers
///                          ** max : 1
///                          ** min : 0
///                          ** default : 0
///                          ** unit : mm
///            * name: transpiration
///                          ** description : transpiration array
///                          ** inputtype : variable
///                          ** variablecategory : state
///                          ** datatype : DOUBLEARRAY
///                          ** len : no_of_soil_layers
///                          ** max : 
///                          ** min : 
///                          ** default : 
///                          ** unit : mm
///            * name: crop_transpiration
///                          ** description : crop transpiration array
///                          ** inputtype : variable
///                          ** variablecategory : state
///                          ** datatype : DOUBLEARRAY
///                          ** len : no_of_soil_layers
///                          ** max : 
///                          ** min : 
///                          ** default : 
///                          ** unit : mm
///            * name: crop_remaining_evapotranspiration
///                          ** description : crop remaining evapotranspiration
///                          ** inputtype : variable
///                          ** variablecategory : state
///                          ** datatype : DOUBLE
///                          ** max : 
///                          ** min : 
///                          ** default : 
///                          ** unit : mm
///            * name: crop_evaporated_from_intercepted
///                          ** description : crop evaporated water from intercepted water
///                          ** inputtype : variable
///                          ** variablecategory : state
///                          ** datatype : DOUBLE
///                          ** max : 
///                          ** min : 
///                          ** default : 
///                          ** unit : mm
///            * name: evapotranspiration
///                          ** description : evapotranspiration array
///                          ** inputtype : variable
///                          ** variablecategory : state
///                          ** datatype : DOUBLEARRAY
///                          ** len : no_of_soil_layers
///                          ** max : 
///                          ** min : 
///                          ** default : 
///                          ** unit : mm
///            * name: actual_evapotranspiration
///                          ** description : actual evapotranspiration
///                          ** inputtype : variable
///                          ** variablecategory : state
///                          ** datatype : DOUBLE
///                          ** max : 
///                          ** min : 0
///                          ** default : 0
///                          ** unit : mm
///            * name: vapor_pressure
///                          ** description : vapor pressure
///                          ** inputtype : variable
///                          ** variablecategory : state
///                          ** datatype : DOUBLE
///                          ** max : 
///                          ** min : 0
///                          ** default : 0
///                          ** unit : kPa
///- outputs:
///            * name: evaporated_from_surface
///                          ** description : 
///                          ** variablecategory : state
///                          ** datatype : DOUBLE
///                          ** max : 
///                          ** min : 0
///                          ** unit : mm
///            * name: actual_evapotranspiration
///                          ** description : actual evapotranspiration
///                          ** variablecategory : state
///                          ** datatype : DOUBLE
///                          ** max : 200
///                          ** min : 0
///                          ** unit : mm
/// </summary>
public class Evapotranspiration
{

    private double _evaporation_zeta;
    /// <summary>
    /// Gets and sets the shape factor
    /// </summary>
    [Description("shape factor")] 
    [Units("dimensionless")] 
    //[Crop2ML(datatype="DOUBLE", min=0, max=40, default=40, parametercategory=constant, inputtype="parameter")] 
    public double evaporation_zeta
    {
        get { return this._evaporation_zeta; }
        set { this._evaporation_zeta= value; } 
    }

    private double _maximum_evaporation_impact_depth;
    /// <summary>
    /// Gets and sets the maximumEvaporationImpactDepth
    /// </summary>
    [Description("maximumEvaporationImpactDepth")] 
    [Units("dm")] 
    //[Crop2ML(datatype="DOUBLE", min=0, max=null, default=5, parametercategory=constant, inputtype="parameter")] 
    public double maximum_evaporation_impact_depth
    {
        get { return this._maximum_evaporation_impact_depth; }
        set { this._maximum_evaporation_impact_depth= value; } 
    }

    private int _no_of_soil_layers;
    /// <summary>
    /// Gets and sets the number of soil layers
    /// </summary>
    [Description("number of soil layers")] 
    [Units("dimensionless")] 
    //[Crop2ML(datatype="INT", min=0, max=null, default=20, parametercategory=constant, inputtype="parameter")] 
    public int no_of_soil_layers
    {
        get { return this._no_of_soil_layers; }
        set { this._no_of_soil_layers= value; } 
    }

    private double[] _layer_thickness;
    /// <summary>
    /// Gets and sets the layer thickness array
    /// </summary>
    [Description("layer thickness array")] 
    [Units("m")] 
    //[Crop2ML(datatype="DOUBLEARRAY", min=null, max=null, default=, parametercategory=constant, inputtype="parameter")] 
    public double[] layer_thickness
    {
        get { return this._layer_thickness; }
        set { this._layer_thickness= value; } 
    }

    private double _reference_albedo;
    /// <summary>
    /// Gets and sets the reference albedo
    /// </summary>
    [Description("reference albedo")] 
    [Units("dimensionless")] 
    //[Crop2ML(datatype="DOUBLE", min=0, max=1, default=0, parametercategory=constant, inputtype="parameter")] 
    public double reference_albedo
    {
        get { return this._reference_albedo; }
        set { this._reference_albedo= value; } 
    }

    private double _stomata_resistance;
    /// <summary>
    /// Gets and sets the stomata resistance
    /// </summary>
    [Description("stomata resistance")] 
    [Units("s/m")] 
    //[Crop2ML(datatype="DOUBLE", min=0, max=10000, default=100, parametercategory=constant, inputtype="parameter")] 
    public double stomata_resistance
    {
        get { return this._stomata_resistance; }
        set { this._stomata_resistance= value; } 
    }

    private int _evaporation_reduction_method;
    /// <summary>
    /// Gets and sets the THESEUS (0) or HERMES (1) evaporation reduction method
    /// </summary>
    [Description("THESEUS (0) or HERMES (1) evaporation reduction method")] 
    [Units("dimensionless")] 
    //[Crop2ML(datatype="INT", min=0, max=1, default=1, parametercategory=constant, inputtype="parameter")] 
    public int evaporation_reduction_method
    {
        get { return this._evaporation_reduction_method; }
        set { this._evaporation_reduction_method= value; } 
    }

    private double _xsa_critical_soil_moisture;
    /// <summary>
    /// Gets and sets the XSACriticalSoilMoisture
    /// </summary>
    [Description("XSACriticalSoilMoisture")] 
    [Units("m3/m3")] 
    //[Crop2ML(datatype="DOUBLE", min=0, max=1.5, default=0.1, parametercategory=constant, inputtype="parameter")] 
    public double xsa_critical_soil_moisture
    {
        get { return this._xsa_critical_soil_moisture; }
        set { this._xsa_critical_soil_moisture= value; } 
    }

    
    /// <summary>
    /// Constructor of the Evapotranspiration component")
    /// </summary>  
    public Evapotranspiration() { }
    
    /// <summary>
    /// Algorithm of the Evapotranspiration component
    /// </summary>
    public void  CalculateModel(EvapotranspirationCompState s, EvapotranspirationCompState s1, EvapotranspirationCompRate r, EvapotranspirationCompAuxiliary a, EvapotranspirationCompExogenous ex)
    {
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
    /// <summary>
    /// 
    /// </summary>
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
    /// <summary>
    /// 
    /// </summary>
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
    /// <summary>
    /// 
    /// </summary>
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
    /// <summary>
    /// 
    /// </summary>
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