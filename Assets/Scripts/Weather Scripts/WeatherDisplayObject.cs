using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeatherDisplayObject : MonoBehaviour
{
    public TextMeshProUGUI city;
    public TextMeshProUGUI description;
    public TextMeshProUGUI temperature;
    public TextMeshProUGUI wind;
    public TextMeshProUGUI humidity;

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
}
