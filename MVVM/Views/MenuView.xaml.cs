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

	private async void OnVolumeClicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new Volume());
	}
	
	private async void OnLengthClicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new Length());
    }

	private async void OnMassClicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new Mass());
    }

	private async void OnTemperatureClicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new Temperature());
    }

	private async void OnAreaClicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new Area());
    }

	private async void OnSpeedClicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new Speed());
    }

	private async void OnEnergyClicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new Energy());
    }

    private async void OnTimeClicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new Time());
    }
}