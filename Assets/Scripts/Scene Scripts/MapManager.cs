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

    private Vector3 cityPos;
    private WeatherDisplayObject wdo;
    private int currentID;
    private GameObject display;

    public void Start()
    {
        StartSearch();
    }

    public void StartSearch()
    {
        rf.OnFindCity.AddListener(CityFound);
        apiController.OnCallComplete.AddListener(SetupWeatherInfo);
        rf.SetFindCity(true);

    }

    private void CityFound()
    {
        if (currentID == rf.CurrentCityID)
            return;

        apiController.MakeAPICall(rf.CurrentCityID.ToString());
        cityPos = rf.CityPosition;
        currentID = rf.CurrentCityID;
    }

    private void SetupWeatherInfo()
    {
        if (display == null)
            display = Instantiate(weatherInfoPrefab);
        
        display.transform.position = cityPos;
        wdo = display.GetComponent<WeatherDisplayObject>();
        string path = SetupPath();
        reader.DisplayData(wdo, path);

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
        apiController.OnCallComplete.RemoveListener(SetupWeatherInfo);
    }

}
