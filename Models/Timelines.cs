using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MauiWeather.Models
{
    public class Timelines
    {
        [JsonPropertyName("daily")]
        public List<DailyForecast> Daily { get; set; }
    }
}
