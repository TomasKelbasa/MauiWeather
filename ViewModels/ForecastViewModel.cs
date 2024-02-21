using MauiWeather.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using RestSharp;
using System.Text.Json;
using CommunityToolkit.Mvvm.Messaging;
using MauiWeather.Services;

namespace MauiWeather.ViewModels
{
    public class ForecastViewModel : INotifyPropertyChanged
    {

        private ForecastModel _fm = null;

        public string InfoText { get; set; } = "Default";

        public bool ShowForecastData { get; set; }
        public bool ShowInfoText { get { return !ShowForecastData; } }

        public Placemark Place
        {
            get { return _fm.Place;}
        }

        public double Latitude
        {
            get { return _fm.Latitude; }
        }

        public double Longitude
        {
            get { return _fm.Longitude; }
        }

        public List<DailyForecast> ForecastData
        {
            get { return _fm.ForeacastData; }
        }

        public ForecastViewModel()
        {
            _fm = new ForecastModel();
            WeakReferenceMessenger.Default.Register<LocationMessage>(this, async (recipient, message) =>
            {
                _fm.Latitude = message.Lat;
                _fm.Longitude = message.Lon;
                _fm.Altitude = message.Alt;
                _fm.Place = message.Location;
                await ProcessForecast();
            });
        }

        public ICommand getData => new Command(async () =>
        {
            // zobrazí zprávu loading
            InfoText = "Loading...";
            OnPropertyChanged(nameof(InfoText));
            ShowForecastData = false;
            OnPropertyChanged(nameof(ShowForecastData));
            OnPropertyChanged(nameof(ShowInfoText));


            var location = await WeatherHelperService.GetCurrentLocation();
            if (location != null) {
                _fm.Latitude = location.Lat;
                _fm.Longitude = location.Lon;
                _fm.Altitude = location.Alt;
                _fm.Place = location.Location;
                await ProcessForecast();
                SendGeolocationData(location);
            }

        });

        private async Task ProcessForecast()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                try
                {
                    var data = await WeatherHelperService.FetchFromApi(_fm.ApiRequestString);
                    var t = JsonSerializer.Deserialize<ForecastResponse>(data.Content);
                    _fm.ForeacastData = t.Timelines.Daily;
                    ShowForecastData = true;
                    OnPropertyChanged(nameof(ShowForecastData));
                    OnPropertyChanged(nameof(ShowInfoText));
                    OnPropertyChanged(nameof(ForecastData));
                }
                catch (HttpRequestException ex)
                {
                    InfoText = ex.Message;
                    OnPropertyChanged(nameof(InfoText));
                    ShowForecastData = false;
                    OnPropertyChanged(nameof(ShowForecastData));
                    OnPropertyChanged(nameof(ShowInfoText));
                }
            }
            else
            {
                ShowForecastData = false;
                OnPropertyChanged(nameof(ShowForecastData));
                OnPropertyChanged(nameof(ShowInfoText));
                InfoText = "No internet connection";
                OnPropertyChanged(nameof(InfoText));
            }

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
