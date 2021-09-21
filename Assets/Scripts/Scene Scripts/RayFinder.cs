using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RayFinder : MonoBehaviour
{
    public string findTag;
    public Camera arCamera;
    
    private bool canFindCity;

    public UnityEvent OnFindCity; 
    public int CurrentCityID { get; set; }
    public Vector3 CityPosition { get; set; }


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

        //Debug.Log("ray pos = " + ray);

        if (Physics.Raycast(ray, out hit, 2f))
        {
            if(hit.collider.tag == findTag)
            {
                Debug.Log("hit city");
                CityPosition = hit.transform.position;
                FindCityID(hit.transform.gameObject);
            }
        }
    }

    private void FindCityID(GameObject go)
    {
        CurrentCityID = go.GetComponent<City>().cityID;
        OnFindCity.Invoke();
    }

    public void SetFindCity(bool value)
    {
        canFindCity = value;
    }
}
