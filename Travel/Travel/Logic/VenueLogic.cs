using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Travel.Model;

namespace Travel.Logic
{
   public class VenueLogic
    {
        public async static Task<List<Venue>> GetVenues(double latitude,double longitude)
        {
            List<Venue> venues = new List<Venue>();
            var url = VenueRoot.GenerateURL(latitude, longitude);

            using (HttpClient client = new HttpClient())
            {   
              var response =  await  client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();
                var valueroot = JsonConvert.DeserializeObject<VenueRoot>(json);
                venues = valueroot.response.venues as List<Venue>;
            }

            return venues;
        }
    }
}
