library(gsubfn)

#' Model of evapotranspiration
#'
#' This function compute the Model of evapotranspiration
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
#' @param snow_depth (mm) depth of snow layer state (0, 0-) 
#' @param developmental_stage (dimensionless) MONICA crop developmental stage state (0, 0-6) 
#' @param crop_reference_evapotranspiration (mm) the crop specific ET0, if no external ET0 and crop is planted state (-1, 0-) 
#' @param reference_evapotranspiration (mm) reference evapotranspiration (ET0) state (0, 0-) 
#' @param actual_evaporation (mm) actual evaporation state (0, 0-) 
#' @param actual_transpiration (mm) actual transpiration state (0, 0-) 
#' @param surface_water_storage (mm) Simulates a virtual layer that contains the surface water state (0, 0-) 
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
model_evapotranspiration <- function (evaporation_zeta,
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
         snow_depth,
         developmental_stage,
         crop_reference_evapotranspiration,
         reference_evapotranspiration,
         actual_evaporation,
         actual_transpiration,
         surface_water_storage,
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
    evaporated_from_surface <- 0.0
    potential_evapotranspiration <- 0.0
    evaporated_from_intercept <- 0.0
    if (developmental_stage > 0)
    {
        if (external_reference_evapotranspiration < 0.0)
        {
            reference_evapotranspiration <- reference_evapotranspiration
        }
        else
        {
            reference_evapotranspiration <- external_reference_evapotranspiration
        }
        potential_evapotranspiration <- crop_remaining_evapotranspiration
        evaporated_from_intercept <- crop_evaporated_from_intercepted
    }
    else
    {
        if (external_reference_evapotranspiration < 0.0)
        {
            result <-  calc_reference_evapotranspiration(height_nn, max_air_temperature, min_air_temperature, relative_humidity, mean_air_temperature, wind_speed, wind_speed_height, global_radiation, julian_day, latitude, reference_albedo, vapor_pressure, stomata_resistance)
            reference_evapotranspiration <- result[[1]]
            vapor_pressure <- result[[2]]
        }
        else
        {
            reference_evapotranspiration <- external_reference_evapotranspiration
        }
        potential_evapotranspiration <- reference_evapotranspiration * kc_factor
    }
    actual_evaporation <- 0.0
    actual_transpiration <- 0.0
    if (potential_evapotranspiration > 6.5)
    {
        potential_evapotranspiration <- 6.5
    }
    evaporation_from_surface <- FALSE
    eRed1 <- 0.0
    eRed2 <- 0.0
    eRed3 <- 0.0
    eReducer <- 0.0
    i <- 0
    if (potential_evapotranspiration > 0.0)
    {
        evaporation_from_surface <- FALSE
        if (surface_water_storage > 0.0)
        {
            evaporation_from_surface <- TRUE
            potential_evapotranspiration <- potential_evapotranspiration * 1.1 / kc_factor
            if (snow_depth > 0.0)
            {
                evaporated_from_surface <- 0.0
            }
            else if ( surface_water_storage < potential_evapotranspiration)
            {
                potential_evapotranspiration <- potential_evapotranspiration - surface_water_storage
                evaporated_from_surface <- surface_water_storage
                surface_water_storage <- 0.0
            }
            else
            {
                surface_water_storage <- surface_water_storage - potential_evapotranspiration
                evaporated_from_surface <- potential_evapotranspiration
                potential_evapotranspiration <- 0.0
            }
            potential_evapotranspiration <- potential_evapotranspiration * kc_factor / 1.1
        }
        if (potential_evapotranspiration > 0.0)
        {
            for( i in seq(0, no_of_soil_layers-1, 1)){
                eRed1 <- e_reducer_1(permanent_wilting_point[i+1], field_capacity[i+1], soil_moisture[i+1], percentage_soil_coverage, potential_evapotranspiration, evaporation_reduction_method, xsa_critical_soil_moisture)
                eRed2 <- 0.0
                if (as.double(i) >= maximum_evaporation_impact_depth)
                {
                    eRed2 <- 0.0
                }
                else
                {
                    eRed2 <- get_deprivation_factor(i + 1, maximum_evaporation_impact_depth, evaporation_zeta, layer_thickness[i+1])
                }
                eRed3 <- 0.0
                if (i > 0 && soil_moisture[i+1] < soil_moisture[i - 1+1])
                {
                    eRed3 <- 0.1
                }
                else
                {
                    eRed3 <- 1.0
                }
                eReducer <- eRed1 * eRed2 * eRed3
                if (developmental_stage > 0)
                {
                    if (percentage_soil_coverage >= 0.0 && percentage_soil_coverage < 1.0)
                    {
                        evaporation[i+1] <- (1.0 - percentage_soil_coverage) * eReducer * potential_evapotranspiration
                    }
                    else if ( percentage_soil_coverage >= 1.0)
                    {
                        evaporation[i+1] <- 0.0
                    }
                    if (snow_depth > 0.0)
                    {
                        evaporation[i+1] <- 0.0
                    }
                    transpiration[i+1] <- crop_transpiration[i+1]
                    if (evaporation_from_surface)
                    {
                        transpiration[i+1] <- percentage_soil_coverage * eReducer * potential_evapotranspiration
                    }
                }
                else
                {
                    if (snow_depth > 0.0)
                    {
                        evaporation[i+1] <- 0.0
                    }
                    else
                    {
                        evaporation[i+1] <- potential_evapotranspiration * eReducer
                        transpiration[i+1] <- 0.0
                    }
                }
                evapotranspiration[i+1] <- evaporation[i+1] + transpiration[i+1]
                soil_moisture[i+1] <- soil_moisture[i+1] - (evapotranspiration[i+1] / 1000.0 / layer_thickness[i+1])
                if (soil_moisture[i+1] < 0.01)
                {
                    soil_moisture[i+1] <- 0.01
                }
                actual_transpiration <- actual_transpiration + transpiration[i+1]
                actual_evaporation <- actual_evaporation + evaporation[i+1]
            }
        }
        actual_evapotranspiration <- actual_transpiration + actual_evaporation + evaporated_from_intercept + evaporated_from_surface
    }
    return (list ("evaporated_from_surface" = evaporated_from_surface,"actual_evapotranspiration" = actual_evapotranspiration))
}

