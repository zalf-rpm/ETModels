from datetime import datetime
from math import *
from Monica_Evapotranspiration.evapotranspiration import model_evapotranspiration, init_evapotranspiration
from Monica_Evapotranspiration.reference_evapotranspiration import model_reference_evapotranspiration, init_reference_evapotranspiration
from Monica_Evapotranspiration.potential_evapotranspiration import model_potential_evapotranspiration, init_potential_evapotranspiration
def model_etcomp(float reference_albedo,
      float stomata_resistance,
      float latitude,
      float height_nn,
      float max_air_temperature,
      float min_air_temperature,
      float mean_air_temperature,
      float relative_humidity,
      float wind_speed,
      float wind_speed_height,
      float global_radiation,
      int julian_day,
      float vapor_pressure,
      float net_radiation,
      float external_reference_evapotranspiration,
      float kc_factor,
      int developmental_stage,
      float crop_reference_evapotranspiration,
      float crop_remaining_evapotranspiration,
      float potential_evapotranspiration,
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
    cdef float reference_evapotranspiration
    reference_evapotranspiration, net_radiation = model_reference_evapotranspiration(reference_albedo,stomata_resistance,latitude,height_nn,max_air_temperature,min_air_temperature,mean_air_temperature,relative_humidity,wind_speed,wind_speed_height,global_radiation,julian_day,vapor_pressure,net_radiation,reference_evapotranspiration)
    potential_evapotranspiration, reference_evapotranspiration = model_potential_evapotranspiration(reference_evapotranspiration,external_reference_evapotranspiration,kc_factor,developmental_stage,crop_reference_evapotranspiration,crop_remaining_evapotranspiration,potential_evapotranspiration)
    evaporated_from_surface, actual_evapotranspiration, actual_evaporation, actual_transpiration, surface_water_storage, soil_moisture = model_evapotranspiration(evaporation_zeta,maximum_evaporation_impact_depth,evaporation_reduction_method,xsa_critical_soil_moisture,no_of_soil_layers,no_of_soil_moisture_layers,layer_thickness,permanent_wilting_point,field_capacity,kc_factor,has_snow_cover,developmental_stage,crop_transpiration,crop_evaporated_from_intercepted,percentage_soil_coverage,potential_evapotranspiration,surface_water_storage,evaporated_from_surface,actual_evaporation,actual_transpiration,soil_moisture,evaporation,transpiration,evapotranspiration,actual_evapotranspiration)

    return (net_radiation, potential_evapotranspiration, reference_evapotranspiration, evaporated_from_surface, actual_evapotranspiration, actual_evaporation, actual_transpiration, surface_water_storage, soil_moisture)

def init_etcomp(float evaporation_zeta,
                  float maximum_evaporation_impact_depth,
                  int evaporation_reduction_method,
                  float xsa_critical_soil_moisture,
                  int no_of_soil_layers,
                  int no_of_soil_moisture_layers,
                  float layer_thickness[no_of_soil_moisture_layers],
                  float permanent_wilting_point[no_of_soil_moisture_layers],
                  float field_capacity[no_of_soil_moisture_layers],
                  float kc_factor,
                  bool has_snow_cover,
                  int developmental_stage,
                  float crop_transpiration[no_of_soil_moisture_layers],
                  float crop_evaporated_from_intercepted,
                  float percentage_soil_coverage,
                  float reference_albedo,
                  float stomata_resistance,
                  float latitude,
                  float height_nn,
                  float max_air_temperature,
                  float min_air_temperature,
                  float mean_air_temperature,
                  float relative_humidity,
                  float wind_speed,
                  float wind_speed_height,
                  float global_radiation,
                  int julian_day,
                  float vapor_pressure,
                  float reference_evapotranspiration,
                  float external_reference_evapotranspiration,
                  float crop_reference_evapotranspiration,
                  float crop_remaining_evapotranspiration):

    cdef float net_radiation
    cdef float potential_evapotranspiration
    cdef float surface_water_storage
    cdef float evaporated_from_surface
    cdef float actual_evaporation
    cdef float actual_transpiration
    cdef float soil_moisture[no_of_soil_moisture_layers]
    cdef float evaporation[no_of_soil_moisture_layers]
    cdef float transpiration[no_of_soil_moisture_layers]
    cdef float evapotranspiration[no_of_soil_moisture_layers]
    cdef float actual_evapotranspiration
    potential_evapotranspiration, surface_water_storage, evaporated_from_surface, actual_evaporation, actual_transpiration, soil_moisture, evaporation, transpiration, evapotranspiration, actual_evapotranspiration = init_evapotranspiration(evaporation_zeta, maximum_evaporation_impact_depth, evaporation_reduction_method, xsa_critical_soil_moisture, no_of_soil_layers, no_of_soil_moisture_layers, layer_thickness, permanent_wilting_point, field_capacity, kc_factor, has_snow_cover, developmental_stage, crop_transpiration, crop_evaporated_from_intercepted, percentage_soil_coverage)
    net_radiation, reference_evapotranspiration = init_reference_evapotranspiration(reference_albedo, stomata_resistance, latitude, height_nn, max_air_temperature, min_air_temperature, mean_air_temperature, relative_humidity, wind_speed, wind_speed_height, global_radiation, julian_day, vapor_pressure)
    potential_evapotranspiration = init_potential_evapotranspiration(reference_evapotranspiration, external_reference_evapotranspiration, kc_factor, developmental_stage, crop_reference_evapotranspiration, crop_remaining_evapotranspiration)
    return (potential_evapotranspiration, surface_water_storage, evaporated_from_surface, actual_evaporation, actual_transpiration, soil_moisture, evaporation, transpiration, evapotranspiration, actual_evapotranspiration, net_radiation, reference_evapotranspiration)
