import numpy
from math import *

def init_reference_evapotranspiration(float reference_albedo,
                                      float stomata_resistance,
                                      float latitude,
                                      float height_nn,
                                      float max_air_temperature,
                                      float min_air_temperature,
                                      float mean_air_temperature,
                                      float relative_humidity,
                                      float wind_speed,
                                      float wind_speed_height,
                                      float global_radiation,
                                      int julian_day,
                                      float vapor_pressure):
    cdef float net_radiation = 0.0
    cdef float reference_evapotranspiration = 0.0

    return  net_radiation, reference_evapotranspiration

def model_reference_evapotranspiration(float reference_albedo,
                                       float stomata_resistance,
                                       float latitude,
                                       float height_nn,
                                       float max_air_temperature,
                                       float min_air_temperature,
                                       float mean_air_temperature,
                                       float relative_humidity,
                                       float wind_speed,
                                       float wind_speed_height,
                                       float global_radiation,
                                       int julian_day,
                                       float vapor_pressure,
                                       float net_radiation,
                                       float reference_evapotranspiration):
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

    cdef float declination
    declination = -23.4 * cos(2.0 * pi * ((julian_day + 10.0) / 365.0))
    # old SINLD
    cdef float declination_sinus
    declination_sinus = sin(declination * pi / 180.0) * sin(latitude * pi / 180.0);
    # old COSLD
    cdef float declination_cosinus
    declination_cosinus = cos(declination * pi / 180.0) * cos(latitude * pi / 180.0);
    cdef float arg_astro_day_length
    arg_astro_day_length = declination_sinus / declination_cosinus
    # The argument of asin must be in the range of - 1 to 1
    arg_astro_day_length = bound(-1.0, arg_astro_day_length, 1.0)
    cdef float astronomic_day_length
    astronomic_day_length = 12.0 * (pi + 2.0 * asin(arg_astro_day_length)) / pi
    cdef float arg_effective_day_length
    arg_effective_day_length = (-sin(8.0 * pi / 180.0) + declination_sinus) / declination_cosinus
    # The argument of asin must be in the range of -1 to 1
    arg_effective_day_length = bound(-1.0, arg_effective_day_length, 1.0)
    #cdef float arg_effective_day_length
    #arg_effective_day_length = 12.0 * (pi + 2.0 * asin(arg_effective_day_length)) / pi
    cdef float arg_photo_day_length
    arg_photo_day_length = (-sin(-6.0 * pi / 180.0) + declination_sinus) / declination_cosinus
    # The argument of asin must be in the range of - 1 to 1
    arg_photo_day_length = bound(-1.0, arg_photo_day_length, 1.0)
    #cdef float photoperiodic_day_length
    #photoperiodic_day_length = 12.0 * (pi + 2.0 * asin(arg_photo_day_length)) / pi;
    # The argument of sqrt must be >= 0
    cdef float arg_phot_act
    arg_phot_act = min(1.0, ((declination_sinus / declination_cosinus) * (declination_sinus / declination_cosinus)))
    cdef float phot_act_radiation_mean
    phot_act_radiation_mean = 3600.0 * (declination_sinus * astronomic_day_length + 24.0 / pi *
                                               declination_cosinus
                                               * sqrt(1.0 - arg_phot_act))
    cdef float clear_day_radiation = 0.0
    if phot_act_radiation_mean > 0.0 and astronomic_day_length > 0.0:
        clear_day_radiation = 0.5 * 1300.0 * phot_act_radiation_mean * exp(-0.14 / (phot_act_radiation_mean
                                                                         / (astronomic_day_length * 3600.0)))
    #double vc_OvercastDayRadiation = 0.2 * clear_day_radiation;
    cdef float SC
    SC = 24.0 * 60.0 / pi * 8.20 * (1.0 + 0.033 * cos(2.0 * pi * julian_day / 365.0));
    # The argument of acos must be in the range of -1 to 1
    cdef float arg_SHA
    arg_SHA = bound(-1.0, -tan(latitude * pi / 180.0) * tan(declination * pi / 180.0), 1.0)
    cdef float SHA
    SHA = acos(arg_SHA)
    # [J cm-2] --> [MJ m-2]
    cdef float extraterrestrial_radiation
    extraterrestrial_radiation = SC * (SHA * declination_sinus + declination_cosinus * sin(SHA)) / 100.0
    # Calculation of atmospheric pressure //[kPA]
    cdef float atmospheric_pressure
    atmospheric_pressure = 101.3 * pow(((293.0 - (0.0065 * height_nn)) / 293.0), 5.26)
    # Calculation of psychrometer constant [kPA °C-1] - air humidity
    cdef float psycrometer_constant
    psycrometer_constant = 0.000665 * atmospheric_pressure
    # Calc. of saturated water vapor pressure at daily max temperature [kPA]
    cdef float saturated_vapor_pressure_max
    saturated_vapor_pressure_max = 0.6108 * exp((17.27 * max_air_temperature) / (237.3 + max_air_temperature))
    # Calc. of saturated water vapor pressure at daily min temperature [kPA]
    cdef float saturated_vapor_pressure_min
    saturated_vapor_pressure_min = 0.6108 * exp((17.27 * min_air_temperature) / (237.3 + min_air_temperature))
    # Calculation of the saturated water vapor pressure [kPA]
    cdef float saturated_vapor_pressure
    saturated_vapor_pressure = (saturated_vapor_pressure_max + saturated_vapor_pressure_min) / 2.0
    if vapor_pressure < 0.0:
        # Calculation of the water vapor pressure
        if relative_humidity <= 0.0:
            # Assuming Tdew = Tmin as suggested in FAO56 Allen et al. 1998
            vapor_pressure = saturated_vapor_pressure_min
        else:
            vapor_pressure = relative_humidity * saturated_vapor_pressure
    # Calculation of the air saturation deficit [kPA]
    cdef float saturation_deficit
    saturation_deficit = saturated_vapor_pressure - vapor_pressure
    # Slope of saturation water vapor pressure-to-temperature relation [kPA °C-1]
    cdef float saturated_vapour_pressure_slope
    saturated_vapour_pressure_slope = (4098.0 * (0.6108 * exp((17.27 * mean_air_temperature) / (
            mean_air_temperature
            + 237.3)))) / ((mean_air_temperature + 237.3) * (mean_air_temperature + 237.3))
    # Calculation of wind speed in 2m height //[m s-1]
    cdef float wind_speed_2m
    wind_speed_2m = max(0.5, wind_speed * (4.87 / (log(67.8 * wind_speed_height - 5.42))))
    # 0.5 minimum allowed wind speed for Penman-Monteith-Method FAO
    # Calculation of the aerodynamic resistance [s m-1]
    #cdef float aerodynamic_resistance
    #aerodynamic_resistance = 208.0 / wind_speed_2m
    # FAO default value [s m-1]
    # stomata_resistance = 100
    cdef float surface_resistance
    surface_resistance = stomata_resistance / 1.44 # [s m - 1]
    cdef float clear_sky_solar_radiation
    clear_sky_solar_radiation = (0.75 + 0.00002 * height_nn) * extraterrestrial_radiation
    cdef float relative_shortwave_radiation
    relative_shortwave_radiation = min(global_radiation / clear_sky_solar_radiation, 1.0) if clear_sky_solar_radiation > 0.0 else 1.0
    cdef float bolzmann_constant = 0.0000000049
    # FAO Green gras reference albedo from Allen et al. (1998)
    cdef float shortwave_radiation
    shortwave_radiation = (1.0 - reference_albedo) * global_radiation
    cdef float longwave_radiation
    longwave_radiation = bolzmann_constant * ((pow((min_air_temperature + 273.16), 4.0)
                                               + pow((max_air_temperature + 273.16), 4.0)) / 2.0) \
                                               * (1.35 * relative_shortwave_radiation - 0.35) \
                                               * (0.34 - 0.14 * sqrt(vapor_pressure))
    net_radiation = shortwave_radiation - longwave_radiation
    # Calculation of the reference evapotranspiration
    # Penman-Monteith-Methode FAO
    # [mm]
    reference_evapotranspiration = ((0.408 * saturated_vapour_pressure_slope * net_radiation)
                                           + (psycrometer_constant * (900.0 / (mean_air_temperature + 273.0))
                                              * wind_speed_2m * saturation_deficit)) \
                                              / (saturated_vapour_pressure_slope + psycrometer_constant
                                                 * (1.0 + (surface_resistance / 208.0) * wind_speed_2m))
    if reference_evapotranspiration < 0.0:
      reference_evapotranspiration = 0.0
    return reference_evapotranspiration, net_radiation
    return  reference_evapotranspiration, net_radiation



def bound(float lower, float value, float upper):
    if value < lower:
        return lower
    if value > upper:
        return upper
    return value

