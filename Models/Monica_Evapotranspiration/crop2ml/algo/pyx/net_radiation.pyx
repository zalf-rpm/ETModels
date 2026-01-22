cdef float clear_sky_solar_radiation
clear_sky_solar_radiation = (0.75 + 0.00002 * height_nn) * extraterrestrial_radiation

cdef float relative_shortwave_radiation
relative_shortwave_radiation = min(global_radiation / clear_sky_solar_radiation, 1.0) if clear_sky_solar_radiation > 0.0 else 1.0

cdef float bolzmann_constant = 0.0000000049

# FAO Green gras reference albedo from Allen et al. (1998)
cdef float shortwave_radiation
shortwave_radiation = (1.0 - reference_albedo) * global_radiation

cdef float longwave_radiation
longwave_radiation = bolzmann_constant * ((pow((min_air_temperature + 273.16), 4.0)
                                           + pow((max_air_temperature + 273.16), 4.0)) / 2.0) \
                                           * (1.35 * relative_shortwave_radiation - 0.35) \
                                           * (0.34 - 0.14 * sqrt(vapor_pressure))
net_radiation = shortwave_radiation - longwave_radiation
