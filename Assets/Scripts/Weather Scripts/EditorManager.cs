using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class EditorManager : MonoBehaviour
{

    public Config config;
    public APIController apiController;
    public JSONReader reader;
    //public RayFinder rf;
    //public GameObject weatherInfoPrefab;
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
        //rf.OnFindCity.AddListener(CityFound);
        apiController.OnCallComplete.AddListener(SetupWeatherInfo);
        // rf.SetFindCity(true);

    }

    public async void CityButtonPress(int ID)
    {
        currentID = ID;
        await GrabCityInfo(ID);
    }

    public async Task GrabCityInfo(int ID)
    {
        var weatherObj = await apiController.CallAPI(ID.ToString());
        //await apiController.MakeAPICall(ID.ToString());
        reader.SetWeatherObject(weatherObj);
        SetupWeatherInfo();
    }

    //private void CityFound()
    //{
    //    if (currentID == rf.CurrentCityID)
    //        return;

    //    apiController.MakeAPICall(rf.CurrentCityID.ToString());
    //    cityPos = rf.CityPosition;
    //    currentID = rf.CurrentCityID;
    //}

    private void SetupWeatherInfo()
    {
        //if (display == null)
        //    display = Instantiate(weatherInfoPrefab);

        //display.transform.position = cityPos;
        wdo = GetComponent<WeatherDisplayObject>();
        string path = SetupPath();
        
        reader.ReadDataDebug(path);
        reader.DisplayData(wdo, path);

    }

    private string SetupPath()
    {
        string savePath = "";
#if UNITY_STANDALONE_WIN
        savePath = Application.dataPath + "/StreamingAssets" + config.localDirectory + $"/{currentID}.json";
        Debug.Log("running windows code");
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
        //rf.OnFindCity.RemoveListener(CityFound);
        apiController.OnCallComplete.RemoveListener(SetupWeatherInfo);
    }
}
