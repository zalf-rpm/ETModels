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
