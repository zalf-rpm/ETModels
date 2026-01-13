# coding: utf8
from copy import copy
from array import array
from math import *
from typing import *
from datetime import datetime

import numpy

#%%CyML Model Begin%%
def model_evapotranspiration(evaporation_zeta:float,
         maximum_evaporation_impact_depth:float,
         no_of_soil_layers:int,
         layer_thickness:'Array[float]',
         reference_albedo:float,
         stomata_resistance:float,
         evaporation_reduction_method:int,
         xsa_critical_soil_moisture:float,
         external_reference_evapotranspiration:float,
         height_nn:float,
         max_air_temperature:float,
         min_air_temperature:float,
         mean_air_temperature:float,
         relative_humidity:float,
         wind_speed:float,
         wind_speed_height:float,
         global_radiation:float,
         julian_day:int,
         latitude:float,
         evaporated_from_surface:float,
         snow_depth:float,
         developmental_stage:int,
         crop_reference_evapotranspiration:float,
         reference_evapotranspiration:float,
         actual_evaporation:float,
         actual_transpiration:float,
         surface_water_storage:float,
         kc_factor:float,
         percentage_soil_coverage:float,
         soil_moisture:'Array[float]',
         permanent_wilting_point:'Array[float]',
         field_capacity:'Array[float]',
         evaporation:'Array[float]',
         transpiration:'Array[float]',
         crop_transpiration:'Array[float]',
         crop_remaining_evapotranspiration:float,
         crop_evaporated_from_intercepted:float,
         evapotranspiration:'Array[float]',
         actual_evapotranspiration:float,
         vapor_pressure:float):
    """
     - Name: Evapotranspiration -Version: 1, -Time step: 1
     - Description:
                 * Title: Model of evapotranspiration
                 * Authors: Claas Nendel (transcription into Crop2ML by Michael Berg-Mohnicke)
                 * Reference: None
                 * Institution: ZALF e.V.
                 * ExtendedDescription: None
                 * ShortDescription: Calculates the evapotranspiration
     - inputs:
                 * name: evaporation_zeta
                               ** description : shape factor
                               ** inputtype : parameter
                               ** parametercategory : constant
                               ** datatype : DOUBLE
                               ** max : 40
                               ** min : 0
                               ** default : 40
                               ** unit : dimensionless
                 * name: maximum_evaporation_impact_depth
                               ** description : maximumEvaporationImpactDepth
                               ** inputtype : parameter
                               ** parametercategory : constant
                               ** datatype : DOUBLE
                               ** max : 
                               ** min : 0
                               ** default : 5
                               ** unit : dm
                 * name: no_of_soil_layers
                               ** description : number of soil layers
                               ** inputtype : parameter
                               ** parametercategory : constant
                               ** datatype : INT
                               ** max : 
                               ** min : 0
                               ** default : 20
                               ** unit : dimensionless
                 * name: layer_thickness
                               ** description : layer thickness array
                               ** inputtype : parameter
                               ** parametercategory : constant
                               ** datatype : DOUBLEARRAY
                               ** len : no_of_soil_layers
                               ** max : 
                               ** min : 
                               ** default : 
                               ** unit : m
                 * name: reference_albedo
                               ** description : reference albedo
                               ** inputtype : parameter
                               ** parametercategory : constant
                               ** datatype : DOUBLE
                               ** max : 1
                               ** min : 0
                               ** default : 0
                               ** unit : dimensionless
                 * name: stomata_resistance
                               ** description : stomata resistance
                               ** inputtype : parameter
                               ** parametercategory : constant
                               ** datatype : DOUBLE
                               ** max : 10000
                               ** min : 0
                               ** default : 100
                               ** unit : s/m
                 * name: evaporation_reduction_method
                               ** description : THESEUS (0) or HERMES (1) evaporation reduction method
                               ** inputtype : parameter
                               ** parametercategory : constant
                               ** datatype : INT
                               ** max : 1
                               ** min : 0
                               ** default : 1
                               ** unit : dimensionless
                 * name: xsa_critical_soil_moisture
                               ** description : XSACriticalSoilMoisture
                               ** inputtype : parameter
                               ** parametercategory : constant
                               ** datatype : DOUBLE
                               ** max : 1.5
                               ** min : 0
                               ** default : 0.1
                               ** unit : m3/m3
                 * name: external_reference_evapotranspiration
                               ** description : externally supplied ET0
                               ** inputtype : variable
                               ** variablecategory : exogenous
                               ** datatype : DOUBLE
                               ** max : 
                               ** min : 0
                               ** default : -1
                               ** unit : mm
                 * name: height_nn
                               ** description : height above sea leavel
                               ** inputtype : variable
                               ** variablecategory : exogenous
                               ** datatype : DOUBLE
                               ** max : 9999
                               ** min : -9999
                               ** default : 0
                               ** unit : m
                 * name: max_air_temperature
                               ** description : daily maximum air temperature
                               ** inputtype : variable
                               ** variablecategory : exogenous
                               ** datatype : DOUBLE
                               ** max : 100
                               ** min : -100
                               ** default : 0
                               ** unit : °C
                 * name: min_air_temperature
                               ** description : daily minimum air temperature
                               ** inputtype : variable
                               ** variablecategory : exogenous
                               ** datatype : DOUBLE
                               ** max : 100
                               ** min : -100
                               ** default : 0
                               ** unit : °C
                 * name: mean_air_temperature
                               ** description : daily average air temperature
                               ** inputtype : variable
                               ** variablecategory : exogenous
                               ** datatype : DOUBLE
                               ** max : 100
                               ** min : -100
                               ** default : 0
                               ** unit : °C
                 * name: relative_humidity
                               ** description : relative humidity
                               ** inputtype : variable
                               ** variablecategory : exogenous
                               ** datatype : DOUBLE
                               ** max : 1
                               ** min : 0
                               ** default : 0
                               ** unit : fraction
                 * name: wind_speed
                               ** description : wind speed measured at wind speed height
                               ** inputtype : variable
                               ** variablecategory : exogenous
                               ** datatype : DOUBLE
                               ** max : 9999
                               ** min : 0
                               ** default : 0
                               ** unit : m/s
                 * name: wind_speed_height
                               ** description : height at which the wind speed has been measured
                               ** inputtype : variable
                               ** variablecategory : exogenous
                               ** datatype : DOUBLE
                               ** max : 9999
                               ** min : -9999
                               ** default : 2
                               ** unit : m
                 * name: global_radiation
                               ** description : global radiation
                               ** inputtype : variable
                               ** variablecategory : exogenous
                               ** datatype : DOUBLE
                               ** max : 50
                               ** min : 0
                               ** default : 0
                               ** unit : MJ/m2
                 * name: julian_day
                               ** description : day of year
                               ** inputtype : variable
                               ** variablecategory : exogenous
                               ** datatype : INT
                               ** max : 366
                               ** min : 1
                               ** default : 1
                               ** unit : day
                 * name: latitude
                               ** description : latitude
                               ** inputtype : variable
                               ** variablecategory : exogenous
                               ** datatype : DOUBLE
                               ** max : 90
                               ** min : -90
                               ** default : 0
                               ** unit : degree
                 * name: evaporated_from_surface
                               ** description : evaporated_from_surface
                               ** inputtype : variable
                               ** variablecategory : state
                               ** datatype : DOUBLE
                               ** max : 
                               ** min : 0
                               ** default : 0
                               ** unit : mm
                 * name: snow_depth
                               ** description : depth of snow layer
                               ** inputtype : variable
                               ** variablecategory : state
                               ** datatype : DOUBLE
                               ** max : 
                               ** min : 0
                               ** default : 0
                               ** unit : mm
                 * name: developmental_stage
                               ** description : MONICA crop developmental stage
                               ** inputtype : variable
                               ** variablecategory : state
                               ** datatype : INT
                               ** max : 6
                               ** min : 0
                               ** default : 0
                               ** unit : dimensionless
                 * name: crop_reference_evapotranspiration
                               ** description : the crop specific ET0, if no external ET0 and crop is planted
                               ** inputtype : variable
                               ** variablecategory : state
                               ** datatype : DOUBLE
                               ** max : 
                               ** min : 0
                               ** default : -1
                               ** unit : mm
                 * name: reference_evapotranspiration
                               ** description : reference evapotranspiration (ET0)
                               ** inputtype : variable
                               ** variablecategory : state
                               ** datatype : DOUBLE
                               ** max : 
                               ** min : 0
                               ** default : 0
                               ** unit : mm
                 * name: actual_evaporation
                               ** description : actual evaporation
                               ** inputtype : variable
                               ** variablecategory : state
                               ** datatype : DOUBLE
                               ** max : 
                               ** min : 0
                               ** default : 0
                               ** unit : mm
                 * name: actual_transpiration
                               ** description : actual transpiration
                               ** inputtype : variable
                               ** variablecategory : state
                               ** datatype : DOUBLE
                               ** max : 
                               ** min : 0
                               ** default : 0
                               ** unit : mm
                 * name: surface_water_storage
                               ** description : Simulates a virtual layer that contains the surface water
                               ** inputtype : variable
                               ** variablecategory : state
                               ** datatype : DOUBLE
                               ** max : 
                               ** min : 0
                               ** default : 0
                               ** unit : mm
                 * name: kc_factor
                               ** description : crop coefficient ETc/ET0
                               ** inputtype : variable
                               ** variablecategory : state
                               ** datatype : DOUBLE
                               ** max : 
                               ** min : 0
                               ** default : 0.75
                               ** unit : dimensionless
                 * name: percentage_soil_coverage
                               ** description : fraction of soil covered by crop
                               ** inputtype : variable
                               ** variablecategory : state
                               ** datatype : DOUBLE
                               ** max : 1
                               ** min : 0
                               ** default : 0
                               ** unit : m2/m2
                 * name: soil_moisture
                               ** description : soil moisture array
                               ** inputtype : variable
                               ** variablecategory : state
                               ** datatype : DOUBLEARRAY
                               ** len : no_of_soil_layers
                               ** max : 
                               ** min : 
                               ** default : 
                               ** unit : m3/m3
                 * name: permanent_wilting_point
                               ** description : permanent wilting point array
                               ** inputtype : variable
                               ** variablecategory : state
                               ** datatype : DOUBLEARRAY
                               ** len : no_of_soil_layers
                               ** max : 2
                               ** min : 0
                               ** default : 0
                               ** unit : m3/m3
                 * name: field_capacity
                               ** description : field capacity array
                               ** inputtype : variable
                               ** variablecategory : state
                               ** datatype : DOUBLEARRAY
                               ** len : no_of_soil_layers
                               ** max : 1
                               ** min : 0
                               ** default : 0
                               ** unit : m3/m3
                 * name: evaporation
                               ** description : evaporation array
                               ** inputtype : variable
                               ** variablecategory : state
                               ** datatype : DOUBLEARRAY
                               ** len : no_of_soil_layers
                               ** max : 1
                               ** min : 0
                               ** default : 0
                               ** unit : mm
                 * name: transpiration
                               ** description : transpiration array
                               ** inputtype : variable
                               ** variablecategory : state
                               ** datatype : DOUBLEARRAY
                               ** len : no_of_soil_layers
                               ** max : 
                               ** min : 
                               ** default : 
                               ** unit : mm
                 * name: crop_transpiration
                               ** description : crop transpiration array
                               ** inputtype : variable
                               ** variablecategory : state
                               ** datatype : DOUBLEARRAY
                               ** len : no_of_soil_layers
                               ** max : 
                               ** min : 
                               ** default : 
                               ** unit : mm
                 * name: crop_remaining_evapotranspiration
                               ** description : crop remaining evapotranspiration
                               ** inputtype : variable
                               ** variablecategory : state
                               ** datatype : DOUBLE
                               ** max : 
                               ** min : 
                               ** default : 
                               ** unit : mm
                 * name: crop_evaporated_from_intercepted
                               ** description : crop evaporated water from intercepted water
                               ** inputtype : variable
                               ** variablecategory : state
                               ** datatype : DOUBLE
                               ** max : 
                               ** min : 
                               ** default : 
                               ** unit : mm
                 * name: evapotranspiration
                               ** description : evapotranspiration array
                               ** inputtype : variable
                               ** variablecategory : state
                               ** datatype : DOUBLEARRAY
                               ** len : no_of_soil_layers
                               ** max : 
                               ** min : 
                               ** default : 
                               ** unit : mm
                 * name: actual_evapotranspiration
                               ** description : actual evapotranspiration
                               ** inputtype : variable
                               ** variablecategory : state
                               ** datatype : DOUBLE
                               ** max : 
                               ** min : 0
                               ** default : 0
                               ** unit : mm
                 * name: vapor_pressure
                               ** description : vapor pressure
                               ** inputtype : variable
                               ** variablecategory : state
                               ** datatype : DOUBLE
                               ** max : 
                               ** min : 0
                               ** default : 0
                               ** unit : kPa
     - outputs:
                 * name: evaporated_from_surface
                               ** description : 
                               ** variablecategory : state
                               ** datatype : DOUBLE
                               ** max : 
                               ** min : 0
                               ** unit : mm
                 * name: actual_evapotranspiration
                               ** description : actual evapotranspiration
                               ** variablecategory : state
                               ** datatype : DOUBLE
                               ** max : 200
                               ** min : 0
                               ** unit : mm
    """

    evaporated_from_surface = 0.0
    potential_evapotranspiration:float = 0.0
    evaporated_from_intercept:float = 0.0
    if developmental_stage > 0:
        if external_reference_evapotranspiration < 0.0:
            reference_evapotranspiration = reference_evapotranspiration
        else:
            reference_evapotranspiration = external_reference_evapotranspiration
        potential_evapotranspiration = crop_remaining_evapotranspiration
        evaporated_from_intercept = crop_evaporated_from_intercepted
    else:
        if external_reference_evapotranspiration < 0.0:
            (reference_evapotranspiration, vapor_pressure) = calc_reference_evapotranspiration(height_nn, max_air_temperature, min_air_temperature, relative_humidity, mean_air_temperature, wind_speed, wind_speed_height, global_radiation, julian_day, latitude, reference_albedo, vapor_pressure, stomata_resistance)
        else:
            reference_evapotranspiration = external_reference_evapotranspiration
        potential_evapotranspiration = reference_evapotranspiration * kc_factor
    actual_evaporation = 0.0
    actual_transpiration = 0.0
    if potential_evapotranspiration > 6.5:
        potential_evapotranspiration = 6.5
    evaporation_from_surface:bool = False
    eRed1:float
    eRed2:float
    eRed3:float
    eReducer:float
    i:int
    if potential_evapotranspiration > 0.0:
        evaporation_from_surface = False
        if surface_water_storage > 0.0:
            evaporation_from_surface = True
            potential_evapotranspiration = potential_evapotranspiration * 1.1 / kc_factor
            if snow_depth > 0.0:
                evaporated_from_surface = 0.0
            elif surface_water_storage < potential_evapotranspiration:
                potential_evapotranspiration = potential_evapotranspiration - surface_water_storage
                evaporated_from_surface = surface_water_storage
                surface_water_storage = 0.0
            else:
                surface_water_storage = surface_water_storage - potential_evapotranspiration
                evaporated_from_surface = potential_evapotranspiration
                potential_evapotranspiration = 0.0
            potential_evapotranspiration = potential_evapotranspiration * kc_factor / 1.1
        if potential_evapotranspiration > 0.0:
            for i in range(0 , no_of_soil_layers , 1):
                eRed1 = e_reducer_1(permanent_wilting_point[i], field_capacity[i], soil_moisture[i], percentage_soil_coverage, potential_evapotranspiration, evaporation_reduction_method, xsa_critical_soil_moisture)
                eRed2 = 0.0
                if float(i) >= maximum_evaporation_impact_depth:
                    eRed2 = 0.0
                else:
                    eRed2 = get_deprivation_factor(i + 1, maximum_evaporation_impact_depth, evaporation_zeta, layer_thickness[i])
                eRed3 = 0.0
                if i > 0 and soil_moisture[i] < soil_moisture[i - 1]:
                    eRed3 = 0.1
                else:
                    eRed3 = 1.0
                eReducer = eRed1 * eRed2 * eRed3
                if developmental_stage > 0:
                    if percentage_soil_coverage >= 0.0 and percentage_soil_coverage < 1.0:
                        evaporation[i] = (1.0 - percentage_soil_coverage) * eReducer * potential_evapotranspiration
                    elif percentage_soil_coverage >= 1.0:
                        evaporation[i] = 0.0
                    if snow_depth > 0.0:
                        evaporation[i] = 0.0
                    transpiration[i] = crop_transpiration[i]
                    if evaporation_from_surface:
                        transpiration[i] = percentage_soil_coverage * eReducer * potential_evapotranspiration
                else:
                    if snow_depth > 0.0:
                        evaporation[i] = 0.0
                    else:
                        evaporation[i] = potential_evapotranspiration * eReducer
                        transpiration[i] = 0.0
                evapotranspiration[i] = evaporation[i] + transpiration[i]
                soil_moisture[i] = soil_moisture[i] - (evapotranspiration[i] / 1000.0 / layer_thickness[i])
                if soil_moisture[i] < 0.01:
                    soil_moisture[i] = 0.01
                actual_transpiration = actual_transpiration + transpiration[i]
                actual_evaporation = actual_evaporation + evaporation[i]
        actual_evapotranspiration = actual_transpiration + actual_evaporation + evaporated_from_intercept + evaporated_from_surface
    return (evaporated_from_surface, actual_evapotranspiration)
