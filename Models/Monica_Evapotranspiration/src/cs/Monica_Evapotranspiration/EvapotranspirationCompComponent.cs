public class EvapotranspirationCompComponent
{
    
    /// <summary>
    /// Constructor of the EvapotranspirationCompComponent component")
    /// </summary>  
    public EvapotranspirationCompComponent() { }
    

    //Declaration of the associated strategies
    Evapotranspiration _Evapotranspiration = new Evapotranspiration();

    public double evaporation_zeta
    {
        get
        {
             return _Evapotranspiration.evaporation_zeta; 
        }
        set
        {
            _Evapotranspiration.evaporation_zeta = value;
        }
    }
    public double maximum_evaporation_impact_depth
    {
        get
        {
             return _Evapotranspiration.maximum_evaporation_impact_depth; 
        }
        set
        {
            _Evapotranspiration.maximum_evaporation_impact_depth = value;
        }
    }
    public int no_of_soil_layers
    {
        get
        {
             return _Evapotranspiration.no_of_soil_layers; 
        }
        set
        {
            _Evapotranspiration.no_of_soil_layers = value;
        }
    }
    public double[] layer_thickness
    {
        get
        {
             return _Evapotranspiration.layer_thickness; 
        }
        set
        {
            _Evapotranspiration.layer_thickness = value;
        }
    }
    public double reference_albedo
    {
        get
        {
             return _Evapotranspiration.reference_albedo; 
        }
        set
        {
            _Evapotranspiration.reference_albedo = value;
        }
    }
    public double stomata_resistance
    {
        get
        {
             return _Evapotranspiration.stomata_resistance; 
        }
        set
        {
            _Evapotranspiration.stomata_resistance = value;
        }
    }
    public int evaporation_reduction_method
    {
        get
        {
             return _Evapotranspiration.evaporation_reduction_method; 
        }
        set
        {
            _Evapotranspiration.evaporation_reduction_method = value;
        }
    }
    public double xsa_critical_soil_moisture
    {
        get
        {
             return _Evapotranspiration.xsa_critical_soil_moisture; 
        }
        set
        {
            _Evapotranspiration.xsa_critical_soil_moisture = value;
        }
    }

    public void  CalculateModel(EvapotranspirationCompState s, EvapotranspirationCompState s1, EvapotranspirationCompRate r, EvapotranspirationCompAuxiliary a, EvapotranspirationCompExogenous ex)
    {
        _Evapotranspiration.CalculateModel(s,s1, r, a, ex);
    }
    
    public EvapotranspirationCompComponent(EvapotranspirationCompComponent toCopy): this() // copy constructor 
    {

        evaporation_zeta = toCopy.evaporation_zeta;
        maximum_evaporation_impact_depth = toCopy.maximum_evaporation_impact_depth;
        no_of_soil_layers = toCopy.no_of_soil_layers;
        
        for (int i = 0; i < 100; i++)
            { layer_thickness[i] = toCopy.layer_thickness[i]; }
    
        reference_albedo = toCopy.reference_albedo;
        stomata_resistance = toCopy.stomata_resistance;
        evaporation_reduction_method = toCopy.evaporation_reduction_method;
        xsa_critical_soil_moisture = toCopy.xsa_critical_soil_moisture;
    }
}