using Mauiverter.MVVM.Views;

namespace Mauiverter.MVVM.Views;

public partial class MenuView : ContentPage
{
	public MenuView()
	{
		InitializeComponent();
	}

	private async void OnInformationClicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new Information());
	}

}