library(gsubfn)

#' Evapotranspiration model
#'
#' This function compute the Evapotranspiration model
#' @param evaporation_zeta (dimensionless) shape factor constant (40, 0-40) 
#' @param maximum_evaporation_impact_depth (dm) maximumEvaporationImpactDepth constant (5, 0-) 
#' @param no_of_soil_layers (dimensionless) number of soil layers constant (20, 0-) 
#' @param layer_thickness (m) layer thickness array constant (, -) 
#' @param reference_albedo (dimensionless) reference albedo constant (0, 0-1) 
#' @param stomata_resistance (s/m) stomata resistance constant (100, 0-10000) 
#' @param evaporation_reduction_method (dimensionless) THESEUS (0) or HERMES (1) evaporation reduction method constant (1, 0-1) 
#' @param xsa_critical_soil_moisture (m3/m3) XSACriticalSoilMoisture constant (0.1, 0-1.5) 
#' @param external_reference_evapotranspiration (mm) externally supplied ET0 exogenous (-1, 0-) 
#' @param height_nn (m) height above sea leavel exogenous (0, -9999-9999) 
#' @param max_air_temperature (°C) daily maximum air temperature exogenous (0, -100-100) 
#' @param min_air_temperature (°C) daily minimum air temperature exogenous (0, -100-100) 
#' @param mean_air_temperature (°C) daily average air temperature exogenous (0, -100-100) 
#' @param relative_humidity (fraction) relative humidity exogenous (0, 0-1) 
#' @param wind_speed (m/s) wind speed measured at wind speed height exogenous (0, 0-9999) 
#' @param wind_speed_height (m) height at which the wind speed has been measured exogenous (2, -9999-9999) 
#' @param global_radiation (MJ/m2) global radiation exogenous (0, 0-50) 
#' @param julian_day (day) day of year exogenous (1, 1-366) 
#' @param latitude (degree) latitude exogenous (0, -90-90) 
#' @param evaporated_from_surface (mm) evaporated_from_surface state (0, 0-) 
#' @param surface_water_storage (mm) Simulates a virtual layer that contains the surface water state (0, 0-) 
#' @param snow_depth (mm) depth of snow layer state (0, 0-) 
#' @param developmental_stage (dimensionless) MONICA crop developmental stage state (0, 0-6) 
#' @param crop_reference_evapotranspiration (mm) the crop specific ET0, if no external ET0 and crop is planted state (-1, 0-) 
#' @param reference_evapotranspiration (mm) reference evapotranspiration (ET0) state (0, 0-) 
#' @param actual_evaporation (mm) actual evaporation state (0, 0-) 
#' @param actual_transpiration (mm) actual transpiration state (0, 0-) 
#' @param kc_factor (dimensionless) crop coefficient ETc/ET0 state (0.75, 0-) 
#' @param percentage_soil_coverage (m2/m2) fraction of soil covered by crop state (0, 0-1) 
#' @param soil_moisture (m3/m3) soil moisture array state (, -) 
#' @param permanent_wilting_point (m3/m3) permanent wilting point array state (0, 0-2) 
#' @param field_capacity (m3/m3) field capacity array state (0, 0-1) 
#' @param evaporation (mm) evaporation array state (0, 0-1) 
#' @param transpiration (mm) transpiration array state (, -) 
#' @param crop_transpiration (mm) crop transpiration array state (, -) 
#' @param crop_remaining_evapotranspiration (mm) crop remaining evapotranspiration state (, -) 
#' @param crop_evaporated_from_intercepted (mm) crop evaporated water from intercepted water state (, -) 
#' @param evapotranspiration (mm) evapotranspiration array state (, -) 
#' @param actual_evapotranspiration (mm) actual evapotranspiration state (0, 0-) 
#' @param vapor_pressure (kPa) vapor pressure state (0, 0-) 
#'
#' @return
#' \describe{
#'   \item{evaporated_from_surface (mm)}{ state (0-)} 
#'   \item{actual_evapotranspiration (mm)}{actual evapotranspiration state (0-)} 
#' }
#' @export
model_evapotranspirationcomp <- function (evaporation_zeta,
         maximum_evaporation_impact_depth,
         no_of_soil_layers,
         layer_thickness,
         reference_albedo,
         stomata_resistance,
         evaporation_reduction_method,
         xsa_critical_soil_moisture,
         external_reference_evapotranspiration,
         height_nn,
         max_air_temperature,
         min_air_temperature,
         mean_air_temperature,
         relative_humidity,
         wind_speed,
         wind_speed_height,
         global_radiation,
         julian_day,
         latitude,
         evaporated_from_surface,
         surface_water_storage,
         snow_depth,
         developmental_stage,
         crop_reference_evapotranspiration,
         reference_evapotranspiration,
         actual_evaporation,
         actual_transpiration,
         kc_factor,
         percentage_soil_coverage,
         soil_moisture,
         permanent_wilting_point,
         field_capacity,
         evaporation,
         transpiration,
         crop_transpiration,
         crop_remaining_evapotranspiration,
         crop_evaporated_from_intercepted,
         evapotranspiration,
         actual_evapotranspiration,
         vapor_pressure){
    result <-  model_evapotranspiration(evaporation_zeta, maximum_evaporation_impact_depth, no_of_soil_layers, layer_thickness, reference_albedo, stomata_resistance, evaporation_reduction_method, xsa_critical_soil_moisture, external_reference_evapotranspiration, height_nn, max_air_temperature, min_air_temperature, mean_air_temperature, relative_humidity, wind_speed, wind_speed_height, global_radiation, julian_day, latitude, evaporated_from_surface, snow_depth, developmental_stage, crop_reference_evapotranspiration, reference_evapotranspiration, actual_evaporation, actual_transpiration, surface_water_storage, kc_factor, percentage_soil_coverage, soil_moisture, permanent_wilting_point, field_capacity, evaporation, transpiration, crop_transpiration, crop_remaining_evapotranspiration, crop_evaporated_from_intercepted, evapotranspiration, actual_evapotranspiration, vapor_pressure)
    evaporated_from_surface <- result[[1]]
    actual_evapotranspiration <- result[[2]]
    return (list ("evaporated_from_surface" = evaporated_from_surface,"actual_evapotranspiration" = actual_evapotranspiration))
}