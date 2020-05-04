using System;
using TravelRecordApp.Model;
using TravelRecordApp.Services;
using TravelRecordApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterPage : ContentPage
	{
        User _user;
        RegisterVM viewModel;
        public RegisterPage ()
		{
			InitializeComponent ();           
            viewModel = new RegisterVM();
            BindingContext = viewModel;
        }       
    }
}