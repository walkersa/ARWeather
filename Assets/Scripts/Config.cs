using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AppConfig", menuName = "Config File")]
public class Config : ScriptableObject
{
    [SerializeField]
    public string url;
    [SerializeField]
    public string key;
    [SerializeField]
    public string localDirectory;
}
