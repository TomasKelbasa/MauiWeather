
using MauiWeather.ViewModels;

namespace MauiWeather.Views;

public partial class Realtime : ContentPage
{
	public Realtime()
	{
        InitializeComponent();
		BindingContext = (Application.Current as App).Rvm;
	}
}