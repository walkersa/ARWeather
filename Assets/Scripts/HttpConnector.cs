using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class HTTPConnector
{
    //specifcally GET a JSON file
    public async Task<TResultType> Get<TResultType>(string url)
    {
        using var www = UnityWebRequest.Get(url);

        www.SetRequestHeader("Content-Type", "application/json");

        var operation = www.SendWebRequest();

        while (!operation.isDone)
            await Task.Yield();

        var jsonResponse = www.downloadHandler.text;

        if (www.result != UnityWebRequest.Result.Success)
            Debug.LogError($"Failed: {www.error}");

        try
        {
            var result = JsonConvert.DeserializeObject<TResultType>(jsonResponse);
            Debug.Log($"Success: {www.downloadHandler.text}");
            return result;
        }
        catch (Exception ex)
        {
            Debug.LogError($"{this} Could not parse repsonse {jsonResponse}.{ex.Message}");
            return default;
        }
    }

    public async Task<Texture2D> GetTexture(string url)
    {
        using var www = UnityWebRequestTexture.GetTexture(url);

        var operation = www.SendWebRequest();

        while (!operation.isDone)
            await Task.Yield();

        if (www.result != UnityWebRequest.Result.Success)
            Debug.LogError($"Failed: {www.error}");

        try
        {
            Texture2D myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            return myTexture;
        }
        catch (Exception ex)
        {
            Debug.LogError($"{this} Could not parse repsonse {ex.Message}");
            return default;
        }
    }

}
