declination, decl_sin, decl_cos = calc_declinations(latitude, julian_day)
astronomic_daylength = calc_astronomic_daylength(latitude, julian_day)
effective_daylength = calc_effective_daylength(latitude, julian_day)
photoperiodic_daylength = calc_photoperiodic_daylength(latitude, julian_day)

# The argument of sqrt must be >= 0
cdef float arg_phot_act
arg_phot_act = min(1.0, ((decl_sin / decl_cos) * (decl_sin / decl_cos)))
cdef float phot_act_radiation_mean
phot_act_radiation_mean = 3600.0 * (decl_sin * astronomic_daylength + 24.0
                                    / pi * decl_cos * sqrt(1.0 - arg_phot_act))

cdef float clear_day_radiation = 0.0
if phot_act_radiation_mean > 0.0 and astronomic_daylength > 0.0:
    clear_day_radiation = (0.5 * 1300.0 * phot_act_radiation_mean
                           * exp(-0.14 / (phot_act_radiation_mean / (astronomic_daylength * 3600.0))))

# Calculation of radiation on an overcast day [J m-2] - old DRO
cdef float overcast_day_radiation
overcast_day_radiation = 0.2 * clear_day_radiation

# Calculation of extraterrestrial radiation - old EXT
cdef float solar_constant
solar_constant = 0.082 # [MJ m-2 d-1] Note: Here is the difference to HERMES, which calculates in [J cm-2 d-1]!
cdef float SC
SC = 24.0 * 60.0 / pi * solar_constant * (1.0 + 0.033 * cos(2.0 * pi * julian_day / 365.0))
cdef float solar_angle
solar_angle = -tan(latitude * pi / 180.0) * tan(declination * pi / 180.0)
solar_angle = bound(-1.0, solar_angle, 1.0)
cdef float sunset_solar_angle
sunset_solar_angle = acos(solar_angle)
cdef float extraterrestrial_radiation # [MJ m - 2]
extraterrestrial_radiation = SC * (sunset_solar_angle * decl_sin + decl_cos * sin(sunset_solar_angle))


if global_radiation <= 0.0 and astronomic_daylength > 0:
    global_radiation = extraterrestrial_radiation * (0.19 + 0.55 * sunshine_hours / astronomic_daylength)
else:
    global_radiation = 0


