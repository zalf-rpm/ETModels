// @@tagdynamic@@
// @@tagdepends: vle.discrete-time @@endtagdepends

#include <vle/DiscreteTime.hpp>
namespace vd = vle::devs;
namespace vv = vle::value;
// Definition du namespace de la classe du modele
namespace record {
using namespace std;
namespace Evapotranspirationcomp {
using namespace vle::discrete_time;
class Evapotranspiration: public DiscreteTimeDyn {
public:
    Evapotranspiration(const vd::DynamicsInit& atom, const vd::InitEventList& events) : DiscreteTimeDyn(atom, events)
    {
        // Ces parametres ont une valeur par defaut utilise si la condition n'est pas definie
        evaporation_zeta = (events.exist("evaporation_zeta")) ? vv::toDouble(events.get("evaporation_zeta")) : 40;
        maximum_evaporation_impact_depth = (events.exist("maximum_evaporation_impact_depth")) ? vv::toDouble(events.get("maximum_evaporation_impact_depth")) : 5;
        no_of_soil_layers = (events.exist("no_of_soil_layers")) ? vv::toInteger(events.get("no_of_soil_layers")) : 20;
        layer_thickness = (events.exist("layer_thickness")) ? vv::toMatrix(events.get("layer_thickness")) : ;
        reference_albedo = (events.exist("reference_albedo")) ? vv::toDouble(events.get("reference_albedo")) : 0;
        stomata_resistance = (events.exist("stomata_resistance")) ? vv::toDouble(events.get("stomata_resistance")) : 100;
        evaporation_reduction_method = (events.exist("evaporation_reduction_method")) ? vv::toInteger(events.get("evaporation_reduction_method")) : 1;
        xsa_critical_soil_moisture = (events.exist("xsa_critical_soil_moisture")) ? vv::toDouble(events.get("xsa_critical_soil_moisture")) : 0.1;
        //  Variables gérées par ce composant
        evaporated_from_surface.init(this,"evaporated_from_surface", events);
        actual_evapotranspiration.init(this,"actual_evapotranspiration", events);
        //  Variables gérées par un autre composant
        snow_depth.init(this,"snow_depth", events);
        developmental_stage.init(this,"developmental_stage", events);
        crop_reference_evapotranspiration.init(this,"crop_reference_evapotranspiration", events);
        reference_evapotranspiration.init(this,"reference_evapotranspiration", events);
        actual_evaporation.init(this,"actual_evaporation", events);
        actual_transpiration.init(this,"actual_transpiration", events);
        surface_water_storage.init(this,"surface_water_storage", events);
        kc_factor.init(this,"kc_factor", events);
        percentage_soil_coverage.init(this,"percentage_soil_coverage", events);
        soil_moisture.init(this,"soil_moisture", events);
        permanent_wilting_point.init(this,"permanent_wilting_point", events);
        field_capacity.init(this,"field_capacity", events);
        evaporation.init(this,"evaporation", events);
        transpiration.init(this,"transpiration", events);
        crop_transpiration.init(this,"crop_transpiration", events);
        crop_remaining_evapotranspiration.init(this,"crop_remaining_evapotranspiration", events);
        crop_evaporated_from_intercepted.init(this,"crop_evaporated_from_intercepted", events);
        evapotranspiration.init(this,"evapotranspiration", events);
        vapor_pressure.init(this,"vapor_pressure", events);
        external_reference_evapotranspiration.init(this,"external_reference_evapotranspiration", events);
        height_nn.init(this,"height_nn", events);
        max_air_temperature.init(this,"max_air_temperature", events);
        min_air_temperature.init(this,"min_air_temperature", events);
        mean_air_temperature.init(this,"mean_air_temperature", events);
        relative_humidity.init(this,"relative_humidity", events);
        wind_speed.init(this,"wind_speed", events);
        wind_speed_height.init(this,"wind_speed_height", events);
        global_radiation.init(this,"global_radiation", events);
        julian_day.init(this,"julian_day", events);
        latitude.init(this,"latitude", events);
    }
    /**
    * @brief Destructeur de la classe du modèle.
    **/
    virtual ~Evapotranspiration() {};
    /**
    * @brief Methode de calcul effectuée à chaque pas de temps
    * @param time la date du pas de temps courant
    */
    virtual void compute(const vd::Time& /*time*/)
    {
        evaporated_from_surface = 0.0;
        double potential_evapotranspiration = 0.0;
        double evaporated_from_intercept = 0.0;
        if (developmental_stage() > 0)
        {
            if (external_reference_evapotranspiration() < 0.0)
            {
                reference_evapotranspiration = reference_evapotranspiration();
            }
            else
            {
                reference_evapotranspiration = external_reference_evapotranspiration();
            }
            potential_evapotranspiration = crop_remaining_evapotranspiration();
            evaporated_from_intercept = crop_evaporated_from_intercepted();
        }
        else
        {
            if (external_reference_evapotranspiration() < 0.0)
            {
                tie(reference_evapotranspiration(), vapor_pressure()) = calc_reference_evapotranspiration(height_nn(), max_air_temperature(), min_air_temperature(), relative_humidity(), mean_air_temperature(), wind_speed(), wind_speed_height(), global_radiation(), julian_day(), latitude(), reference_albedo, vapor_pressure(), stomata_resistance);
            }
            else
            {
                reference_evapotranspiration = external_reference_evapotranspiration();
            }
            potential_evapotranspiration = reference_evapotranspiration() * kc_factor();
        }
        actual_evaporation = 0.0;
        actual_transpiration = 0.0;
        if (potential_evapotranspiration > 6.5)
        {
            potential_evapotranspiration = 6.5;
        }
        bool evaporation_from_surface = false;
        double eRed1;
        double eRed2;
        double eRed3;
        double eReducer;
        int i;
        if (potential_evapotranspiration > 0.0)
        {
            evaporation_from_surface = false;
            if (surface_water_storage() > 0.0)
            {
                evaporation_from_surface = true;
                potential_evapotranspiration = potential_evapotranspiration * 1.1 / kc_factor();
                if (snow_depth() > 0.0)
                {
                    evaporated_from_surface = 0.0;
                }
                else if ( surface_water_storage() < potential_evapotranspiration)
                {
                    potential_evapotranspiration = potential_evapotranspiration - surface_water_storage();
                    evaporated_from_surface = surface_water_storage();
                    surface_water_storage = 0.0;
                }
                else
                {
                    surface_water_storage = surface_water_storage() - potential_evapotranspiration;
                    evaporated_from_surface = potential_evapotranspiration;
                    potential_evapotranspiration = 0.0;
                }
                potential_evapotranspiration = potential_evapotranspiration * kc_factor() / 1.1;
            }
            if (potential_evapotranspiration > 0.0)
            {
                for (i=0 ; i!=no_of_soil_layers ; i+=1)
                {
                    eRed1 = e_reducer_1(permanent_wilting_point()[i], field_capacity()[i], soil_moisture()[i], percentage_soil_coverage(), potential_evapotranspiration, evaporation_reduction_method, xsa_critical_soil_moisture);
                    eRed2 = 0.0;
                    if (float(i) >= maximum_evaporation_impact_depth)
                    {
                        eRed2 = 0.0;
                    }
                    else
                    {
                        eRed2 = get_deprivation_factor(i + 1, maximum_evaporation_impact_depth, evaporation_zeta, layer_thickness[i]);
                    }
                    eRed3 = 0.0;
                    if (i > 0 && soil_moisture()[i] < soil_moisture()[i - 1])
                    {
                        eRed3 = 0.1;
                    }
                    else
                    {
                        eRed3 = 1.0;
                    }
                    eReducer = eRed1 * eRed2 * eRed3;
                    if (developmental_stage() > 0)
                    {
                        if (percentage_soil_coverage() >= 0.0 && percentage_soil_coverage() < 1.0)
                        {
                            evaporation()[i] = (1.0 - percentage_soil_coverage()) * eReducer * potential_evapotranspiration;
                        }
                        else if ( percentage_soil_coverage() >= 1.0)
                        {
                            evaporation()[i] = 0.0;
                        }
                        if (snow_depth() > 0.0)
                        {
                            evaporation()[i] = 0.0;
                        }
                        transpiration()[i] = crop_transpiration()[i];
                        if (evaporation_from_surface)
                        {
                            transpiration()[i] = percentage_soil_coverage() * eReducer * potential_evapotranspiration;
                        }
                    }
                    else
                    {
                        if (snow_depth() > 0.0)
                        {
                            evaporation()[i] = 0.0;
                        }
                        else
                        {
                            evaporation()[i] = potential_evapotranspiration * eReducer;
                            transpiration()[i] = 0.0;
                        }
                    }
                    evapotranspiration()[i] = evaporation()[i] + transpiration()[i];
                    soil_moisture()[i] = soil_moisture()[i] - (evapotranspiration()[i] / 1000.0 / layer_thickness[i]);
                    if (soil_moisture()[i] < 0.01)
                    {
                        soil_moisture()[i] = 0.01;
                    }
                    actual_transpiration = actual_transpiration() + transpiration()[i];
                    actual_evaporation = actual_evaporation() + evaporation()[i];
                }
            }
            actual_evapotranspiration = actual_transpiration() + actual_evaporation() + evaporated_from_intercept + evaporated_from_surface();
        }
    }
private:
    //Variables d'etat
    /**
    * @brief evaporated_from_surface (mm)
    */
    Var evaporated_from_surface;/**
    * @brief actual evapotranspiration (mm)
    */
    Var actual_evapotranspiration;