get_deprivation_factor <- function (layer_no,
         deprivation_depth,
         zeta,
         layer_thickness){
    ltf <- 0.0
    ltf <- deprivation_depth / (layer_thickness * 10.0)
    deprivation_factor <- 0.0
    c2 <- 0.0
    c3 <- 0.0
    if (abs(zeta) < 0.0003)
    {
        deprivation_factor <- 2.0 / ltf - (1.0 / (ltf * ltf) * (2 * layer_no - 1))
    }
    else
    {
        c2 <- log((ltf + (zeta * layer_no)) / (ltf + (zeta * (layer_no - 1))))
        c3 <- zeta / (ltf * (zeta + 1.0))
        deprivation_factor <- (c2 - c3) / (log(zeta + 1.0) - (zeta / (zeta + 1.0)))
    }
    return( deprivation_factor)
}

bound <- function (lower,
         value,
         upper){
    if (value < lower)
    {
        return( lower)
    }
    if (value > upper)
    {
        return( upper)
    }
    return( value)
}

calc_reference_evapotranspiration <- function (height_nn,
         max_air_temperature,
         min_air_temperature,
         relative_humidity,
         mean_air_temperature,
         wind_speed,
         wind_speed_height,
         global_radiation,
         julian_day,
         latitude,
         reference_albedo,
         vapor_pressure,
         stomata_resistance){
    declination <- 0.0
    declination <- -23.4 * cos(2.0 * pi * ((julian_day + 10.0) / 365.0))
    declination_sinus <- 0.0
    declination_sinus <- sin(declination * pi / 180.0) * sin(latitude * pi / 180.0)
    declination_cosinus <- 0.0
    declination_cosinus <- cos(declination * pi / 180.0) * cos(latitude * pi / 180.0)
    arg_astro_day_length <- 0.0
    arg_astro_day_length <- declination_sinus / declination_cosinus
    arg_astro_day_length <- bound(-1.0, arg_astro_day_length, 1.0)
    astronomic_day_length <- 0.0
    astronomic_day_length <- 12.0 * (pi + (2.0 * asin(arg_astro_day_length))) / pi
    arg_effective_day_length <- 0.0
    arg_effective_day_length <- (-sin((8.0 * pi / 180.0)) + declination_sinus) / declination_cosinus
    arg_effective_day_length <- bound(-1.0, arg_effective_day_length, 1.0)
    arg_photo_day_length <- 0.0
    arg_photo_day_length <- (-sin((-6.0 * pi / 180.0)) + declination_sinus) / declination_cosinus
    arg_photo_day_length <- bound(-1.0, arg_photo_day_length, 1.0)
    arg_phot_act <- 0.0
    arg_phot_act <- min(1.0, declination_sinus / declination_cosinus * (declination_sinus / declination_cosinus))
    phot_act_radiation_mean <- 0.0
    phot_act_radiation_mean <- 3600.0 * (declination_sinus * astronomic_day_length + (24.0 / pi * declination_cosinus * sqrt((1.0 - arg_phot_act))))
    clear_day_radiation <- 0.0
    if (phot_act_radiation_mean > 0.0 && astronomic_day_length > 0.0)
    {
        clear_day_radiation <- 0.5 * 1300.0 * phot_act_radiation_mean * exp(-0.14 / (phot_act_radiation_mean / (astronomic_day_length * 3600.0)))
    }
    SC <- 0.0
    SC <- 24.0 * 60.0 / pi * 8.20 * (1.0 + (0.033 * cos(2.0 * pi * julian_day / 365.0)))
    arg_SHA <- 0.0
    arg_SHA <- bound(-1.0, -tan((latitude * pi / 180.0)) * tan(declination * pi / 180.0), 1.0)
    SHA <- 0.0
    SHA <- acos(arg_SHA)
    extraterrestrial_radiation <- 0.0
    extraterrestrial_radiation <- SC * (SHA * declination_sinus + (declination_cosinus * sin(SHA))) / 100.0
    atmospheric_pressure <- 0.0
    atmospheric_pressure <- 101.3 * ((293.0 - (0.0065 * height_nn)) / 293.0) ^ 5.26
    psycrometer_constant <- 0.0
    psycrometer_constant <- 0.000665 * atmospheric_pressure
    saturated_vapor_pressure_max <- 0.0
    saturated_vapor_pressure_max <- 0.6108 * exp(17.27 * max_air_temperature / (237.3 + max_air_temperature))
    saturated_vapor_pressure_min <- 0.0
    saturated_vapor_pressure_min <- 0.6108 * exp(17.27 * min_air_temperature / (237.3 + min_air_temperature))
    saturated_vapor_pressure <- 0.0
    saturated_vapor_pressure <- (saturated_vapor_pressure_max + saturated_vapor_pressure_min) / 2.0
    if (vapor_pressure < 0.0)
    {
        if (relative_humidity <= 0.0)
        {
            vapor_pressure <- saturated_vapor_pressure_min
        }
        else
        {
            vapor_pressure <- relative_humidity * saturated_vapor_pressure
        }
    }
    saturation_deficit <- 0.0
    saturation_deficit <- saturated_vapor_pressure - vapor_pressure
    saturated_vapour_pressure_slope <- 0.0
    saturated_vapour_pressure_slope <- 4098.0 * (0.6108 * exp(17.27 * mean_air_temperature / (mean_air_temperature + 237.3))) / ((mean_air_temperature + 237.3) * (mean_air_temperature + 237.3))
    wind_speed_2m <- 0.0
    wind_speed_2m <- max(0.5, wind_speed * (4.87 / log((67.8 * wind_speed_height - 5.42))))
    surface_resistance <- 0.0
    surface_resistance <- stomata_resistance / 1.44
    clear_sky_solar_radiation <- 0.0
    clear_sky_solar_radiation <- (0.75 + (0.00002 * height_nn)) * extraterrestrial_radiation
    relative_shortwave_radiation <- 0.0
    relative_shortwave_radiation <-  if (clear_sky_solar_radiation > 0.0)min(global_radiation / clear_sky_solar_radiation, 1.0) else 1.0
    bolzmann_constant <- 0.0000000049
    shortwave_radiation <- 0.0
    shortwave_radiation <- (1.0 - reference_albedo) * global_radiation
    longwave_radiation <- 0.0
    longwave_radiation <- bolzmann_constant * (((min_air_temperature + 273.16) ^ 4.0 + (max_air_temperature + 273.16) ^ 4.0) / 2.0) * (1.35 * relative_shortwave_radiation - 0.35) * (0.34 - (0.14 * sqrt(vapor_pressure)))
    net_radiation <- 0.0
    net_radiation <- shortwave_radiation - longwave_radiation
    reference_evapotranspiration <- 0.0
    reference_evapotranspiration <- (0.408 * saturated_vapour_pressure_slope * net_radiation + (psycrometer_constant * (900.0 / (mean_air_temperature + 273.0)) * wind_speed_2m * saturation_deficit)) / (saturated_vapour_pressure_slope + (psycrometer_constant * (1.0 + (surface_resistance / 208.0 * wind_speed_2m))))
    if (reference_evapotranspiration < 0.0)
    {
        reference_evapotranspiration <- 0.0
    }
    return (list ("reference_evapotranspiration" = reference_evapotranspiration,"vapor_pressure" = vapor_pressure))
}

