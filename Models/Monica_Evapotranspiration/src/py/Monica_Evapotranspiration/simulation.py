from . import EvapotranspirationCompComponent
import pandas as pd
import os

def simulation(datafile, vardata, params, init):
    rep = os.path.dirname(datafile)
    out = os.path.join(rep, 'output.csv')
    df = pd.read_csv(datafile, sep = ";")

    # inputs values
    t_external_reference_evapotranspiration = df[vardata.loc[vardata["Variables"]=="external_reference_evapotranspiration","Data columns"].iloc[0]].to_list()
    t_height_nn = df[vardata.loc[vardata["Variables"]=="height_nn","Data columns"].iloc[0]].to_list()
    t_max_air_temperature = df[vardata.loc[vardata["Variables"]=="max_air_temperature","Data columns"].iloc[0]].to_list()
    t_min_air_temperature = df[vardata.loc[vardata["Variables"]=="min_air_temperature","Data columns"].iloc[0]].to_list()
    t_mean_air_temperature = df[vardata.loc[vardata["Variables"]=="mean_air_temperature","Data columns"].iloc[0]].to_list()
    t_relative_humidity = df[vardata.loc[vardata["Variables"]=="relative_humidity","Data columns"].iloc[0]].to_list()
    t_wind_speed = df[vardata.loc[vardata["Variables"]=="wind_speed","Data columns"].iloc[0]].to_list()
    t_wind_speed_height = df[vardata.loc[vardata["Variables"]=="wind_speed_height","Data columns"].iloc[0]].to_list()
    t_global_radiation = df[vardata.loc[vardata["Variables"]=="global_radiation","Data columns"].iloc[0]].to_list()
    t_julian_day = df[vardata.loc[vardata["Variables"]=="julian_day","Data columns"].iloc[0]].to_list()
    t_latitude = df[vardata.loc[vardata["Variables"]=="latitude","Data columns"].iloc[0]].to_list()
    t_evaporated_from_surface = df[vardata.loc[vardata["Variables"]=="evaporated_from_surface","Data columns"].iloc[0]].to_list()
    t_surface_water_storage = df[vardata.loc[vardata["Variables"]=="surface_water_storage","Data columns"].iloc[0]].to_list()
    t_snow_depth = df[vardata.loc[vardata["Variables"]=="snow_depth","Data columns"].iloc[0]].to_list()
    t_developmental_stage = df[vardata.loc[vardata["Variables"]=="developmental_stage","Data columns"].iloc[0]].to_list()
    t_crop_reference_evapotranspiration = df[vardata.loc[vardata["Variables"]=="crop_reference_evapotranspiration","Data columns"].iloc[0]].to_list()
    t_reference_evapotranspiration = df[vardata.loc[vardata["Variables"]=="reference_evapotranspiration","Data columns"].iloc[0]].to_list()
    t_actual_evaporation = df[vardata.loc[vardata["Variables"]=="actual_evaporation","Data columns"].iloc[0]].to_list()
    t_actual_transpiration = df[vardata.loc[vardata["Variables"]=="actual_transpiration","Data columns"].iloc[0]].to_list()
    t_kc_factor = df[vardata.loc[vardata["Variables"]=="kc_factor","Data columns"].iloc[0]].to_list()
    t_percentage_soil_coverage = df[vardata.loc[vardata["Variables"]=="percentage_soil_coverage","Data columns"].iloc[0]].to_list()
    t_soil_moisture = df[vardata.loc[vardata["Variables"]=="soil_moisture","Data columns"].iloc[0]].to_list()
    t_permanent_wilting_point = df[vardata.loc[vardata["Variables"]=="permanent_wilting_point","Data columns"].iloc[0]].to_list()
    t_field_capacity = df[vardata.loc[vardata["Variables"]=="field_capacity","Data columns"].iloc[0]].to_list()
    t_evaporation = df[vardata.loc[vardata["Variables"]=="evaporation","Data columns"].iloc[0]].to_list()
    t_transpiration = df[vardata.loc[vardata["Variables"]=="transpiration","Data columns"].iloc[0]].to_list()
    t_crop_transpiration = df[vardata.loc[vardata["Variables"]=="crop_transpiration","Data columns"].iloc[0]].to_list()
    t_crop_remaining_evapotranspiration = df[vardata.loc[vardata["Variables"]=="crop_remaining_evapotranspiration","Data columns"].iloc[0]].to_list()
    t_crop_evaporated_from_intercepted = df[vardata.loc[vardata["Variables"]=="crop_evaporated_from_intercepted","Data columns"].iloc[0]].to_list()
    t_evapotranspiration = df[vardata.loc[vardata["Variables"]=="evapotranspiration","Data columns"].iloc[0]].to_list()
    t_actual_evapotranspiration = df[vardata.loc[vardata["Variables"]=="actual_evapotranspiration","Data columns"].iloc[0]].to_list()
    t_vapor_pressure = df[vardata.loc[vardata["Variables"]=="vapor_pressure","Data columns"].iloc[0]].to_list()

    #parameters
    evaporation_zeta = params.loc[params["name"]=="evaporation_zeta", "value"].iloc[0]
    maximum_evaporation_impact_depth = params.loc[params["name"]=="maximum_evaporation_impact_depth", "value"].iloc[0]
    no_of_soil_layers = params.loc[params["name"]=="no_of_soil_layers", "value"].iloc[0]
    layer_thickness = params.loc[params["name"]=="layer_thickness", "value"].iloc[0]
    reference_albedo = params.loc[params["name"]=="reference_albedo", "value"].iloc[0]
    stomata_resistance = params.loc[params["name"]=="stomata_resistance", "value"].iloc[0]
    evaporation_reduction_method = params.loc[params["name"]=="evaporation_reduction_method", "value"].iloc[0]
    xsa_critical_soil_moisture = params.loc[params["name"]=="xsa_critical_soil_moisture", "value"].iloc[0]

    #initialization

    #outputs
    output_names = ["evaporated_from_surface","actual_evapotranspiration"]

    df_out = pd.DataFrame(columns = output_names)
    for i in range(0,len(df.index)-1):
        external_reference_evapotranspiration = t_external_reference_evapotranspiration[i]
        height_nn = t_height_nn[i]
        max_air_temperature = t_max_air_temperature[i]
        min_air_temperature = t_min_air_temperature[i]
        mean_air_temperature = t_mean_air_temperature[i]
        relative_humidity = t_relative_humidity[i]
        wind_speed = t_wind_speed[i]
        wind_speed_height = t_wind_speed_height[i]
        global_radiation = t_global_radiation[i]
        julian_day = t_julian_day[i]
        latitude = t_latitude[i]
        evaporated_from_surface = t_evaporated_from_surface[i]
        surface_water_storage = t_surface_water_storage[i]
        snow_depth = t_snow_depth[i]
        developmental_stage = t_developmental_stage[i]
        crop_reference_evapotranspiration = t_crop_reference_evapotranspiration[i]
        reference_evapotranspiration = t_reference_evapotranspiration[i]
        actual_evaporation = t_actual_evaporation[i]
        actual_transpiration = t_actual_transpiration[i]
        kc_factor = t_kc_factor[i]
        percentage_soil_coverage = t_percentage_soil_coverage[i]
        soil_moisture = t_soil_moisture[i]
        permanent_wilting_point = t_permanent_wilting_point[i]
        field_capacity = t_field_capacity[i]
        evaporation = t_evaporation[i]
        transpiration = t_transpiration[i]
        crop_transpiration = t_crop_transpiration[i]
        crop_remaining_evapotranspiration = t_crop_remaining_evapotranspiration[i]
        crop_evaporated_from_intercepted = t_crop_evaporated_from_intercepted[i]
        evapotranspiration = t_evapotranspiration[i]
        actual_evapotranspiration = t_actual_evapotranspiration[i]
        vapor_pressure = t_vapor_pressure[i]
        evaporated_from_surface,actual_evapotranspiration= EvapotranspirationCompComponent.model_evapotranspirationcomp(evaporation_zeta,maximum_evaporation_impact_depth,no_of_soil_layers,layer_thickness,reference_albedo,stomata_resistance,evaporation_reduction_method,xsa_critical_soil_moisture,external_reference_evapotranspiration,height_nn,max_air_temperature,min_air_temperature,mean_air_temperature,relative_humidity,wind_speed,wind_speed_height,global_radiation,julian_day,latitude,evaporated_from_surface,surface_water_storage,snow_depth,developmental_stage,crop_reference_evapotranspiration,reference_evapotranspiration,actual_evaporation,actual_transpiration,kc_factor,percentage_soil_coverage,soil_moisture,permanent_wilting_point,field_capacity,evaporation,transpiration,crop_transpiration,crop_remaining_evapotranspiration,crop_evaporated_from_intercepted,evapotranspiration,actual_evapotranspiration,vapor_pressure)

        df_out.loc[i] = [evaporated_from_surface,actual_evapotranspiration]
    df_out.insert(0, 'date', pd.to_datetime(df.year*10000 + df.month*100 + df.day, format='%Y%m%d'), True)
    df_out.set_index("date", inplace=True)
    df_out.to_csv(out, sep=";")
    return df_out