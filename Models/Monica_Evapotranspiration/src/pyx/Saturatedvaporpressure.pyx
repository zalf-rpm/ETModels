import numpy
from math import *

def model_saturatedvaporpressure(float max_air_temperature,
                                 float min_air_temperature,
                                 float relative_humidity):
    """
    MONICA saturated vapor pressure
    Author: Claas Nendel (transcription into Crop2ML by Michael Berg-Mohnicke)
    Reference: 
        
    Institution: ZALF e.V.
    ExtendedDescription: 
        
    ShortDescription: Calculates saturated vapor pressure as in the MONICA model
    """

    cdef float saturated_vapor_pressure
    cdef float vapor_pressure
    # Calc. of saturated water vapor pressure at daily max temperature [kPA]
    cdef float saturated_vapor_pressure_max
    saturated_vapor_pressure_max = 0.6108 * exp((17.27 * max_air_temperature) / (237.3 + max_air_temperature))
    # Calc. of saturated water vapor pressure at daily min temperature [kPA]
    cdef float saturated_vapor_pressure_min
    saturated_vapor_pressure_min = 0.6108 * exp((17.27 * min_air_temperature) / (237.3 + min_air_temperature))
    # Calculation of the saturated water vapor pressure [kPA]
    saturated_vapor_pressure = (saturated_vapor_pressure_max + saturated_vapor_pressure_min) / 2.0
    # Calculation of the water vapor pressure
    if relative_humidity <= 0.0:
        # Assuming Tdew = Tmin as suggested in FAO56 Allen et al. 1998
        vapor_pressure = saturated_vapor_pressure_min
    else:
        vapor_pressure = relative_humidity * saturated_vapor_pressure
    return  saturated_vapor_pressure, vapor_pressure



