import numpy
from math import *

def model_evapotranspiration(float evaporation_zeta,
                             float maximum_evaporation_impact_depth,
                             int no_of_soil_layers,
                             float layer_thickness[no_of_soil_layers],
                             float reference_albedo,
                             float stomata_resistance,
                             int evaporation_reduction_method,
                             float xsa_critical_soil_moisture,
                             float external_reference_evapotranspiration,
                             float height_nn,
                             float max_air_temperature,
                             float min_air_temperature,
                             float mean_air_temperature,
                             float relative_humidity,
                             float wind_speed,
                             float wind_speed_height,
                             float global_radiation,
                             int julian_day,
                             float latitude,
                             float evaporated_from_surface,
                             float surface_water_storage,
                             float snow_depth,
                             int developmental_stage,
                             float crop_reference_evapotranspiration,
                             float reference_evapotranspiration,
                             float actual_evaporation,
                             float actual_transpiration,
                             float surface_water_storage,
                             float kc_factor,
                             float percentage_soil_coverage,
                             float soil_moisture[no_of_soil_layers],
                             float permanent_wilting_point[no_of_soil_layers],
                             float field_capacity[no_of_soil_layers],
                             float evaporation[no_of_soil_layers],
                             float transpiration[no_of_soil_layers],
                             float crop_transpiration[no_of_soil_layers],
                             float crop_remaining_evapotranspiration,
                             float crop_evaporated_from_intercepted,
                             float evapotranspiration[no_of_soil_layers],
                             float actual_evapotranspiration,
                             float vapor_pressure):
    """
    Model of evapotranspiration
    Author: Claas Nendel (transcription into Crop2ML by Michael Berg-Mohnicke)
    Reference: None
    Institution: ZALF e.V.
    ExtendedDescription: None
    ShortDescription: Calculates the evapotranspiration
    """

    # developmental_stage = 0
    # external_reference_evapotranspiration = 0
    # reference_evapotranspiration = 0
    # kc_factor = 0
    # snow_depth = 0
    # no_of_soil_layers = 20
    # layer_thickness = []
    # soil_moisture = []
    # permanent_wilting_point = []
    # field_capacity = []
    # evaporation = []
    # transpiration = []
    # crop_transpiration = []
    # percentage_soil_coverage = 0
    # evapotranspiration = []
    # surface_water_storage = 0
    # maximum_evaporation_impact_depth = 0
    # evaporation_zeta = 0
    # height_nn = 0
    # max_air_temperature = 0
    # min_air_temperature = 0
    # relative_humidity = 0
    # mean_air_temperature = 0
    # wind_speed = 0
    # wind_speed_height = 0
    # global_radiation = 0
    # julian_day = 0
    # latitude = 0
    # reference_albedo = 0
    # vapor_pressure = 0
    # stomata_resistance = 100
    # crop_remaining_evapotranspiration = 0
    # crop_evaporated_from_intercepted = 0
    cdef bool evaporation_from_surface = False
    cdef float eRed1
    cdef float eRed2
    cdef float eRed3
    cdef float eReducer
    cdef int i
    cdef float potential_evapotranspiration = 0.0
    cdef float evaporated_from_intercept = 0.0
    evaporated_from_surface = 0.0
    #snow_depth = snowComponent->getSnowDepth();
    # calculate soil evaporation until max 0.4m depth
    # evaporation_zeta = _params.pm_EvaporationZeta;
    # @todo <b>Claas:</b> pm_MaximumEvaporationImpactDepth is dependent on soil type
    # something has to be done there
    # this is the depth until which the evaporation can penetrate maximally
    # maximum_evaporation_impact_depth = _params.pm_MaximumEvaporationImpactDepth;
    # If a crop grows, ETp is taken from crop module
    if developmental_stage > 0:
        # Reference evapotranspiration is only grabbed here for consistent output in monica.cpp
        if external_reference_evapotranspiration < 0.0:
            reference_evapotranspiration = reference_evapotranspiration #monica.cropGrowth()->get_ReferenceEvapotranspiration();
        else:
            reference_evapotranspiration = external_reference_evapotranspiration
        # Remaining ET from crop module already includes Kc factor and evaporation
        # from interception storage
        potential_evapotranspiration = crop_remaining_evapotranspiration #monica.cropGrowth()->get_RemainingEvapotranspiration();
        evaporated_from_intercept = crop_evaporated_from_intercepted #monica.cropGrowth()->get_EvaporatedFromIntercept();
    else: # if no crop grows ETp is calculated from ET0 * kc
        if external_reference_evapotranspiration < 0.0:
            reference_evapotranspiration, vapor_pressure = \
                calc_reference_evapotranspiration(height_nn, max_air_temperature,
                                                  min_air_temperature, relative_humidity,
                                                  mean_air_temperature, wind_speed,
                                                  wind_speed_height,
                                                  global_radiation, julian_day, latitude,
                                                  reference_albedo, vapor_pressure, stomata_resistance)
        else:
            reference_evapotranspiration = external_reference_evapotranspiration
        potential_evapotranspiration = reference_evapotranspiration * kc_factor # - vm_InterceptionReference;
    actual_evaporation = 0.0
    actual_transpiration = 0.0
    # from HERMES:
    if potential_evapotranspiration > 6.5:
        potential_evapotranspiration = 6.5
    if potential_evapotranspiration > 0.0:
        evaporation_from_surface = False
        # If surface is water-logged, subsequent evaporation from surface water sources
        if surface_water_storage > 0.0:
            evaporation_from_surface = True
            # Water surface evaporates with Kc = 1.1.
            potential_evapotranspiration = potential_evapotranspiration * 1.1 / kc_factor
            # If a snow layer is present no water evaporates from surface water sources
            if snow_depth > 0.0:
                evaporated_from_surface = 0.0
            elif surface_water_storage < potential_evapotranspiration:
                potential_evapotranspiration -= surface_water_storage
                evaporated_from_surface = surface_water_storage
                surface_water_storage = 0.0
            else:
                surface_water_storage -= potential_evapotranspiration
                evaporated_from_surface = potential_evapotranspiration
                potential_evapotranspiration = 0.0;
            potential_evapotranspiration = potential_evapotranspiration * kc_factor / 1.1
        # Evaporation from soil
        if potential_evapotranspiration > 0.0:
            for i in range(no_of_soil_layers):
                eRed1 = e_reducer_1(permanent_wilting_point[i], field_capacity[i], soil_moisture[i],
                                    percentage_soil_coverage, potential_evapotranspiration,
                                    evaporation_reduction_method, xsa_critical_soil_moisture)
                eRed2 = 0.0
                if float(i) >= maximum_evaporation_impact_depth:
                    # layer is too deep for evaporation
                    eRed2 = 0.0
                else:
                    # 2nd factor to reduce actual evapotranspiration by
                    # MaximumEvaporationImpactDepth and evaporation_zeta
                    eRed2 = get_deprivation_factor(i + 1, maximum_evaporation_impact_depth, evaporation_zeta, layer_thickness[i])
                eRed3 = 0.0
                if i > 0 and soil_moisture[i] < soil_moisture[i - 1]:
                    # 3rd factor to consider if above layer contains more water than
                    # the adjacent layer below, evaporation will be significantly reduced
                    eRed3 = 0.1
                else:
                    eRed3 = 1.0
                # EReducer-> factor to reduce evaporation
                eReducer = eRed1 * eRed2 * eRed3
                if developmental_stage > 0:
                    # vegetation is present
                    # Interpolation between [0,1]
                    if percentage_soil_coverage >= 0.0 and percentage_soil_coverage < 1.0:
                        evaporation[i] = (1.0 - percentage_soil_coverage) * eReducer * potential_evapotranspiration
                    elif percentage_soil_coverage >= 1.0:
                        evaporation[i] = 0.0
                    if snow_depth > 0.0:
                        evaporation[i] = 0.0
                    # Transpiration is derived from ET0; Soil coverage and Kc factors
                    # already considered in crop part!
                    transpiration[i] = crop_transpiration[i] #monica.cropGrowth()->get_Transpiration(i);
                    # Transpiration is capped in case potential ET after surface
                    # and interception evaporation has occurred on same day
                    if evaporation_from_surface:
                        transpiration[i] = percentage_soil_coverage * eReducer * potential_evapotranspiration
                else: # no vegetation present
                    if snow_depth > 0.0:
                        evaporation[i] = 0.0
                    else:
                        evaporation[i] = potential_evapotranspiration * eReducer
                        transpiration[i] = 0.0
                evapotranspiration[i] = evaporation[i] + transpiration[i]
                soil_moisture[i] -= evapotranspiration[i] / 1000.0 / layer_thickness[i]
                # general restriction of soil moisture reduction due to evaporation
                if soil_moisture[i] < 0.01:
                    soil_moisture[i] = 0.01
                actual_transpiration += transpiration[i]
                actual_evaporation += evaporation[i]
        actual_evapotranspiration = actual_transpiration + actual_evaporation + evaporated_from_intercept + evaporated_from_surface
    return  evaporated_from_surface, actual_evapotranspiration



