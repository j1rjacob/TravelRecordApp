using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}
        
        private void LoginButton_Clicked(object sender, EventArgs e)
        {
            var isEmailEmpty = string.IsNullOrEmpty(emailEntry.Text);
            var isPasswordEmpty = string.IsNullOrEmpty(passwordEntry.Text);

            if(isEmailEmpty || isPasswordEmpty)
            {

            }
            else
            {               
                Navigation.PushAsync(new HomePage());
            }
        }
    }
}
