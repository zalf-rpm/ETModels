using Models.Core;
using Models.Utilities;
using System; 
namespace Models.Crop2ML;
     

/// <summary>
///  EvapotranspirationComp component
/// </summary>
public class EvapotranspirationCompComponent 
{

    /// <summary>
    ///  constructor of EvapotranspirationComp component
    /// </summary>
    public EvapotranspirationCompComponent() {}

    //Declaration of the associated strategies
    Evapotranspiration _Evapotranspiration = new Evapotranspiration();

    /// <summary>
    /// Gets and sets the shape factor
    /// </summary>
    [Description("shape factor")] 
    [Units("dimensionless")] 
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

    /// <summary>
    /// Gets and sets the maximumEvaporationImpactDepth
    /// </summary>
    [Description("maximumEvaporationImpactDepth")] 
    [Units("dm")] 
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

    /// <summary>
    /// Gets and sets the number of soil layers
    /// </summary>
    [Description("number of soil layers")] 
    [Units("dimensionless")] 
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

    /// <summary>
    /// Gets and sets the layer thickness array
    /// </summary>
    [Description("layer thickness array")] 
    [Units("m")] 
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

    /// <summary>
    /// Gets and sets the reference albedo
    /// </summary>
    [Description("reference albedo")] 
    [Units("dimensionless")] 
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

    /// <summary>
    /// Gets and sets the stomata resistance
    /// </summary>
    [Description("stomata resistance")] 
    [Units("s/m")] 
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

    /// <summary>
    /// Gets and sets the THESEUS (0) or HERMES (1) evaporation reduction method
    /// </summary>
    [Description("THESEUS (0) or HERMES (1) evaporation reduction method")] 
    [Units("dimensionless")] 
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

    /// <summary>
    /// Gets and sets the XSACriticalSoilMoisture
    /// </summary>
    [Description("XSACriticalSoilMoisture")] 
    [Units("m3/m3")] 
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

    /// <summary>
    /// Algorithm of EvapotranspirationComp component
    /// </summary>
    public void CalculateModel(EvapotranspirationCompState s,EvapotranspirationCompState s1,EvapotranspirationCompRate r,EvapotranspirationCompAuxiliary a,EvapotranspirationCompExogenous ex)
    {
        _Evapotranspiration.CalculateModel(s,s1, r, a, ex);
    }

    /// <summary>
    /// Initialization of EvapotranspirationComp component
    /// </summary>
    public void Init(EvapotranspirationCompState s, EvapotranspirationCompState s1, EvapotranspirationCompRate r, EvapotranspirationCompAuxiliary a, EvapotranspirationCompExogenous ex)
    {
    }

    /// <summary>
    /// constructor copy of EvapotranspirationComp component
    /// </summary>
    /// <param name="toCopy"></param>
    public EvapotranspirationCompComponent(EvapotranspirationCompComponent toCopy): this() // copy constructor 
    {
        evaporation_zeta = toCopy.evaporation_zeta;
        maximum_evaporation_impact_depth = toCopy.maximum_evaporation_impact_depth;
        no_of_soil_layers = toCopy.no_of_soil_layers;
        
        for (int i = 0; i < no_of_soil_layers; i++)
            { layer_thickness[i] = toCopy.layer_thickness[i]; }
    
        reference_albedo = toCopy.reference_albedo;
        stomata_resistance = toCopy.stomata_resistance;
        evaporation_reduction_method = toCopy.evaporation_reduction_method;
        xsa_critical_soil_moisture = toCopy.xsa_critical_soil_moisture;
}
}