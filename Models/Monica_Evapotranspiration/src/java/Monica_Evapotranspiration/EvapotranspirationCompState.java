import  java.io.*;
import  java.util.*;
import java.time.LocalDateTime;
public class EvapotranspirationCompState
{
    private double evaporated_from_surface;
    private double surface_water_storage;
    private double snow_depth;
    private Integer developmental_stage;
    private double crop_reference_evapotranspiration;
    private double reference_evapotranspiration;
    private double actual_evaporation;
    private double actual_transpiration;
    private double kc_factor;
    private double percentage_soil_coverage;
    private Double [] soil_moisture;
    private Double [] permanent_wilting_point;
    private Double [] field_capacity;
    private Double [] evaporation;
    private Double [] transpiration;
    private Double [] crop_transpiration;
    private double crop_remaining_evapotranspiration;
    private double crop_evaporated_from_intercepted;
    private Double [] evapotranspiration;
    private double actual_evapotranspiration;
    private double vapor_pressure;
    
    public EvapotranspirationCompState() { }
    
    public EvapotranspirationCompState(EvapotranspirationCompState toCopy, boolean copyAll) // copy constructor 
    {
        if (copyAll)
        {
            this.evaporated_from_surface = toCopy.getevaporated_from_surface();
            this.surface_water_storage = toCopy.getsurface_water_storage();
            this.snow_depth = toCopy.getsnow_depth();
            this.developmental_stage = toCopy.getdevelopmental_stage();
            this.crop_reference_evapotranspiration = toCopy.getcrop_reference_evapotranspiration();
            this.reference_evapotranspiration = toCopy.getreference_evapotranspiration();
            this.actual_evaporation = toCopy.getactual_evaporation();
            this.actual_transpiration = toCopy.getactual_transpiration();
            this.kc_factor = toCopy.getkc_factor();
            this.percentage_soil_coverage = toCopy.getpercentage_soil_coverage();
            soil_moisture = new Double[toCopy.getsoil_moisture().length];
        for (int i = 0; i < toCopy.getsoil_moisture().length; i++)
        {
            soil_moisture[i] = toCopy.getsoil_moisture()[i];
        }
            permanent_wilting_point = new Double[toCopy.getpermanent_wilting_point().length];
        for (int i = 0; i < toCopy.getpermanent_wilting_point().length; i++)
        {
            permanent_wilting_point[i] = toCopy.getpermanent_wilting_point()[i];
        }
            field_capacity = new Double[toCopy.getfield_capacity().length];
        for (int i = 0; i < toCopy.getfield_capacity().length; i++)
        {
            field_capacity[i] = toCopy.getfield_capacity()[i];
        }
            evaporation = new Double[toCopy.getevaporation().length];
        for (int i = 0; i < toCopy.getevaporation().length; i++)
        {
            evaporation[i] = toCopy.getevaporation()[i];
        }
            transpiration = new Double[toCopy.gettranspiration().length];
        for (int i = 0; i < toCopy.gettranspiration().length; i++)
        {
            transpiration[i] = toCopy.gettranspiration()[i];
        }
            crop_transpiration = new Double[toCopy.getcrop_transpiration().length];
        for (int i = 0; i < toCopy.getcrop_transpiration().length; i++)
        {
            crop_transpiration[i] = toCopy.getcrop_transpiration()[i];
        }
            this.crop_remaining_evapotranspiration = toCopy.getcrop_remaining_evapotranspiration();
            this.crop_evaporated_from_intercepted = toCopy.getcrop_evaporated_from_intercepted();
            evapotranspiration = new Double[toCopy.getevapotranspiration().length];
        for (int i = 0; i < toCopy.getevapotranspiration().length; i++)
        {
            evapotranspiration[i] = toCopy.getevapotranspiration()[i];
        }
            this.actual_evapotranspiration = toCopy.getactual_evapotranspiration();
            this.vapor_pressure = toCopy.getvapor_pressure();
        }
    }
    public double getevaporated_from_surface()
    { return evaporated_from_surface; }

    public void setevaporated_from_surface(double _evaporated_from_surface)
    { this.evaporated_from_surface= _evaporated_from_surface; } 
    
