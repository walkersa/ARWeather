using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Set Collection", menuName = "Create Set Collection")]
public class SetCollection : ScriptableObject
{
    public List<GameObject> setPrefabs = new List<GameObject>();

    public GameObject FetchSet(int index)
    {
        return setPrefabs[index];
    }
}
