using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MauiWeather.Models
{
    public class ForecastWeatherData
    {
        [JsonPropertyName("temperatureAvg")]
        public double? Temperature { get; set; }
        [JsonPropertyName("temperatureApparentAvg")]
        public double? TemperatureApparent { get; set; }

        [JsonPropertyName("temperatureMin")]
        public double? TemperatureNight { get; set; }

        [JsonPropertyName("humidityAvg")]
        public double? Humidity { get; set; }
        [JsonPropertyName("precipitationProbabilityAvg")]
        public double? PrecipitationProbability { get; set; }
        [JsonPropertyName("weatherCodeMax")]
        public int? WeatherCode { get; set; }
    }

}
