developmental_stage = 0
external_reference_evapotranspiration = 0
reference_evapotranspiration = 0
kc_factor = 0
snow_depth = 0
no_of_soil_layers = 20
layer_thickness = []
soil_moisture = []
evaporation = []
transpiration = []
crop_transpiration = []
percentage_soil_coverage = 0
evapotranspiration = []
# void SoilMoisture::fm_Evapotranspiration(double vs_HeightNN,
#                                              double vw_MaxAirTemperature,
#                                              double vw_MinAirTemperature,
#                                              double vw_RelativeHumidity,
#                                              double vw_MeanAirTemperature,
#                                              double vw_WindSpeed,
#                                              double vw_WindSpeedHeight,
#                                              double vw_GlobalRadiation,
#                                              int vc_DevelopmentalStage,
#                                              int vs_JulianDay,
#                                              double vs_Latitude,
#                                              ) {

cdef float potential_evapotranspiration
# local var
potential_evapotranspiration = 0.0

cdef float evaporated_from_intercept
# local var
evaporated_from_intercept = 0.0

evaporated_from_surface = 0.0
# state

#snow_depth = snowComponent->getSnowDepth();
# state

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
    potential_evapotranspiration = monica.cropGrowth()->get_RemainingEvapotranspiration();
    evaporated_from_intercept = monica.cropGrowth()->get_EvaporatedFromIntercept();
else: # if no crop grows ETp is calculated from ET0 * kc
    if external_reference_evapotranspiration < 0.0:
        reference_evapotranspiration = referenceEvapotranspiration(vs_HeightNN, vw_MaxAirTemperature,
                                                                     vw_MinAirTemperature, vw_RelativeHumidity,
                                                                     vw_MeanAirTemperature, vw_WindSpeed,
                                                                     vw_WindSpeedHeight,
                                                                     vw_GlobalRadiation, vs_JulianDay, vs_Latitude);
    else:
        reference_evapotranspiration = external_reference_evapotranspiration

    potential_evapotranspiration = reference_evapotranspiration * kc_factor # - vm_InterceptionReference;

actual_evaporation = 0.0
actual_transpiration = 0.0

# from HERMES:
if potential_evapotranspiration > 6.5:
    potential_evapotranspiration = 6.5

if potential_evapotranspiration > 0.0:
    cdef bool evaporation_from_surface
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
            potential_evapotranspiration -= vm_SurfaceWaterStorage
            evaporated_from_surface = surface_water_storage
            surface_water_storage = 0.0
        else:
            surface_water_storage -= potential_evapotranspiration
            evaporated_from_surface = potential_evapotranspiration
            potential_evapotranspiration = 0.0;
        potential_evapotranspiration = potential_evapotranspiration * kc_factor / 1.1

    # Evaporation from soil
    if potential_evapotranspiration > 0:
        for i in range(no_of_soil_layers):
            cdef float eRed1
            eRed1 = eReducer1(i, percentage_soil_coverage, potential_evapotranspiration)

            cdef float eRed2
            eRed2 = 0.0
            if i >= maximum_evaporation_impact_depth:
                # layer is too deep for evaporation
                eRed2 = 0.0
            else:
                # 2nd factor to reduce actual evapotranspiration by
                # MaximumEvaporationImpactDepth and evaporation_zeta
                eRed2 = get_deprivation_factor(i + 1, maximum_evaporation_impact_depth, evaporation_zeta, layer_thickness[i])

            cdef float eRed3
            eRed3 = 0.0

            if i > 0 and soil_moisture[i] < soil_moisture[i - 1]:
                # 3rd factor to consider if above layer contains more water than
                # the adjacent layer below, evaporation will be significantly reduced
                eRed3 = 0.1
            else:
                eRed3 = 1.0

            # EReducer-> factor to reduce evaporation
            cdef float eReducer
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

