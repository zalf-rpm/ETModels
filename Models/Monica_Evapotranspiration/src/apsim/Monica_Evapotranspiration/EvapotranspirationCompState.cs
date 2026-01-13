using System;
using System.Collections.Generic;
using Models.Core;
namespace Models.Crop2ML;

/// <summary>
/// state variables class of the EvapotranspirationComp component
/// </summary>
public class EvapotranspirationCompState
{
    private double _evaporated_from_surface;
    private double _surface_water_storage;
    private double _snow_depth;
    private int _developmental_stage;
    private double _crop_reference_evapotranspiration;
    private double _reference_evapotranspiration;
    private double _actual_evaporation;
    private double _actual_transpiration;
    private double _kc_factor;
    private double _percentage_soil_coverage;
    private double[] _soil_moisture;
    private double[] _permanent_wilting_point;
    private double[] _field_capacity;
    private double[] _evaporation;
    private double[] _transpiration;
    private double[] _crop_transpiration;
    private double _crop_remaining_evapotranspiration;
    private double _crop_evaporated_from_intercepted;
    private double[] _evapotranspiration;
    private double _actual_evapotranspiration;
    private double _vapor_pressure;

    /// <summary>
    /// Constructor EvapotranspirationCompState domain class
    /// </summary>
    public EvapotranspirationCompState() { }

    /// <summary>
    /// Copy constructor
    /// </summary>
    /// <param name="toCopy"></param>
    /// <param name="copyAll"></param>
    public EvapotranspirationCompState(EvapotranspirationCompState toCopy, bool copyAll) // copy constructor 
    {
        if (copyAll)
        {
            evaporated_from_surface = toCopy.evaporated_from_surface;
            surface_water_storage = toCopy.surface_water_storage;
            snow_depth = toCopy.snow_depth;
            developmental_stage = toCopy.developmental_stage;
            crop_reference_evapotranspiration = toCopy.crop_reference_evapotranspiration;
            reference_evapotranspiration = toCopy.reference_evapotranspiration;
            actual_evaporation = toCopy.actual_evaporation;
            actual_transpiration = toCopy.actual_transpiration;
            kc_factor = toCopy.kc_factor;
            percentage_soil_coverage = toCopy.percentage_soil_coverage;
            soil_moisture = new double[toCopy.soil_moisture.Length];
        for (int i = 0; i < toCopy.soil_moisture.Length; i++)
            { soil_moisture[i] = toCopy.soil_moisture[i]; }
    
            permanent_wilting_point = new double[toCopy.permanent_wilting_point.Length];
        for (int i = 0; i < toCopy.permanent_wilting_point.Length; i++)
            { permanent_wilting_point[i] = toCopy.permanent_wilting_point[i]; }
    
            field_capacity = new double[toCopy.field_capacity.Length];
        for (int i = 0; i < toCopy.field_capacity.Length; i++)
            { field_capacity[i] = toCopy.field_capacity[i]; }
    
            evaporation = new double[toCopy.evaporation.Length];
        for (int i = 0; i < toCopy.evaporation.Length; i++)
            { evaporation[i] = toCopy.evaporation[i]; }
    
            transpiration = new double[toCopy.transpiration.Length];
        for (int i = 0; i < toCopy.transpiration.Length; i++)
            { transpiration[i] = toCopy.transpiration[i]; }
    
            crop_transpiration = new double[toCopy.crop_transpiration.Length];
        for (int i = 0; i < toCopy.crop_transpiration.Length; i++)
            { crop_transpiration[i] = toCopy.crop_transpiration[i]; }
    
            crop_remaining_evapotranspiration = toCopy.crop_remaining_evapotranspiration;
            crop_evaporated_from_intercepted = toCopy.crop_evaporated_from_intercepted;
            evapotranspiration = new double[toCopy.evapotranspiration.Length];
        for (int i = 0; i < toCopy.evapotranspiration.Length; i++)
            { evapotranspiration[i] = toCopy.evapotranspiration[i]; }
    
            actual_evapotranspiration = toCopy.actual_evapotranspiration;
            vapor_pressure = toCopy.vapor_pressure;
        }
    }

    /// <summary>
    /// Gets and sets the evaporated_from_surface
    /// </summary>
    [Description("evaporated_from_surface")] 
    [Units("mm")] 
    public double evaporated_from_surface
    {
        get { return this._evaporated_from_surface; }
        set { this._evaporated_from_surface= value; } 
    }

    /// <summary>
    /// Gets and sets the Simulates a virtual layer that contains the surface water
    /// </summary>
    [Description("Simulates a virtual layer that contains the surface water")] 
    [Units("mm")] 
    public double surface_water_storage
    {
        get { return this._surface_water_storage; }
        set { this._surface_water_storage= value; } 
    }

    /// <summary>
    /// Gets and sets the depth of snow layer
    /// </summary>
    [Description("depth of snow layer")] 
    [Units("mm")] 
    public double snow_depth
    {
        get { return this._snow_depth; }
        set { this._snow_depth= value; } 
    }

    /// <summary>
    /// Gets and sets the MONICA crop developmental stage
    /// </summary>
    [Description("MONICA crop developmental stage")] 
    [Units("dimensionless")] 
    public int developmental_stage
    {
        get { return this._developmental_stage; }
        set { this._developmental_stage= value; } 
    }

