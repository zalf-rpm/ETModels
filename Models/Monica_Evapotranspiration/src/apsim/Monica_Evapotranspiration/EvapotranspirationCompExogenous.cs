using System;
using System.Collections.Generic;
using Models.Core;
namespace Models.Crop2ML;

/// <summary>
/// exogenous variables class of the EvapotranspirationComp component
/// </summary>
public class EvapotranspirationCompExogenous
{
    private double _external_reference_evapotranspiration;
    private double _height_nn;
    private double _max_air_temperature;
    private double _min_air_temperature;
    private double _mean_air_temperature;
    private double _relative_humidity;
    private double _wind_speed;
    private double _wind_speed_height;
    private double _global_radiation;
    private int _julian_day;
    private double _latitude;

    /// <summary>
    /// Constructor EvapotranspirationCompExogenous domain class
    /// </summary>
    public EvapotranspirationCompExogenous() { }

    /// <summary>
    /// Copy constructor
    /// </summary>
    /// <param name="toCopy"></param>
    /// <param name="copyAll"></param>
    public EvapotranspirationCompExogenous(EvapotranspirationCompExogenous toCopy, bool copyAll) // copy constructor 
    {
        if (copyAll)
        {
            external_reference_evapotranspiration = toCopy.external_reference_evapotranspiration;
            height_nn = toCopy.height_nn;
            max_air_temperature = toCopy.max_air_temperature;
            min_air_temperature = toCopy.min_air_temperature;
            mean_air_temperature = toCopy.mean_air_temperature;
            relative_humidity = toCopy.relative_humidity;
            wind_speed = toCopy.wind_speed;
            wind_speed_height = toCopy.wind_speed_height;
            global_radiation = toCopy.global_radiation;
            julian_day = toCopy.julian_day;
            latitude = toCopy.latitude;
        }
    }

    /// <summary>
    /// Gets and sets the externally supplied ET0
    /// </summary>
    [Description("externally supplied ET0")] 
    [Units("mm")] 
    public double external_reference_evapotranspiration
    {
        get { return this._external_reference_evapotranspiration; }
        set { this._external_reference_evapotranspiration= value; } 
    }

    /// <summary>
    /// Gets and sets the height above sea leavel
    /// </summary>
    [Description("height above sea leavel")] 
    [Units("m")] 
    public double height_nn
    {
        get { return this._height_nn; }
        set { this._height_nn= value; } 
    }

    /// <summary>
    /// Gets and sets the daily maximum air temperature
    /// </summary>
    [Description("daily maximum air temperature")] 
    [Units("°C")] 
    public double max_air_temperature
    {
        get { return this._max_air_temperature; }
        set { this._max_air_temperature= value; } 
    }

    /// <summary>
    /// Gets and sets the daily minimum air temperature
    /// </summary>
    [Description("daily minimum air temperature")] 
    [Units("°C")] 
    public double min_air_temperature
    {
        get { return this._min_air_temperature; }
        set { this._min_air_temperature= value; } 
    }

    /// <summary>
    /// Gets and sets the daily average air temperature
    /// </summary>
    [Description("daily average air temperature")] 
    [Units("°C")] 
    public double mean_air_temperature
    {
        get { return this._mean_air_temperature; }
        set { this._mean_air_temperature= value; } 
    }

    /// <summary>
    /// Gets and sets the relative humidity
    /// </summary>
    [Description("relative humidity")] 
    [Units("fraction")] 
    public double relative_humidity
    {
        get { return this._relative_humidity; }
        set { this._relative_humidity= value; } 
    }

    /// <summary>
    /// Gets and sets the wind speed measured at wind speed height
    /// </summary>
    [Description("wind speed measured at wind speed height")] 
    [Units("m/s")] 
    public double wind_speed
    {
        get { return this._wind_speed; }
        set { this._wind_speed= value; } 
    }

    /// <summary>
    /// Gets and sets the height at which the wind speed has been measured
    /// </summary>
    [Description("height at which the wind speed has been measured")] 
    [Units("m")] 
    public double wind_speed_height
    {
        get { return this._wind_speed_height; }
        set { this._wind_speed_height= value; } 
    }

    /// <summary>
    /// Gets and sets the global radiation
    /// </summary>
    [Description("global radiation")] 
    [Units("MJ/m2")] 
    public double global_radiation
    {
        get { return this._global_radiation; }
        set { this._global_radiation= value; } 
    }

    /// <summary>
    /// Gets and sets the day of year
    /// </summary>
    [Description("day of year")] 
    [Units("day")] 
    public int julian_day
    {
        get { return this._julian_day; }
        set { this._julian_day= value; } 
    }

    /// <summary>
    /// Gets and sets the latitude
    /// </summary>
    [Description("latitude")] 
    [Units("degree")] 
    public double latitude
    {
        get { return this._latitude; }
        set { this._latitude= value; } 
    }

}