#%%CyML Model End%%

def get_deprivation_factor(layer_no:int,
         deprivation_depth:float,
         zeta:float,
         layer_thickness:float):
    ltf:float
    ltf = deprivation_depth / (layer_thickness * 10.0)
    deprivation_factor:float
    c2:float
    c3:float
    if abs(zeta) < 0.0003:
        deprivation_factor = 2.0 / ltf - (1.0 / (ltf * ltf) * (2 * layer_no - 1))
    else:
        c2 = log((ltf + (zeta * layer_no)) / (ltf + (zeta * (layer_no - 1))))
        c3 = zeta / (ltf * (zeta + 1.0))
        deprivation_factor = (c2 - c3) / (log(zeta + 1.0) - (zeta / (zeta + 1.0)))
    return deprivation_factor

def bound(lower:float,
         value:float,
         upper:float):
    if value < lower:
        return lower
    if value > upper:
        return upper
    return value

def calc_reference_evapotranspiration(height_nn:float,
         max_air_temperature:float,
         min_air_temperature:float,
         relative_humidity:float,
         mean_air_temperature:float,
         wind_speed:float,
         wind_speed_height:float,
         global_radiation:float,
         julian_day:int,
         latitude:float,
         reference_albedo:float,
         vapor_pressure:float,
         stomata_resistance:float):
    declination:float
    declination = -23.4 * cos(2.0 * pi * ((julian_day + 10.0) / 365.0))
    declination_sinus:float
    declination_sinus = sin(declination * pi / 180.0) * sin(latitude * pi / 180.0)
    declination_cosinus:float
    declination_cosinus = cos(declination * pi / 180.0) * cos(latitude * pi / 180.0)
    arg_astro_day_length:float
    arg_astro_day_length = declination_sinus / declination_cosinus
    arg_astro_day_length = bound(-1.0, arg_astro_day_length, 1.0)
    astronomic_day_length:float
    astronomic_day_length = 12.0 * (pi + (2.0 * asin(arg_astro_day_length))) / pi
    arg_effective_day_length:float
    arg_effective_day_length = (-sin((8.0 * pi / 180.0)) + declination_sinus) / declination_cosinus
    arg_effective_day_length = bound(-1.0, arg_effective_day_length, 1.0)
    arg_photo_day_length:float
    arg_photo_day_length = (-sin((-6.0 * pi / 180.0)) + declination_sinus) / declination_cosinus
    arg_photo_day_length = bound(-1.0, arg_photo_day_length, 1.0)
    arg_phot_act:float
    arg_phot_act = min(1.0, declination_sinus / declination_cosinus * (declination_sinus / declination_cosinus))
    phot_act_radiation_mean:float
    phot_act_radiation_mean = 3600.0 * (declination_sinus * astronomic_day_length + (24.0 / pi * declination_cosinus * sqrt((1.0 - arg_phot_act))))
    clear_day_radiation:float = 0.0
    if phot_act_radiation_mean > 0.0 and astronomic_day_length > 0.0:
        clear_day_radiation = 0.5 * 1300.0 * phot_act_radiation_mean * exp(-0.14 / (phot_act_radiation_mean / (astronomic_day_length * 3600.0)))
    SC:float
    SC = 24.0 * 60.0 / pi * 8.20 * (1.0 + (0.033 * cos(2.0 * pi * julian_day / 365.0)))
    arg_SHA:float
    arg_SHA = bound(-1.0, -tan((latitude * pi / 180.0)) * tan(declination * pi / 180.0), 1.0)
    SHA:float
    SHA = acos(arg_SHA)
    extraterrestrial_radiation:float
    extraterrestrial_radiation = SC * (SHA * declination_sinus + (declination_cosinus * sin(SHA))) / 100.0
    atmospheric_pressure:float
    atmospheric_pressure = 101.3 * pow((293.0 - (0.0065 * height_nn)) / 293.0, 5.26)
    psycrometer_constant:float
    psycrometer_constant = 0.000665 * atmospheric_pressure
    saturated_vapor_pressure_max:float
    saturated_vapor_pressure_max = 0.6108 * exp(17.27 * max_air_temperature / (237.3 + max_air_temperature))
    saturated_vapor_pressure_min:float
    saturated_vapor_pressure_min = 0.6108 * exp(17.27 * min_air_temperature / (237.3 + min_air_temperature))
    saturated_vapor_pressure:float
    saturated_vapor_pressure = (saturated_vapor_pressure_max + saturated_vapor_pressure_min) / 2.0
    if vapor_pressure < 0.0:
        if relative_humidity <= 0.0:
            vapor_pressure = saturated_vapor_pressure_min
        else:
            vapor_pressure = relative_humidity * saturated_vapor_pressure
    saturation_deficit:float
    saturation_deficit = saturated_vapor_pressure - vapor_pressure
    saturated_vapour_pressure_slope:float
    saturated_vapour_pressure_slope = 4098.0 * (0.6108 * exp(17.27 * mean_air_temperature / (mean_air_temperature + 237.3))) / ((mean_air_temperature + 237.3) * (mean_air_temperature + 237.3))
    wind_speed_2m:float
    wind_speed_2m = max(0.5, wind_speed * (4.87 / log((67.8 * wind_speed_height - 5.42))))
    surface_resistance:float
    surface_resistance = stomata_resistance / 1.44
    clear_sky_solar_radiation:float
    clear_sky_solar_radiation = (0.75 + (0.00002 * height_nn)) * extraterrestrial_radiation
    relative_shortwave_radiation:float
    relative_shortwave_radiation = min(global_radiation / clear_sky_solar_radiation, 1.0) if clear_sky_solar_radiation > 0.0 else 1.0
    bolzmann_constant:float = 0.0000000049
    shortwave_radiation:float
    shortwave_radiation = (1.0 - reference_albedo) * global_radiation
    longwave_radiation:float
    longwave_radiation = bolzmann_constant * ((pow(min_air_temperature + 273.16, 4.0) + pow(max_air_temperature + 273.16, 4.0)) / 2.0) * (1.35 * relative_shortwave_radiation - 0.35) * (0.34 - (0.14 * sqrt(vapor_pressure)))
    net_radiation:float
    net_radiation = shortwave_radiation - longwave_radiation
    reference_evapotranspiration:float
    reference_evapotranspiration = (0.408 * saturated_vapour_pressure_slope * net_radiation + (psycrometer_constant * (900.0 / (mean_air_temperature + 273.0)) * wind_speed_2m * saturation_deficit)) / (saturated_vapour_pressure_slope + (psycrometer_constant * (1.0 + (surface_resistance / 208.0 * wind_speed_2m))))
    if reference_evapotranspiration < 0.0:
        reference_evapotranspiration = 0.0
    return (reference_evapotranspiration, vapor_pressure)

