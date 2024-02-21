namespace MauiWeather.Views;

public partial class About : ContentPage
{
	public About()
	{
		InitializeComponent();
        BindingContext = (Application.Current as App).Avm;
    }
}