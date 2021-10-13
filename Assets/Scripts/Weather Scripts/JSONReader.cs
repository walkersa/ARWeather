using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System;
using System.Threading.Tasks;
using UnityEngine.Networking;
using TMPro;

public class JSONReader : MonoBehaviour
{
    public WeatherDataObject wdo = new WeatherDataObject();
    public TextMeshProUGUI debugText;

    public void SetWeatherObject(WeatherDataObject obj)
    {
        wdo = obj;
    }
    
    //debugging method
    public void ReadDataDebug(string path)
    {
        Debug.Log("read path = "+ path);
        wdo = JsonConvert.DeserializeObject<WeatherDataObject>(File.ReadAllText(path));
        if(wdo == null)
        {
            Debug.Log("no weather data");
            return;
        }
        Debug.Log("City = " + wdo.name);
        Debug.Log("ID = " + wdo.weather[0].id);
        Debug.Log("Temp = " + wdo.main.temp);
        Debug.Log("humidity = " + wdo.main.humidity);
        Debug.Log("Description = " + wdo.weather[0].description);
        Debug.Log("Wind speed = " + wdo.wind.speed);
    }

    public void DisplayData(WeatherDisplayObject display, string path)
    {
#if UNITY_ANDROID
        string j = RetrieveJsonString(path);
        if (String.IsNullOrEmpty(j))
        {
            Debug.LogError("invalid path to streaming assets folder " + j);
            debugText.text = "invalid path to streaming assets folder " + j;
            return;
        }
        wdo = JsonConvert.DeserializeObject<WeatherDataObject>(File.ReadAllText(path));
#endif

#if UNITY_EDITOR

#endif
        display.DisplayCityValue(wdo.name);
        display.DisplayDescriptionValue(wdo.weather[0].description);
        display.DisplayTemperatureValue(wdo.main.temp);
        display.DisplayWindValue(wdo.wind.speed);
        display.DisplayHumidityValue(wdo.main.humidity);
        display.SetSetup(wdo.weather[0].description);
    }

    private string RetrieveJsonString(string path)
    {
        using (UnityWebRequest androidRequest = UnityWebRequest.Get(path))
        {
            androidRequest.SendWebRequest();
            if (androidRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                string n = "";
                return n;
            }
            else
            {
                string j = androidRequest.downloadHandler.text;
                return j;
            }

        }
    }
}
