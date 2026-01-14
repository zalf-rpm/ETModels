from datetime import datetime
from math import *
from Monica_Evapotranspiration.evapotranspiration import model_evapotranspiration, init_evapotranspiration
def model_evapotranspirationcomp(float evaporation_zeta,
      float maximum_evaporation_impact_depth,
      int no_of_soil_layers,
      int no_of_soil_moisture_layers,
      float layer_thickness[no_of_soil_moisture_layers],
      float reference_albedo,
      float stomata_resistance,
      int evaporation_reduction_method,
      float xsa_critical_soil_moisture,
      float external_reference_evapotranspiration,
      float height_nn,
      float max_air_temperature,
      float min_air_temperature,
      float mean_air_temperature,
      float relative_humidity,
      float wind_speed,
      float wind_speed_height,
      float global_radiation,
      int julian_day,
      float latitude,
      float evaporated_from_surface,
      float surface_water_storage,
      bool has_snow_cover,
      int developmental_stage,
      float crop_reference_evapotranspiration,
      float reference_evapotranspiration,
      float actual_evaporation,
      float actual_transpiration,
      float kc_factor,
      float percentage_soil_coverage,
      float soil_moisture[no_of_soil_moisture_layers],
      float permanent_wilting_point[no_of_soil_moisture_layers],
      float field_capacity[no_of_soil_moisture_layers],
      float evaporation[no_of_soil_moisture_layers],
      float transpiration[no_of_soil_moisture_layers],
      float crop_transpiration[no_of_soil_moisture_layers],
      float crop_remaining_evapotranspiration,
      float crop_evaporated_from_intercepted,
      float evapotranspiration[no_of_soil_moisture_layers],
      float actual_evapotranspiration,
      float vapor_pressure,
      float net_radiation):
    evaporated_from_surface, actual_evapotranspiration, reference_evapotranspiration, actual_evaporation, actual_transpiration, surface_water_storage, soil_moisture, net_radiation = model_evapotranspiration(evaporation_zeta,maximum_evaporation_impact_depth,reference_albedo,stomata_resistance,evaporation_reduction_method,xsa_critical_soil_moisture,latitude,height_nn,no_of_soil_layers,no_of_soil_moisture_layers,layer_thickness,permanent_wilting_point,field_capacity,external_reference_evapotranspiration,max_air_temperature,min_air_temperature,mean_air_temperature,relative_humidity,wind_speed,wind_speed_height,global_radiation,julian_day,kc_factor,has_snow_cover,developmental_stage,crop_reference_evapotranspiration,crop_transpiration,crop_remaining_evapotranspiration,crop_evaporated_from_intercepted,percentage_soil_coverage,vapor_pressure,surface_water_storage,evaporated_from_surface,reference_evapotranspiration,actual_evaporation,actual_transpiration,soil_moisture,evaporation,transpiration,evapotranspiration,actual_evapotranspiration,net_radiation)

    return (evaporated_from_surface, actual_evapotranspiration, reference_evapotranspiration, actual_evaporation, actual_transpiration, surface_water_storage, soil_moisture, net_radiation)

def init_evapotranspirationcomp(float evaporation_zeta,
                                  float maximum_evaporation_impact_depth,
                                  float reference_albedo,
                                  float stomata_resistance,
                                  int evaporation_reduction_method,
                                  float xsa_critical_soil_moisture,
                                  float latitude,
                                  float height_nn,
                                  int no_of_soil_layers,
                                  int no_of_soil_moisture_layers,
                                  float layer_thickness[no_of_soil_moisture_layers],
                                  float permanent_wilting_point[no_of_soil_moisture_layers],
                                  float field_capacity[no_of_soil_moisture_layers],
                                  float external_reference_evapotranspiration,
                                  float max_air_temperature,
                                  float min_air_temperature,
                                  float mean_air_temperature,
                                  float relative_humidity,
                                  float wind_speed,
                                  float wind_speed_height,
                                  float global_radiation,
                                  int julian_day,
                                  float kc_factor,
                                  bool has_snow_cover,
                                  int developmental_stage,
                                  float crop_reference_evapotranspiration,
                                  float crop_transpiration[no_of_soil_moisture_layers],
                                  float crop_remaining_evapotranspiration,
                                  float crop_evaporated_from_intercepted,
                                  float percentage_soil_coverage,
                                  float vapor_pressure):

    cdef float evaporated_from_surface
    cdef float surface_water_storage
    cdef float reference_evapotranspiration
    cdef float actual_evaporation
    cdef float actual_transpiration
    cdef float soil_moisture[no_of_soil_moisture_layers]
    cdef float evaporation[no_of_soil_moisture_layers]
    cdef float transpiration[no_of_soil_moisture_layers]
    cdef float evapotranspiration[no_of_soil_moisture_layers]
    cdef float actual_evapotranspiration
    cdef float net_radiation
    surface_water_storage, evaporated_from_surface, reference_evapotranspiration, actual_evaporation, actual_transpiration, soil_moisture, evaporation, transpiration, evapotranspiration, actual_evapotranspiration, net_radiation = init_evapotranspiration(evaporation_zeta, maximum_evaporation_impact_depth, reference_albedo, stomata_resistance, evaporation_reduction_method, xsa_critical_soil_moisture, latitude, height_nn, no_of_soil_layers, no_of_soil_moisture_layers, layer_thickness, permanent_wilting_point, field_capacity, external_reference_evapotranspiration, max_air_temperature, min_air_temperature, mean_air_temperature, relative_humidity, wind_speed, wind_speed_height, global_radiation, julian_day, kc_factor, has_snow_cover, developmental_stage, crop_reference_evapotranspiration, crop_transpiration, crop_remaining_evapotranspiration, crop_evaporated_from_intercepted, percentage_soil_coverage, vapor_pressure)
    return (surface_water_storage, evaporated_from_surface, reference_evapotranspiration, actual_evaporation, actual_transpiration, soil_moisture, evaporation, transpiration, evapotranspiration, actual_evapotranspiration, net_radiation)
