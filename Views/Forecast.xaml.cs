using MauiWeather.Models;
using MauiWeather.ViewModels;

namespace MauiWeather.Views;

public partial class Forecast : ContentPage
{
	public Forecast()
	{
		InitializeComponent();
		BindingContext = (Application.Current as App).Fwm;
	}
}