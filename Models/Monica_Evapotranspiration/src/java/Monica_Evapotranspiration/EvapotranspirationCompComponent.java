public class EvapotranspirationCompComponent
{
    
    public EvapotranspirationCompComponent() { }

    Evapotranspiration _Evapotranspiration = new Evapotranspiration();

    public double getevaporation_zeta()
    { return _Evapotranspiration.getevaporation_zeta(); }
    public void setevaporation_zeta(double _evaporation_zeta){
    _Evapotranspiration.setevaporation_zeta(_evaporation_zeta);
    }

    public double getmaximum_evaporation_impact_depth()
    { return _Evapotranspiration.getmaximum_evaporation_impact_depth(); }
    public void setmaximum_evaporation_impact_depth(double _maximum_evaporation_impact_depth){
    _Evapotranspiration.setmaximum_evaporation_impact_depth(_maximum_evaporation_impact_depth);
    }

    public Integer getno_of_soil_layers()
    { return _Evapotranspiration.getno_of_soil_layers(); }
    public void setno_of_soil_layers(Integer _no_of_soil_layers){
    _Evapotranspiration.setno_of_soil_layers(_no_of_soil_layers);
    }

    public Double [] getlayer_thickness()
    { return _Evapotranspiration.getlayer_thickness(); }
    public void setlayer_thickness(Double [] _layer_thickness){
    _Evapotranspiration.setlayer_thickness(_layer_thickness);
    }

    public double getreference_albedo()
    { return _Evapotranspiration.getreference_albedo(); }
    public void setreference_albedo(double _reference_albedo){
    _Evapotranspiration.setreference_albedo(_reference_albedo);
    }

    public double getstomata_resistance()
    { return _Evapotranspiration.getstomata_resistance(); }
    public void setstomata_resistance(double _stomata_resistance){
    _Evapotranspiration.setstomata_resistance(_stomata_resistance);
    }

    public Integer getevaporation_reduction_method()
    { return _Evapotranspiration.getevaporation_reduction_method(); }
    public void setevaporation_reduction_method(Integer _evaporation_reduction_method){
    _Evapotranspiration.setevaporation_reduction_method(_evaporation_reduction_method);
    }

    public double getxsa_critical_soil_moisture()
    { return _Evapotranspiration.getxsa_critical_soil_moisture(); }
    public void setxsa_critical_soil_moisture(double _xsa_critical_soil_moisture){
    _Evapotranspiration.setxsa_critical_soil_moisture(_xsa_critical_soil_moisture);
    }
    public void  Calculate_Model(EvapotranspirationCompState s, EvapotranspirationCompState s1, EvapotranspirationCompRate r, EvapotranspirationCompAuxiliary a, EvapotranspirationCompExogenous ex)
    {
        _Evapotranspiration.Calculate_Model(s, s1, r, a, ex);
    }
    private double evaporation_zeta;
    private double maximum_evaporation_impact_depth;
    private Integer no_of_soil_layers;
    private Double [] layer_thickness;
    private double reference_albedo;
    private double stomata_resistance;
    private Integer evaporation_reduction_method;
    private double xsa_critical_soil_moisture;
    public EvapotranspirationCompComponent(EvapotranspirationCompComponent toCopy) // copy constructor 
    {
        this.evaporation_zeta = toCopy.getevaporation_zeta();
        this.maximum_evaporation_impact_depth = toCopy.getmaximum_evaporation_impact_depth();
        this.no_of_soil_layers = toCopy.getno_of_soil_layers();
        
        for (int i = 0; i < toCopy.getlayer_thickness().length; i++)
        {
            layer_thickness[i] = toCopy.getlayer_thickness()[i];
        }
        this.reference_albedo = toCopy.getreference_albedo();
        this.stomata_resistance = toCopy.getstomata_resistance();
        this.evaporation_reduction_method = toCopy.getevaporation_reduction_method();
        this.xsa_critical_soil_moisture = toCopy.getxsa_critical_soil_moisture();

    }
}