import numpy
from math import *

def model_radiation(float latitude,
                    float global_radiation,
                    int julian_day,
                    float sunshine_hours):
    """
    MONICA radiation calculations
    Author: Claas Nendel (transcription into Crop2ML by Michael Berg-Mohnicke)
    Reference: 
            Taken from the original HERMES model, Kersebaum, K.C. and Richter J.
            (1991): Modelling nitrogen dynamics in a plant-soil system with a
            simple model for advisory purposes. Fert. Res. 27 (2-3), 273 - 281.
        
    Institution: ZALF e.V.
    ExtendedDescription: 
        
    ShortDescription: Calculates the MONICA reference evapotranspiration
    """

    cdef float declination
    cdef float astronomic_daylength
    cdef float effective_daylength
    cdef float photoperiodic_daylength
    cdef float sunshine_hours_global_radiation
    cdef float extraterrestrial_radiation
    cdef float clear_day_radiation
    cdef float overcast_day_radiation
    cdef float phot_act_radiation_mean
    cdef float decl_sin
    cdef float decl_cos
    declination, decl_sin, decl_cos = calc_declinations(latitude, julian_day)
    astronomic_daylength = calc_astronomic_daylength(latitude, julian_day)
    effective_daylength = calc_effective_daylength(latitude, julian_day)
    photoperiodic_daylength = calc_photoperiodic_daylength(latitude, julian_day)
    # The argument of sqrt must be >= 0
    cdef float arg_phot_act
    arg_phot_act = min(1.0, ((decl_sin / decl_cos) * (decl_sin / decl_cos)))
    #cdef float phot_act_radiation_mean
    phot_act_radiation_mean = 3600.0 * (decl_sin * astronomic_daylength + 24.0
                                        / pi * decl_cos * sqrt(1.0 - arg_phot_act))
    #cdef float clear_day_radiation = 0.0
    if phot_act_radiation_mean > 0.0 and astronomic_daylength > 0.0:
        clear_day_radiation = (0.5 * 1300.0 * phot_act_radiation_mean
                               * exp(-0.14 / (phot_act_radiation_mean / (astronomic_daylength * 3600.0))))
    # Calculation of radiation on an overcast day [J m-2] - old DRO
    #cdef float overcast_day_radiation
    overcast_day_radiation = 0.2 * clear_day_radiation
    # Calculation of extraterrestrial radiation - old EXT
    cdef float solar_constant
    solar_constant = 0.082 # [MJ m-2 d-1] Note: Here is the difference to HERMES, which calculates in [J cm-2 d-1]!
    cdef float SC
    SC = 24.0 * 60.0 / pi * solar_constant * (1.0 + 0.033 * cos(2.0 * pi * julian_day / 365.0))
    cdef float solar_angle
    solar_angle = -tan(latitude * pi / 180.0) * tan(declination * pi / 180.0)
    solar_angle = bound(-1.0, solar_angle, 1.0)
    cdef float sunset_solar_angle
    sunset_solar_angle = acos(solar_angle)
    #cdef float extraterrestrial_radiation # [MJ m - 2]
    extraterrestrial_radiation = SC * (sunset_solar_angle * decl_sin + decl_cos * sin(sunset_solar_angle))
    if sunshine_hours > 0.0:
        if global_radiation <= 0.0 and astronomic_daylength > 0.0:
            sunshine_hours_global_radiation = extraterrestrial_radiation * (0.19 + 0.55 * sunshine_hours / astronomic_daylength)
        else:
            sunshine_hours_global_radiation = 0.0
    return  declination, astronomic_daylength, effective_daylength, photoperiodic_daylength, sunshine_hours_global_radiation, extraterrestrial_radiation, clear_day_radiation, overcast_day_radiation, phot_act_radiation_mean



def bound(float lower, float value, float upper):
    if value < lower:
        return lower
    if value > upper:
        return upper
    return value

def calc_declinations(float latitude, int julian_day):
  # calculation of declination - old DEC
  cdef float declination
  declination = -23.4 * cos(2.0 * pi * ((julian_day + 10.0) / 365.0))

  # old SINLD
  cdef float decl_sin
  decl_sin = sin(declination * pi / 180.0) * sin(latitude * pi / 180.0)

  # old COSLD
  cdef float decl_cos
  decl_cos = cos(declination * pi / 180.0) * cos(latitude * pi / 180.0)

  return declination, decl_sin, decl_cos



def calc_astronomic_daylength(float latitude, int julian_day):
  cdef float declination
  cdef float decl_sin
  cdef float decl_cos
  declination, decl_sin, decl_cos = calc_declinations(latitude, julian_day)

  # Calculation of the atmospheric day length -> old DL
  cdef float astro_daylength
  astro_daylength = decl_sin / decl_cos
  astro_daylength = bound(-1.0, astro_daylength, 1.0) # The argument of asin must be in the range of -1 to 1
  cdef float astronomic_daylength
  astronomic_daylength = 12.0 * (pi + 2.0 * asin(astronomic_daylength)) / pi

  return astronomic_daylength



def calc_effective_daylength(float latitude, int julian_day):
  cdef float declination
  cdef float decl_sin
  cdef float decl_cos
  declination, decl_sin, decl_cos = calc_declinations(latitude, julian_day)

  # Calculation of the effective day length = old DLE
  cdef float eff_daylength
  eff_daylength = (-sin(8.0 * pi / 180.0) + decl_sin) / decl_cos
  eff_daylength = bound(-1.0, eff_daylength, 1.0) # The argument of asin must be in the range of -1 to 1
  cdef float effective_daylength
  effective_daylength = 12.0 * (pi + 2.0 * asin(eff_daylength)) / pi

  return effective_daylength



def calc_photoperiodic_daylength(float latitude, int julian_day):
  cdef float declination
  cdef float decl_sin
  cdef float decl_cos
  declination, decl_sin, decl_cos = calc_declinations(latitude, julian_day)

  # old DLP
  cdef float photo_daylength
  photo_daylength = (-sin(-6.0 * pi / 180.0) + decl_sin) / decl_cos
  photo_daylength = bound(-1.0, photo_daylength, 1.0) # The argument of asin must be in the range of -1 to 1
  cdef float photoperiodic_daylength
  photoperiodic_daylength = 12.0 * (pi + 2.0 * asin(photo_daylength)) / pi

  return photoperiodic_daylength

