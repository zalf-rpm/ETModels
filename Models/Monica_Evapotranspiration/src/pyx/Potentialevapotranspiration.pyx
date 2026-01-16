import numpy
from math import *

def init_potentialevapotranspiration(float reference_evapotranspiration,
                                     float external_reference_evapotranspiration,
                                     float kc_factor,
                                     int developmental_stage,
                                     float crop_reference_evapotranspiration,
                                     float crop_remaining_evapotranspiration):
    cdef float potential_evapotranspiration = 0.0

    return  potential_evapotranspiration

def model_potentialevapotranspiration(float reference_evapotranspiration,
                                      float external_reference_evapotranspiration,
                                      float kc_factor,
                                      int developmental_stage,
                                      float crop_reference_evapotranspiration,
                                      float crop_remaining_evapotranspiration,
                                      float potential_evapotranspiration):
    """
    MONICA potential evapotranspiration calculation
    Author: Claas Nendel (transcription into Crop2ML by Michael Berg-Mohnicke)
    Reference: None
    Institution: ZALF e.V.
    ExtendedDescription: None
    ShortDescription: Calculates the MONICA potential evapotranspiration
    """

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
    else: # if no crop grows ETp is calculated from ET0 * kc
        if external_reference_evapotranspiration >= 0.0:
            reference_evapotranspiration = external_reference_evapotranspiration
        potential_evapotranspiration = reference_evapotranspiration * kc_factor # - vm_InterceptionReference;
    # from HERMES:
    if potential_evapotranspiration > 6.5:
        potential_evapotranspiration = 6.5
    return  potential_evapotranspiration, reference_evapotranspiration



