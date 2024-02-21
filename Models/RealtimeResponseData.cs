using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MauiWeather.Models
{
    public class RealtimeResponseData
    {
        [JsonPropertyName("time")]
        public DateTime Time { get; set; }
        [JsonPropertyName("values")]
        public RealtimeWeatherData Values { get; set; }

    }
}
