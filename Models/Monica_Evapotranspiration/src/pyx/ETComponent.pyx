from datetime import datetime
from math import *
from Monica_Evapotranspiration.radiation import model_radiation
from Monica_Evapotranspiration.netradiation import model_netradiation
from Monica_Evapotranspiration.stomataresistance import model_stomataresistance
from Monica_Evapotranspiration.saturatedvaporpressure import model_saturatedvaporpressure
from Monica_Evapotranspiration.saturationvaporpressuredeficit import model_saturationvaporpressuredeficit
from Monica_Evapotranspiration.referenceevapotranspiration import model_referenceevapotranspiration
from Monica_Evapotranspiration.potentialevapotranspiration import model_potentialevapotranspiration
from Monica_Evapotranspiration.evapotranspiration import model_evapotranspiration, init_evapotranspiration
def model_et(float latitude,
      float global_radiation,
      int julian_day,
      float sunshine_hours,
      float reference_albedo,
      float height_nn,
      float max_air_temperature,
      float min_air_temperature,
      float vapor_pressure,
      float relative_humidity,
      float saturation_beta,
      float stomata_conductance_alpha,
      int carboxylation_pathway,
      float gross_photosynthesis_reference_mol,
      float atmospheric_co2_concentration,
      float mean_air_temperature,
      float wind_speed,
      float wind_speed_height,
      float stomata_resistance,
      float net_radiation,
      float kc_factor,
      int developmental_stage,
      float crop_remaining_evapotranspiration,
      float evaporation_zeta,
      float maximum_evaporation_impact_depth,
      int evaporation_reduction_method,
      float xsa_critical_soil_moisture,
      int no_of_soil_layers,
      int no_of_soil_moisture_layers,
      float layer_thickness[no_of_soil_moisture_layers],
      float permanent_wilting_point[no_of_soil_moisture_layers],
      float field_capacity[no_of_soil_moisture_layers],
      bool has_snow_cover,
      float crop_transpiration[no_of_soil_moisture_layers],
      float crop_evaporated_from_intercepted,
      float percentage_soil_coverage,
      float surface_water_storage,
      float evaporated_from_surface,
      float actual_evaporation,
      float actual_transpiration,
      float soil_moisture[no_of_soil_moisture_layers],
      float evaporation[no_of_soil_moisture_layers],
      float transpiration[no_of_soil_moisture_layers],
      float evapotranspiration[no_of_soil_moisture_layers],
      float actual_evapotranspiration):
    cdef float declination
    cdef float astronomic_daylength
    cdef float effective_daylength
    cdef float photoperiodic_daylength
    cdef float sunshine_hours_global_radiation
    cdef float extraterrestrial_radiation
    cdef float clear_day_radiation
    cdef float overcast_day_radiation
    cdef float phot_act_radiation_mean
    cdef float saturation_vapor_pressure_deficit
    cdef float saturated_vapor_pressure
    cdef float reference_evapotranspiration
    cdef float potential_evapotranspiration_cap
    cdef float potential_evapotranspiration
    declination, astronomic_daylength, effective_daylength, photoperiodic_daylength, sunshine_hours_global_radiation, extraterrestrial_radiation, clear_day_radiation, overcast_day_radiation, phot_act_radiation_mean = model_radiation(latitude,global_radiation,julian_day,sunshine_hours)
    saturated_vapor_pressure, vapor_pressure = model_saturatedvaporpressure(max_air_temperature,min_air_temperature,relative_humidity)
    net_radiation = model_netradiation(height_nn,reference_albedo,global_radiation,max_air_temperature,min_air_temperature,vapor_pressure,extraterrestrial_radiation)
    saturation_vapor_pressure_deficit = model_saturationvaporpressuredeficit(vapor_pressure,saturated_vapor_pressure)
    stomata_resistance = model_stomataresistance(saturation_beta,stomata_conductance_alpha,carboxylation_pathway,atmospheric_co2_concentration,saturation_vapor_pressure_deficit,gross_photosynthesis_reference_mol)
    reference_evapotranspiration = model_referenceevapotranspiration(height_nn,mean_air_temperature,wind_speed,wind_speed_height,net_radiation,stomata_resistance,saturation_vapor_pressure_deficit)
    potential_evapotranspiration = model_potentialevapotranspiration(potential_evapotranspiration_cap,kc_factor,developmental_stage,crop_remaining_evapotranspiration,reference_evapotranspiration)
    evaporated_from_surface, actual_evapotranspiration, actual_evaporation, actual_transpiration, surface_water_storage, soil_moisture = model_evapotranspiration(evaporation_zeta,maximum_evaporation_impact_depth,evaporation_reduction_method,xsa_critical_soil_moisture,no_of_soil_layers,no_of_soil_moisture_layers,layer_thickness,permanent_wilting_point,field_capacity,kc_factor,has_snow_cover,developmental_stage,crop_transpiration,crop_evaporated_from_intercepted,percentage_soil_coverage,potential_evapotranspiration,surface_water_storage,evaporated_from_surface,actual_evaporation,actual_transpiration,soil_moisture,evaporation,transpiration,evapotranspiration,actual_evapotranspiration)

    return (reference_evapotranspiration, potential_evapotranspiration)

def init_et(float latitude,
              float global_radiation,
              int julian_day,
              float sunshine_hours,
              float height_nn,
              float reference_albedo,
              float max_air_temperature,
              float min_air_temperature,
              float vapor_pressure,
              float extraterrestrial_radiation,
              float saturation_beta,
              float stomata_conductance_alpha,
              int carboxylation_pathway,
              float atmospheric_co2_concentration,
              float relative_humidity,
              float mean_air_temperature,
              float wind_speed,
              float wind_speed_height,
              float potential_evapotranspiration_cap,
              float kc_factor,
              int developmental_stage,
              float crop_remaining_evapotranspiration,
              float evaporation_zeta,
              float maximum_evaporation_impact_depth,
              int evaporation_reduction_method,
              float xsa_critical_soil_moisture,
              int no_of_soil_layers,
              int no_of_soil_moisture_layers,
              float layer_thickness[no_of_soil_moisture_layers],
              float permanent_wilting_point[no_of_soil_moisture_layers],
              float field_capacity[no_of_soil_moisture_layers],
              bool has_snow_cover,
              float crop_transpiration[no_of_soil_moisture_layers],
              float crop_evaporated_from_intercepted,
              float percentage_soil_coverage):

    cdef float surface_water_storage
    cdef float evaporated_from_surface
    cdef float actual_evaporation
    cdef float actual_transpiration
    cdef float soil_moisture[no_of_soil_moisture_layers]
    cdef float evaporation[no_of_soil_moisture_layers]
    cdef float transpiration[no_of_soil_moisture_layers]
    cdef float evapotranspiration[no_of_soil_moisture_layers]
    cdef float actual_evapotranspiration
    surface_water_storage, evaporated_from_surface, actual_evaporation, actual_transpiration, soil_moisture, evaporation, transpiration, evapotranspiration, actual_evapotranspiration = init_evapotranspiration(evaporation_zeta, maximum_evaporation_impact_depth, evaporation_reduction_method, xsa_critical_soil_moisture, no_of_soil_layers, no_of_soil_moisture_layers, layer_thickness, permanent_wilting_point, field_capacity, kc_factor, has_snow_cover, developmental_stage, crop_transpiration, crop_evaporated_from_intercepted, percentage_soil_coverage)
    return (surface_water_storage, evaporated_from_surface, actual_evaporation, actual_transpiration, soil_moisture, evaporation, transpiration, evapotranspiration, actual_evapotranspiration)