    public double getsurface_water_storage()
    { return surface_water_storage; }

    public void setsurface_water_storage(double _surface_water_storage)
    { this.surface_water_storage= _surface_water_storage; } 
    
    public double getsnow_depth()
    { return snow_depth; }

    public void setsnow_depth(double _snow_depth)
    { this.snow_depth= _snow_depth; } 
    
    public Integer getdevelopmental_stage()
    { return developmental_stage; }

    public void setdevelopmental_stage(Integer _developmental_stage)
    { this.developmental_stage= _developmental_stage; } 
    
    public double getcrop_reference_evapotranspiration()
    { return crop_reference_evapotranspiration; }

    public void setcrop_reference_evapotranspiration(double _crop_reference_evapotranspiration)
    { this.crop_reference_evapotranspiration= _crop_reference_evapotranspiration; } 
    
    public double getreference_evapotranspiration()
    { return reference_evapotranspiration; }

    public void setreference_evapotranspiration(double _reference_evapotranspiration)
    { this.reference_evapotranspiration= _reference_evapotranspiration; } 
    
    public double getactual_evaporation()
    { return actual_evaporation; }

    public void setactual_evaporation(double _actual_evaporation)
    { this.actual_evaporation= _actual_evaporation; } 
    
    public double getactual_transpiration()
    { return actual_transpiration; }

    public void setactual_transpiration(double _actual_transpiration)
    { this.actual_transpiration= _actual_transpiration; } 
    
    public double getkc_factor()
    { return kc_factor; }

    public void setkc_factor(double _kc_factor)
    { this.kc_factor= _kc_factor; } 
    
    public double getpercentage_soil_coverage()
    { return percentage_soil_coverage; }

    public void setpercentage_soil_coverage(double _percentage_soil_coverage)
    { this.percentage_soil_coverage= _percentage_soil_coverage; } 
    
    public Double [] getsoil_moisture()
    { return soil_moisture; }

    public void setsoil_moisture(Double [] _soil_moisture)
    { this.soil_moisture= _soil_moisture; } 
    
    public Double [] getpermanent_wilting_point()
    { return permanent_wilting_point; }

    public void setpermanent_wilting_point(Double [] _permanent_wilting_point)
    { this.permanent_wilting_point= _permanent_wilting_point; } 
    
    public Double [] getfield_capacity()
    { return field_capacity; }

    public void setfield_capacity(Double [] _field_capacity)
    { this.field_capacity= _field_capacity; } 
    
    public Double [] getevaporation()
    { return evaporation; }

    public void setevaporation(Double [] _evaporation)
    { this.evaporation= _evaporation; } 
    
    public Double [] gettranspiration()
    { return transpiration; }

    public void settranspiration(Double [] _transpiration)
    { this.transpiration= _transpiration; } 
    
    public Double [] getcrop_transpiration()
    { return crop_transpiration; }

    public void setcrop_transpiration(Double [] _crop_transpiration)
    { this.crop_transpiration= _crop_transpiration; } 
    
    public double getcrop_remaining_evapotranspiration()
    { return crop_remaining_evapotranspiration; }

    public void setcrop_remaining_evapotranspiration(double _crop_remaining_evapotranspiration)
    { this.crop_remaining_evapotranspiration= _crop_remaining_evapotranspiration; } 
    
    public double getcrop_evaporated_from_intercepted()
    { return crop_evaporated_from_intercepted; }

    public void setcrop_evaporated_from_intercepted(double _crop_evaporated_from_intercepted)
    { this.crop_evaporated_from_intercepted= _crop_evaporated_from_intercepted; } 
    
    public Double [] getevapotranspiration()
    { return evapotranspiration; }

    public void setevapotranspiration(Double [] _evapotranspiration)
    { this.evapotranspiration= _evapotranspiration; } 
    
    public double getactual_evapotranspiration()
    { return actual_evapotranspiration; }

    public void setactual_evapotranspiration(double _actual_evapotranspiration)
    { this.actual_evapotranspiration= _actual_evapotranspiration; } 
    
    public double getvapor_pressure()
    { return vapor_pressure; }

    public void setvapor_pressure(double _vapor_pressure)
    { this.vapor_pressure= _vapor_pressure; } 
    
}