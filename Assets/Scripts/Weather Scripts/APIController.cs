using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class APIController : MonoBehaviour
{
    public Config config;

    public UnityEvent OnCallComplete;

    private JSONWriter writer;

    private void Start()
    {
        writer = GetComponent<JSONWriter>();
    }

    public async Task MakeAPICall(string id)
    {
        //await CallAPI(id);
        //await CallAPI(id);
    }

    //public async Task CallAPI(string cityID)
    //{
    //    var client = new HttpClient();
    //    HttpResponseMessage response = await client.GetAsync(BuildAPIAddress(cityID));
    //    response.EnsureSuccessStatusCode();
    //    Debug.Log("success");
    //    var result = await response.Content.ReadAsStringAsync();
    //    writer.OutputJSON(result, cityID);
    //    Debug.Log(result);
    //    OnCallComplete.Invoke();
    //}

    public async Task<WeatherDataObject> CallAPI(string cityID)
    {
        HTTPConnector connection = new HTTPConnector();
        var result = await connection.Get<WeatherDataObject>(BuildAPIAddress(cityID));
        //writer.OutputJSON(result, cityID);
        Debug.Log(result.name);
        //OnCallComplete.Invoke();
        return result;
    }

    private string BuildAPIAddress(string cityID)
    {
        return config.url + cityID + config.key;
    }

    //private async void OnGUI()
    //{
    //    if (GUI.Button(new Rect(10, 10, 150, 100), "Call API"))
    //    {
    //       await CallAPI();
    //    }
    //}
}

