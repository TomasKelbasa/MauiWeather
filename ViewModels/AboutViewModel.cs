using CommunityToolkit.Mvvm.Messaging;
using MauiWeather.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiWeather.ViewModels
{
    public class AboutViewModel : INotifyPropertyChanged
    {
        private AboutModel _am;

        public Placemark Place { get { return _am.Place; } }
        public double Latitude { get { return _am.Latitude; } }
        public double Longitude { get { return _am.Longitude; } }
        public double Altitude { get { return _am.Altitude; } }

        public AboutViewModel() 
        {
            _am = new AboutModel();
            WeakReferenceMessenger.Default.Register<LocationMessage>(this, (recipient, message) =>
            {
                _am.Latitude = message.Lat;
                _am.Longitude = message.Lon;
                _am.Altitude = message.Alt;
                _am.Place = message.Location;
                OnPropertyChanged(nameof(Latitude));
                OnPropertyChanged(nameof(Longitude));
                OnPropertyChanged(nameof(Altitude));
                OnPropertyChanged(nameof(Place));

            });
        }

        public ICommand OpenURL => new Command<string>(async (url) => {
            try
            {
                Uri uri = new Uri(url);
                await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }catch (Exception ex)
            {

            }
        });

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
