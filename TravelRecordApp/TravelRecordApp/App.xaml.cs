using System;
using TravelRecordApp.Model;
using TravelRecordApp.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Skip)]
namespace TravelRecordApp
{
	public partial class App : Application
	{
        public static string DatabaseLocation = string.Empty;
        public static User user = new User();
        public static MongoDBService _mongoDBService;

        public App ()
		{
		    InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
		}
        public App(string databaseLocation)
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
            DatabaseLocation = databaseLocation;
            _mongoDBService = new MongoDBService("TravelRecordDB");
        }

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
