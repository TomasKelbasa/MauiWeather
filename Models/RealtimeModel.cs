using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiWeather.Models
{
    public class RealtimeModel
    {
        public Placemark Place { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Altitude { get; set; }

        public string Units { get; set; } = "metric";

        public string Location
        {
            get
            {
                return Latitude.ToString().Replace(',', '.') + ", " + Longitude.ToString().Replace(',', '.');
            }
        }

        public string ApiRequestString
        {
            get
            {
                return $"https://api.tomorrow.io/v4/weather/realtime?location={Location}&units={Units}&apikey={ApiKey.Key}";
            }
        }

        public RealtimeResponseData WeatherData { get; set; }
    }
}
