using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class JSONWriter : MonoBehaviour
{
    public Config config;
    public TextMeshProUGUI debugText;

    public void OutputJSON(string s, string id)
    {
        string savePath = "";
#if UNITY_EDITOR
        savePath = Application.dataPath + "/StreamingAssets" + config.localDirectory + $"/{id}.json";
#endif

#if UNITY_ANDROID
        //savePath = Application.streamingAssetsPath + customPath + $"/{id}.json";
        savePath = Application.persistentDataPath + $"/{id}.json";
#endif
        debugText.text = savePath;
        File.WriteAllText(savePath, s);
        Debug.Log("write");

        JSONReader reader = GetComponent<JSONReader>();
    }
}