def e_reducer_1(pwp:float,
         fc:float,
         sm:float,
         percentage_soil_coverage:float,
         reference_evapotranspiration:float,
         evaporation_reduction_method:int,
         xsa_critical_soil_moisture:float):
    sm = max(0.33 * pwp, sm)
    relative_evaporable_water:float
    relative_evaporable_water = min(1.0, (sm - (0.33 * pwp)) / (fc - (0.33 * pwp)))
    e_reduction_factor:float = 0.0
    critical_soil_moisture:float
    reducer:float
    xsa:float
    if evaporation_reduction_method == 0:
        critical_soil_moisture = 0.65 * fc
        if percentage_soil_coverage > 0.0:
            reducer = 1.0
            if reference_evapotranspiration > 2.5:
                xsa = (0.65 * fc - pwp) * (fc - pwp)
                reducer = xsa + ((1 - xsa) / 17.5 * (reference_evapotranspiration - 2.5))
            else:
                reducer = xsa_critical_soil_moisture / 2.5 * reference_evapotranspiration
            critical_soil_moisture = fc * reducer
        if sm > critical_soil_moisture:
            e_reduction_factor = 1.0
        elif sm > (0.33 * pwp):
            e_reduction_factor = relative_evaporable_water
        else:
            e_reduction_factor = 0.0
    else:
        if relative_evaporable_water > 0.33:
            e_reduction_factor = 1.0 - (0.1 * (1.0 - relative_evaporable_water) / (1.0 - 0.33))
        elif relative_evaporable_water > 0.22:
            e_reduction_factor = 0.9 - (0.625 * (0.33 - relative_evaporable_water) / (0.33 - 0.22))
        elif relative_evaporable_water > 0.2:
            e_reduction_factor = 0.275 - (0.225 * (0.22 - relative_evaporable_water) / (0.22 - 0.2))
        else:
            e_reduction_factor = 0.05 - (0.05 * (0.2 - relative_evaporable_water) / 0.2)
    return e_reduction_factor