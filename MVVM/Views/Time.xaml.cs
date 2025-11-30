using Mauiverter.MVVM.ViewModels;

namespace Mauiverter.MVVM.Views;

public partial class Time : ContentPage
{
	public Time()
	{
		InitializeComponent();
	}

	private async void OnBackClicked(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
    }
}