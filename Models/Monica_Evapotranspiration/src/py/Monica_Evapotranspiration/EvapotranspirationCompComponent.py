# coding: utf8
from copy import copy
from array import array
from math import *
from typing import *
from datetime import datetime

from Monica_Evapotranspiration.evapotranspiration import model_evapotranspiration

#%%CyML Model Begin%%
def model_evapotranspirationcomp(evaporation_zeta:float,
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
         surface_water_storage:float,
         snow_depth:float,
         developmental_stage:int,
         crop_reference_evapotranspiration:float,
         reference_evapotranspiration:float,
         actual_evaporation:float,
         actual_transpiration:float,
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
     - Name: EvapotranspirationComp -Version: 1, -Time step: 1
     - Description:
                 * Title: Evapotranspiration model
                 * Authors: Michael Berg-Mohnicke
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
                 * name: surface_water_storage
                               ** description : Simulates a virtual layer that contains the surface water
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

    (evaporated_from_surface, actual_evapotranspiration) = model_evapotranspiration(evaporation_zeta, maximum_evaporation_impact_depth, no_of_soil_layers, layer_thickness, reference_albedo, stomata_resistance, evaporation_reduction_method, xsa_critical_soil_moisture, external_reference_evapotranspiration, height_nn, max_air_temperature, min_air_temperature, mean_air_temperature, relative_humidity, wind_speed, wind_speed_height, global_radiation, julian_day, latitude, evaporated_from_surface, snow_depth, developmental_stage, crop_reference_evapotranspiration, reference_evapotranspiration, actual_evaporation, actual_transpiration, surface_water_storage, kc_factor, percentage_soil_coverage, soil_moisture, permanent_wilting_point, field_capacity, evaporation, transpiration, crop_transpiration, crop_remaining_evapotranspiration, crop_evaporated_from_intercepted, evapotranspiration, actual_evapotranspiration, vapor_pressure)
    return (evaporated_from_surface, actual_evapotranspiration)
#%%CyML Model End%%