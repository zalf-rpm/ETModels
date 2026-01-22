import numpy
from math import *

def model_referenceevapotranspiration(float height_nn,
                                      float mean_air_temperature,
                                      float wind_speed,
                                      float wind_speed_height,
                                      float net_radiation,
                                      float stomata_resistance,
                                      float saturation_vapor_pressure_deficit):
    """
    MONICA reference evapotranspiration
    Author: Claas Nendel (transcription into Crop2ML by Michael Berg-Mohnicke)
    Reference: 
            Allen RG, Pereira LS, Raes D, Smith M. (1998) Crop evapotranspiration. Guidelines for computing crop water requirements. FAO Irrigation and
            Drainage Paper 56, FAO, Roma
        
    Institution: ZALF e.V.
    ExtendedDescription: 
            A method following Penman-Monteith as described by the FAO in Allen
            RG, Pereira LS, Raes D, Smith M. (1998) Crop evapotranspiration.
            Guidelines for computing crop water requirements. FAO Irrigation and
            Drainage Paper 56, FAO, Roma
        
    ShortDescription: Calculates the MONICA reference evapotranspiration
    """

    cdef float reference_evapotranspiration
    # Calculation of atmospheric pressure //[kPA]
    cdef float atmospheric_pressure
    atmospheric_pressure = 101.3 * pow(((293.0 - (0.0065 * height_nn)) / 293.0), 5.26)
    # Calculation of psychrometer constant [kPA Â°C-1] - air humidity
    cdef float psycrometer_constant
    psycrometer_constant = 0.000665 * atmospheric_pressure
    # Slope of saturation water vapor pressure-to-temperature relation [kPA Â°C-1]
    cdef float saturated_vapour_pressure_slope
    saturated_vapour_pressure_slope = (4098.0 * (0.6108 * exp((17.27 * mean_air_temperature) / (
            mean_air_temperature + 237.3)))) / ((mean_air_temperature + 237.3) * (mean_air_temperature + 237.3))
    # Calculation of wind speed in 2m height //[m s-1]
    cdef float wind_speed_2m
    wind_speed_2m = max(0.5, wind_speed * (4.87 / (log(67.8 * wind_speed_height - 5.42))))
    # 0.5 minimum allowed wind speed for Penman-Monteith-Method FAO
    # Calculation of the aerodynamic resistance [s m-1]
    cdef float aerodynamic_resistance
    aerodynamic_resistance = 208.0 / wind_speed_2m
    cdef float surface_resistance
    surface_resistance = stomata_resistance / 1.44 # [s m - 1]
    # Calculation of the reference evapotranspiration
    # Penman-Monteith-Methode FAO
    # [mm]
    reference_evapotranspiration = ((0.408 * saturated_vapour_pressure_slope * net_radiation)
                                           + (psycrometer_constant * (900.0 / (mean_air_temperature + 273.0))
                                              * wind_speed_2m * saturation_vapor_pressure_deficit)) \
                                              / (saturated_vapour_pressure_slope + psycrometer_constant
                                                 #* (1.0 + (surface_resistance / 208.0) * wind_speed_2m))
                                                 * (1.0 + surface_resistance / aerodynamic_resistance))
    if reference_evapotranspiration < 0.0:
      reference_evapotranspiration = 0.0
    return  reference_evapotranspiration



