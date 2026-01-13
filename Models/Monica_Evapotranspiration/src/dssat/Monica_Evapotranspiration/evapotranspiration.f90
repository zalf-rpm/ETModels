MODULE Evapotranspirationmod
    IMPLICIT NONE
CONTAINS

    SUBROUTINE model_evapotranspiration(evaporation_zeta, &
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
        snow_depth, &
        developmental_stage, &
        crop_reference_evapotranspiration, &
        reference_evapotranspiration, &
        actual_evaporation, &
        actual_transpiration, &
        surface_water_storage, &
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
        REAL, INTENT(IN) :: snow_depth
        INTEGER, INTENT(IN) :: developmental_stage
        REAL, INTENT(IN) :: crop_reference_evapotranspiration
        REAL, INTENT(IN) :: reference_evapotranspiration
        REAL, INTENT(IN) :: actual_evaporation
        REAL, INTENT(IN) :: actual_transpiration
        REAL, INTENT(IN) :: surface_water_storage
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
        REAL:: potential_evapotranspiration
        REAL:: evaporated_from_intercept
        LOGICAL:: evaporation_from_surface
        REAL:: eRed1
        REAL:: eRed2
        REAL:: eRed3
        REAL:: eReducer
        INTEGER:: i
        potential_evapotranspiration = 0.0
        evaporated_from_intercept = 0.0
        evaporation_from_surface = .FALSE.
        !- Name: Evapotranspiration -Version: 1, -Time step: 1
        !- Description:
    !            * Title: Model of evapotranspiration
    !            * Authors: Claas Nendel (transcription into Crop2ML by Michael Berg-Mohnicke)
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
    !            * name: surface_water_storage
    !                          ** description : Simulates a virtual layer that contains the surface water
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
        evaporated_from_surface = 0.0
        IF(developmental_stage .GT. 0) THEN
            IF(external_reference_evapotranspiration .LT. 0.0) THEN
                reference_evapotranspiration = reference_evapotranspiration
            ELSE
                reference_evapotranspiration = external_reference_evapotranspiration
            END IF
            potential_evapotranspiration = crop_remaining_evapotranspiration
            evaporated_from_intercept = crop_evaporated_from_intercepted
        ELSE
            IF(external_reference_evapotranspiration .LT. 0.0) THEN
                call calc_reference_evapotranspiration(height_nn,  &
                        max_air_temperature, min_air_temperature, relative_humidity,  &
                        mean_air_temperature, wind_speed, wind_speed_height,  &
                        global_radiation, julian_day, latitude, reference_albedo,  &
                        vapor_pressure, stomata_resistance,reference_evapotranspiration)
            ELSE
                reference_evapotranspiration = external_reference_evapotranspiration
            END IF
            potential_evapotranspiration = reference_evapotranspiration *  &
                    kc_factor
        END IF
        actual_evaporation = 0.0
        actual_transpiration = 0.0
        IF(potential_evapotranspiration .GT. 6.5) THEN
            potential_evapotranspiration = 6.5
        END IF
        IF(potential_evapotranspiration .GT. 0.0) THEN
            evaporation_from_surface = .FALSE.
            IF(surface_water_storage .GT. 0.0) THEN
                evaporation_from_surface = .TRUE.
                potential_evapotranspiration = potential_evapotranspiration * 1.1 /  &
                        kc_factor
                IF(snow_depth .GT. 0.0) THEN
                    evaporated_from_surface = 0.0
                ELSE IF ( surface_water_storage .LT. potential_evapotranspiration)  &
                        THEN
                    potential_evapotranspiration = potential_evapotranspiration -  &
                            surface_water_storage
                    evaporated_from_surface = surface_water_storage
                    surface_water_storage = 0.0
                ELSE
                    surface_water_storage = surface_water_storage -  &
                            potential_evapotranspiration
                    evaporated_from_surface = potential_evapotranspiration
                    potential_evapotranspiration = 0.0
                END IF
                potential_evapotranspiration = potential_evapotranspiration *  &
                        kc_factor / 1.1
            END IF
            IF(potential_evapotranspiration .GT. 0.0) THEN
                DO i = 0 , no_of_soil_layers-1, 1
                    eRed1 = e_reducer_1(permanent_wilting_point(i+1),  &
                            field_capacity(i+1), soil_moisture(i+1), percentage_soil_coverage,  &
                            potential_evapotranspiration, evaporation_reduction_method,  &
                            xsa_critical_soil_moisture)
                    eRed2 = 0.0
                    IF(REAL(i) .GE. maximum_evaporation_impact_depth) THEN
                        eRed2 = 0.0
                    ELSE
                        eRed2 = get_deprivation_factor(i + 1,  &
                                maximum_evaporation_impact_depth, evaporation_zeta,  &
                                layer_thickness(i+1))
                    END IF
                    eRed3 = 0.0
                    IF(i .GT. 0 .AND. soil_moisture(i+1) .LT. soil_moisture(i - 1+1)) THEN
                        eRed3 = 0.1
                    ELSE
                        eRed3 = 1.0
                    END IF
                    eReducer = eRed1 * eRed2 * eRed3
                    IF(developmental_stage .GT. 0) THEN
                        IF(percentage_soil_coverage .GE. 0.0 .AND. percentage_soil_coverage  &
                                .LT. 1.0) THEN
                            evaporation(i+1) = (1.0 - percentage_soil_coverage) * eReducer *  &
                                    potential_evapotranspiration
                        ELSE IF ( percentage_soil_coverage .GE. 1.0) THEN
                            evaporation(i+1) = 0.0
                        END IF
                        IF(snow_depth .GT. 0.0) THEN
                            evaporation(i+1) = 0.0
                        END IF
                        transpiration(i+1) = crop_transpiration(i+1)
                        IF(evaporation_from_surface) THEN
                            transpiration(i+1) = percentage_soil_coverage * eReducer *  &
                                    potential_evapotranspiration
                        END IF
                    ELSE
                        IF(snow_depth .GT. 0.0) THEN
                            evaporation(i+1) = 0.0
                        ELSE
                            evaporation(i+1) = potential_evapotranspiration * eReducer
                            transpiration(i+1) = 0.0
                        END IF
                    END IF
                    evapotranspiration(i+1) = evaporation(i+1) + transpiration(i+1)
                    soil_moisture(i+1) = soil_moisture(i+1) - (evapotranspiration(i+1) /  &
                            1000.0 / layer_thickness(i+1))
                    IF(soil_moisture(i+1) .LT. 0.01) THEN
                        soil_moisture(i+1) = 0.01
                    END IF
                    actual_transpiration = actual_transpiration + transpiration(i+1)
                    actual_evaporation = actual_evaporation + evaporation(i+1)
                END DO
            END IF
            actual_evapotranspiration = actual_transpiration + actual_evaporation  &
                    + evaporated_from_intercept + evaporated_from_surface
        END IF
    END SUBROUTINE model_evapotranspiration

    FUNCTION get_deprivation_factor(layer_no, &
        deprivation_depth, &
        zeta, &
        layer_thickness) RESULT(deprivation_factor)
        IMPLICIT NONE
        INTEGER, INTENT(IN) :: layer_no
        REAL, INTENT(IN) :: deprivation_depth
        REAL, INTENT(IN) :: zeta
        REAL, INTENT(IN) :: layer_thickness
        REAL:: deprivation_factor
        REAL:: ltf
        REAL:: c2
        REAL:: c3
        REAL:: res_cyml
        ltf = deprivation_depth / (layer_thickness * 10.0)
        IF(ABS(zeta) .LT. 0.0003) THEN
            deprivation_factor = 2.0 / ltf - (1.0 / (ltf * ltf) * (2 * layer_no -  &
                    1))
        ELSE
            c2 = LOG((ltf + (zeta * layer_no)) / (ltf + (zeta * (layer_no - 1))))
            c3 = zeta / (ltf * (zeta + 1.0))
            deprivation_factor = (c2 - c3) / (LOG(zeta + 1.0) - (zeta / (zeta +  &
                    1.0)))
        END IF
    END FUNCTION get_deprivation_factor

    FUNCTION bound(lower, &
        value, &
        upper) RESULT(res_cyml)
        IMPLICIT NONE
        REAL, INTENT(IN) :: lower
        REAL, INTENT(IN) :: value
        REAL, INTENT(IN) :: upper
        REAL:: res_cyml
        IF(value .LT. lower) THEN
            res_cyml = lower
            RETURN
        END IF
        IF(value .GT. upper) THEN
            res_cyml = upper
            RETURN
        END IF
        res_cyml = value
        RETURN
    END FUNCTION bound

    SUBROUTINE calc_reference_evapotranspiration(height_nn, &
        max_air_temperature, &
        min_air_temperature, &
        relative_humidity, &
        mean_air_temperature, &
        wind_speed, &
        wind_speed_height, &
        global_radiation, &
        julian_day, &
        latitude, &
        reference_albedo, &
        vapor_pressure, &
        stomata_resistance, &
        reference_evapotranspiration)
        IMPLICIT NONE
        INTEGER:: i_cyml_r
        REAL, INTENT(IN) :: height_nn
        REAL, INTENT(IN) :: max_air_temperature
        REAL, INTENT(IN) :: min_air_temperature
        REAL, INTENT(IN) :: relative_humidity
        REAL, INTENT(IN) :: mean_air_temperature
        REAL, INTENT(IN) :: wind_speed
        REAL, INTENT(IN) :: wind_speed_height
        REAL, INTENT(IN) :: global_radiation
        INTEGER, INTENT(IN) :: julian_day
        REAL, INTENT(IN) :: latitude
        REAL, INTENT(IN) :: reference_albedo
        REAL, INTENT(INOUT) :: vapor_pressure
        REAL, INTENT(IN) :: stomata_resistance
        REAL:: declination
        REAL:: declination_sinus
        REAL:: declination_cosinus
        REAL:: arg_astro_day_length
        REAL:: astronomic_day_length
        REAL:: arg_effective_day_length
        REAL:: arg_photo_day_length
        REAL:: arg_phot_act
        REAL:: phot_act_radiation_mean
        REAL:: clear_day_radiation
        REAL:: SC
        REAL:: arg_SHA
        REAL:: SHA
        REAL:: extraterrestrial_radiation
        REAL:: atmospheric_pressure
        REAL:: psycrometer_constant
        REAL:: saturated_vapor_pressure_max
        REAL:: saturated_vapor_pressure_min
        REAL:: saturated_vapor_pressure
        REAL:: saturation_deficit
        REAL:: saturated_vapour_pressure_slope
        REAL:: wind_speed_2m
        REAL:: surface_resistance
        REAL:: clear_sky_solar_radiation
        REAL:: relative_shortwave_radiation
        REAL:: bolzmann_constant
        REAL:: shortwave_radiation
        REAL:: longwave_radiation
        REAL:: net_radiation
        REAL, INTENT(OUT) :: reference_evapotranspiration
        clear_day_radiation = 0.0
        bolzmann_constant = 0.0000000049
        declination = (-23.4) * COS(2.0 * 3.14159265 * ((julian_day + 10.0) /  &
                365.0))
        declination_sinus = SIN(declination * 3.14159265 / 180.0) *  &
                SIN(latitude * 3.14159265 / 180.0)
        declination_cosinus = COS(declination * 3.14159265 / 180.0) *  &
                COS(latitude * 3.14159265 / 180.0)
        arg_astro_day_length = declination_sinus / declination_cosinus
        arg_astro_day_length = bound(-1.0, arg_astro_day_length, 1.0)
        astronomic_day_length = 12.0 * (3.14159265 + (2.0 *  &
                ASIN(arg_astro_day_length))) / 3.14159265
        arg_effective_day_length = (-SIN(8.0 * 3.14159265 / 180.0) +  &
                declination_sinus) / declination_cosinus
        arg_effective_day_length = bound(-1.0, arg_effective_day_length, 1.0)
        arg_photo_day_length = (-SIN((-6.0) * 3.14159265 / 180.0) +  &
                declination_sinus) / declination_cosinus
        arg_photo_day_length = bound(-1.0, arg_photo_day_length, 1.0)
        arg_phot_act = MIN(1.0, declination_sinus / declination_cosinus *  &
                (declination_sinus / declination_cosinus))
        phot_act_radiation_mean = 3600.0 * (declination_sinus *  &
                astronomic_day_length + (24.0 / 3.14159265 * declination_cosinus *  &
                SQRT((1.0 - arg_phot_act))))
        IF(phot_act_radiation_mean .GT. 0.0 .AND. astronomic_day_length .GT.  &
                0.0) THEN
            clear_day_radiation = 0.5 * 1300.0 * phot_act_radiation_mean *  &
                    EXP((-0.14) / (phot_act_radiation_mean / (astronomic_day_length *  &
                    3600.0)))
        END IF
        SC = 24.0 * 60.0 / 3.14159265 * 8.20 * (1.0 + (0.033 * COS(2.0 *  &
                3.14159265 * julian_day / 365.0)))
        arg_SHA = bound(-1.0, (-TAN(latitude * 3.14159265 / 180.0)) *  &
                TAN(declination * 3.14159265 / 180.0), 1.0)
        SHA = ACOS(arg_SHA)
        extraterrestrial_radiation = SC * (SHA * declination_sinus +  &
                (declination_cosinus * SIN(SHA))) / 100.0
        atmospheric_pressure = 101.3 *  (((293.0 - (0.0065 * height_nn)) /  &
                293.0) ** 5.26)
        psycrometer_constant = 0.000665 * atmospheric_pressure
        saturated_vapor_pressure_max = 0.6108 * EXP(17.27 *  &
                max_air_temperature / (237.3 + max_air_temperature))
        saturated_vapor_pressure_min = 0.6108 * EXP(17.27 *  &
                min_air_temperature / (237.3 + min_air_temperature))
        saturated_vapor_pressure = (saturated_vapor_pressure_max +  &
                saturated_vapor_pressure_min) / 2.0
        IF(vapor_pressure .LT. 0.0) THEN
            IF(relative_humidity .LE. 0.0) THEN
                vapor_pressure = saturated_vapor_pressure_min
            ELSE
                vapor_pressure = relative_humidity * saturated_vapor_pressure
            END IF
        END IF
        saturation_deficit = saturated_vapor_pressure - vapor_pressure
        saturated_vapour_pressure_slope = 4098.0 * (0.6108 * EXP(17.27 *  &
                mean_air_temperature / (mean_air_temperature + 237.3))) /  &
                ((mean_air_temperature + 237.3) * (mean_air_temperature + 237.3))
        wind_speed_2m = MAX(0.5, wind_speed * (4.87 / LOG((67.8 *  &
                wind_speed_height - 5.42))))
        surface_resistance = stomata_resistance / 1.44
        clear_sky_solar_radiation = (0.75 + (0.00002 * height_nn)) *  &
                extraterrestrial_radiation
        IF (clear_sky_solar_radiation .GT. 0.0) THEN
            relative_shortwave_radiation=MIN(global_radiation /  &
                    clear_sky_solar_radiation, 1.0)
        ELSE
            relative_shortwave_radiation=1.0
        END IF
        shortwave_radiation = (1.0 - reference_albedo) * global_radiation
        longwave_radiation = bolzmann_constant * (( ((min_air_temperature +  &
                273.16) ** 4.0) +  ((max_air_temperature + 273.16) ** 4.0)) / 2.0) *  &
                (1.35 * relative_shortwave_radiation - 0.35) * (0.34 - (0.14 *  &
                SQRT(vapor_pressure)))
        net_radiation = shortwave_radiation - longwave_radiation
        reference_evapotranspiration = (0.408 *  &
                saturated_vapour_pressure_slope * net_radiation +  &
                (psycrometer_constant * (900.0 / (mean_air_temperature + 273.0)) *  &
                wind_speed_2m * saturation_deficit)) /  &
                (saturated_vapour_pressure_slope + (psycrometer_constant * (1.0 +  &
                (surface_resistance / 208.0 * wind_speed_2m))))
        IF(reference_evapotranspiration .LT. 0.0) THEN
            reference_evapotranspiration = 0.0
        END IF
    END SUBROUTINE calc_reference_evapotranspiration

    FUNCTION e_reducer_1(pwp, &
        fc, &
        sm, &
        percentage_soil_coverage, &
        reference_evapotranspiration, &
        evaporation_reduction_method, &
        xsa_critical_soil_moisture) RESULT(e_reduction_factor)
        IMPLICIT NONE
        REAL, INTENT(IN) :: pwp
        REAL, INTENT(IN) :: fc
        REAL, INTENT(IN) :: sm
        REAL, INTENT(IN) :: percentage_soil_coverage
        REAL, INTENT(IN) :: reference_evapotranspiration
        INTEGER, INTENT(IN) :: evaporation_reduction_method
        REAL, INTENT(IN) :: xsa_critical_soil_moisture
        REAL:: e_reduction_factor
        REAL:: relative_evaporable_water
        REAL:: critical_soil_moisture
        REAL:: reducer
        REAL:: xsa
        REAL:: res_cyml
        e_reduction_factor = 0.0
        sm = MAX(0.33 * pwp, sm)
        relative_evaporable_water = MIN(1.0, (sm - (0.33 * pwp)) / (fc -  &
                (0.33 * pwp)))
        IF(evaporation_reduction_method .EQ. 0) THEN
            critical_soil_moisture = 0.65 * fc
            IF(percentage_soil_coverage .GT. 0.0) THEN
                reducer = 1.0
                IF(reference_evapotranspiration .GT. 2.5) THEN
                    xsa = (0.65 * fc - pwp) * (fc - pwp)
                    reducer = xsa + ((1 - xsa) / 17.5 * (reference_evapotranspiration -  &
                            2.5))
                ELSE
                    reducer = xsa_critical_soil_moisture / 2.5 *  &
                            reference_evapotranspiration
                END IF
                critical_soil_moisture = fc * reducer
            END IF
            IF(sm .GT. critical_soil_moisture) THEN
                e_reduction_factor = 1.0
            ELSE IF ( sm .GT. (0.33 * pwp)) THEN
                e_reduction_factor = relative_evaporable_water
            ELSE
                e_reduction_factor = 0.0
            END IF
        ELSE
            IF(relative_evaporable_water .GT. 0.33) THEN
                e_reduction_factor = 1.0 - (0.1 * (1.0 - relative_evaporable_water) /  &
                        (1.0 - 0.33))
            ELSE IF ( relative_evaporable_water .GT. 0.22) THEN
                e_reduction_factor = 0.9 - (0.625 * (0.33 -  &
                        relative_evaporable_water) / (0.33 - 0.22))
            ELSE IF ( relative_evaporable_water .GT. 0.2) THEN
                e_reduction_factor = 0.275 - (0.225 * (0.22 -  &
                        relative_evaporable_water) / (0.22 - 0.2))
            ELSE
                e_reduction_factor = 0.05 - (0.05 * (0.2 - relative_evaporable_water)  &
                        / 0.2)
            END IF
        END IF
    END FUNCTION e_reducer_1

END MODULE
