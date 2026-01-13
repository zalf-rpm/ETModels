package net.simplace.sim.components.Monica_Evapotranspiration;
import  java.io.*;
import  java.util.*;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.time.LocalDateTime;
import net.simplace.sim.model.FWSimComponent;
import net.simplace.sim.util.FWSimVarMap;
import net.simplace.sim.util.FWSimVariable;
import net.simplace.sim.util.FWSimVariable.CONTENT_TYPE;
import net.simplace.sim.util.FWSimVariable.DATA_TYPE;
import org.jdom2.Element;


public class Evapotranspiration extends FWSimComponent
{
    private FWSimVariable<Double> evaporation_zeta;
    private FWSimVariable<Double> maximum_evaporation_impact_depth;
    private FWSimVariable<Integer> no_of_soil_layers;
    private FWSimVariable<Double[]> layer_thickness;
    private FWSimVariable<Double> reference_albedo;
    private FWSimVariable<Double> stomata_resistance;
    private FWSimVariable<Integer> evaporation_reduction_method;
    private FWSimVariable<Double> xsa_critical_soil_moisture;
    private FWSimVariable<Double> external_reference_evapotranspiration;
    private FWSimVariable<Double> height_nn;
    private FWSimVariable<Double> max_air_temperature;
    private FWSimVariable<Double> min_air_temperature;
    private FWSimVariable<Double> mean_air_temperature;
    private FWSimVariable<Double> relative_humidity;
    private FWSimVariable<Double> wind_speed;
    private FWSimVariable<Double> wind_speed_height;
    private FWSimVariable<Double> global_radiation;
    private FWSimVariable<Integer> julian_day;
    private FWSimVariable<Double> latitude;
    private FWSimVariable<Double> evaporated_from_surface;
    private FWSimVariable<Double> snow_depth;
    private FWSimVariable<Integer> developmental_stage;
    private FWSimVariable<Double> crop_reference_evapotranspiration;
    private FWSimVariable<Double> reference_evapotranspiration;
    private FWSimVariable<Double> actual_evaporation;
    private FWSimVariable<Double> actual_transpiration;
    private FWSimVariable<Double> surface_water_storage;
    private FWSimVariable<Double> kc_factor;
    private FWSimVariable<Double> percentage_soil_coverage;
    private FWSimVariable<Double[]> soil_moisture;
    private FWSimVariable<Double[]> permanent_wilting_point;
    private FWSimVariable<Double[]> field_capacity;
    private FWSimVariable<Double[]> evaporation;
    private FWSimVariable<Double[]> transpiration;
    private FWSimVariable<Double[]> crop_transpiration;
    private FWSimVariable<Double> crop_remaining_evapotranspiration;
    private FWSimVariable<Double> crop_evaporated_from_intercepted;
    private FWSimVariable<Double[]> evapotranspiration;
    private FWSimVariable<Double> actual_evapotranspiration;
    private FWSimVariable<Double> vapor_pressure;

    public Evapotranspiration(String aName, HashMap<String, FWSimVariable<?>> aFieldMap, HashMap<String, String> aInputMap, Element aSimComponentElement, FWSimVarMap aVarMap, int aOrderNumber)
    {
        super(aName, aFieldMap, aInputMap, aSimComponentElement, aVarMap, aOrderNumber);
    }

    public Evapotranspiration(){
        super();
    }

