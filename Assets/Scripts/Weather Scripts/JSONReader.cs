using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class JSONReader : MonoBehaviour
{
    public WeatherDataObject wdo = new WeatherDataObject();
    
    public void ReadData(string path)
    {
        wdo = JsonConvert.DeserializeObject<WeatherDataObject>(File.ReadAllText(path));
        Debug.Log("ID = " + wdo.list[0].weather[0].id);
        Debug.Log("City = " + wdo.city.name);
        Debug.Log("Temp = " + wdo.list[0].main.temp);
        Debug.Log("humidity = " + wdo.list[0].main.humidity);
        Debug.Log("Description = " + wdo.list[0].weather[0].description);
        Debug.Log("Wind speed = " + wdo.list[0].wind.speed);
        DisplayData();
    }

    private void DisplayData()
    {
        WeatherDisplayObject display = GetComponent<WeatherDisplayObject>();
        display.DisplayCityValue(wdo.city.name);
        display.DisplayDescriptionValue(wdo.list[0].weather[0].description);
        display.DisplayTemperatureValue(wdo.list[0].main.temp);
        display.DisplayWindValue(wdo.list[0].wind.speed);
        display.DisplayHumidityValue(wdo.list[0].main.humidity);
        display.SetSetup(wdo.list[0].weather[0].description);
    }

    //private void OnGUI()
    //{
    //    if (GUI.Button(new Rect(10, 120, 150, 100), "Read Json"))
    //    {
    //        Debug.Log("clicker!");
    //        ReadData();
    //    }
    //}
}
