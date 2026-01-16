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
