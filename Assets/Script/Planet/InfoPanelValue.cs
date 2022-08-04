using System;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanelValue : MonoBehaviour
{
    public Text MercuryPlanetName;
    public Text MercuryDescription;
    public Text MercuryGravityValue;
    public Text MercuryMassValue;
    public Text MercuryRadiusValue;
    public Text MercuryVelocityValue;
    public Text MercuryDistanceValue;
    public Text MercuryTemperatureValue;
    public Text MercuryDayValue;
    public Text MercuryYearValue;
    
    public Text VenusPlanetName;
    public Text VenusDescription;
    public Text VenusGravityValue;
    public Text VenusMassValue;
    public Text VenusRadiusValue;
    public Text VenusVelocityValue;
    public Text VenusDistanceValue;
    public Text VenusTemperatureValue;
    public Text VenusDayValue;
    public Text VenusYearValue;
    
    public Text EarthPlanetName;
    public Text EarthDescription;
    public Text EarthGravityValue;
    public Text EarthMassValue;
    public Text EarthRadiusValue;
    public Text EarthVelocityValue;
    public Text EarthDistanceValue;
    public Text EarthTemperatureValue;
    public Text EarthDayValue;
    public Text EarthYearValue;
    
    public Text MarsPlanetName;
    public Text MarsDescription;
    public Text MarsGravityValue;
    public Text MarsMassValue;
    public Text MarsRadiusValue;
    public Text MarsVelocityValue;
    public Text MarsDistanceValue;
    public Text MarsTemperatureValue;
    public Text MarsDayValue;
    public Text MarsYearValue;
    
    public Text JupiterPlanetName;
    public Text JupiterDescription;
    public Text JupiterGravityValue;
    public Text JupiterMassValue;
    public Text JupiterRadiusValue;
    public Text JupiterVelocityValue;
    public Text JupiterDistanceValue;
    public Text JupiterTemperatureValue;
    public Text JupiterDayValue;
    public Text JupiterYearValue;
    
    public Text SaturnPlanetName;
    public Text SaturnDescription;
    public Text SaturnGravityValue;
    public Text SaturnMassValue;
    public Text SaturnRadiusValue;
    public Text SaturnVelocityValue;
    public Text SaturnDistanceValue;
    public Text SaturnTemperatureValue;
    public Text SaturnDayValue;
    public Text SaturnYearValue;
    
    public Text UranusPlanetName;
    public Text UranusDescription;
    public Text UranusGravityValue;
    public Text UranusMassValue;
    public Text UranusRadiusValue;
    public Text UranusVelocityValue;
    public Text UranusDistanceValue;
    public Text UranusTemperatureValue;
    public Text UranusDayValue;
    public Text UranusYearValue;
    
    public Text NeptunePlanetName;
    public Text NeptuneDescription;
    public Text NeptuneGravityValue;
    public Text NeptuneMassValue;
    public Text NeptuneRadiusValue;
    public Text NeptuneVelocityValue;
    public Text NeptuneDistanceValue;
    public Text NeptuneTemperatureValue;
    public Text NeptuneDayValue;
    public Text NeptuneYearValue;

    public void Awake()
    {
        PlanetsAttribute.INITIALIZE_PLANETS_ATTRIBUTES();
    }

    public void Start()
    {
        loadInfoValue();
    }

    public void ResetValue()
    {

        MercuryGravityValue.text = 3.7f.ToString();// ms^-2
        MercuryMassValue.text = 0.0553f.ToString(); //earth
        MercuryRadiusValue.text = 2439.7f.ToString(); // km
        MercuryVelocityValue.text = 47.4f.ToString(); //kms^-1  (orbital velocity)
        MercuryDistanceValue.text = 57.9f.ToString(); // 10^6km
        MercuryTemperatureValue.text = 167.0f.ToString(); //mean temperature in Centigrate
        MercuryDayValue.text = 4222.6f.ToString(); // hours
        MercuryYearValue.text = 88.ToString();//earth days

        VenusGravityValue.text = 8.9f.ToString();
        VenusMassValue.text = 0.815f.ToString();
        VenusRadiusValue.text = 6051.8f.ToString();
        VenusVelocityValue.text = 35.0f.ToString();
        VenusDistanceValue.text = 108.2f.ToString();
        VenusTemperatureValue.text = 464.0f.ToString();
        VenusDayValue.text = 2802.0f.ToString();
        VenusYearValue.text = 224.7f.ToString();

        EarthGravityValue.text = 9.8f.ToString();
        EarthMassValue.text = 1.0f.ToString();
        EarthRadiusValue.text = 6371.0f.ToString();
        EarthVelocityValue.text = 29.8f.ToString();
        EarthDistanceValue.text = 149.6f.ToString();
        EarthTemperatureValue.text = 15.0f.ToString();
        EarthDayValue.text = 24.0f.ToString();
        EarthYearValue.text = 365.2f.ToString();

        MarsGravityValue.text = 3.7f.ToString();
        MarsMassValue.text = 0.107f.ToString();
        MarsRadiusValue.text = 3389.5f.ToString();
        MarsVelocityValue.text = 24.1f.ToString();
        MarsDistanceValue.text = 227.9f.ToString();
        MarsTemperatureValue.text = "-65.0f";
        MarsDayValue.text = 24.7f.ToString();
        MarsYearValue.text = 687.0f.ToString();

        JupiterGravityValue.text = "23.1f";
        JupiterGravityValue.text = "317.83f";
        JupiterRadiusValue.text = "69911.0f";
        JupiterVelocityValue.text = "13.1f";
        JupiterDistanceValue.text = "778.6f";
        JupiterTemperatureValue.text = "-110.0f";
        JupiterDayValue.text = "9.9f";
        JupiterYearValue.text = "4331.0f";

        SaturnGravityValue.text = 9.0f.ToString();
        SaturnMassValue.text = 95.162f.ToString();
        SaturnRadiusValue.text = 58232.0f.ToString();
        SaturnVelocityValue.text = 9.7f.ToString();
        SaturnDistanceValue.text = 1433.5f.ToString();
        SaturnTemperatureValue.text = "-140.0f";
        SaturnDayValue.text = 10.7f.ToString();
        SaturnYearValue.text = 10747.0f.ToString();

        UranusGravityValue.text = 8.7f.ToString();
        UranusMassValue.text = 14.536f.ToString();
        UranusRadiusValue.text = 25362.0f.ToString();
        UranusVelocityValue.text = 6.8f.ToString();
        UranusDistanceValue.text = 2872.5f.ToString();
        UranusTemperatureValue.text = "-195.0f";
        UranusDayValue.text = 17.2f.ToString();
        UranusYearValue.text = 30589.0f.ToString();

        NeptuneGravityValue.text = 11.0f.ToString();
        NeptuneMassValue.text = 17.147f.ToString();
        NeptuneRadiusValue.text = 24622.0f.ToString();
        NeptuneVelocityValue.text = 5.4f.ToString();
        NeptuneDistanceValue.text = 4495.1f.ToString();
        NeptuneTemperatureValue.text = "-200.0f";
        NeptuneDayValue.text = 16.1f.ToString();
        NeptuneYearValue.text = 59800.0f.ToString();
    }

    public void loadInfoValue()
    {
        MercuryPlanetName.text = PlanetsAttribute.planets[0].planetName;
        MercuryDescription.text = PlanetsAttribute.planets[0].description;
        MercuryGravityValue.text = PlanetsAttribute.planets[0].gravityValue.ToString();
        MercuryMassValue.text = PlanetsAttribute.planets[0].massValue.ToString();
        MercuryRadiusValue.text = PlanetsAttribute.planets[0].radiusValue.ToString();
        MercuryVelocityValue.text = PlanetsAttribute.planets[0].velocityValue.ToString();
        MercuryDistanceValue.text = PlanetsAttribute.planets[0].distanceValue.ToString();
        MercuryTemperatureValue.text = PlanetsAttribute.planets[0].temperatureValue.ToString();
        MercuryDayValue.text = PlanetsAttribute.planets[0].dayValue.ToString();
        MercuryYearValue.text = PlanetsAttribute.planets[0].yearValue.ToString();
        
        VenusPlanetName.text = PlanetsAttribute.planets[1].planetName;
        VenusDescription.text = PlanetsAttribute.planets[1].description;
        VenusGravityValue.text = PlanetsAttribute.planets[1].gravityValue.ToString();
        VenusMassValue.text = PlanetsAttribute.planets[1].massValue.ToString();
        VenusRadiusValue.text = PlanetsAttribute.planets[1].radiusValue.ToString();
        VenusVelocityValue.text = PlanetsAttribute.planets[1].velocityValue.ToString();
        VenusDistanceValue.text = PlanetsAttribute.planets[1].distanceValue.ToString();
        VenusTemperatureValue.text = PlanetsAttribute.planets[1].temperatureValue.ToString();
        VenusDayValue.text = PlanetsAttribute.planets[1].dayValue.ToString();
        VenusYearValue.text = PlanetsAttribute.planets[1].yearValue.ToString();

        EarthPlanetName.text = PlanetsAttribute.planets[2].planetName;
        EarthDescription.text = PlanetsAttribute.planets[2].description;
        EarthGravityValue.text = PlanetsAttribute.planets[2].gravityValue.ToString();
        EarthMassValue.text = PlanetsAttribute.planets[2].massValue.ToString();
        EarthRadiusValue.text = PlanetsAttribute.planets[2].radiusValue.ToString();
        EarthVelocityValue.text = PlanetsAttribute.planets[2].velocityValue.ToString();
        EarthDistanceValue.text = PlanetsAttribute.planets[2].distanceValue.ToString();
        EarthTemperatureValue.text = PlanetsAttribute.planets[2].temperatureValue.ToString();
        EarthDayValue.text = PlanetsAttribute.planets[2].dayValue.ToString();
        EarthYearValue.text = PlanetsAttribute.planets[2].yearValue.ToString();
        
        MarsPlanetName.text = PlanetsAttribute.planets[3].planetName;
        MarsDescription.text = PlanetsAttribute.planets[3].description;
        MarsGravityValue.text = PlanetsAttribute.planets[3].gravityValue.ToString();
        MarsMassValue.text = PlanetsAttribute.planets[3].massValue.ToString();
        MarsRadiusValue.text = PlanetsAttribute.planets[3].radiusValue.ToString();
        MarsVelocityValue.text = PlanetsAttribute.planets[3].velocityValue.ToString();
        MarsDistanceValue.text = PlanetsAttribute.planets[3].distanceValue.ToString();
        MarsTemperatureValue.text = PlanetsAttribute.planets[3].temperatureValue.ToString();
        MarsDayValue.text = PlanetsAttribute.planets[3].dayValue.ToString();
        MarsYearValue.text = PlanetsAttribute.planets[3].yearValue.ToString();
        
        JupiterPlanetName.text = PlanetsAttribute.planets[4].planetName;
        JupiterDescription.text = PlanetsAttribute.planets[4].description;
        JupiterGravityValue.text = PlanetsAttribute.planets[4].gravityValue.ToString();
        JupiterMassValue.text = PlanetsAttribute.planets[4].massValue.ToString();
        JupiterRadiusValue.text = PlanetsAttribute.planets[4].radiusValue.ToString();
        JupiterVelocityValue.text = PlanetsAttribute.planets[4].velocityValue.ToString();
        JupiterDistanceValue.text = PlanetsAttribute.planets[4].distanceValue.ToString();
        JupiterTemperatureValue.text = PlanetsAttribute.planets[4].temperatureValue.ToString();
        JupiterDayValue.text = PlanetsAttribute.planets[4].dayValue.ToString();
        JupiterYearValue.text = PlanetsAttribute.planets[4].yearValue.ToString();
        
        SaturnPlanetName.text = PlanetsAttribute.planets[5].planetName;
        SaturnDescription.text = PlanetsAttribute.planets[5].description;
        SaturnGravityValue.text = PlanetsAttribute.planets[5].gravityValue.ToString();
        SaturnMassValue.text = PlanetsAttribute.planets[5].massValue.ToString();
        SaturnRadiusValue.text = PlanetsAttribute.planets[5].radiusValue.ToString();
        SaturnVelocityValue.text = PlanetsAttribute.planets[5].velocityValue.ToString();
        SaturnDistanceValue.text = PlanetsAttribute.planets[5].distanceValue.ToString();
        SaturnTemperatureValue.text = PlanetsAttribute.planets[5].temperatureValue.ToString();
        SaturnDayValue.text = PlanetsAttribute.planets[5].dayValue.ToString();
        SaturnYearValue.text = PlanetsAttribute.planets[5].yearValue.ToString();
        
        UranusPlanetName.text = PlanetsAttribute.planets[6].planetName;
        UranusDescription.text = PlanetsAttribute.planets[6].description;
        UranusGravityValue.text = PlanetsAttribute.planets[6].gravityValue.ToString();
        UranusMassValue.text = PlanetsAttribute.planets[6].massValue.ToString();
        UranusRadiusValue.text = PlanetsAttribute.planets[6].radiusValue.ToString();
        UranusVelocityValue.text = PlanetsAttribute.planets[6].velocityValue.ToString();
        UranusDistanceValue.text = PlanetsAttribute.planets[6].distanceValue.ToString();
        UranusTemperatureValue.text = PlanetsAttribute.planets[6].temperatureValue.ToString();
        UranusDayValue.text = PlanetsAttribute.planets[6].dayValue.ToString();
        UranusYearValue.text = PlanetsAttribute.planets[6].yearValue.ToString();
        
        NeptunePlanetName.text = PlanetsAttribute.planets[7].planetName;
        NeptuneDescription.text = PlanetsAttribute.planets[7].description;
        NeptuneGravityValue.text = PlanetsAttribute.planets[7].gravityValue.ToString();
        NeptuneMassValue.text = PlanetsAttribute.planets[7].massValue.ToString();
        NeptuneRadiusValue.text = PlanetsAttribute.planets[7].radiusValue.ToString();
        NeptuneVelocityValue.text = PlanetsAttribute.planets[7].velocityValue.ToString();
        NeptuneDistanceValue.text = PlanetsAttribute.planets[7].distanceValue.ToString();
        NeptuneTemperatureValue.text = PlanetsAttribute.planets[7].temperatureValue.ToString();
        NeptuneDayValue.text = PlanetsAttribute.planets[7].dayValue.ToString();
        NeptuneYearValue.text = PlanetsAttribute.planets[7].yearValue.ToString();
        
        
    }
    
}
