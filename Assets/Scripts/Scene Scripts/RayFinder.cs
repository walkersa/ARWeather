using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayFinder : MonoBehaviour
{
    public string findTag;
    public Camera arCamera;
    
    private bool canFindCity;


    private void FixedUpdate()
    {
        if (!canFindCity)
            return;

        RayToFindCity();
    }

    private void RayToFindCity()
    {
        Ray ray = arCamera.ViewportPointToRay(arCamera.transform.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2f))
        {
            if(hit.collider.tag == findTag)
            {
                //get city id and feed fetch data
            }
        }
    }

    public void SetFindCity(bool value)
    {
        canFindCity = value;
    }
}
