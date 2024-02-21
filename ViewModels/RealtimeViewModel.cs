using CommunityToolkit.Mvvm.Messaging;
using MauiWeather.Models;
using MauiWeather.Services;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiWeather.ViewModels
{
    public class RealtimeViewModel : INotifyPropertyChanged
    {

        private RealtimeModel _rm;

        public Placemark Place
        {
            get { return _rm.Place; }
        }

        public bool ShowWeather { get; set; }
        public bool ShowInfoText { get { return !ShowWeather; } }

        public RealtimeResponseData WeatherData
        {
            get { return _rm.WeatherData; }
        }

        public string InfoText { get; set; }

        public RealtimeViewModel()
        {
            _rm = new RealtimeModel();
            InfoText = "Please wait";
            ShowWeather = false;
            OnPropertyChanged(nameof(InfoText));
            OnPropertyChanged(nameof(ShowWeather));
            OnPropertyChanged(nameof(ShowInfoText));

            WeakReferenceMessenger.Default.Register<LocationMessage>(this, async (recipient, message) =>
            {
                _rm.Latitude = message.Lat;
                _rm.Longitude = message.Lon;
                _rm.Altitude = message.Alt;
                _rm.Place = message.Location;
                await ProcessWeather();

            });

            Inicialize();
        }

        public async Task Inicialize()
        {
            InfoText = "Loading...";
            OnPropertyChanged(nameof(InfoText));
            ShowWeather = false;
            OnPropertyChanged(nameof(ShowWeather));
            OnPropertyChanged(nameof(ShowInfoText));

            var location = await WeatherHelperService.GetCurrentLocation();
            _rm.Latitude = location.Lat;
            _rm.Longitude = location.Lon;
            _rm.Altitude = location.Alt;
            _rm.Place = location.Location;
            OnPropertyChanged(nameof(Place));
            await ProcessWeather();
            SendGeolocationData(location);
        }

        public ICommand getData => new Command(async () =>
        {
            // zobrazí zprávu loading
            InfoText = "Loading...";
            OnPropertyChanged(nameof(InfoText));
            ShowWeather = false;
            OnPropertyChanged(nameof(ShowWeather));
            OnPropertyChanged(nameof(ShowInfoText));

            var location = await WeatherHelperService.GetCurrentLocation();
            _rm.Latitude = location.Lat;
            _rm.Longitude = location.Lon;
            _rm.Altitude = location.Alt;
            _rm.Place = location.Location;
            OnPropertyChanged(nameof(Place));
            await ProcessWeather();
            SendGeolocationData(location);

        });

        private async Task ProcessWeather()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                try
                {
                    var data = await WeatherHelperService.FetchFromApi(_rm.ApiRequestString);
                    var t = JsonSerializer.Deserialize<RealtimeResponse>(data.Content);
                    _rm.WeatherData = t.Data;
                    OnPropertyChanged(nameof(WeatherData));
                    ShowWeather = true;
                    OnPropertyChanged(nameof(ShowWeather));
                    OnPropertyChanged(nameof(ShowInfoText));
                }
                catch (HttpRequestException ex)
                {
                    InfoText = ex.Message;
                    OnPropertyChanged(nameof(InfoText));
                    ShowWeather = false;
                    OnPropertyChanged(nameof(ShowWeather));
                    OnPropertyChanged(nameof(ShowInfoText));
                }
            }
            else
            {
                OnNoInternetConnection();
            }

        }

        private void OnNoInternetConnection()
        {
            InfoText = "No internet connection";
            OnPropertyChanged(nameof(InfoText));
            ShowWeather = false;
            OnPropertyChanged(nameof(ShowWeather));
            OnPropertyChanged(nameof(ShowInfoText));
        }

        private void SendGeolocationData(LocationMessage lm)
        {
            WeakReferenceMessenger.Default.Send(lm);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
