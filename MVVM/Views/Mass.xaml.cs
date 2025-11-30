using Mauiverter.MVVM.ViewModels;

namespace Mauiverter.MVVM.Views;

public partial class Mass : ContentPage
{
	public Mass()
	{
		InitializeComponent();
	}

	private async void OnBackClicked(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
    }
}