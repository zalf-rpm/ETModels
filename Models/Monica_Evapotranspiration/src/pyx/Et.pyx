import numpy
from math import *

def init_et(float evaporation_zeta,
            float maximum_evaporation_impact_depth,
            int evaporation_reduction_method,
            float xsa_critical_soil_moisture,
            int no_of_soil_layers,
            int no_of_soil_moisture_layers,
            float layer_thickness[no_of_soil_moisture_layers],
            float permanent_wilting_point[no_of_soil_moisture_layers],
            float field_capacity[no_of_soil_moisture_layers],
            float kc_factor,
            bool has_snow_cover,
            int developmental_stage,
            float crop_transpiration[no_of_soil_moisture_layers],
            float crop_evaporated_from_intercepted,
            float percentage_soil_coverage):
    cdef float potential_evapotranspiration = 0.0
    cdef float surface_water_storage = 0.0
    cdef float evaporated_from_surface = 0.0
    cdef float actual_evaporation = 0.0
    cdef float actual_transpiration = 0.0
    cdef float soil_moisture[no_of_soil_moisture_layers]
    cdef float evaporation[no_of_soil_moisture_layers]
    cdef float transpiration[no_of_soil_moisture_layers]
    cdef float evapotranspiration[no_of_soil_moisture_layers]
    cdef float actual_evapotranspiration = 0.0
    soil_moisture = array('f', [0.0]*no_of_soil_moisture_layers)
    evaporation = array('f', [0.0]*no_of_soil_moisture_layers)
    transpiration = array('f', [0.0]*no_of_soil_moisture_layers)
    evapotranspiration = array('f', [0.0]*no_of_soil_moisture_layers)

    return  potential_evapotranspiration, surface_water_storage, evaporated_from_surface, actual_evaporation, actual_transpiration, soil_moisture, evaporation, transpiration, evapotranspiration, actual_evapotranspiration

def model_et(float evaporation_zeta,
             float maximum_evaporation_impact_depth,
             int evaporation_reduction_method,
             float xsa_critical_soil_moisture,
             int no_of_soil_layers,
             int no_of_soil_moisture_layers,
             float layer_thickness[no_of_soil_moisture_layers],
             float permanent_wilting_point[no_of_soil_moisture_layers],
             float field_capacity[no_of_soil_moisture_layers],
             float kc_factor,
             bool has_snow_cover,
             int developmental_stage,
             float crop_transpiration[no_of_soil_moisture_layers],
             float crop_evaporated_from_intercepted,
             float percentage_soil_coverage,
             float potential_evapotranspiration,
             float surface_water_storage,
             float evaporated_from_surface,
             float actual_evaporation,
             float actual_transpiration,
             float soil_moisture[no_of_soil_moisture_layers],
             float evaporation[no_of_soil_moisture_layers],
             float transpiration[no_of_soil_moisture_layers],
             float evapotranspiration[no_of_soil_moisture_layers],
             float actual_evapotranspiration):
    """
    MONICA evapotranspiration model
    Author: Claas Nendel (transcription into Crop2ML by Michael Berg-Mohnicke)
    Reference: None
    Institution: ZALF e.V.
    ExtendedDescription: None
    ShortDescription: Calculates the MONICA evapotranspiration
    """

    cdef float evaporated_from_intercept
    if developmental_stage > 0:
        evaporated_from_intercept = crop_evaporated_from_intercepted
    else:
        evaporated_from_intercept = 0.0
    evaporated_from_surface = 0.0
    actual_evaporation = 0.0
    actual_transpiration = 0.0
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
    return  evaporated_from_surface, actual_evapotranspiration, actual_evaporation, actual_transpiration, surface_water_storage, soil_moisture



def bound(float lower, float value, float upper):
    if value < lower:
        return lower
    if value > upper:
        return upper
    return value

def get_deprivation_factor(int layer_no, float deprivation_depth, float zeta, float layer_thickness):
    # factor f(depth) to distribute the PET along the soil profil/rooting zone

    # factor to introduce layer thickness in this algorithm,
    # to allow layer thickness scaling (Claas Nendel)
    cdef float ltf
    ltf = deprivation_depth / (layer_thickness * 10.0)

    cdef float deprivation_factor
    cdef float c2
    cdef float c3
    if abs(zeta) < 0.0003:
        deprivation_factor = 2.0 / ltf - 1.0 / (ltf * ltf) * (2 * layer_no - 1)
    else:
        c2 = log((ltf + zeta * layer_no) / (ltf + zeta * (layer_no - 1)))
        c3 = zeta / (ltf * (zeta + 1.0))
        deprivation_factor = (c2 - c3) / (log(zeta + 1.0) - zeta / (zeta + 1.0))

    return deprivation_factor


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

    sm = max(0.33 * pwp, sm)
    cdef float relative_evaporable_water
    relative_evaporable_water = min(1.0, (sm - 0.33 * pwp) / (fc - 0.33 * pwp))

    cdef float e_reduction_factor = 0.0
    cdef float critical_soil_moisture
    cdef float reducer
    cdef float xsa
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

