using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Geolocator;
using SQLite;
using Travel.Logic;
using Travel.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travel
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class newPage : ContentPage
	{
		public newPage ()
		{
			InitializeComponent ();
		}

        protected async override void OnAppearing()
        {
            base.OnAppearing();


            var locator = CrossGeolocator.Current;
           // var position = await locator.GetPositionAsync();
            //var venues = await VenueLogic.GetVenues(position.Latitude, position.Longitude);
            var venues = await VenueLogic.GetVenues(-12.0741888, -77.03101439999999);
            venueListView.ItemsSource = venues;

        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try { 
            var selectedVenue = venueListView.SelectedItem as Venue;
            var firstCategory = selectedVenue.categories.FirstOrDefault();
                Post post = new Post()
                {
                    Experience = expretienceEntry.Text,
                    VanueName = selectedVenue.name,
                    CategoryId = firstCategory.id,
                    CategoryName = firstCategory.name,
                    Latitud = selectedVenue.location.lat,
                    Longitud = selectedVenue.location.lng,
                    Address = selectedVenue.location.address,
                    Distance = selectedVenue.location.distance
                
            };

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Post>();
                int rows = conn.Insert(post);

                if (rows > 0)
                    DisplayAlert("Success", "Experience succesfully inserter", "Ok");
                else
                    DisplayAlert("Failure", "Experience failed to be inserted", "Ok");
            }

            }
            catch (NullReferenceException nre)
            {

            }catch(Exception ex)
            {

            }
        }
    }
}