def get_deprivation_factor(int layer_no, float deprivation_depth, float zeta, float layer_thickness):
    # factor f(depth) to distribute the PET along the soil profil/rooting zone
    cdef float deprivation_factor
    cdef float c2
    cdef float c3

    # factor to introduce layer thickness in this algorithm,
    # to allow layer thickness scaling (Claas Nendel)
    cdef float ltf = deprivation_depth / (layer_thickness * 10.0)
    #ltf = deprivation_depth / (layer_thickness * 10.0)

    if abs(zeta) < 0.0003:
        deprivation_factor = 2.0 / ltf - 1.0 / (ltf * ltf) * (2 * layer_no - 1)
    else:
        c2 = log((ltf + zeta * layer_no) / (ltf + zeta * (layer_no - 1)))
        c3 = zeta / (ltf * (zeta + 1.0))
        deprivation_factor = (c2 - c3) / (log(zeta + 1.0) - zeta / (zeta + 1.0))

    return deprivation_factor



def bound(float lower, float value, float upper):
    if value < lower:
        return lower
    if value > upper:
        return upper
    return value


# A method following Penman-Monteith as described by the FAO in Allen
# RG, Pereira LS, Raes D, Smith M. (1998) Crop evapotranspiration.
# Guidelines for computing crop water requirements. FAO Irrigation and
# Drainage Paper 56, FAO, Roma
# @return reference_evapotranspiration
# @param height_nn [m]
# @param max_air_temperature [Â°C]
# @param min_air_temperature [Â°C]
# @param relative_humidity [fraction]
# @param mean_air_temperature [Â°C]
# @param wind_speed [m/s]
# @param wind_speed_height [m]
# @param global_radiation [MJ/m2]
# @param julian_day [d]
# @parm latitude [degree]

