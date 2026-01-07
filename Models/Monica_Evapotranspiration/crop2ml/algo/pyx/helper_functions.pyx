# PET deprivation distribution (factor as function of depth).
# The PET is spread over the deprivation depth. This function computes
# the factor/weight for a given layer/depth[dm] (layer_no).
# @return deprivation_factor
# @param layer_no [1..NLAYER]
# @param deprivation_depth [dm] maximum deprivation depth
# @param zeta [0..40] shape factor
# @param layer_thickness
def get_deprivation_factor(int layer_no, float deprivation_depth, float zeta, float layer_thickness):
    # factor f(depth) to distribute the PET along the soil profil/rooting zone
    cdef float deprivation_factor

    # factor to introduce layer thickness in this algorithm,
    # to allow layer thickness scaling (Claas Nendel)
    cdef float ltf
    ltf = deprivation_depth / (layer_thickness * 10.0)

    if abs(zeta) < 0.0003:
        deprivation_factor = 2.0 / ltf - 1.0 / (ltf * ltf) * (2 * layer_no - 1)
    else:
        cdef float c2
        c2 = log((ltf + zeta * layer_no) / (ltf + zeta * (layer_no - 1)))

        cdef float c3
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
# @param vs_HeightNN
# @param vw_MaxAirTemperature
# @param vw_MinAirTemperature
# @param vw_RelativeHumidity
# @param vw_MeanAirTemperature
# @param vw_WindSpeed
# @param vw_WindSpeedHeight
# @param vw_GlobalRadiation
# @return
def calc_reference_evapotranspiration(float vs_HeightNN, float vw_MaxAirTemperature,
    float vw_MinAirTemperature, double vw_RelativeHumidity,
        float vw_MeanAirTemperature, float vw_WindSpeed,
            float vw_WindSpeedHeight, double vw_GlobalRadiation, int vs_JulianDay,
                float vs_Latitude):
    cdef float declination
    declination = -23.4 * cos(2.0 * pi * ((vs_JulianDay + 10.0) / 365.0))

    # old SINLD
    cdef float declination_sinus
    declination_sinus = sin(declination * pi / 180.0) * sin(vs_Latitude * pi / 180.0);

    # old COSLD
    cdef float declination_cosinus
    declination_cosinus = cos(declination * pi / 180.0) * cos(vs_Latitude * pi / 180.0);

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

    cdef float arg_phot_act
    # The argument of sqrt must be >= 0
    arg_phot_act = min(1.0, ((declination_sinus / declination_cosinus) * (declination_sinus / declination_cosinus)))
    cdef float phot_act_radiation_mean
    phot_act_radiation_mean = 3600.0 * (declination_sinus * astronomic_day_length + 24.0 / pi *
                                               declination_cosinus
                                               * sqrt(1.0 - arg_phot_act))

    cdef float clear_day_radiation
    clear_day_radiation = 0
    if phot_act_radiation_mean > 0 && astronomic_day_length > 0:
        clear_day_radiation = 0.5 * 1300.0 * phot_act_radiation_mean * exp(-0.14 / (phot_act_radiation_mean
                                                                         / (astronomic_day_length * 3600.0)))

    #double vc_OvercastDayRadiation = 0.2 * clear_day_radiation;
    cdef float SC
    SC = 24.0 * 60.0 / pi * 8.20 * (1.0 + 0.033 * cos(2.0 * pi * vs_JulianDay / 365.0));
    cdef float arg_SHA
    # The argument of acos must be in the range of -1 to 1
    arg_SHA = bound(-1.0, -tan(vs_Latitude * pi / 180.0) * tan(declination * pi / 180.0),
                           1.0)
    cdef float SHA
    SHA = acos(arg_SHA)

    cdef float extraterrestrial_radiation # [J cm-2] --> [MJ m-2]
    extraterrestrial_radiation = SC * (SHA * declination_sinus + declination_cosinus * sin(SHA)) / 100.0

    # Calculation of atmospheric pressure //[kPA]
    cdef float atmospheric_pressure
    atmospheric_pressure = 101.3 * pow(((293.0 - (0.0065 * vs_HeightNN)) / 293.0), 5.26)

    # Calculation of psychrometer constant [kPA °C-1] - air humidity
    cdef float psycrometer_constant
    psycrometer_constant = 0.000665 * atmospheric_pressure

    # Calc. of saturated water vapour pressure at daily max temperature [kPA]
    cdef float saturated_vapour_pressure_max
    saturated_vapour_pressure_max = 0.6108 * exp((17.27 * vw_MaxAirTemperature) / (237.3 + vw_MaxAirTemperature))

    # Calc. of saturated water vapour pressure at daily min temperature [kPA]
    cdef float saturated_vapour_pressure_min
    saturated_vapour_pressure_min = 0.6108 * exp((17.27 * vw_MinAirTemperature) / (237.3 + vw_MinAirTemperature))

    # Calculation of the saturated water vapour pressure [kPA]
    cdef float saturated_vapour_pressure
    saturated_vapour_pressure = (saturated_vapour_pressure_max + saturated_vapour_pressure_min) / 2.0

    if _vaporPressure < 0:
        # Calculation of the water vapour pressure
        if vw_RelativeHumidity <= 0.0:
            # Assuming Tdew = Tmin as suggested in FAO56 Allen et al. 1998
            _vaporPressure = saturated_vapour_pressure_min;
        else:
            _vaporPressure = vw_RelativeHumidity * saturated_vapour_pressure


    # Calculation of the air saturation deficit [kPA]
    cdef float saturation_deficit
    saturation_deficit = saturated_vapour_pressure - _vaporPressure

    # Slope of saturation water vapour pressure-to-temperature relation [kPA °C-1]
    cdef float saturated_vapour_pressure_slope
    saturated_vapour_pressure_slope = (4098.0 * (0.6108 * exp((17.27 * vw_MeanAirTemperature) / (
                                                              vw_MeanAirTemperature
                                                              + 237.3)))) / ((vw_MeanAirTemperature + 237.3) * (vw_MeanAirTemperature + 237.3))

    # Calculation of wind speed in 2m height //[m s-1]
    cdef float wind_speed_2m
    wind_speed_2m = max(0.5, vw_WindSpeed * (4.87 / (log(67.8 * vw_WindSpeedHeight - 5.42))))
    # 0.5 minimum allowed windspeed for Penman-Monteith-Method FAO

    # Calculation of the aerodynamic resistance [s m-1]
    #cdef float aerodynamic_resistance
    #aerodynamic_resistance = 208.0 / wind_speed_2m

    # FAO default value [s m-1]
    vc_StomataResistance = 100

    cdef float surface_resistance
    surface_resistance = vc_StomataResistance / 1.44 # [s m - 1]

    cdef float clear_sky_solar_radiation
    clear_sky_solar_radiation = (0.75 + 0.00002 * vs_HeightNN) * extraterrestrial_radiation

    cdef float relative_shortwave_radiation
    relative_shortwave_radiation = min(vw_GlobalRadiation / clear_sky_solar_radiation, 1.0) if clear_sky_solar_radiation > 0 else 1.0

    cdef float bolzmann_constant
    bolzmann_constant = 0.0000000049

    # FAO Green gras reference albedo from Allen et al. (1998)
    cdef float shortwave_radiation
    shortwave_radiation = (1.0 - cropPs.pc_ReferenceAlbedo) * vw_GlobalRadiation

    cdef float longwave_radiation
    longwave_radiation = bolzmann_constant * ((pow((vw_MinAirTemperature + 273.16), 4.0)
                                               + pow((vw_MaxAirTemperature + 273.16), 4.0)) / 2.0) \
                                               * (1.35 * relative_shortwave_radiation - 0.35) \
                                               * (0.34 - 0.14 * sqrt(_vaporPressure))
    vw_NetRadiation = shortwave_radiation - longwave_radiation

    # Calculation of the reference evapotranspiration
    # Penman-Monteith-Methode FAO
    # [mm]
    cdef float reference_evapotranspiration
    reference_evapotranspiration = ((0.408 * saturated_vapour_pressure_slope * vw_NetRadiation)
                                           + (psycrometer_constant * (900.0 / (vw_MeanAirTemperature + 273.0))
                                              * wind_speed_2m * saturation_deficit)) \
                                              / (saturated_vapour_pressure_slope + psycrometer_constant
                                                 * (1.0 + (surface_resistance / 208.0) * wind_speed_2m))

    if reference_Evapotranspiration < 0.0:
      reference_evapotranspiration = 0.0

    return reference_evapotranspiration








