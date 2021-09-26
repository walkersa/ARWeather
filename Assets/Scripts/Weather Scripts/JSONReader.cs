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
    
    //debugging method
    public void ReadDataDebug(string path)
    {
        wdo = JsonConvert.DeserializeObject<WeatherDataObject>(File.ReadAllText(path));
        Debug.Log("ID = " + wdo.list[0].weather[0].id);
        Debug.Log("City = " + wdo.city.name);
        Debug.Log("Temp = " + wdo.list[0].main.temp);
        Debug.Log("humidity = " + wdo.list[0].main.humidity);
        Debug.Log("Description = " + wdo.list[0].weather[0].description);
        Debug.Log("Wind speed = " + wdo.list[0].wind.speed);
    }

    public void DisplayData(WeatherDisplayObject display, string path)
    {
#if UNITY_ANDROID
        //string j = RetrieveJsonString(path);
        //if (String.IsNullOrEmpty(j))
        //{
        //    Debug.LogError("invalid path to streaming assets folder " + j);
        //    debugText.text = "invalid path to streaming assets folder " + j;
        //    return;
        //}
        wdo = JsonConvert.DeserializeObject<WeatherDataObject>(File.ReadAllText(path));
#endif

#if UNITY_EDITOR
        wdo = JsonConvert.DeserializeObject<WeatherDataObject>(File.ReadAllText(path));
#endif
        display.DisplayCityValue(wdo.city.name);
        display.DisplayDescriptionValue(wdo.list[0].weather[0].description);
        display.DisplayTemperatureValue(wdo.list[0].main.temp);
        display.DisplayWindValue(wdo.list[0].wind.speed);
        display.DisplayHumidityValue(wdo.list[0].main.humidity);
        //display.SetSetup(wdo.list[0].weather[0].description);
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

    //private void OnGUI()
    //{
    //    if (GUI.Button(new Rect(10, 120, 150, 100), "Read Json"))
    //    {
    //        Debug.Log("clicker!");
    //        ReadData();
    //    }
    //}
}
