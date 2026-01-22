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
