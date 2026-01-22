import numpy
from math import *

def model_saturationvaporpressuredeficit(float vapor_pressure,
                                         float saturated_vapor_pressure):
    """
    MONICA saturation vapor pressure deficit
    Author: Claas Nendel (transcription into Crop2ML by Michael Berg-Mohnicke)
    Reference: 
        
    Institution: ZALF e.V.
    ExtendedDescription: 
        
    ShortDescription: Calculates saturation vapor pressure deficit as in the MONICA model
    """

    cdef float saturation_vapor_pressure_deficit
    # Calculation of the air saturation deficit [kPA]
    saturation_vapor_pressure_deficit = saturated_vapor_pressure - vapor_pressure
    return  saturation_vapor_pressure_deficit



