using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MauiWeather.Models
{
    public class ForecastResponse
    {
        [JsonPropertyName("timelines")]
        public Timelines Timelines { get; set; }
        [JsonPropertyName("location")]
        public WeatherLocation Location { get; set; }
    }
}
