evaporated_from_surface = 0.0

#snow_depth = snowComponent->getSnowDepth();

# @todo <b>Claas:</b> pm_MaximumEvaporationImpactDepth is dependent on soil type
# something has to be done there
# this is the depth until which the evaporation can penetrate maximally
# maximum_evaporation_impact_depth = _params.pm_MaximumEvaporationImpactDepth;

cdef float potential_evapotranspiration = 0.0
cdef float evaporated_from_intercept = 0.0
# If a crop grows, ETp is taken from crop module
if developmental_stage > 0:
    # Reference evapotranspiration is only grabbed here for consistent output in monica.cpp
    if external_reference_evapotranspiration < 0.0:
        reference_evapotranspiration = crop_reference_evapotranspiration #monica.cropGrowth()->get_ReferenceEvapotranspiration();
    else:
        reference_evapotranspiration = external_reference_evapotranspiration

    # Remaining ET from crop module already includes Kc factor and evaporation
    # from interception storage
    potential_evapotranspiration = crop_remaining_evapotranspiration #monica.cropGrowth()->get_RemainingEvapotranspiration();
    evaporated_from_intercept = crop_evaporated_from_intercepted #monica.cropGrowth()->get_EvaporatedFromIntercept();
else: # if no crop grows ETp is calculated from ET0 * kc
    if external_reference_evapotranspiration < 0.0:
        reference_evapotranspiration, net_radiation = \
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

cdef bool evaporation_from_surface = False
cdef float eRed1
cdef float eRed2
cdef float eRed3
cdef float eReducer
cdef int i
if potential_evapotranspiration > 0.0:
    evaporation_from_surface = False

    # If surface is water-logged, subsequent evaporation from surface water sources
    if surface_water_storage > 0.0:
        evaporation_from_surface = True

        # Water surface evaporates with Kc = 1.1.
        potential_evapotranspiration = potential_evapotranspiration * 1.1 / kc_factor

        # If a snow layer is present no water evaporates from surface water sources
        if has_snow_cover:
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

                if has_snow_cover:
                    evaporation[i] = 0.0

                # Transpiration is derived from ET0; Soil coverage and Kc factors
                # already considered in crop part!
                transpiration[i] = crop_transpiration[i] #monica.cropGrowth()->get_Transpiration(i);

                # Transpiration is capped in case potential ET after surface
                # and interception evaporation has occurred on same day
                if evaporation_from_surface:
                    transpiration[i] = percentage_soil_coverage * eReducer * potential_evapotranspiration

            else: # no vegetation present
                if has_snow_cover:
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

