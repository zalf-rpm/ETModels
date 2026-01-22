import numpy
from math import *

def model_stomataresistance(float saturation_beta,
                            float stomata_conductance_alpha,
                            int carboxylation_pathway,
                            float atmospheric_co2_concentration,
                            float saturation_vapor_pressure_deficit,
                            float gross_photosynthesis_reference_mol):
    """
    MONICA stomata resistance
    Author: Claas Nendel (transcription into Crop2ML by Michael Berg-Mohnicke)
    Reference: 
        
    Institution: ZALF e.V.
    ExtendedDescription: 
        
    ShortDescription: Calculates the MONICA reference evapotranspiration
    """

    cdef float stomata_resistance
    if carboxylation_pathway > 0:
        if gross_photosynthesis_reference_mol <= 0.0:
          stomata_resistance = 999999.9 # [s m-1]
        elif carboxylation_pathway == 1:
          # [s m-1]
          stomata_resistance = (
                  (atmospheric_co2_concentration * (1.0 + saturation_vapor_pressure_deficit / saturation_beta))
                  / (stomata_conductance_alpha * gross_photosynthesis_reference_mol))
        else:
          # [s m-1]
          stomata_resistance = (
                  (atmospheric_co2_concentration * (1.0 + saturation_vapor_pressure_deficit / saturation_beta))
                  / (stomata_conductance_alpha * gross_photosynthesis_reference_mol))
    return  stomata_resistance



