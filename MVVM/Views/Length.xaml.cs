using Mauiverter.MVVM.ViewModels;

namespace Mauiverter.MVVM.Views;

public partial class Length : ContentPage
{
	public Length()
	{
		InitializeComponent();
	}

	private async void OnBackClicked(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
    }
}