e_reducer_1 <- function (pwp,
         fc,
         sm,
         percentage_soil_coverage,
         reference_evapotranspiration,
         evaporation_reduction_method,
         xsa_critical_soil_moisture){
    sm <- max(0.33 * pwp, sm)
    relative_evaporable_water <- 0.0
    relative_evaporable_water <- min(1.0, (sm - (0.33 * pwp)) / (fc - (0.33 * pwp)))
    e_reduction_factor <- 0.0
    critical_soil_moisture <- 0.0
    reducer <- 0.0
    xsa <- 0.0
    if (evaporation_reduction_method == 0)
    {
        critical_soil_moisture <- 0.65 * fc
        if (percentage_soil_coverage > 0.0)
        {
            reducer <- 1.0
            if (reference_evapotranspiration > 2.5)
            {
                xsa <- (0.65 * fc - pwp) * (fc - pwp)
                reducer <- xsa + ((1 - xsa) / 17.5 * (reference_evapotranspiration - 2.5))
            }
            else
            {
                reducer <- xsa_critical_soil_moisture / 2.5 * reference_evapotranspiration
            }
            critical_soil_moisture <- fc * reducer
        }
        if (sm > critical_soil_moisture)
        {
            e_reduction_factor <- 1.0
        }
        else if ( sm > (0.33 * pwp))
        {
            e_reduction_factor <- relative_evaporable_water
        }
        else
        {
            e_reduction_factor <- 0.0
        }
    }
    else
    {
        if (relative_evaporable_water > 0.33)
        {
            e_reduction_factor <- 1.0 - (0.1 * (1.0 - relative_evaporable_water) / (1.0 - 0.33))
        }
        else if ( relative_evaporable_water > 0.22)
        {
            e_reduction_factor <- 0.9 - (0.625 * (0.33 - relative_evaporable_water) / (0.33 - 0.22))
        }
        else if ( relative_evaporable_water > 0.2)
        {
            e_reduction_factor <- 0.275 - (0.225 * (0.22 - relative_evaporable_water) / (0.22 - 0.2))
        }
        else
        {
            e_reduction_factor <- 0.05 - (0.05 * (0.2 - relative_evaporable_water) / 0.2)
        }
    }
    return( e_reduction_factor)
}