    //Entrées
    /**
        * @brief depth of snow layer (mm)
        */
    Var snow_depth;/**
        * @brief MONICA crop developmental stage (dimensionless)
        */
    Var developmental_stage;/**
        * @brief the crop specific ET0, if no external ET0 and crop is planted (mm)
        */
    Var crop_reference_evapotranspiration;/**
        * @brief reference evapotranspiration (ET0) (mm)
        */
    Var reference_evapotranspiration;/**
        * @brief actual evaporation (mm)
        */
    Var actual_evaporation;/**
        * @brief actual transpiration (mm)
        */
    Var actual_transpiration;/**
        * @brief Simulates a virtual layer that contains the surface water (mm)
        */
    Var surface_water_storage;/**
        * @brief crop coefficient ETc/ET0 (dimensionless)
        */
    Var kc_factor;/**
        * @brief fraction of soil covered by crop (m2/m2)
        */
    Var percentage_soil_coverage;/**
        * @brief soil moisture array (m3/m3)
        */
    Var soil_moisture;/**
        * @brief permanent wilting point array (m3/m3)
        */
    Var permanent_wilting_point;/**
        * @brief field capacity array (m3/m3)
        */
    Var field_capacity;/**
        * @brief evaporation array (mm)
        */
    Var evaporation;/**
        * @brief transpiration array (mm)
        */
    Var transpiration;/**
        * @brief crop transpiration array (mm)
        */
    Var crop_transpiration;/**
        * @brief crop remaining evapotranspiration (mm)
        */
    Var crop_remaining_evapotranspiration;/**
        * @brief crop evaporated water from intercepted water (mm)
        */
    Var crop_evaporated_from_intercepted;/**
        * @brief evapotranspiration array (mm)
        */
    Var evapotranspiration;/**
        * @brief vapor pressure (kPa)
        */
    Var vapor_pressure;/**
        * @brief externally supplied ET0 (mm)
        */
    Var external_reference_evapotranspiration;/**
        * @brief height above sea leavel (m)
        */
    Var height_nn;/**
        * @brief daily maximum air temperature (°C)
        */
    Var max_air_temperature;/**
        * @brief daily minimum air temperature (°C)
        */
    Var min_air_temperature;/**
        * @brief daily average air temperature (°C)
        */
    Var mean_air_temperature;/**
        * @brief relative humidity (fraction)
        */
    Var relative_humidity;/**
        * @brief wind speed measured at wind speed height (m/s)
        */
    Var wind_speed;/**
        * @brief height at which the wind speed has been measured (m)
        */
    Var wind_speed_height;/**
        * @brief global radiation (MJ/m2)
        */
    Var global_radiation;/**
        * @brief day of year (day)
        */
    Var julian_day;/**
        * @brief latitude (degree)
        */
    Var latitude;