def calc_reference_evapotranspiration(float height_nn,
                                      float max_air_temperature,
                                      float min_air_temperature,
                                      float relative_humidity,
                                      float mean_air_temperature,
                                      float wind_speed,
                                      float wind_speed_height,
                                      float global_radiation,
                                      int julian_day,
                                      float latitude,
                                      float reference_albedo,
                                      float vapor_pressure,
                                      float stomata_resistance):
    cdef float declination = -23.4 * cos(2.0 * pi * ((julian_day + 10.0) / 365.0))

    # old SINLD
    cdef float declination_sinus = sin(declination * pi / 180.0) * sin(latitude * pi / 180.0);

    # old COSLD
    cdef float declination_cosinus = cos(declination * pi / 180.0) * cos(latitude * pi / 180.0);

    cdef float arg_astro_day_length = declination_sinus / declination_cosinus
    # The argument of asin must be in the range of - 1 to 1
    arg_astro_day_length = bound(-1.0, arg_astro_day_length, 1.0)
    cdef float astronomic_day_length = 12.0 * (pi + 2.0 * asin(arg_astro_day_length)) / pi

    cdef float arg_effective_day_length = (-sin(8.0 * pi / 180.0) + declination_sinus) / declination_cosinus
    # The argument of asin must be in the range of -1 to 1
    arg_effective_day_length = bound(-1.0, arg_effective_day_length, 1.0)
    #cdef float arg_effective_day_length
    #arg_effective_day_length = 12.0 * (pi + 2.0 * asin(arg_effective_day_length)) / pi

    cdef float arg_photo_day_length = (-sin(-6.0 * pi / 180.0) + declination_sinus) / declination_cosinus
    # The argument of asin must be in the range of - 1 to 1
    arg_photo_day_length = bound(-1.0, arg_photo_day_length, 1.0)
    #cdef float photoperiodic_day_length
    #photoperiodic_day_length = 12.0 * (pi + 2.0 * asin(arg_photo_day_length)) / pi;

    # The argument of sqrt must be >= 0
    cdef float arg_phot_act = min(1.0, ((declination_sinus / declination_cosinus) * (declination_sinus / declination_cosinus)))
    cdef float phot_act_radiation_mean = 3600.0 * (declination_sinus * astronomic_day_length + 24.0 / pi *
                                               declination_cosinus
                                               * sqrt(1.0 - arg_phot_act))

    cdef float clear_day_radiation = 0.0
    if phot_act_radiation_mean > 0.0 and astronomic_day_length > 0.0:
        clear_day_radiation = 0.5 * 1300.0 * phot_act_radiation_mean * exp(-0.14 / (phot_act_radiation_mean
                                                                         / (astronomic_day_length * 3600.0)))

    #double vc_OvercastDayRadiation = 0.2 * clear_day_radiation;
    cdef float SC = 24.0 * 60.0 / pi * 8.20 * (1.0 + 0.033 * cos(2.0 * pi * julian_day / 365.0));
    # The argument of acos must be in the range of -1 to 1
    cdef float arg_SHA = bound(-1.0, -tan(latitude * pi / 180.0) * tan(declination * pi / 180.0), 1.0)
    cdef float SHA = acos(arg_SHA)

    # [J cm-2] --> [MJ m-2]
    cdef float extraterrestrial_radiation = SC * (SHA * declination_sinus + declination_cosinus * sin(SHA)) / 100.0

    # Calculation of atmospheric pressure //[kPA]
    cdef float atmospheric_pressure = 101.3 * pow(((293.0 - (0.0065 * height_nn)) / 293.0), 5.26)

    # Calculation of psychrometer constant [kPA Â°C-1] - air humidity
    cdef float psycrometer_constant = 0.000665 * atmospheric_pressure

    # Calc. of saturated water vapor pressure at daily max temperature [kPA]
    cdef float saturated_vapor_pressure_max = 0.6108 * exp((17.27 * max_air_temperature) / (237.3 + max_air_temperature))

    # Calc. of saturated water vapor pressure at daily min temperature [kPA]
    cdef float saturated_vapor_pressure_min = 0.6108 * exp((17.27 * min_air_temperature) / (237.3 + min_air_temperature))

    # Calculation of the saturated water vapor pressure [kPA]
    cdef float saturated_vapor_pressure = (saturated_vapor_pressure_max + saturated_vapor_pressure_min) / 2.0

    if vapor_pressure < 0.0:
        # Calculation of the water vapor pressure
        if relative_humidity <= 0.0:
            # Assuming Tdew = Tmin as suggested in FAO56 Allen et al. 1998
            vapor_pressure = saturated_vapor_pressure_min
        else:
            vapor_pressure = relative_humidity * saturated_vapor_pressure


    # Calculation of the air saturation deficit [kPA]
    cdef float saturation_deficit = saturated_vapor_pressure - vapor_pressure

    # Slope of saturation water vapor pressure-to-temperature relation [kPA Â°C-1]
    cdef float saturated_vapour_pressure_slope = (4098.0 * (0.6108 * exp((17.27 * mean_air_temperature) / (
            mean_air_temperature
            + 237.3)))) / ((mean_air_temperature + 237.3) * (mean_air_temperature + 237.3))

    # Calculation of wind speed in 2m height //[m s-1]
    cdef float wind_speed_2m = max(0.5, wind_speed * (4.87 / (log(67.8 * wind_speed_height - 5.42))))
    # 0.5 minimum allowed wind speed for Penman-Monteith-Method FAO

    # Calculation of the aerodynamic resistance [s m-1]
    #cdef float aerodynamic_resistance
    #aerodynamic_resistance = 208.0 / wind_speed_2m

    # FAO default value [s m-1]
    # stomata_resistance = 100

    cdef float surface_resistance = stomata_resistance / 1.44 # [s m - 1]

    cdef float clear_sky_solar_radiation = (0.75 + 0.00002 * height_nn) * extraterrestrial_radiation

    cdef float relative_shortwave_radiation = min(global_radiation / clear_sky_solar_radiation, 1.0) if clear_sky_solar_radiation > 0.0 else 1.0

    cdef float bolzmann_constant = 0.0000000049

    # FAO Green gras reference albedo from Allen et al. (1998)
    cdef float shortwave_radiation = (1.0 - reference_albedo) * global_radiation

    cdef float longwave_radiation = bolzmann_constant * ((pow((min_air_temperature + 273.16), 4.0)
                                               + pow((max_air_temperature + 273.16), 4.0)) / 2.0) \
                                               * (1.35 * relative_shortwave_radiation - 0.35) \
                                               * (0.34 - 0.14 * sqrt(vapor_pressure))
    cdef float net_radiation = shortwave_radiation - longwave_radiation

    # Calculation of the reference evapotranspiration
    # Penman-Monteith-Methode FAO
    # [mm]
    cdef float reference_evapotranspiration = ((0.408 * saturated_vapour_pressure_slope * net_radiation)
                                           + (psycrometer_constant * (900.0 / (mean_air_temperature + 273.0))
                                              * wind_speed_2m * saturation_deficit)) \
                                              / (saturated_vapour_pressure_slope + psycrometer_constant
                                                 * (1.0 + (surface_resistance / 208.0) * wind_speed_2m))

    if reference_evapotranspiration < 0.0:
      reference_evapotranspiration = 0.0

    return reference_evapotranspiration, vapor_pressure#, net_radiation



