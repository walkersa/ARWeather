using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public Config config;
    public APIController apiController;
    public JSONReader reader;
    public RayFinder rf;
    public GameObject weatherInfoPrefab;
    public TextMeshProUGUI debugText;
    public bool inEditor;

    private Vector3 cityPos;
    private WeatherDisplayObject wdo;
    private int currentID;
    private GameObject display;
    private WeatherDataObject currentData;

    public void Start()
    {
        StartSearch();
    }

    public void StartSearch()
    {
        rf.OnFindCity.AddListener(CityFound);
        //apiController.OnCallComplete.AddListener(SetupWeatherInfo);

        if(!inEditor)
            rf.SetFindCity(true);

    }

    private async void CityFound()
    {
        Debug.Log("city found");
        if (currentID == rf.CurrentCityID)
        {
            Debug.Log("city ID same");
            return;
        }
            

        currentData = await apiController.CallAPI(rf.CurrentCityID.ToString());
        cityPos = rf.CityPosition;
        currentID = rf.CurrentCityID;
        SetupWeatherInfo();
        Debug.Log("MM current ID value = " + currentID);
    }

    private void SetupWeatherInfo()
    {
        if (display == null)
            display = Instantiate(weatherInfoPrefab);
        
        display.transform.position = cityPos;
        wdo = display.GetComponent<WeatherDisplayObject>();
        ShowData();

    }

    private void ShowData()
    {
        wdo.DisplayCityValue(currentData.name);
        wdo.DisplayDescriptionValue(currentData.weather[0].description);
        wdo.DisplayTemperatureValue(currentData.main.temp);
        wdo.DisplayWindValue(currentData.wind.speed);
        wdo.DisplayHumidityValue(currentData.main.humidity);
        wdo.SetSetup(currentData.weather[0].description);
    }

    private string SetupPath()
    {
        string savePath = "";
#if UNITY_EDITOR
        savePath = Application.dataPath + "/StreamingAssets" + config.localDirectory + $"/{currentID}.json";
#endif

#if UNITY_ANDROID
        //savePath = Application.streamingAssetsPath + customPath + $"/{currentID}.json";
        savePath = Application.persistentDataPath + $"/{currentID}.json";
#endif
        debugText.text = savePath;
        Debug.Log("path = " + savePath);
        return savePath;
    }

    private void OnDisable()
    {
        rf.OnFindCity.RemoveListener(CityFound);
        //apiController.OnCallComplete.RemoveListener(SetupWeatherInfo);
    }

}
