import  java.io.*;
import  java.util.*;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.time.LocalDateTime;
public class Evapotranspiration
{
    private double evaporation_zeta;
    public double getevaporation_zeta()
    { return evaporation_zeta; }

    public void setevaporation_zeta(double _evaporation_zeta)
    { this.evaporation_zeta= _evaporation_zeta; } 
    
    private double maximum_evaporation_impact_depth;
    public double getmaximum_evaporation_impact_depth()
    { return maximum_evaporation_impact_depth; }

    public void setmaximum_evaporation_impact_depth(double _maximum_evaporation_impact_depth)
    { this.maximum_evaporation_impact_depth= _maximum_evaporation_impact_depth; } 
    
    private Integer no_of_soil_layers;
    public Integer getno_of_soil_layers()
    { return no_of_soil_layers; }

    public void setno_of_soil_layers(Integer _no_of_soil_layers)
    { this.no_of_soil_layers= _no_of_soil_layers; } 
    
    private Double [] layer_thickness;
    public Double [] getlayer_thickness()
    { return layer_thickness; }

    public void setlayer_thickness(Double [] _layer_thickness)
    { this.layer_thickness= _layer_thickness; } 
    
    private double reference_albedo;
    public double getreference_albedo()
    { return reference_albedo; }

    public void setreference_albedo(double _reference_albedo)
    { this.reference_albedo= _reference_albedo; } 
    
    private double stomata_resistance;
    public double getstomata_resistance()
    { return stomata_resistance; }

    public void setstomata_resistance(double _stomata_resistance)
    { this.stomata_resistance= _stomata_resistance; } 
    
    private Integer evaporation_reduction_method;
    public Integer getevaporation_reduction_method()
    { return evaporation_reduction_method; }

    public void setevaporation_reduction_method(Integer _evaporation_reduction_method)
    { this.evaporation_reduction_method= _evaporation_reduction_method; } 
    
    private double xsa_critical_soil_moisture;
    public double getxsa_critical_soil_moisture()
    { return xsa_critical_soil_moisture; }

    public void setxsa_critical_soil_moisture(double _xsa_critical_soil_moisture)
    { this.xsa_critical_soil_moisture= _xsa_critical_soil_moisture; } 
    
