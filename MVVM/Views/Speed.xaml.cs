using Mauiverter.MVVM.ViewModels;

namespace Mauiverter.MVVM.Views;

public partial class Speed : ContentPage
{
	public Speed()
	{
		InitializeComponent();
	}

	private async void OnBackClicked(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
    }
}