    /// <summary>
    /// Gets and sets the the crop specific ET0, if no external ET0 and crop is planted
    /// </summary>
    [Description("the crop specific ET0, if no external ET0 and crop is planted")] 
    [Units("mm")] 
    public double crop_reference_evapotranspiration
    {
        get { return this._crop_reference_evapotranspiration; }
        set { this._crop_reference_evapotranspiration= value; } 
    }

    /// <summary>
    /// Gets and sets the reference evapotranspiration (ET0)
    /// </summary>
    [Description("reference evapotranspiration (ET0)")] 
    [Units("mm")] 
    public double reference_evapotranspiration
    {
        get { return this._reference_evapotranspiration; }
        set { this._reference_evapotranspiration= value; } 
    }

    /// <summary>
    /// Gets and sets the actual evaporation
    /// </summary>
    [Description("actual evaporation")] 
    [Units("mm")] 
    public double actual_evaporation
    {
        get { return this._actual_evaporation; }
        set { this._actual_evaporation= value; } 
    }

    /// <summary>
    /// Gets and sets the actual transpiration
    /// </summary>
    [Description("actual transpiration")] 
    [Units("mm")] 
    public double actual_transpiration
    {
        get { return this._actual_transpiration; }
        set { this._actual_transpiration= value; } 
    }

    /// <summary>
    /// Gets and sets the crop coefficient ETc/ET0
    /// </summary>
    [Description("crop coefficient ETc/ET0")] 
    [Units("dimensionless")] 
    public double kc_factor
    {
        get { return this._kc_factor; }
        set { this._kc_factor= value; } 
    }

    /// <summary>
    /// Gets and sets the fraction of soil covered by crop
    /// </summary>
    [Description("fraction of soil covered by crop")] 
    [Units("m2/m2")] 
    public double percentage_soil_coverage
    {
        get { return this._percentage_soil_coverage; }
        set { this._percentage_soil_coverage= value; } 
    }

    /// <summary>
    /// Gets and sets the soil moisture array
    /// </summary>
    [Description("soil moisture array")] 
    [Units("m3/m3")] 
    public double[] soil_moisture
    {
        get { return this._soil_moisture; }
        set { this._soil_moisture= value; } 
    }

    /// <summary>
    /// Gets and sets the permanent wilting point array
    /// </summary>
    [Description("permanent wilting point array")] 
    [Units("m3/m3")] 
    public double[] permanent_wilting_point
    {
        get { return this._permanent_wilting_point; }
        set { this._permanent_wilting_point= value; } 
    }

    /// <summary>
    /// Gets and sets the field capacity array
    /// </summary>
    [Description("field capacity array")] 
    [Units("m3/m3")] 
    public double[] field_capacity
    {
        get { return this._field_capacity; }
        set { this._field_capacity= value; } 
    }

    /// <summary>
    /// Gets and sets the evaporation array
    /// </summary>
    [Description("evaporation array")] 
    [Units("mm")] 
    public double[] evaporation
    {
        get { return this._evaporation; }
        set { this._evaporation= value; } 
    }

    /// <summary>
    /// Gets and sets the transpiration array
    /// </summary>
    [Description("transpiration array")] 
    [Units("mm")] 
    public double[] transpiration
    {
        get { return this._transpiration; }
        set { this._transpiration= value; } 
    }

    /// <summary>
    /// Gets and sets the crop transpiration array
    /// </summary>
    [Description("crop transpiration array")] 
    [Units("mm")] 
    public double[] crop_transpiration
    {
        get { return this._crop_transpiration; }
        set { this._crop_transpiration= value; } 
    }

    /// <summary>
    /// Gets and sets the crop remaining evapotranspiration
    /// </summary>
    [Description("crop remaining evapotranspiration")] 
    [Units("mm")] 
    public double crop_remaining_evapotranspiration
    {
        get { return this._crop_remaining_evapotranspiration; }
        set { this._crop_remaining_evapotranspiration= value; } 
    }

    /// <summary>
    /// Gets and sets the crop evaporated water from intercepted water
    /// </summary>
    [Description("crop evaporated water from intercepted water")] 
    [Units("mm")] 
    public double crop_evaporated_from_intercepted
    {
        get { return this._crop_evaporated_from_intercepted; }
        set { this._crop_evaporated_from_intercepted= value; } 
    }

    /// <summary>
    /// Gets and sets the evapotranspiration array
    /// </summary>
    [Description("evapotranspiration array")] 
    [Units("mm")] 
    public double[] evapotranspiration
    {
        get { return this._evapotranspiration; }
        set { this._evapotranspiration= value; } 
    }

    /// <summary>
    /// Gets and sets the actual evapotranspiration
    /// </summary>
    [Description("actual evapotranspiration")] 
    [Units("mm")] 
    public double actual_evapotranspiration
    {
        get { return this._actual_evapotranspiration; }
        set { this._actual_evapotranspiration= value; } 
    }

    /// <summary>
    /// Gets and sets the vapor pressure
    /// </summary>
    [Description("vapor pressure")] 
    [Units("kPa")] 
    public double vapor_pressure
    {
        get { return this._vapor_pressure; }
        set { this._vapor_pressure= value; } 
    }

}