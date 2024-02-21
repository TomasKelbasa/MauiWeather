using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiWeather.Models
{
    public class ForecastModel
    {
        public Placemark Place { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Altitude { get; set; }
        public string Location { get 
            {
                return Latitude.ToString().Replace(',', '.') + ", " + Longitude.ToString().Replace(',', '.');
            }
        }

        public string ApiRequestString { get
            {
                return $"https://api.tomorrow.io/v4/weather/forecast?location={Location}&timesteps={TimeSteps}&units={Units}&apikey={ApiKey.Key}";
            } 
        }

        public string Units { get; set; } = "metric";

        // only allowed 1d (1 day) or 1h (1 hour)
        public string TimeSteps { get; set; } = "1d";

        public List<DailyForecast> ForeacastData { get; set; }
    }
}
