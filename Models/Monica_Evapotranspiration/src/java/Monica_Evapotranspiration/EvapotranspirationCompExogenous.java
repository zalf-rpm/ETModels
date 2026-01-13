import  java.io.*;
import  java.util.*;
import java.time.LocalDateTime;

public class EvapotranspirationCompExogenous
{
    private double external_reference_evapotranspiration;
    private double height_nn;
    private double max_air_temperature;
    private double min_air_temperature;
    private double mean_air_temperature;
    private double relative_humidity;
    private double wind_speed;
    private double wind_speed_height;
    private double global_radiation;
    private Integer julian_day;
    private double latitude;
    
    public EvapotranspirationCompExogenous() { }
    
    public EvapotranspirationCompExogenous(EvapotranspirationCompExogenous toCopy, boolean copyAll) // copy constructor 
    {
        if (copyAll)
        {
            this.external_reference_evapotranspiration = toCopy.getexternal_reference_evapotranspiration();
            this.height_nn = toCopy.getheight_nn();
            this.max_air_temperature = toCopy.getmax_air_temperature();
            this.min_air_temperature = toCopy.getmin_air_temperature();
            this.mean_air_temperature = toCopy.getmean_air_temperature();
            this.relative_humidity = toCopy.getrelative_humidity();
            this.wind_speed = toCopy.getwind_speed();
            this.wind_speed_height = toCopy.getwind_speed_height();
            this.global_radiation = toCopy.getglobal_radiation();
            this.julian_day = toCopy.getjulian_day();
            this.latitude = toCopy.getlatitude();
        }
    }
    public double getexternal_reference_evapotranspiration()
    { return external_reference_evapotranspiration; }

    public void setexternal_reference_evapotranspiration(double _external_reference_evapotranspiration)
    { this.external_reference_evapotranspiration= _external_reference_evapotranspiration; } 
    
    public double getheight_nn()
    { return height_nn; }

    public void setheight_nn(double _height_nn)
    { this.height_nn= _height_nn; } 
    
    public double getmax_air_temperature()
    { return max_air_temperature; }

    public void setmax_air_temperature(double _max_air_temperature)
    { this.max_air_temperature= _max_air_temperature; } 
    
    public double getmin_air_temperature()
    { return min_air_temperature; }

    public void setmin_air_temperature(double _min_air_temperature)
    { this.min_air_temperature= _min_air_temperature; } 
    
    public double getmean_air_temperature()
    { return mean_air_temperature; }

    public void setmean_air_temperature(double _mean_air_temperature)
    { this.mean_air_temperature= _mean_air_temperature; } 
    
    public double getrelative_humidity()
    { return relative_humidity; }

    public void setrelative_humidity(double _relative_humidity)
    { this.relative_humidity= _relative_humidity; } 
    
    public double getwind_speed()
    { return wind_speed; }

    public void setwind_speed(double _wind_speed)
    { this.wind_speed= _wind_speed; } 
    
    public double getwind_speed_height()
    { return wind_speed_height; }

    public void setwind_speed_height(double _wind_speed_height)
    { this.wind_speed_height= _wind_speed_height; } 
    
    public double getglobal_radiation()
    { return global_radiation; }

    public void setglobal_radiation(double _global_radiation)
    { this.global_radiation= _global_radiation; } 
    
    public Integer getjulian_day()
    { return julian_day; }

    public void setjulian_day(Integer _julian_day)
    { this.julian_day= _julian_day; } 
    
    public double getlatitude()
    { return latitude; }

    public void setlatitude(double _latitude)
    { this.latitude= _latitude; } 
    
}