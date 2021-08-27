using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherInfo
{
    
    public WeatherMain main { get; set; }
    public List<WeatherDescription> weather { get; set; }

    public WeatherWind wind { get; set; }

    public class WeatherMain
    {
        
        public string temp { get; set; }
        public string feels_like { get; set; }
        public string temp_min { get; set; }
        public string temp_max { get; set; }
        public string humidity { get; set; }
    }

    public class WeatherDescription
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
    }

    public class WeatherWind
    {
        public string speed { get; set; }
        public string deg { get; set; }
        public string gust { get; set; }
    }

}