    @Override
    public HashMap<String, FWSimVariable<?>> createVariables()
    {
        addVariable(FWSimVariable.createSimVariable("evaporation_zeta", "shape factor", DATA_TYPE.DOUBLE, CONTENT_TYPE.constant,"dimensionless", 0, 40, 40, this));
        addVariable(FWSimVariable.createSimVariable("maximum_evaporation_impact_depth", "maximumEvaporationImpactDepth", DATA_TYPE.DOUBLE, CONTENT_TYPE.constant,"dm", 0, null, 5, this));
        addVariable(FWSimVariable.createSimVariable("no_of_soil_layers", "number of soil layers", DATA_TYPE.INT, CONTENT_TYPE.constant,"dimensionless", 0, null, 20, this));
        addVariable(FWSimVariable.createSimVariable("layer_thickness", "layer thickness array", DATA_TYPE.DOUBLEARRAY, CONTENT_TYPE.constant,"m", null, null, null, this));
        addVariable(FWSimVariable.createSimVariable("reference_albedo", "reference albedo", DATA_TYPE.DOUBLE, CONTENT_TYPE.constant,"dimensionless", 0, 1, 0, this));
        addVariable(FWSimVariable.createSimVariable("stomata_resistance", "stomata resistance", DATA_TYPE.DOUBLE, CONTENT_TYPE.constant,"s/m", 0, 10000, 100, this));
        addVariable(FWSimVariable.createSimVariable("evaporation_reduction_method", "THESEUS (0) or HERMES (1) evaporation reduction method", DATA_TYPE.INT, CONTENT_TYPE.constant,"dimensionless", 0, 1, 1, this));
        addVariable(FWSimVariable.createSimVariable("xsa_critical_soil_moisture", "XSACriticalSoilMoisture", DATA_TYPE.DOUBLE, CONTENT_TYPE.constant,"m3/m3", 0, 1.5, 0.1, this));
        addVariable(FWSimVariable.createSimVariable("external_reference_evapotranspiration", "externally supplied ET0", DATA_TYPE.DOUBLE, CONTENT_TYPE.input,"mm", 0, null, -1, this));
        addVariable(FWSimVariable.createSimVariable("height_nn", "height above sea leavel", DATA_TYPE.DOUBLE, CONTENT_TYPE.input,"m", -9999, 9999, 0, this));
        addVariable(FWSimVariable.createSimVariable("max_air_temperature", "daily maximum air temperature", DATA_TYPE.DOUBLE, CONTENT_TYPE.input,"°C", -100, 100, 0, this));
        addVariable(FWSimVariable.createSimVariable("min_air_temperature", "daily minimum air temperature", DATA_TYPE.DOUBLE, CONTENT_TYPE.input,"°C", -100, 100, 0, this));
        addVariable(FWSimVariable.createSimVariable("mean_air_temperature", "daily average air temperature", DATA_TYPE.DOUBLE, CONTENT_TYPE.input,"°C", -100, 100, 0, this));
        addVariable(FWSimVariable.createSimVariable("relative_humidity", "relative humidity", DATA_TYPE.DOUBLE, CONTENT_TYPE.input,"fraction", 0, 1, 0, this));
        addVariable(FWSimVariable.createSimVariable("wind_speed", "wind speed measured at wind speed height", DATA_TYPE.DOUBLE, CONTENT_TYPE.input,"m/s", 0, 9999, 0, this));
        addVariable(FWSimVariable.createSimVariable("wind_speed_height", "height at which the wind speed has been measured", DATA_TYPE.DOUBLE, CONTENT_TYPE.input,"m", -9999, 9999, 2, this));
        addVariable(FWSimVariable.createSimVariable("global_radiation", "global radiation", DATA_TYPE.DOUBLE, CONTENT_TYPE.input,"MJ/m2", 0, 50, 0, this));
        addVariable(FWSimVariable.createSimVariable("julian_day", "day of year", DATA_TYPE.INT, CONTENT_TYPE.input,"day", 1, 366, 1, this));
        addVariable(FWSimVariable.createSimVariable("latitude", "latitude", DATA_TYPE.DOUBLE, CONTENT_TYPE.input,"degree", -90, 90, 0, this));
        addVariable(FWSimVariable.createSimVariable("evaporated_from_surface", "evaporated_from_surface", DATA_TYPE.DOUBLE, CONTENT_TYPE.state,"mm", 0, null, 0, this));
        addVariable(FWSimVariable.createSimVariable("snow_depth", "depth of snow layer", DATA_TYPE.DOUBLE, CONTENT_TYPE.state,"mm", 0, null, 0, this));
        addVariable(FWSimVariable.createSimVariable("developmental_stage", "MONICA crop developmental stage", DATA_TYPE.INT, CONTENT_TYPE.state,"dimensionless", 0, 6, 0, this));
        addVariable(FWSimVariable.createSimVariable("crop_reference_evapotranspiration", "the crop specific ET0, if no external ET0 and crop is planted", DATA_TYPE.DOUBLE, CONTENT_TYPE.state,"mm", 0, null, -1, this));
        addVariable(FWSimVariable.createSimVariable("reference_evapotranspiration", "reference evapotranspiration (ET0)", DATA_TYPE.DOUBLE, CONTENT_TYPE.state,"mm", 0, null, 0, this));
        addVariable(FWSimVariable.createSimVariable("actual_evaporation", "actual evaporation", DATA_TYPE.DOUBLE, CONTENT_TYPE.state,"mm", 0, null, 0, this));
        addVariable(FWSimVariable.createSimVariable("actual_transpiration", "actual transpiration", DATA_TYPE.DOUBLE, CONTENT_TYPE.state,"mm", 0, null, 0, this));
        addVariable(FWSimVariable.createSimVariable("surface_water_storage", "Simulates a virtual layer that contains the surface water", DATA_TYPE.DOUBLE, CONTENT_TYPE.state,"mm", 0, null, 0, this));
        addVariable(FWSimVariable.createSimVariable("kc_factor", "crop coefficient ETc/ET0", DATA_TYPE.DOUBLE, CONTENT_TYPE.state,"dimensionless", 0, null, 0.75, this));
        addVariable(FWSimVariable.createSimVariable("percentage_soil_coverage", "fraction of soil covered by crop", DATA_TYPE.DOUBLE, CONTENT_TYPE.state,"m2/m2", 0, 1, 0, this));
        addVariable(FWSimVariable.createSimVariable("soil_moisture", "soil moisture array", DATA_TYPE.DOUBLEARRAY, CONTENT_TYPE.state,"m3/m3", null, null, null, this));
        addVariable(FWSimVariable.createSimVariable("permanent_wilting_point", "permanent wilting point array", DATA_TYPE.DOUBLEARRAY, CONTENT_TYPE.state,"m3/m3", 0, 2, 0, this));
        addVariable(FWSimVariable.createSimVariable("field_capacity", "field capacity array", DATA_TYPE.DOUBLEARRAY, CONTENT_TYPE.state,"m3/m3", 0, 1, 0, this));
        addVariable(FWSimVariable.createSimVariable("evaporation", "evaporation array", DATA_TYPE.DOUBLEARRAY, CONTENT_TYPE.state,"mm", 0, 1, 0, this));
        addVariable(FWSimVariable.createSimVariable("transpiration", "transpiration array", DATA_TYPE.DOUBLEARRAY, CONTENT_TYPE.state,"mm", null, null, null, this));
        addVariable(FWSimVariable.createSimVariable("crop_transpiration", "crop transpiration array", DATA_TYPE.DOUBLEARRAY, CONTENT_TYPE.state,"mm", null, null, null, this));
        addVariable(FWSimVariable.createSimVariable("crop_remaining_evapotranspiration", "crop remaining evapotranspiration", DATA_TYPE.DOUBLE, CONTENT_TYPE.state,"mm", null, null, null, this));
        addVariable(FWSimVariable.createSimVariable("crop_evaporated_from_intercepted", "crop evaporated water from intercepted water", DATA_TYPE.DOUBLE, CONTENT_TYPE.state,"mm", null, null, null, this));
        addVariable(FWSimVariable.createSimVariable("evapotranspiration", "evapotranspiration array", DATA_TYPE.DOUBLEARRAY, CONTENT_TYPE.state,"mm", null, null, null, this));
        addVariable(FWSimVariable.createSimVariable("actual_evapotranspiration", "actual evapotranspiration", DATA_TYPE.DOUBLE, CONTENT_TYPE.state,"mm", 0, null, 0, this));
        addVariable(FWSimVariable.createSimVariable("vapor_pressure", "vapor pressure", DATA_TYPE.DOUBLE, CONTENT_TYPE.state,"kPa", 0, null, 0, this));

        return iFieldMap;
    }
    @Override
    protected void process()
    {
        calc_reference_evapotranspiration zz_calc_reference_evapotranspiration;
        double t_evaporation_zeta = evaporation_zeta.getValue();
        double t_maximum_evaporation_impact_depth = maximum_evaporation_impact_depth.getValue();
        Integer t_no_of_soil_layers = no_of_soil_layers.getValue();
        Double [] t_layer_thickness = layer_thickness.getValue();
        double t_reference_albedo = reference_albedo.getValue();
        double t_stomata_resistance = stomata_resistance.getValue();
        Integer t_evaporation_reduction_method = evaporation_reduction_method.getValue();
        double t_xsa_critical_soil_moisture = xsa_critical_soil_moisture.getValue();
        double t_external_reference_evapotranspiration = external_reference_evapotranspiration.getValue();
        double t_height_nn = height_nn.getValue();
        double t_max_air_temperature = max_air_temperature.getValue();
        double t_min_air_temperature = min_air_temperature.getValue();
        double t_mean_air_temperature = mean_air_temperature.getValue();
        double t_relative_humidity = relative_humidity.getValue();
        double t_wind_speed = wind_speed.getValue();
        double t_wind_speed_height = wind_speed_height.getValue();
        double t_global_radiation = global_radiation.getValue();
        Integer t_julian_day = julian_day.getValue();
        double t_latitude = latitude.getValue();
        double t_evaporated_from_surface = evaporated_from_surface.getValue();
        double t_snow_depth = snow_depth.getValue();
        Integer t_developmental_stage = developmental_stage.getValue();
        double t_crop_reference_evapotranspiration = crop_reference_evapotranspiration.getValue();
        double t_reference_evapotranspiration = reference_evapotranspiration.getValue();
        double t_actual_evaporation = actual_evaporation.getValue();
        double t_actual_transpiration = actual_transpiration.getValue();
        double t_surface_water_storage = surface_water_storage.getValue();
        double t_kc_factor = kc_factor.getValue();
        double t_percentage_soil_coverage = percentage_soil_coverage.getValue();
        Double [] t_soil_moisture = soil_moisture.getValue();
        Double [] t_permanent_wilting_point = permanent_wilting_point.getValue();
        Double [] t_field_capacity = field_capacity.getValue();
        Double [] t_evaporation = evaporation.getValue();
        Double [] t_transpiration = transpiration.getValue();
        Double [] t_crop_transpiration = crop_transpiration.getValue();
        double t_crop_remaining_evapotranspiration = crop_remaining_evapotranspiration.getValue();
        double t_crop_evaporated_from_intercepted = crop_evaporated_from_intercepted.getValue();
        Double [] t_evapotranspiration = evapotranspiration.getValue();
        double t_actual_evapotranspiration = actual_evapotranspiration.getValue();
        double t_vapor_pressure = vapor_pressure.getValue();
        t_evaporated_from_surface = 0.0d;
        double potential_evapotranspiration = 0.0;
        double evaporated_from_intercept = 0.0;
        if (t_developmental_stage > 0)
        {
            if (t_external_reference_evapotranspiration < 0.0d)
            {
                t_reference_evapotranspiration = t_reference_evapotranspiration;
            }
            else
            {
                t_reference_evapotranspiration = t_external_reference_evapotranspiration;
            }
            potential_evapotranspiration = t_crop_remaining_evapotranspiration;
            evaporated_from_intercept = t_crop_evaporated_from_intercepted;
        }
        else
        {
            if (t_external_reference_evapotranspiration < 0.0d)
            {
                zz_calc_reference_evapotranspiration = Calculate_calc_reference_evapotranspiration(t_height_nn, t_max_air_temperature, t_min_air_temperature, t_relative_humidity, t_mean_air_temperature, t_wind_speed, t_wind_speed_height, t_global_radiation, t_julian_day, t_latitude, t_reference_albedo, t_vapor_pressure, t_stomata_resistance);
                t_reference_evapotranspiration = zz_calc_reference_evapotranspiration.getreference_evapotranspiration();
                t_vapor_pressure = zz_calc_reference_evapotranspiration.getvapor_pressure();
            }
            else
            {
                t_reference_evapotranspiration = t_external_reference_evapotranspiration;
            }
            potential_evapotranspiration = t_reference_evapotranspiration * t_kc_factor;
        }
        t_actual_evaporation = 0.0d;
        t_actual_transpiration = 0.0d;
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
            if (t_surface_water_storage > 0.0d)
            {
                evaporation_from_surface = true;
                potential_evapotranspiration = potential_evapotranspiration * 1.1d / t_kc_factor;
                if (t_snow_depth > 0.0d)
                {
                    t_evaporated_from_surface = 0.0d;
                }
                else if ( t_surface_water_storage < potential_evapotranspiration)
                {
                    potential_evapotranspiration = potential_evapotranspiration - t_surface_water_storage;
                    t_evaporated_from_surface = t_surface_water_storage;
                    t_surface_water_storage = 0.0d;
                }
                else
                {
                    t_surface_water_storage = t_surface_water_storage - potential_evapotranspiration;
                    t_evaporated_from_surface = potential_evapotranspiration;
                    potential_evapotranspiration = 0.0d;
                }
                potential_evapotranspiration = potential_evapotranspiration * t_kc_factor / 1.1d;
            }
            if (potential_evapotranspiration > 0.0d)
            {
                for (i=0 ; i!=t_no_of_soil_layers ; i+=1)
                {
                    eRed1 = e_reducer_1(t_permanent_wilting_point[i], t_field_capacity[i], t_soil_moisture[i], t_percentage_soil_coverage, potential_evapotranspiration, t_evaporation_reduction_method, t_xsa_critical_soil_moisture);
                    eRed2 = 0.0d;
                    if ((double)(i) >= t_maximum_evaporation_impact_depth)
                    {
                        eRed2 = 0.0d;
                    }
                    else
                    {
                        eRed2 = get_deprivation_factor(i + 1, t_maximum_evaporation_impact_depth, t_evaporation_zeta, t_layer_thickness[i]);
                    }
                    eRed3 = 0.0d;
                    if (i > 0 && t_soil_moisture[i] < t_soil_moisture[i - 1])
                    {
                        eRed3 = 0.1d;
                    }
                    else
                    {
                        eRed3 = 1.0d;
                    }
                    eReducer = eRed1 * eRed2 * eRed3;
                    if (t_developmental_stage > 0)
                    {
                        if (t_percentage_soil_coverage >= 0.0d && t_percentage_soil_coverage < 1.0d)
                        {
                            t_evaporation[i] = (1.0d - t_percentage_soil_coverage) * eReducer * potential_evapotranspiration;
                        }
                        else if ( t_percentage_soil_coverage >= 1.0d)
                        {
                            t_evaporation[i] = 0.0d;
                        }
                        if (t_snow_depth > 0.0d)
                        {
                            t_evaporation[i] = 0.0d;
                        }
                        t_transpiration[i] = t_crop_transpiration[i];
                        if (evaporation_from_surface)
                        {
                            t_transpiration[i] = t_percentage_soil_coverage * eReducer * potential_evapotranspiration;
                        }
                    }
                    else
                    {
                        if (t_snow_depth > 0.0d)
                        {
                            t_evaporation[i] = 0.0d;
                        }
                        else
                        {
                            t_evaporation[i] = potential_evapotranspiration * eReducer;
                            t_transpiration[i] = 0.0d;
                        }
                    }
                    t_evapotranspiration[i] = t_evaporation[i] + t_transpiration[i];
                    t_soil_moisture[i] = t_soil_moisture[i] - (t_evapotranspiration[i] / 1000.0d / t_layer_thickness[i]);
                    if (t_soil_moisture[i] < 0.01d)
                    {
                        t_soil_moisture[i] = 0.01d;
                    }
                    t_actual_transpiration = t_actual_transpiration + t_transpiration[i];
                    t_actual_evaporation = t_actual_evaporation + t_evaporation[i];
                }
            }
            t_actual_evapotranspiration = t_actual_transpiration + t_actual_evaporation + evaporated_from_intercept + t_evaporated_from_surface;
        }
        evaporated_from_surface.setValue(t_evaporated_from_surface, this);
        actual_evapotranspiration.setValue(t_actual_evapotranspiration, this);
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
        double t_reference_evapotranspiration;
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

    @Override
    protected void init()
    {
    }
    public HashMap<String, FWSimVariable<?>> fillTestVariables(int aParamIndex, TEST_STATE aDefineOrCheck)
    {
        return iFieldMap;
    }

    @Override
    protected FWSimComponent clone(FWSimVarMap aVarMap)
    {
        return new Evapotranspiration(iName, iFieldMap, iInputMap, iSimComponentElement, aVarMap, iOrderNumber);
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