using Plugin.Geolocator;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travel
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MapPage : ContentPage
	{

        Plugin.Geolocator.Abstractions.IGeolocator locator;
        public MapPage ()
		{
			InitializeComponent ();
           
		}

        protected async override void OnAppearing()
        {
            base.OnAppearing();
               locator = CrossGeolocator.Current;
                locator.PositionChanged += Locator_PositionChanged;
            await locator.StartListeningAsync(TimeSpan.FromSeconds(0), 100);
            var position = await locator.GetPositionAsync();
            var center = new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude);
            var span = new Xamarin.Forms.Maps.MapSpan(center, 1, 1);
            locationsMap.MoveToRegion(span);

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Post>();
                var posts = conn.Table<Post>().ToList();
                DisplayInMap(posts);
            }
        }

        private void DisplayInMap(List<Post> posts)
        {
            try
            {
                foreach (var post in posts)
                {
                    var position = new Xamarin.Forms.Maps.Position(post.Latitud, post.Longitud);
                    var pin = new Xamarin.Forms.Maps.Pin()
                    {
                        Type = Xamarin.Forms.Maps.PinType.SavedPin,
                        Address = post.Address,
                        Position = position,
                        Label = post.VanueName
                    };
                    locationsMap.Pins.Add(pin);
                }
            }
            catch (NullReferenceException nre) { }
            catch (Exception ex) { }
        }

        private void Locator_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            var center = new Xamarin.Forms.Maps.Position(e.Position.Latitude, e.Position.Longitude);
            var span = new Xamarin.Forms.Maps.MapSpan(center, 1, 1);
            locationsMap.MoveToRegion(span);
        }
        protected async override void OnDisappearing()
        {
            base.OnDisappearing();
            await locator.StopListeningAsync();
        }

    }
}