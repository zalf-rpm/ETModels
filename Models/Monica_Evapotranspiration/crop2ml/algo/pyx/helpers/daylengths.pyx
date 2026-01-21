from math import *

def calc_declinations(latitude, julian_day):
  # calculation of declination - old DEC
  cdef float declination
  declination = -23.4 * cos(2.0 * pi * ((julian_day + 10.0) / 365.0))

  # old SINLD
  cdef float decl_sin
  decl_sin = sin(declination * pi / 180.0) * sin(latitude * pi / 180.0)

  # old COSLD
  cdef float decl_cos
  decl_cos = cos(declination * pi / 180.0) * cos(latitude * pi / 180.0)

  return declination, decl_sin, decl_cos


def calc_astronomic_daylength(latitude, julian_day):
  cdef float declination, decl_sin, decl_cos
  declination, decl_sin, decl_cos = calc_declinations(latitude, julian_day)

  # Calculation of the atmospheric day length -> old DL
  cdef float astro_daylength
  astro_daylength = decl_sin / decl_cos
  astro_daylength = bound(-1.0, astro_daylength, 1.0) # The argument of asin must be in the range of -1 to 1
  cdef float astronomic_daylength
  astronomic_daylength = 12.0 * (pi + 2.0 * asin(astronomic_daylength)) / pi

  return astronomic_daylength


def calc_effective_daylength(latitude, julian_day):
  cdef float declination, decl_sin, decl_cos
  declination, decl_sin, decl_cos = calc_declinations(latitude, julian_day)

  # Calculation of the effective day length = old DLE
  cdef float eff_daylength
  eff_daylength = (-sin(8.0 * pi / 180.0) + decl_sin) / decl_cos
  eff_daylength = bound(-1.0, eff_daylength, 1.0) # The argument of asin must be in the range of -1 to 1
  cdef float effective_daylength
  effective_daylength = 12.0 * (pi + 2.0 * asin(eff_daylength)) / pi

  return effective_daylength


def calc_photoperiodic_daylength(latitude, julian_day):
  cdef float declination, decl_sin, decl_cos
  declination, decl_sin, decl_cos = calc_declinations(latitude, julian_day)

  # old DLP
  cdef float photo_daylength
  photo_daylength = (-sin(-6.0 * pi / 180.0) + decl_sin) / decl_cos
  photo_daylength = bound(-1.0, photo_daylength, 1.0) # The argument of asin must be in the range of -1 to 1
  cdef float photoperiodicDayLength
  photoperiodic_daylength = 12.0 * (pi + 2.0 * asin(photo_daylength)) / pi

  return photoperiodic_daylength