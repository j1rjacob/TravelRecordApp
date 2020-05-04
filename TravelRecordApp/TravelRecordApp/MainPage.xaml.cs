using System;
using System.Linq;
using TravelRecordApp.Model;
using TravelRecordApp.Services;
using TravelRecordApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    public partial class MainPage : ContentPage
	{
        MainVM viewModel;
        public readonly MongoDBService _mongoDBService;
		public MainPage()
		{
			InitializeComponent();
            var assembly = typeof(MainPage);
            viewModel = new MainVM();
            BindingContext = viewModel
            iconImage.Source = ImageSource.FromResource("TravelRecordApp.Assets.Images.plane.png", assembly);
            _mongoDBService = new MongoDBService("TravelRecordDB");
        }
    }
}
