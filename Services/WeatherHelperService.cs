using MauiWeather.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiWeather.Services
{
    public class WeatherHelperService : IWeatherHelperService
    {

        private static CancellationTokenSource _cancelTokenSource;
        private static bool _isCheckingLocation;

        public static async Task<LocationMessage> GetCurrentLocation()
        {
            try
            {
                _isCheckingLocation = true;

                GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(3));

                _cancelTokenSource = new CancellationTokenSource();

                Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

                LocationMessage result = new LocationMessage();

                if (location != null)
                {
                    result.Lat = location.Latitude;
                    result.Lon = location.Longitude;
                    result.Alt = location.Altitude ?? 0;

                    IEnumerable<Placemark> placemarks = await Geocoding.Default.GetPlacemarksAsync(location.Latitude, location.Longitude);

                    Placemark placemark = placemarks?.FirstOrDefault();

                    result.Location = placemark;

                }

                return result;
            }

            // Catch one of the following exceptions:
            //   FeatureNotSupportedException
            //   FeatureNotEnabledException
            //   PermissionException
            catch (Exception ex)
            {
                // Unable to get location
                return new LocationMessage() { Alt = -1, Lon = -1, Lat = -1 };
            }
            finally
            {
                _isCheckingLocation = false;
            }
        }

        public static async Task<RestResponse> FetchFromApi(string apiRequest) 
        {

            var options = new RestClientOptions(apiRequest);
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");
            var response = await client.GetAsync(request);
            return response;

        }
    }
}