# Calculation of evaporation reduction by soil moisture content
# @param pwp = permanent wilting point at layer
# @param fc = field capacity at layer
# @param sm = soil moisture at layer
# @param percentageSoilCoverage
# @param referenceEvapotranspiration
# @return Value for evaporation reduction by soil moisture content

def e_reducer_1(float pwp,
                float fc,
                float sm,
                float percentage_soil_coverage,
                float reference_evapotranspiration,
                int evaporation_reduction_method,
                float xsa_critical_soil_moisture):

    cdef float e_reduction_factor = 0.0
    cdef float critical_soil_moisture
    cdef float reducer
    cdef float xsa

    #double pwp = soilColumn[layerIndex].vs_PermanentWiltingPoint();
    #double fc = soilColumn[layerIndex].vs_FieldCapacity();
    #double sm = max(0.33 * pwp, soilColumn[layerIndex].get_Vs_SoilMoisture_m3());
    sm = max(0.33 * pwp, sm)
    cdef float relative_evaporable_water = min(1.0, (sm - 0.33 * pwp) / (fc - 0.33 * pwp))

    if evaporation_reduction_method == 0: # THESEUS
        critical_soil_moisture = 0.65 * fc
        if percentage_soil_coverage > 0.0:
            reducer = 1.0
            if reference_evapotranspiration > 2.5:
                xsa = (0.65 * fc - pwp) * (fc - pwp)
                reducer = xsa + (((1 - xsa) / 17.5) * (reference_evapotranspiration - 2.5))
            else:
                # XSACriticalSoilMoisture = parameter for the slope of the deprivation function
                reducer = xsa_critical_soil_moisture / 2.5 * reference_evapotranspiration
            critical_soil_moisture = fc * reducer

        # Calculation of an evaporation-reducing factor in relation to soil water content
        if sm > critical_soil_moisture:
            # Moisture is higher than critical value so there is a
            # normal evaporation and nothing must be reduced
            e_reduction_factor = 1.0
        elif sm > 0.33 * pwp: # critical value is reached, actual evaporation is below potential
            # moisture is higher than 30% of permanent wilting point
            e_reduction_factor = relative_evaporable_water
        else:
            # if moisture is below 30% of wilting point nothing can be evaporated
            e_reduction_factor = 0.0
    else: # if evaporationReductionMethod == 1: # HERMES
        #default:
        if relative_evaporable_water > 0.33:
            e_reduction_factor = 1.0 - (0.1 * (1.0 - relative_evaporable_water) / (1.0 - 0.33))
        elif relative_evaporable_water > 0.22:
            e_reduction_factor = 0.9 - (0.625 * (0.33 - relative_evaporable_water) / (0.33 - 0.22))
        elif relative_evaporable_water > 0.2:
            e_reduction_factor = 0.275 - (0.225 * (0.22 - relative_evaporable_water) / (0.22 - 0.2))
        else:
            e_reduction_factor = 0.05 - (0.05 * (0.2 - relative_evaporable_water) / 0.2)

    return e_reduction_factor

