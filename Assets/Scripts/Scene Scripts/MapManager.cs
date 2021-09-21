using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public APIController apiController;
    public JSONReader reader;
    public RayFinder rf;
    public GameObject weatherInfoPrefab;

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
        reader.DisplayData(wdo);

    }

    private void OnDisable()
    {
        rf.OnFindCity.RemoveListener(CityFound);
        apiController.OnCallComplete.RemoveListener(SetupWeatherInfo);
    }

}
