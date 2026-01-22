# Calc. of saturated water vapor pressure at daily max temperature [kPA]
cdef float saturated_vapor_pressure_max
saturated_vapor_pressure_max = 0.6108 * exp((17.27 * max_air_temperature) / (237.3 + max_air_temperature))

# Calc. of saturated water vapor pressure at daily min temperature [kPA]
cdef float saturated_vapor_pressure_min
saturated_vapor_pressure_min = 0.6108 * exp((17.27 * min_air_temperature) / (237.3 + min_air_temperature))

# Calculation of the saturated water vapor pressure [kPA]
saturated_vapor_pressure = (saturated_vapor_pressure_max + saturated_vapor_pressure_min) / 2.0

# Calculation of the water vapor pressure
if relative_humidity <= 0.0:
    # Assuming Tdew = Tmin as suggested in FAO56 Allen et al. 1998
    vapor_pressure = saturated_vapor_pressure_min
else:
    vapor_pressure = relative_humidity * saturated_vapor_pressure
