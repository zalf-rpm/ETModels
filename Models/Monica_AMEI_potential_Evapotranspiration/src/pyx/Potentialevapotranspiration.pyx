import numpy
from math import *

def model_potentialevapotranspiration(float potential_evapotranspiration_cap,
                                      float kc_factor,
                                      int developmental_stage,
                                      float crop_remaining_evapotranspiration,
                                      float reference_evapotranspiration):
    """
    MONICA potential evapotranspiration calculation
    Author: Claas Nendel (transcription into Crop2ML by Michael Berg-Mohnicke)
    Reference: None
    Institution: ZALF e.V.
    ExtendedDescription: None
    ShortDescription: Calculates the MONICA potential evapotranspiration
    """

    cdef float potential_evapotranspiration
    # If a crop grows, ETp is taken from crop module
    if developmental_stage > 0:
        # Remaining ET from crop module already includes Kc factor and evaporation
        # from interception storage
        potential_evapotranspiration = crop_remaining_evapotranspiration #monica.cropGrowth()->get_RemainingEvapotranspiration();
    else: # if no crop grows ETp is calculated from ET0 * kc
        potential_evapotranspiration = reference_evapotranspiration * kc_factor # - vm_InterceptionReference;
    # from HERMES:
    if potential_evapotranspiration > potential_evapotranspiration_cap:
        potential_evapotranspiration = potential_evapotranspiration_cap
    return  potential_evapotranspiration



