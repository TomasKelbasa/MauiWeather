using MauiWeather.ViewModels;
using MauiWeather.Views;

namespace MauiWeather
{
    public partial class App : Application
    {
        public ForecastViewModel Fwm {  get; set; }
        public RealtimeViewModel Rvm { get; set; }
        public AboutViewModel Avm { get; set; }

        public App()
        {
            InitializeComponent();

            Fwm = new ForecastViewModel();
            Rvm = new RealtimeViewModel();
            Avm = new AboutViewModel();

            MainPage = new AppShell();
        }
    }
}