    //Paramètres du modele
    /**
    * @brief shape factor (dimensionless)
    */
    double evaporation_zeta;
    /**
    * @brief maximumEvaporationImpactDepth (dm)
    */
    double maximum_evaporation_impact_depth;
    /**
    * @brief number of soil layers (dimensionless)
    */
    int no_of_soil_layers;
    /**
    * @brief layer thickness array (m)
    */
    std::vector<double>  layer_thickness;
    /**
    * @brief reference albedo (dimensionless)
    */
    double reference_albedo;
    /**
    * @brief stomata resistance (s/m)
    */
    double stomata_resistance;
    /**
    * @brief THESEUS (0) or HERMES (1) evaporation reduction method (dimensionless)
    */
    int evaporation_reduction_method;
    /**
    * @brief XSACriticalSoilMoisture (m3/m3)
    */
    double xsa_critical_soil_moisture;
    double Evapotranspiration:: get_deprivation_factor(int layer_no, double deprivation_depth, double zeta, double layer_thickness)
    {
        double ltf;
        ltf = deprivation_depth / (layer_thickness * 10.0);
        double deprivation_factor;
        double c2;
        double c3;
        if (std::abs(zeta) < 0.0003)
        {
            deprivation_factor = 2.0 / ltf - (1.0 / (ltf * ltf) * (2 * layer_no - 1));
        }
        else
        {
            c2 = std::log((ltf + (zeta * layer_no)) / (ltf + (zeta * (layer_no - 1))));
            c3 = zeta / (ltf * (zeta + 1.0));
            deprivation_factor = (c2 - c3) / (std::log(zeta + 1.0) - (zeta / (zeta + 1.0)));
        }
        return deprivation_factor;
    }
    double Evapotranspiration:: bound(double lower, double value, double upper)
    {
        if (value < lower)
        {
            return lower;
        }
        if (value > upper)
        {
            return upper;
        }
        return value;
    }
    std::tuple<double,double> Evapotranspiration:: calc_reference_evapotranspiration(double height_nn, double max_air_temperature, double min_air_temperature, double relative_humidity, double mean_air_temperature, double wind_speed, double wind_speed_height, double global_radiation, int julian_day, double latitude, double reference_albedo, double vapor_pressure, double stomata_resistance)
    {
        double declination;
        declination = -23.4 * std::cos(2.0 * M_PI * ((julian_day() + 10.0) / 365.0));
        double declination_sinus;
        declination_sinus = std::sin(declination * M_PI / 180.0) * std::sin(latitude() * M_PI / 180.0);
        double declination_cosinus;
        declination_cosinus = std::cos(declination * M_PI / 180.0) * std::cos(latitude() * M_PI / 180.0);
        double arg_astro_day_length;
        arg_astro_day_length = declination_sinus / declination_cosinus;
        arg_astro_day_length = bound(-1.0, arg_astro_day_length, 1.0);
        double astronomic_day_length;
        astronomic_day_length = 12.0 * (M_PI + (2.0 * std::asin(arg_astro_day_length))) / M_PI;
        double arg_effective_day_length;
        arg_effective_day_length = (-std::sin((8.0 * M_PI / 180.0)) + declination_sinus) / declination_cosinus;
        arg_effective_day_length = bound(-1.0, arg_effective_day_length, 1.0);
        double arg_photo_day_length;
        arg_photo_day_length = (-std::sin((-6.0 * M_PI / 180.0)) + declination_sinus) / declination_cosinus;
        arg_photo_day_length = bound(-1.0, arg_photo_day_length, 1.0);
        double arg_phot_act;
        arg_phot_act = std::min(1.0, declination_sinus / declination_cosinus * (declination_sinus / declination_cosinus));
        double phot_act_radiation_mean;
        phot_act_radiation_mean = 3600.0 * (declination_sinus * astronomic_day_length + (24.0 / M_PI * declination_cosinus * std::sqrt((1.0 - arg_phot_act))));
        double clear_day_radiation = 0.0;
        if (phot_act_radiation_mean > 0.0 && astronomic_day_length > 0.0)
        {
            clear_day_radiation = 0.5 * 1300.0 * phot_act_radiation_mean * std::exp(-0.14 / (phot_act_radiation_mean / (astronomic_day_length * 3600.0)));
        }
        double SC;
        SC = 24.0 * 60.0 / M_PI * 8.20 * (1.0 + (0.033 * std::cos(2.0 * M_PI * julian_day() / 365.0)));
        double arg_SHA;
        arg_SHA = bound(-1.0, -std::tan((latitude() * M_PI / 180.0)) * std::tan(declination * M_PI / 180.0), 1.0);
        double SHA;
        SHA = std::acos(arg_SHA);
        double extraterrestrial_radiation;
        extraterrestrial_radiation = SC * (SHA * declination_sinus + (declination_cosinus * std::sin(SHA))) / 100.0;
        double atmospheric_pressure;
        atmospheric_pressure = 101.3 * std::pow((293.0 - (0.0065 * height_nn())) / 293.0, 5.26);
        double psycrometer_constant;
        psycrometer_constant = 0.000665 * atmospheric_pressure;
        double saturated_vapor_pressure_max;
        saturated_vapor_pressure_max = 0.6108 * std::exp(17.27 * max_air_temperature() / (237.3 + max_air_temperature()));
        double saturated_vapor_pressure_min;
        saturated_vapor_pressure_min = 0.6108 * std::exp(17.27 * min_air_temperature() / (237.3 + min_air_temperature()));
        double saturated_vapor_pressure;
        saturated_vapor_pressure = (saturated_vapor_pressure_max + saturated_vapor_pressure_min) / 2.0;
        if (vapor_pressure() < 0.0)
        {
            if (relative_humidity() <= 0.0)
            {
                vapor_pressure = saturated_vapor_pressure_min;
            }
            else
            {
                vapor_pressure = relative_humidity() * saturated_vapor_pressure;
            }
        }
        double saturation_deficit;
        saturation_deficit = saturated_vapor_pressure - vapor_pressure();
        double saturated_vapour_pressure_slope;
        saturated_vapour_pressure_slope = 4098.0 * (0.6108 * std::exp(17.27 * mean_air_temperature() / (mean_air_temperature() + 237.3))) / ((mean_air_temperature() + 237.3) * (mean_air_temperature() + 237.3));
        double wind_speed_2m;
        wind_speed_2m = std::max(0.5, wind_speed() * (4.87 / std::log((67.8 * wind_speed_height() - 5.42))));
        double surface_resistance;
        surface_resistance = stomata_resistance / 1.44;
        double clear_sky_solar_radiation;
        clear_sky_solar_radiation = (0.75 + (0.00002 * height_nn())) * extraterrestrial_radiation;
        double relative_shortwave_radiation;
        relative_shortwave_radiation = clear_sky_solar_radiation > 0.0 ? std::min(global_radiation() / clear_sky_solar_radiation, 1.0) : 1.0;
        double bolzmann_constant = 0.0000000049;
        double shortwave_radiation;
        shortwave_radiation = (1.0 - reference_albedo) * global_radiation();
        double longwave_radiation;
        longwave_radiation = bolzmann_constant * ((std::pow(min_air_temperature() + 273.16, 4.0) + std::pow(max_air_temperature() + 273.16, 4.0)) / 2.0) * (1.35 * relative_shortwave_radiation - 0.35) * (0.34 - (0.14 * std::sqrt(vapor_pressure())));
        double net_radiation;
        net_radiation = shortwave_radiation - longwave_radiation;
        reference_evapotranspiration = (0.408 * saturated_vapour_pressure_slope * net_radiation + (psycrometer_constant * (900.0 / (mean_air_temperature() + 273.0)) * wind_speed_2m * saturation_deficit)) / (saturated_vapour_pressure_slope + (psycrometer_constant * (1.0 + (surface_resistance / 208.0 * wind_speed_2m))));
        if (reference_evapotranspiration() < 0.0)
        {
            reference_evapotranspiration = 0.0;
        }
        return make_tuple(reference_evapotranspiration(), vapor_pressure());
    }
    double Evapotranspiration:: e_reducer_1(double pwp, double fc, double sm, double percentage_soil_coverage, double reference_evapotranspiration, int evaporation_reduction_method, double xsa_critical_soil_moisture)
    {
        sm = std::max(0.33 * pwp, sm);
        double relative_evaporable_water;
        relative_evaporable_water = std::min(1.0, (sm - (0.33 * pwp)) / (fc - (0.33 * pwp)));
        double e_reduction_factor = 0.0;
        double critical_soil_moisture;
        double reducer;
        double xsa;
        if (evaporation_reduction_method == 0)
        {
            critical_soil_moisture = 0.65 * fc;
            if (percentage_soil_coverage() > 0.0)
            {
                reducer = 1.0;
                if (reference_evapotranspiration() > 2.5)
                {
                    xsa = (0.65 * fc - pwp) * (fc - pwp);
                    reducer = xsa + ((1 - xsa) / 17.5 * (reference_evapotranspiration() - 2.5));
                }
                else
                {
                    reducer = xsa_critical_soil_moisture / 2.5 * reference_evapotranspiration();
                }
                critical_soil_moisture = fc * reducer;
            }
            if (sm > critical_soil_moisture)
            {
                e_reduction_factor = 1.0;
            }
            else if ( sm > (0.33 * pwp))
            {
                e_reduction_factor = relative_evaporable_water;
            }
            else
            {
                e_reduction_factor = 0.0;
            }
        }
        else
        {
            if (relative_evaporable_water > 0.33)
            {
                e_reduction_factor = 1.0 - (0.1 * (1.0 - relative_evaporable_water) / (1.0 - 0.33));
            }
            else if ( relative_evaporable_water > 0.22)
            {
                e_reduction_factor = 0.9 - (0.625 * (0.33 - relative_evaporable_water) / (0.33 - 0.22));
            }
            else if ( relative_evaporable_water > 0.2)
            {
                e_reduction_factor = 0.275 - (0.225 * (0.22 - relative_evaporable_water) / (0.22 - 0.2));
            }
            else
            {
                e_reduction_factor = 0.05 - (0.05 * (0.2 - relative_evaporable_water) / 0.2);
            }
        }
        return e_reduction_factor;
    }
};
}
}
DECLARE_DYNAMICS(record::Evapotranspirationcomp::Evapotranspiration); // balise specifique VLE