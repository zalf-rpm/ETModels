from datetime import datetime
from math import *
from Monica_AMEI_potential_Evapotranspiration.saturationvaporpressuredeficit import model_saturationvaporpressuredeficit
from Monica_AMEI_potential_Evapotranspiration.referenceevapotranspiration import model_referenceevapotranspiration
from Monica_AMEI_potential_Evapotranspiration.potentialevapotranspiration import model_potentialevapotranspiration
def model_ameipotet(float vapor_pressure,
      float saturated_vapor_pressure,
      float height_nn,
      float mean_air_temperature,
      float wind_speed,
      float wind_speed_height,
      float stomata_resistance,
      float net_radiation,
      float kc_factor,
      int developmental_stage,
      float crop_remaining_evapotranspiration):
    cdef float saturation_vapor_pressure_deficit
    cdef float reference_evapotranspiration
    cdef float potential_evapotranspiration_cap
    cdef float potential_evapotranspiration
    saturation_vapor_pressure_deficit = model_saturationvaporpressuredeficit(vapor_pressure,saturated_vapor_pressure)
    reference_evapotranspiration = model_referenceevapotranspiration(height_nn,mean_air_temperature,wind_speed,wind_speed_height,net_radiation,stomata_resistance,saturation_vapor_pressure_deficit)
    potential_evapotranspiration = model_potentialevapotranspiration(potential_evapotranspiration_cap,kc_factor,developmental_stage,crop_remaining_evapotranspiration,reference_evapotranspiration)

    return (reference_evapotranspiration, potential_evapotranspiration)