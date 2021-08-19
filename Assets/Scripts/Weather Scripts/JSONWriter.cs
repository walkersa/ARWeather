using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JSONWriter : MonoBehaviour
{
    public string customPath;

    public void OutputJSON(string s, string id)
    {
        string savePath = Application.dataPath + customPath + $"/{id}.json";
        File.WriteAllText(savePath, s);
        Debug.Log("write");

        JSONReader reader = GetComponent<JSONReader>();
        reader.ReadData(savePath);
    }
}
