using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class APIController : MonoBehaviour
{
    public string _apiAddress;
    public string _apiKey;

    public UnityEvent OnCallComplete;

    private JSONWriter writer;

    private void Start()
    {
        writer = GetComponent<JSONWriter>();
    }

    public async void MakeAPICall(string id)
    {
        await CallAPI(id);
    }

    private async Task CallAPI(string cityID)
    {
        var client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync(BuildAPIAddress(cityID));
        response.EnsureSuccessStatusCode();
        Debug.Log("success");
        var result = await response.Content.ReadAsStringAsync();
        writer.OutputJSON(result, cityID);
        Debug.Log(result);
        OnCallComplete.Invoke();
    }

    private string BuildAPIAddress(string cityID)
    {
        return _apiAddress + cityID + _apiKey;
    }

    //private async void OnGUI()
    //{
    //    if (GUI.Button(new Rect(10, 10, 150, 100), "Call API"))
    //    {
    //       await CallAPI();
    //    }
    //}
}

