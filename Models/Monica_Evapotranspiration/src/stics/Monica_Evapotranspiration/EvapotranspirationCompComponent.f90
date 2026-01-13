MODULE Evapotranspirationcompmod
    USE Evapotranspirationmod
    IMPLICIT NONE
CONTAINS

    SUBROUTINE model_evapotranspirationcomp(evaporation_zeta, &
        maximum_evaporation_impact_depth, &
        no_of_soil_layers, &
        layer_thickness, &
        reference_albedo, &
        stomata_resistance, &
        evaporation_reduction_method, &
        xsa_critical_soil_moisture, &
        external_reference_evapotranspiration, &
        height_nn, &
        max_air_temperature, &
        min_air_temperature, &
        mean_air_temperature, &
        relative_humidity, &
        wind_speed, &
        wind_speed_height, &
        global_radiation, &
        julian_day, &
        latitude, &
        evaporated_from_surface, &
        surface_water_storage, &
        snow_depth, &
        developmental_stage, &
        crop_reference_evapotranspiration, &
        reference_evapotranspiration, &
        actual_evaporation, &
        actual_transpiration, &
        kc_factor, &
        percentage_soil_coverage, &
        soil_moisture, &
        permanent_wilting_point, &
        field_capacity, &
        evaporation, &
        transpiration, &
        crop_transpiration, &
        crop_remaining_evapotranspiration, &
        crop_evaporated_from_intercepted, &
        evapotranspiration, &
        actual_evapotranspiration, &
        vapor_pressure)
        IMPLICIT NONE
        INTEGER:: i_cyml_r
        REAL, INTENT(IN) :: evaporation_zeta
        REAL, INTENT(IN) :: maximum_evaporation_impact_depth
        INTEGER, INTENT(IN) :: no_of_soil_layers
        REAL , DIMENSION(no_of_soil_layers ), INTENT(IN) :: layer_thickness
        REAL, INTENT(IN) :: reference_albedo
        REAL, INTENT(IN) :: stomata_resistance
        INTEGER, INTENT(IN) :: evaporation_reduction_method
        REAL, INTENT(IN) :: xsa_critical_soil_moisture
        REAL, INTENT(IN) :: external_reference_evapotranspiration
        REAL, INTENT(IN) :: height_nn
        REAL, INTENT(IN) :: max_air_temperature
        REAL, INTENT(IN) :: min_air_temperature
        REAL, INTENT(IN) :: mean_air_temperature
        REAL, INTENT(IN) :: relative_humidity
        REAL, INTENT(IN) :: wind_speed
        REAL, INTENT(IN) :: wind_speed_height
        REAL, INTENT(IN) :: global_radiation
        INTEGER, INTENT(IN) :: julian_day
        REAL, INTENT(IN) :: latitude
        REAL, INTENT(INOUT) :: evaporated_from_surface
        REAL, INTENT(IN) :: surface_water_storage
        REAL, INTENT(IN) :: snow_depth
        INTEGER, INTENT(IN) :: developmental_stage
        REAL, INTENT(IN) :: crop_reference_evapotranspiration
        REAL, INTENT(IN) :: reference_evapotranspiration
        REAL, INTENT(IN) :: actual_evaporation
        REAL, INTENT(IN) :: actual_transpiration
        REAL, INTENT(IN) :: kc_factor
        REAL, INTENT(IN) :: percentage_soil_coverage
        REAL , DIMENSION(no_of_soil_layers ), ALLOCATABLE , INTENT(IN) ::  &
                soil_moisture
        REAL , DIMENSION(no_of_soil_layers ), ALLOCATABLE , INTENT(IN) ::  &
                permanent_wilting_point
        REAL , DIMENSION(no_of_soil_layers ), ALLOCATABLE , INTENT(IN) ::  &
                field_capacity
        REAL , DIMENSION(no_of_soil_layers ), ALLOCATABLE , INTENT(IN) ::  &
                evaporation
        REAL , DIMENSION(no_of_soil_layers ), ALLOCATABLE , INTENT(IN) ::  &
                transpiration
        REAL , DIMENSION(no_of_soil_layers ), ALLOCATABLE , INTENT(IN) ::  &
                crop_transpiration
        REAL, INTENT(IN) :: crop_remaining_evapotranspiration
        REAL, INTENT(IN) :: crop_evaporated_from_intercepted
        REAL , DIMENSION(no_of_soil_layers ), ALLOCATABLE , INTENT(IN) ::  &
                evapotranspiration
        REAL, INTENT(INOUT) :: actual_evapotranspiration
        REAL, INTENT(IN) :: vapor_pressure
        !- Name: EvapotranspirationComp -Version: 1, -Time step: 1
        !- Description:
    !            * Title: Evapotranspiration model
    !            * Authors: Michael Berg-Mohnicke
    !            * Reference: None
    !            * Institution: ZALF e.V.
    !            * ExtendedDescription: None
    !            * ShortDescription: Calculates the evapotranspiration
        !- inputs:
    !            * name: evaporation_zeta
    !                          ** description : shape factor
    !                          ** inputtype : parameter
    !                          ** parametercategory : constant
    !                          ** datatype : DOUBLE
    !                          ** max : 40
    !                          ** min : 0
    !                          ** default : 40
    !                          ** unit : dimensionless
    !            * name: maximum_evaporation_impact_depth
    !                          ** description : maximumEvaporationImpactDepth
    !                          ** inputtype : parameter
    !                          ** parametercategory : constant
    !                          ** datatype : DOUBLE
    !                          ** max : 
    !                          ** min : 0
    !                          ** default : 5
    !                          ** unit : dm
    !            * name: no_of_soil_layers
    !                          ** description : number of soil layers
    !                          ** inputtype : parameter
    !                          ** parametercategory : constant
    !                          ** datatype : INT
    !                          ** max : 
    !                          ** min : 0
    !                          ** default : 20
    !                          ** unit : dimensionless
    !            * name: layer_thickness
    !                          ** description : layer thickness array
    !                          ** inputtype : parameter
    !                          ** parametercategory : constant
    !                          ** datatype : DOUBLEARRAY
    !                          ** len : no_of_soil_layers
    !                          ** max : 
    !                          ** min : 
    !                          ** default : 
    !                          ** unit : m
    !            * name: reference_albedo
    !                          ** description : reference albedo
    !                          ** inputtype : parameter
    !                          ** parametercategory : constant
    !                          ** datatype : DOUBLE
    !                          ** max : 1
    !                          ** min : 0
    !                          ** default : 0
    !                          ** unit : dimensionless
    !            * name: stomata_resistance
    !                          ** description : stomata resistance
    !                          ** inputtype : parameter
    !                          ** parametercategory : constant
    !                          ** datatype : DOUBLE
    !                          ** max : 10000
    !                          ** min : 0
    !                          ** default : 100
    !                          ** unit : s/m
    !            * name: evaporation_reduction_method
    !                          ** description : THESEUS (0) or HERMES (1) evaporation reduction method
    !                          ** inputtype : parameter
    !                          ** parametercategory : constant
    !                          ** datatype : INT
    !                          ** max : 1
    !                          ** min : 0
    !                          ** default : 1
    !                          ** unit : dimensionless
    !            * name: xsa_critical_soil_moisture
    !                          ** description : XSACriticalSoilMoisture
    !                          ** inputtype : parameter
    !                          ** parametercategory : constant
    !                          ** datatype : DOUBLE
    !                          ** max : 1.5
    !                          ** min : 0
    !                          ** default : 0.1
    !                          ** unit : m3/m3
    !            * name: external_reference_evapotranspiration
    !                          ** description : externally supplied ET0
    !                          ** inputtype : variable
    !                          ** variablecategory : exogenous
    !                          ** datatype : DOUBLE
    !                          ** max : 
    !                          ** min : 0
    !                          ** default : -1
    !                          ** unit : mm
    !            * name: height_nn
    !                          ** description : height above sea leavel
    !                          ** inputtype : variable
    !                          ** variablecategory : exogenous
    !                          ** datatype : DOUBLE
    !                          ** max : 9999
    !                          ** min : -9999
    !                          ** default : 0
    !                          ** unit : m
    !            * name: max_air_temperature
    !                          ** description : daily maximum air temperature
    !                          ** inputtype : variable
    !                          ** variablecategory : exogenous
    !                          ** datatype : DOUBLE
    !                          ** max : 100
    !                          ** min : -100
    !                          ** default : 0
    !                          ** unit : °C
    !            * name: min_air_temperature
    !                          ** description : daily minimum air temperature
    !                          ** inputtype : variable
    !                          ** variablecategory : exogenous
    !                          ** datatype : DOUBLE
    !                          ** max : 100
    !                          ** min : -100
    !                          ** default : 0
    !                          ** unit : °C
    !            * name: mean_air_temperature
    !                          ** description : daily average air temperature
    !                          ** inputtype : variable
    !                          ** variablecategory : exogenous
    !                          ** datatype : DOUBLE
    !                          ** max : 100
    !                          ** min : -100
    !                          ** default : 0
    !                          ** unit : °C
    !            * name: relative_humidity
    !                          ** description : relative humidity
    !                          ** inputtype : variable
    !                          ** variablecategory : exogenous
    !                          ** datatype : DOUBLE
    !                          ** max : 1
    !                          ** min : 0
    !                          ** default : 0
    !                          ** unit : fraction
    !            * name: wind_speed
    !                          ** description : wind speed measured at wind speed height
    !                          ** inputtype : variable
    !                          ** variablecategory : exogenous
    !                          ** datatype : DOUBLE
    !                          ** max : 9999
    !                          ** min : 0
    !                          ** default : 0
    !                          ** unit : m/s
    !            * name: wind_speed_height
    !                          ** description : height at which the wind speed has been measured
    !                          ** inputtype : variable
    !                          ** variablecategory : exogenous
    !                          ** datatype : DOUBLE
    !                          ** max : 9999
    !                          ** min : -9999
    !                          ** default : 2
    !                          ** unit : m
    !            * name: global_radiation
    !                          ** description : global radiation
    !                          ** inputtype : variable
    !                          ** variablecategory : exogenous
    !                          ** datatype : DOUBLE
    !                          ** max : 50
    !                          ** min : 0
    !                          ** default : 0
    !                          ** unit : MJ/m2
    !            * name: julian_day
    !                          ** description : day of year
    !                          ** inputtype : variable
    !                          ** variablecategory : exogenous
    !                          ** datatype : INT
    !                          ** max : 366
    !                          ** min : 1
    !                          ** default : 1
    !                          ** unit : day
    !            * name: latitude
    !                          ** description : latitude
    !                          ** inputtype : variable
    !                          ** variablecategory : exogenous
    !                          ** datatype : DOUBLE
    !                          ** max : 90
    !                          ** min : -90
    !                          ** default : 0
    !                          ** unit : degree
    !            * name: evaporated_from_surface
    !                          ** description : evaporated_from_surface
    !                          ** inputtype : variable
    !                          ** variablecategory : state
    !                          ** datatype : DOUBLE
    !                          ** max : 
    !                          ** min : 0
    !                          ** default : 0
    !                          ** unit : mm
    !            * name: surface_water_storage
    !                          ** description : Simulates a virtual layer that contains the surface water
    !                          ** inputtype : variable
    !                          ** variablecategory : state
    !                          ** datatype : DOUBLE
    !                          ** max : 
    !                          ** min : 0
    !                          ** default : 0
    !                          ** unit : mm
    !            * name: snow_depth
    !                          ** description : depth of snow layer
    !                          ** inputtype : variable
    !                          ** variablecategory : state
    !                          ** datatype : DOUBLE
    !                          ** max : 
    !                          ** min : 0
    !                          ** default : 0
    !                          ** unit : mm
    !            * name: developmental_stage
    !                          ** description : MONICA crop developmental stage
    !                          ** inputtype : variable
    !                          ** variablecategory : state
    !                          ** datatype : INT
    !                          ** max : 6
    !                          ** min : 0
    !                          ** default : 0
    !                          ** unit : dimensionless
    !            * name: crop_reference_evapotranspiration
    !                          ** description : the crop specific ET0, if no external ET0 and crop is planted
    !                          ** inputtype : variable
    !                          ** variablecategory : state
    !                          ** datatype : DOUBLE
    !                          ** max : 
    !                          ** min : 0
    !                          ** default : -1
    !                          ** unit : mm
    !            * name: reference_evapotranspiration
    !                          ** description : reference evapotranspiration (ET0)
    !                          ** inputtype : variable
    !                          ** variablecategory : state
    !                          ** datatype : DOUBLE
    !                          ** max : 
    !                          ** min : 0
    !                          ** default : 0
    !                          ** unit : mm
    !            * name: actual_evaporation
    !                          ** description : actual evaporation
    !                          ** inputtype : variable
    !                          ** variablecategory : state
    !                          ** datatype : DOUBLE
    !                          ** max : 
    !                          ** min : 0
    !                          ** default : 0
    !                          ** unit : mm
    !            * name: actual_transpiration
    !                          ** description : actual transpiration
    !                          ** inputtype : variable
    !                          ** variablecategory : state
    !                          ** datatype : DOUBLE
    !                          ** max : 
    !                          ** min : 0
    !                          ** default : 0
    !                          ** unit : mm
    !            * name: kc_factor
    !                          ** description : crop coefficient ETc/ET0
    !                          ** inputtype : variable
    !                          ** variablecategory : state
    !                          ** datatype : DOUBLE
    !                          ** max : 
    !                          ** min : 0
    !                          ** default : 0.75
    !                          ** unit : dimensionless
    !            * name: percentage_soil_coverage
    !                          ** description : fraction of soil covered by crop
    !                          ** inputtype : variable
    !                          ** variablecategory : state
    !                          ** datatype : DOUBLE
    !                          ** max : 1
    !                          ** min : 0
    !                          ** default : 0
    !                          ** unit : m2/m2
    !            * name: soil_moisture
    !                          ** description : soil moisture array
    !                          ** inputtype : variable
    !                          ** variablecategory : state
    !                          ** datatype : DOUBLEARRAY
    !                          ** len : no_of_soil_layers
    !                          ** max : 
    !                          ** min : 
    !                          ** default : 
    !                          ** unit : m3/m3
    !            * name: permanent_wilting_point
    !                          ** description : permanent wilting point array
    !                          ** inputtype : variable
    !                          ** variablecategory : state
    !                          ** datatype : DOUBLEARRAY
    !                          ** len : no_of_soil_layers
    !                          ** max : 2
    !                          ** min : 0
    !                          ** default : 0
    !                          ** unit : m3/m3
    !            * name: field_capacity
    !                          ** description : field capacity array
    !                          ** inputtype : variable
    !                          ** variablecategory : state
    !                          ** datatype : DOUBLEARRAY
    !                          ** len : no_of_soil_layers
    !                          ** max : 1
    !                          ** min : 0
    !                          ** default : 0
    !                          ** unit : m3/m3
    !            * name: evaporation
    !                          ** description : evaporation array
    !                          ** inputtype : variable
    !                          ** variablecategory : state
    !                          ** datatype : DOUBLEARRAY
    !                          ** len : no_of_soil_layers
    !                          ** max : 1
    !                          ** min : 0
    !                          ** default : 0
    !                          ** unit : mm
    !            * name: transpiration
    !                          ** description : transpiration array
    !                          ** inputtype : variable
    !                          ** variablecategory : state
    !                          ** datatype : DOUBLEARRAY
    !                          ** len : no_of_soil_layers
    !                          ** max : 
    !                          ** min : 
    !                          ** default : 
    !                          ** unit : mm
    !            * name: crop_transpiration
    !                          ** description : crop transpiration array
    !                          ** inputtype : variable
    !                          ** variablecategory : state
    !                          ** datatype : DOUBLEARRAY
    !                          ** len : no_of_soil_layers
    !                          ** max : 
    !                          ** min : 
    !                          ** default : 
    !                          ** unit : mm
    !            * name: crop_remaining_evapotranspiration
    !                          ** description : crop remaining evapotranspiration
    !                          ** inputtype : variable
    !                          ** variablecategory : state
    !                          ** datatype : DOUBLE
    !                          ** max : 
    !                          ** min : 
    !                          ** default : 
    !                          ** unit : mm
    !            * name: crop_evaporated_from_intercepted
    !                          ** description : crop evaporated water from intercepted water
    !                          ** inputtype : variable
    !                          ** variablecategory : state
    !                          ** datatype : DOUBLE
    !                          ** max : 
    !                          ** min : 
    !                          ** default : 
    !                          ** unit : mm
    !            * name: evapotranspiration
    !                          ** description : evapotranspiration array
    !                          ** inputtype : variable
    !                          ** variablecategory : state
    !                          ** datatype : DOUBLEARRAY
    !                          ** len : no_of_soil_layers
    !                          ** max : 
    !                          ** min : 
    !                          ** default : 
    !                          ** unit : mm
    !            * name: actual_evapotranspiration
    !                          ** description : actual evapotranspiration
    !                          ** inputtype : variable
    !                          ** variablecategory : state
    !                          ** datatype : DOUBLE
    !                          ** max : 
    !                          ** min : 0
    !                          ** default : 0
    !                          ** unit : mm
    !            * name: vapor_pressure
    !                          ** description : vapor pressure
    !                          ** inputtype : variable
    !                          ** variablecategory : state
    !                          ** datatype : DOUBLE
    !                          ** max : 
    !                          ** min : 0
    !                          ** default : 0
    !                          ** unit : kPa
        !- outputs:
    !            * name: evaporated_from_surface
    !                          ** description : 
    !                          ** variablecategory : state
    !                          ** datatype : DOUBLE
    !                          ** max : 
    !                          ** min : 0
    !                          ** unit : mm
    !            * name: actual_evapotranspiration
    !                          ** description : actual evapotranspiration
    !                          ** variablecategory : state
    !                          ** datatype : DOUBLE
    !                          ** max : 200
    !                          ** min : 0
    !                          ** unit : mm
        call model_evapotranspiration(evaporation_zeta,  &
                maximum_evaporation_impact_depth, no_of_soil_layers, layer_thickness,  &
                reference_albedo, stomata_resistance, evaporation_reduction_method,  &
                xsa_critical_soil_moisture, external_reference_evapotranspiration,  &
                height_nn, max_air_temperature, min_air_temperature,  &
                mean_air_temperature, relative_humidity, wind_speed,  &
                wind_speed_height, global_radiation, julian_day, latitude,  &
                evaporated_from_surface, snow_depth, developmental_stage,  &
                crop_reference_evapotranspiration, reference_evapotranspiration,  &
                actual_evaporation, actual_transpiration, surface_water_storage,  &
                kc_factor, percentage_soil_coverage, soil_moisture,  &
                permanent_wilting_point, field_capacity, evaporation, transpiration,  &
                crop_transpiration, crop_remaining_evapotranspiration,  &
                crop_evaporated_from_intercepted, evapotranspiration,  &
                actual_evapotranspiration, vapor_pressure)
    END SUBROUTINE model_evapotranspirationcomp

END MODULE
