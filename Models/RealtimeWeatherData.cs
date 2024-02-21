using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MauiWeather.Models
{
    public class RealtimeWeatherData
    {

        [JsonPropertyName("cloudBase")]
        public double? CloudBase { get; set; }
        public double? CloudCeiling { get; set; }
        public int? CloudCover { get; set; }
        public double? DewPoint { get; set; }
        public int? FreezingRainIntensity { get; set; }
        [JsonPropertyName("humidity")]
        public int? Humidity { get; set; }
        public int? PrecipitationProbability { get; set; }
        public double? PressureSurfaceLevel { get; set; }
        [JsonPropertyName("rainIntensity")]
        public int? RainIntensity { get; set; }
        public int? SleetIntensity { get; set; }
        public int? SnowIntensity { get; set; }
        [JsonPropertyName("temperature")]
        public double? Temperature { get; set; }
        [JsonPropertyName("temperatureApparent")]
        public double? TemperatureApparent { get; set; }
        public int? UvHealthConcern { get; set; }
        [JsonPropertyName("uvIndex")]
        public int? UvIndex { get; set; }
        [JsonPropertyName("visibility")]
        public double? Visibility { get; set; }
        [JsonPropertyName("weatherCode")]
        public int? WeatherCode { get; set; }
        public int? WindDirection { get; set; }
        public double? WindGust { get; set; }
        [JsonPropertyName("windSpeed")]
        public double? WindSpeed { get; set; }
    }

}
