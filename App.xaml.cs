using Mauiverter.MVVM.Views;

namespace Mauiverter
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

            //var navpage = new NavigationPage(new MenuView());
            //return new Window(navpage);

         protected override Window CreateWindow(IActivationState? activationState)
         {
            var navPage = new NavigationPage(new MenuView());
            return new Window(navPage);
         }
    }
}
