using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CountryCollection : MonoBehaviour
{
    public string path;
    public string countryCode;
    public class Place
    {
        public string id { get; set; }

        public string name { get; set; }
        public string country { get; set; }
    }

    public List<Place> places = new List<Place>();
    public List<Place> singleCountryList = new List<Place>();
    public List<string> invalidPlaces;

    public void FindPlaces()
    {
        places = DeserializeToList<Place>();
        FilterCountry();
        Debug.Log("Random from list = " + singleCountryList[55].name + singleCountryList[55].id);
        APIController api = GetComponent<APIController>();
        api.MakeAPICall(singleCountryList[55].id);
    }

    public List<T> DeserializeToList<T>()
    {
        invalidPlaces = null;
        var array = JArray.Parse(File.ReadAllText(path));
        List<T> objectsList = new List<T>();
        foreach (var item in array)
        {
            try
            {
                objectsList.Add(item.ToObject<T>());
            }
            catch
            {
                invalidPlaces = invalidPlaces ?? new List<string>();
                invalidPlaces.Add(item.ToString());
            }
        }
        return objectsList;
    }

    private void FilterCountry()
    {
        foreach (var item in places)
        {
            if(item.country == countryCode)
            {
                singleCountryList.Add(item);
            }
        }
    }

}
