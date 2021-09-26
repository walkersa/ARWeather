using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class RayFinder : MonoBehaviour
{
    public string findTag;
    public Camera arCamera;
    public TextMeshProUGUI debugText;
    
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
        Ray ray = arCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        //Debug.Log("ray pos = " + ray);

        if (Physics.Raycast(ray, out hit, 2f))
        {
            if(hit.collider.tag == findTag)
            {
                debugText.text = "city hit";
                CityPosition = hit.transform.position;
                FindCityID(hit.transform.gameObject);
                CitySelected(hit.transform.gameObject);
            }
        }
    }

    private void CitySelected(GameObject go)
    {
        Renderer rend = go.GetComponentInChildren<Renderer>();
        rend.material.SetColor("_Color", Color.blue);
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
