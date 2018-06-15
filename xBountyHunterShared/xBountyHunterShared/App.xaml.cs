using Xamarin.Forms;
using xBountyHunterShared.Views;
using System;
using xBountyHunterShared.Extras;
using System.Threading.Tasks;

namespace xBountyHunterShared
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainTabbedPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