    public Evapotranspiration() { }
    public void  Calculate_Model(EvapotranspirationCompState s, EvapotranspirationCompState s1, EvapotranspirationCompRate r, EvapotranspirationCompAuxiliary a,  EvapotranspirationCompExogenous ex)
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
        calc_reference_evapotranspiration zz_calc_reference_evapotranspiration;
        double external_reference_evapotranspiration = ex.getexternal_reference_evapotranspiration();
        double height_nn = ex.getheight_nn();
        double max_air_temperature = ex.getmax_air_temperature();
        double min_air_temperature = ex.getmin_air_temperature();
        double mean_air_temperature = ex.getmean_air_temperature();
        double relative_humidity = ex.getrelative_humidity();
        double wind_speed = ex.getwind_speed();
        double wind_speed_height = ex.getwind_speed_height();
        double global_radiation = ex.getglobal_radiation();
        Integer julian_day = ex.getjulian_day();
        double latitude = ex.getlatitude();
        double evaporated_from_surface = s.getevaporated_from_surface();
        double snow_depth = s.getsnow_depth();
        Integer developmental_stage = s.getdevelopmental_stage();
        double crop_reference_evapotranspiration = s.getcrop_reference_evapotranspiration();
        double reference_evapotranspiration = s.getreference_evapotranspiration();
        double actual_evaporation = s.getactual_evaporation();
        double actual_transpiration = s.getactual_transpiration();
        double surface_water_storage = s.getsurface_water_storage();
        double kc_factor = s.getkc_factor();
        double percentage_soil_coverage = s.getpercentage_soil_coverage();
        Double [] soil_moisture = s.getsoil_moisture();
        Double [] permanent_wilting_point = s.getpermanent_wilting_point();
        Double [] field_capacity = s.getfield_capacity();
        Double [] evaporation = s.getevaporation();
        Double [] transpiration = s.gettranspiration();
        Double [] crop_transpiration = s.getcrop_transpiration();
        double crop_remaining_evapotranspiration = s.getcrop_remaining_evapotranspiration();
        double crop_evaporated_from_intercepted = s.getcrop_evaporated_from_intercepted();
        Double [] evapotranspiration = s.getevapotranspiration();
        double actual_evapotranspiration = s.getactual_evapotranspiration();
        double vapor_pressure = s.getvapor_pressure();
        evaporated_from_surface = 0.0d;
        double potential_evapotranspiration = 0.0;
        double evaporated_from_intercept = 0.0;
        if (developmental_stage > 0)
        {
            if (external_reference_evapotranspiration < 0.0d)
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
            if (external_reference_evapotranspiration < 0.0d)
            {
                zz_calc_reference_evapotranspiration = Calculate_calc_reference_evapotranspiration(height_nn, max_air_temperature, min_air_temperature, relative_humidity, mean_air_temperature, wind_speed, wind_speed_height, global_radiation, julian_day, latitude, reference_albedo, vapor_pressure, stomata_resistance);
                reference_evapotranspiration = zz_calc_reference_evapotranspiration.getreference_evapotranspiration();
                vapor_pressure = zz_calc_reference_evapotranspiration.getvapor_pressure();
            }
            else
            {
                reference_evapotranspiration = external_reference_evapotranspiration;
            }
            potential_evapotranspiration = reference_evapotranspiration * kc_factor;
        }
        actual_evaporation = 0.0d;
        actual_transpiration = 0.0d;
        if (potential_evapotranspiration > 6.5d)
        {
            potential_evapotranspiration = 6.5d;
        }
        Boolean evaporation_from_surface = false;
        double eRed1;
        double eRed2;
        double eRed3;
        double eReducer;
        Integer i;
        if (potential_evapotranspiration > 0.0d)
        {
            evaporation_from_surface = false;
            if (surface_water_storage > 0.0d)
            {
                evaporation_from_surface = true;
                potential_evapotranspiration = potential_evapotranspiration * 1.1d / kc_factor;
                if (snow_depth > 0.0d)
                {
                    evaporated_from_surface = 0.0d;
                }
                else if ( surface_water_storage < potential_evapotranspiration)
                {
                    potential_evapotranspiration = potential_evapotranspiration - surface_water_storage;
                    evaporated_from_surface = surface_water_storage;
                    surface_water_storage = 0.0d;
                }
                else
                {
                    surface_water_storage = surface_water_storage - potential_evapotranspiration;
                    evaporated_from_surface = potential_evapotranspiration;
                    potential_evapotranspiration = 0.0d;
                }
                potential_evapotranspiration = potential_evapotranspiration * kc_factor / 1.1d;
            }
            if (potential_evapotranspiration > 0.0d)
            {
                for (i=0 ; i!=no_of_soil_layers ; i+=1)
                {
                    eRed1 = e_reducer_1(permanent_wilting_point[i], field_capacity[i], soil_moisture[i], percentage_soil_coverage, potential_evapotranspiration, evaporation_reduction_method, xsa_critical_soil_moisture);
                    eRed2 = 0.0d;
                    if ((double)(i) >= maximum_evaporation_impact_depth)
                    {
                        eRed2 = 0.0d;
                    }
                    else
                    {
                        eRed2 = get_deprivation_factor(i + 1, maximum_evaporation_impact_depth, evaporation_zeta, layer_thickness[i]);
                    }
                    eRed3 = 0.0d;
                    if (i > 0 && soil_moisture[i] < soil_moisture[i - 1])
                    {
                        eRed3 = 0.1d;
                    }
                    else
                    {
                        eRed3 = 1.0d;
                    }
                    eReducer = eRed1 * eRed2 * eRed3;
                    if (developmental_stage > 0)
                    {
                        if (percentage_soil_coverage >= 0.0d && percentage_soil_coverage < 1.0d)
                        {
                            evaporation[i] = (1.0d - percentage_soil_coverage) * eReducer * potential_evapotranspiration;
                        }
                        else if ( percentage_soil_coverage >= 1.0d)
                        {
                            evaporation[i] = 0.0d;
                        }
                        if (snow_depth > 0.0d)
                        {
                            evaporation[i] = 0.0d;
                        }
                        transpiration[i] = crop_transpiration[i];
                        if (evaporation_from_surface)
                        {
                            transpiration[i] = percentage_soil_coverage * eReducer * potential_evapotranspiration;
                        }
                    }
                    else
                    {
                        if (snow_depth > 0.0d)
                        {
                            evaporation[i] = 0.0d;
                        }
                        else
                        {
                            evaporation[i] = potential_evapotranspiration * eReducer;
                            transpiration[i] = 0.0d;
                        }
                    }
                    evapotranspiration[i] = evaporation[i] + transpiration[i];
                    soil_moisture[i] = soil_moisture[i] - (evapotranspiration[i] / 1000.0d / layer_thickness[i]);
                    if (soil_moisture[i] < 0.01d)
                    {
                        soil_moisture[i] = 0.01d;
                    }
                    actual_transpiration = actual_transpiration + transpiration[i];
                    actual_evaporation = actual_evaporation + evaporation[i];
                }
            }
            actual_evapotranspiration = actual_transpiration + actual_evaporation + evaporated_from_intercept + evaporated_from_surface;
        }
        s.setevaporated_from_surface(evaporated_from_surface);
        s.setactual_evapotranspiration(actual_evapotranspiration);
    }
    public static double get_deprivation_factor(Integer layer_no, double deprivation_depth, double zeta, double layer_thickness)
    {
        double ltf;
        ltf = deprivation_depth / (layer_thickness * 10.0d);
        double deprivation_factor;
        double c2;
        double c3;
        if (Math.abs(zeta) < 0.0003d)
        {
            deprivation_factor = 2.0d / ltf - (1.0d / (ltf * ltf) * (2 * layer_no - 1));
        }
        else
        {
            c2 = Math.log((ltf + (zeta * layer_no)) / (ltf + (zeta * (layer_no - 1))));
            c3 = zeta / (ltf * (zeta + 1.0d));
            deprivation_factor = (c2 - c3) / (Math.log(zeta + 1.0d) - (zeta / (zeta + 1.0d)));
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
    public calc_reference_evapotranspiration Calculate_calc_reference_evapotranspiration (double height_nn, double max_air_temperature, double min_air_temperature, double relative_humidity, double mean_air_temperature, double wind_speed, double wind_speed_height, double global_radiation, Integer julian_day, double latitude, double reference_albedo, double vapor_pressure, double stomata_resistance)
    {
        double declination;
        declination = -23.4d * Math.cos(2.0d * Math.PI * ((julian_day + 10.0d) / 365.0d));
        double declination_sinus;
        declination_sinus = Math.sin(declination * Math.PI / 180.0d) * Math.sin(latitude * Math.PI / 180.0d);
        double declination_cosinus;
        declination_cosinus = Math.cos(declination * Math.PI / 180.0d) * Math.cos(latitude * Math.PI / 180.0d);
        double arg_astro_day_length;
        arg_astro_day_length = declination_sinus / declination_cosinus;
        arg_astro_day_length = bound(-1.0d, arg_astro_day_length, 1.0d);
        double astronomic_day_length;
        astronomic_day_length = 12.0d * (Math.PI + (2.0d * Math.asin(arg_astro_day_length))) / Math.PI;
        double arg_effective_day_length;
        arg_effective_day_length = (-Math.sin((8.0d * Math.PI / 180.0d)) + declination_sinus) / declination_cosinus;
        arg_effective_day_length = bound(-1.0d, arg_effective_day_length, 1.0d);
        double arg_photo_day_length;
        arg_photo_day_length = (-Math.sin((-6.0d * Math.PI / 180.0d)) + declination_sinus) / declination_cosinus;
        arg_photo_day_length = bound(-1.0d, arg_photo_day_length, 1.0d);
        double arg_phot_act;
        arg_phot_act = Math.min(1.0d, declination_sinus / declination_cosinus * (declination_sinus / declination_cosinus));
        double phot_act_radiation_mean;
        phot_act_radiation_mean = 3600.0d * (declination_sinus * astronomic_day_length + (24.0d / Math.PI * declination_cosinus * Math.sqrt((1.0d - arg_phot_act))));
        double clear_day_radiation = 0.0;
        if (phot_act_radiation_mean > 0.0d && astronomic_day_length > 0.0d)
        {
            clear_day_radiation = 0.5d * 1300.0d * phot_act_radiation_mean * Math.exp(-0.14d / (phot_act_radiation_mean / (astronomic_day_length * 3600.0d)));
        }
        double SC;
        SC = 24.0d * 60.0d / Math.PI * 8.20d * (1.0d + (0.033d * Math.cos(2.0d * Math.PI * julian_day / 365.0d)));
        double arg_SHA;
        arg_SHA = bound(-1.0d, -Math.tan((latitude * Math.PI / 180.0d)) * Math.tan(declination * Math.PI / 180.0d), 1.0d);
        double SHA;
        SHA = Math.acos(arg_SHA);
        double extraterrestrial_radiation;
        extraterrestrial_radiation = SC * (SHA * declination_sinus + (declination_cosinus * Math.sin(SHA))) / 100.0d;
        double atmospheric_pressure;
        atmospheric_pressure = 101.3d * Math.pow((293.0d - (0.0065d * height_nn)) / 293.0d, 5.26d);
        double psycrometer_constant;
        psycrometer_constant = 0.000665d * atmospheric_pressure;
        double saturated_vapor_pressure_max;
        saturated_vapor_pressure_max = 0.6108d * Math.exp(17.27d * max_air_temperature / (237.3d + max_air_temperature));
        double saturated_vapor_pressure_min;
        saturated_vapor_pressure_min = 0.6108d * Math.exp(17.27d * min_air_temperature / (237.3d + min_air_temperature));
        double saturated_vapor_pressure;
        saturated_vapor_pressure = (saturated_vapor_pressure_max + saturated_vapor_pressure_min) / 2.0d;
        if (vapor_pressure < 0.0d)
        {
            if (relative_humidity <= 0.0d)
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
        saturated_vapour_pressure_slope = 4098.0d * (0.6108d * Math.exp(17.27d * mean_air_temperature / (mean_air_temperature + 237.3d))) / ((mean_air_temperature + 237.3d) * (mean_air_temperature + 237.3d));
        double wind_speed_2m;
        wind_speed_2m = Math.max(0.5d, wind_speed * (4.87d / Math.log((67.8d * wind_speed_height - 5.42d))));
        double surface_resistance;
        surface_resistance = stomata_resistance / 1.44d;
        double clear_sky_solar_radiation;
        clear_sky_solar_radiation = (0.75d + (0.00002d * height_nn)) * extraterrestrial_radiation;
        double relative_shortwave_radiation;
        relative_shortwave_radiation = clear_sky_solar_radiation > 0.0d ? Math.min(global_radiation / clear_sky_solar_radiation, 1.0d) : 1.0d;
        double bolzmann_constant = 0.0000000049;
        double shortwave_radiation;
        shortwave_radiation = (1.0d - reference_albedo) * global_radiation;
        double longwave_radiation;
        longwave_radiation = bolzmann_constant * ((Math.pow(min_air_temperature + 273.16d, 4.0d) + Math.pow(max_air_temperature + 273.16d, 4.0d)) / 2.0d) * (1.35d * relative_shortwave_radiation - 0.35d) * (0.34d - (0.14d * Math.sqrt(vapor_pressure)));
        double net_radiation;
        net_radiation = shortwave_radiation - longwave_radiation;
        double reference_evapotranspiration;
        reference_evapotranspiration = (0.408d * saturated_vapour_pressure_slope * net_radiation + (psycrometer_constant * (900.0d / (mean_air_temperature + 273.0d)) * wind_speed_2m * saturation_deficit)) / (saturated_vapour_pressure_slope + (psycrometer_constant * (1.0d + (surface_resistance / 208.0d * wind_speed_2m))));
        if (reference_evapotranspiration < 0.0d)
        {
            reference_evapotranspiration = 0.0d;
        }
        return new calc_reference_evapotranspiration(reference_evapotranspiration, vapor_pressure);
    }
    public static double e_reducer_1(double pwp, double fc, double sm, double percentage_soil_coverage, double reference_evapotranspiration, Integer evaporation_reduction_method, double xsa_critical_soil_moisture)
    {
        sm = Math.max(0.33d * pwp, sm);
        double relative_evaporable_water;
        relative_evaporable_water = Math.min(1.0d, (sm - (0.33d * pwp)) / (fc - (0.33d * pwp)));
        double e_reduction_factor = 0.0;
        double critical_soil_moisture;
        double reducer;
        double xsa;
        if (evaporation_reduction_method == 0)
        {
            critical_soil_moisture = 0.65d * fc;
            if (percentage_soil_coverage > 0.0d)
            {
                reducer = 1.0d;
                if (reference_evapotranspiration > 2.5d)
                {
                    xsa = (0.65d * fc - pwp) * (fc - pwp);
                    reducer = xsa + ((1 - xsa) / 17.5d * (reference_evapotranspiration - 2.5d));
                }
                else
                {
                    reducer = xsa_critical_soil_moisture / 2.5d * reference_evapotranspiration;
                }
                critical_soil_moisture = fc * reducer;
            }
            if (sm > critical_soil_moisture)
            {
                e_reduction_factor = 1.0d;
            }
            else if ( sm > (0.33d * pwp))
            {
                e_reduction_factor = relative_evaporable_water;
            }
            else
            {
                e_reduction_factor = 0.0d;
            }
        }
        else
        {
            if (relative_evaporable_water > 0.33d)
            {
                e_reduction_factor = 1.0d - (0.1d * (1.0d - relative_evaporable_water) / (1.0d - 0.33d));
            }
            else if ( relative_evaporable_water > 0.22d)
            {
                e_reduction_factor = 0.9d - (0.625d * (0.33d - relative_evaporable_water) / (0.33d - 0.22d));
            }
            else if ( relative_evaporable_water > 0.2d)
            {
                e_reduction_factor = 0.275d - (0.225d * (0.22d - relative_evaporable_water) / (0.22d - 0.2d));
            }
            else
            {
                e_reduction_factor = 0.05d - (0.05d * (0.2d - relative_evaporable_water) / 0.2d);
            }
        }
        return e_reduction_factor;
    }
}
final class calc_reference_evapotranspiration {
    private double reference_evapotranspiration;
    public double getreference_evapotranspiration()
    { return reference_evapotranspiration; }

    public void setreference_evapotranspiration(double _reference_evapotranspiration)
    { this.reference_evapotranspiration= _reference_evapotranspiration; } 
    
    private double vapor_pressure;
    public double getvapor_pressure()
    { return vapor_pressure; }

    public void setvapor_pressure(double _vapor_pressure)
    { this.vapor_pressure= _vapor_pressure; } 
    
    public calc_reference_evapotranspiration(double reference_evapotranspiration,double vapor_pressure)
    {
        this.reference_evapotranspiration = reference_evapotranspiration;
        this.vapor_pressure = vapor_pressure;
    }
}