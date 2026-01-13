using APSIM.Shared.Utilities;
using Models.Climate;
using Models.Core;
using Models.Interfaces;
using Models.PMF;
using Models.Soils;
using Models.Surface;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Models.Crop2ML;

/// <summary>
///  This class encapsulates the EvapotranspirationCompComponent
/// </summary>
[Serializable]
[PresenterName("UserInterface.Presenters.PropertyPresenter")]
[ViewName("UserInterface.Views.PropertyView")]
[ValidParent(ParentType = typeof(Zone))]
class EvapotranspirationCompWrapper :  Model
{
    [Link] Clock clock = null;
    //[Link] Weather weather = null; // other links

    private EvapotranspirationCompState s;
    private EvapotranspirationCompState s1;
    private EvapotranspirationCompRate r;
    private EvapotranspirationCompAuxiliary a;
    private EvapotranspirationCompExogenous ex;
    private EvapotranspirationCompComponent evapotranspirationcompComponent;

    /// <summary>
    ///  The constructor of the Wrapper of the EvapotranspirationCompComponent
    /// </summary>
    public EvapotranspirationCompWrapper()
    {
        s = new EvapotranspirationCompState();
        s1 = new EvapotranspirationCompState();
        r = new EvapotranspirationCompRate();
        a = new EvapotranspirationCompAuxiliary();
        ex = new EvapotranspirationCompExogenous();
        evapotranspirationcompComponent = new EvapotranspirationCompComponent();
    }

    /// <summary>
    ///  The get method of the evaporated_from_surface output variable
    /// </summary>
    [Description("evaporated_from_surface")]
    [Units("mm")]
    public double evaporated_from_surface{ get { return s.evaporated_from_surface;}} 
     

    /// <summary>
    ///  The get method of the actual evapotranspiration output variable
    /// </summary>
    [Description("actual evapotranspiration")]
    [Units("mm")]
    public double actual_evapotranspiration{ get { return s.actual_evapotranspiration;}} 
     

    /// <summary>
    ///  The Constructor copy of the wrapper of the EvapotranspirationCompComponent
    /// </summary>
    /// <param name="toCopy"></param>
    /// <param name="copyAll"></param>
    public EvapotranspirationCompWrapper(EvapotranspirationCompWrapper toCopy, bool copyAll) 
    {
        s = (toCopy.s != null) ? new EvapotranspirationCompState(toCopy.s, copyAll) : null;
        r = (toCopy.r != null) ? new EvapotranspirationCompRate(toCopy.r, copyAll) : null;
        a = (toCopy.a != null) ? new EvapotranspirationCompAuxiliary(toCopy.a, copyAll) : null;
        ex = (toCopy.ex != null) ? new EvapotranspirationCompExogenous(toCopy.ex, copyAll) : null;
        if (copyAll)
        {
            evapotranspirationcompComponent = (toCopy.evapotranspirationcompComponent != null) ? new EvapotranspirationCompComponent(toCopy.evapotranspirationcompComponent) : null;
        }
    }

    /// <summary>
    ///  The Initialization method of the wrapper of the EvapotranspirationCompComponent
    /// </summary>
    public void Init(){
        setExogenous();
        loadParameters();
        evapotranspirationcompComponent.Init(s, s1, r, a, ex);
    }

    /// <summary>
    ///  Load parameters of the wrapper of the EvapotranspirationCompComponent
    /// </summary>
    private void loadParameters()
    {
        evapotranspirationcompComponent.evaporation_zeta = 40; 
        evapotranspirationcompComponent.maximum_evaporation_impact_depth = 5; 
        evapotranspirationcompComponent.no_of_soil_layers = 20; 
        evapotranspirationcompComponent.layer_thickness = null; // To be modified
        evapotranspirationcompComponent.reference_albedo = 0; 
        evapotranspirationcompComponent.stomata_resistance = 100; 
        evapotranspirationcompComponent.evaporation_reduction_method = 1; 
        evapotranspirationcompComponent.xsa_critical_soil_moisture = 0.1; 
    }

    /// <summary>
    ///  Set exogenous variables of the wrapper of the EvapotranspirationCompComponent
    /// </summary>
    private void setExogenous()
    {
        ex.external_reference_evapotranspiration = null; // To be modified
        ex.height_nn = null; // To be modified
        ex.max_air_temperature = null; // To be modified
        ex.min_air_temperature = null; // To be modified
        ex.mean_air_temperature = null; // To be modified
        ex.relative_humidity = null; // To be modified
        ex.wind_speed = null; // To be modified
        ex.wind_speed_height = null; // To be modified
        ex.global_radiation = null; // To be modified
        ex.julian_day = null; // To be modified
        ex.latitude = null; // To be modified
    }

    [EventSubscribe("Crop2MLProcess")]
    public void CalculateModel(object sender, EventArgs e)
    {
        if (clock.Today == clock.StartDate)
        {
            Init();
        }
        setExogenous();
        evapotranspirationcompComponent.CalculateModel(s,s1, r, a, ex);
    }

}