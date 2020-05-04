using Plugin.Geolocator;
using System;
using System.Linq;
using TravelRecordApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewTravelPage : ContentPage
	{
        Post _post
        public NewTravelPage ()
		{
			InitializeComponent ();
            _post = new Post();
            containerStackLayout.BindingContext = _post;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();

            var venues = await Venue.GetVenuesAsync(position.Latitude, position.Longitude);
            venueListView.ItemsSource = venues;
        }

        private async void ToolbarItem_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                var selectedVenue = venueListView.SelectedItem as Venue;
                var firstCategory = selectedVenue.categories.FirstOrDefault();

                _post.CategoryId = firstCategory.id;
                _post.CategoryName = firstCategory.name;
                _post.Address = selectedVenue.location.address;
                _post.Latitude = selectedVenue.location.lat;
                _post.Longitude = selectedVenue.location.lng;
                _post.VenueName = selectedVenue.name;
                _post.UserId = App.user.Id;
                

                //using (var conn = new SQLiteConnection(App.DatabaseLocation))
                //{
                //    conn.CreateTable<Post>();
                //    int rows = conn.Insert(post);

                //    if (rows > 0)
                //        DisplayAlert("Success", "Experience successfully inserted!", "Ok");
                //    else
                //        DisplayAlert("Failed", "Experience failed to insert!", "Ok");
                //}

                Post.Insert(post);
                await DisplayAlert("Success", "Experience successfully inserted!", "Ok");

            }
            catch (NullReferenceException nre)
            {
                await DisplayAlert("Failed", "Experience failed to insert!", "Ok");
            }
            catch (System.Exception)
            {
                await DisplayAlert("Failed", "Experience failed to insert!", "Ok");
            }

        }
    }
}