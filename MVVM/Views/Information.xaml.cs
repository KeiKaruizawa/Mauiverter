using Mauiverter.MVVM.ViewModels;

namespace Mauiverter.MVVM.Views;

public partial class Information : ContentPage
{
	public Information()
	{
		InitializeComponent();
	}

	private async void OnBackClicked(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}
}