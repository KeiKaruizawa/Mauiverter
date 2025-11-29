using Mauiverter.MVVM.Views;

namespace Mauiverter
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //var navpage = new NavigationPage(new MenuView());
            //return new Window(navpage);

            MainPage = new NavigationPage(new MenuView());
        }
    }
}
