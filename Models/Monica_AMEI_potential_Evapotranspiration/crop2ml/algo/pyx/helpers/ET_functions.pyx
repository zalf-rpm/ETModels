from math import *

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