def doThermalConductivityCoeffs(floatarray thermCondPar2,
         int numLayers,
         floatarray bulkDensity,
         int numNodes,
         floatarray thermCondPar3,
         floatarray thermCondPar4,
         floatarray clay,
         floatarray thermCondPar1):
    cdef int layer
    cdef float oldGC1[]
    cdef float oldGC2[]
    cdef float oldGC3[]
    cdef float oldGC4[]
    cdef int element
    oldGC1=thermCondPar1
    thermCondPar1=array('f', [0.0]*(numNodes + 1))
    if oldGC1 is not None:
        thermCondPar1[0:min(numNodes + 1, len(oldGC1))]=oldGC1[0:min(numNodes + 1, len(oldGC1))]
    oldGC2=thermCondPar2
    thermCondPar2=array('f', [0.0]*(numNodes + 1))
    if oldGC2 is not None:
        thermCondPar2[0:min(numNodes + 1, len(oldGC2))]=oldGC2[0:min(numNodes + 1, len(oldGC2))]
    oldGC3=thermCondPar3
    thermCondPar3=array('f', [0.0]*(numNodes + 1))
    if oldGC3 is not None:
        thermCondPar3[0:min(numNodes + 1, len(oldGC3))]=oldGC3[0:min(numNodes + 1, len(oldGC3))]
    oldGC4=thermCondPar4
    thermCondPar4=array('f', [0.0]*(numNodes + 1))
    if oldGC4 is not None:
        thermCondPar4[0:min(numNodes + 1, len(oldGC4))]=oldGC4[0:min(numNodes + 1, len(oldGC4))]
    for layer in range(1 , numLayers + 1 + 1 , 1):
        element=layer
        thermCondPar1[element]=0.65 - (0.78 * bulkDensity[layer]) + (0.6 * pow(bulkDensity[layer], 2))
        thermCondPar2[element]=1.06 * bulkDensity[layer]
        thermCondPar3[element]=1.0 + Divide(2.6, sqrt(clay[layer]), float(0))
        thermCondPar4[element]=0.03 + (0.1 * pow(bulkDensity[layer], 2))
    return (thermCondPar2, thermCondPar3, thermCondPar4, thermCondPar1)

def readParam(float bareSoilRoughness,
         floatarray newTemperature,
         float soilRoughnessHeight,
         floatarray soilTemp,
         floatarray thermCondPar2,
         int numLayers,
         floatarray bulkDensity,
         int numNodes,
         floatarray thermCondPar3,
         floatarray thermCondPar4,
         floatarray clay,
         floatarray thermCondPar1,
         float weather_Tav,
         int clock_Today_DayOfYear,
         int surfaceNode,
         float weather_Amp,
         floatarray thickness,
         float weather_Latitude):
    (thermCondPar2, thermCondPar3, thermCondPar4, thermCondPar1)=doThermalConductivityCoeffs(thermCondPar2, numLayers, bulkDensity, numNodes, thermCondPar3, thermCondPar4, clay, thermCondPar1)
    soilTemp=calcSoilTemperature(soilTemp, weather_Tav, clock_Today_DayOfYear, surfaceNode, numNodes, weather_Amp, thickness, weather_Latitude)
    newTemperature[0:0 + len(soilTemp)]=soilTemp
    soilRoughnessHeight=bareSoilRoughness
    return (newTemperature, soilTemp, thermCondPar2, thermCondPar3, thermCondPar4, thermCondPar1, soilRoughnessHeight)