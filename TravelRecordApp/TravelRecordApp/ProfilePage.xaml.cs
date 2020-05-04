using SQLite;
using System.Collections.Generic;
using System.Linq;
using TravelRecordApp.Model;
using TravelRecordApp.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfilePage : ContentPage
	{
        public ProfilePage ()
		{
			InitializeComponent ();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            //using (var conn = new SQLiteConnection(App.DatabaseLocation))
            //{
                //var postTable = conn.Table<Post>().ToList();
                var posts = await Post.Read();
            var postTable = posts.Where(p => p.UserId == App.user.Id).ToList();            

            var categoriesCount = Post.PostCategories(postTable);                

            categoriesListView.ItemsSource = categoriesCount;

            postCountLabel.Text = postTable.Count.ToString();
            //}
        }
    }
}