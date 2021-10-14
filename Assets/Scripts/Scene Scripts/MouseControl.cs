using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
    public RayFinder rayFinder;

    private void OnMouseDown()
    {
        Debug.Log("mouse down");
        rayFinder.FindCity();
    }
}
