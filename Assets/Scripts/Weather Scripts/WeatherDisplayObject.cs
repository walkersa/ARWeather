using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class WeatherDisplayObject : MonoBehaviour
{
    public TextMeshProUGUI city;
    public TextMeshProUGUI description;
    public TextMeshProUGUI temperature;
    public TextMeshProUGUI wind;
    public TextMeshProUGUI humidity;

    public WeatherConditions conditions;
    public SetCollection setCollection;

    public void DisplayCityValue(string value)
    {
        city.text = value;
    }

    public void DisplayDescriptionValue(string value)
    {
        description.text = value;
    }

    public void DisplayTemperatureValue(string value)
    {
        string describer = $"Current Temp: {value} °C";
        temperature.text = describer;
    }

    public void DisplayWindValue(string value)
    {
        string describer = $"Wind Speed: {value} m/s";
        wind.text = describer;
    }

    public void DisplayHumidityValue(string value)
    {
        string describer = $"Humidity: {value}%";
        humidity.text = describer;
    }

    public void SetSetup(string c)
    {
        GameObject newSet = Instantiate(setCollection.FetchSet((int)SetupCondition(c)));
    }

    //this should get updated to use the IDs - right now its just testing the concept - https://openweathermap.org/weather-conditions
    public WeatherConditions SetupCondition(string c)
    {
        switch(c){
            case "Clear":
                return WeatherConditions.clear_sky;
            case "few clouds":
                return WeatherConditions.few_clouds;
            case "Clouds":
                return WeatherConditions.scattered_clouds;
            case "broken clouds":
                return WeatherConditions.broken_clouds;
            case "shower rain":
                return WeatherConditions.shower_rain;
            case "Rain":
                return WeatherConditions.rain;
            case "thunderstorm":
                return WeatherConditions.thuderstorm;
            case "snow":
                return WeatherConditions.snow;
            case "mist":
                return WeatherConditions.mist;
            default:
                Debug.LogWarning("no valid weather condition found");
                return WeatherConditions.clear_sky;

        }
    }
}
