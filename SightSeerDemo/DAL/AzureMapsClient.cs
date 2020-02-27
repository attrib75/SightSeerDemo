using Newtonsoft.Json;
using SightSeerDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SightSeerDemo.DAL
{
    /// <summary>
    /// Contains methods to call Azure Maps API
    /// </summary>
    public class AzureMapsClient
    {
        private static string key;
        private static string clientId;
        private static string URL = $"https://atlas.microsoft.com/route/directions/json?&api-version=1.0";

        public AzureMapsClient(string _apiKey, string _clientId)

        {
            key = _apiKey;
            clientId = _clientId;
        }

        /// <summary>
        /// Get a navigation route for a list of destinations
        /// </summary>
        /// <param name="_latLongs"></param>
        /// <returns></returns>
        public async Task<RouteResponse> GetRoute(List<LatLong> _latLongs)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("x-ms-client-id", key);
                string coordString = string.Join(":", _latLongs.Select(d => d.DLat.ToString() + "," + d.DLong.ToString()));
                string uri = URL + $"&subscription-key={key}&query={coordString}";

                HttpResponseMessage response = await client.GetAsync(uri);

                string contentString = await response.Content.ReadAsStringAsync();

                RouteResponse RouteResponse = JsonConvert.DeserializeObject<RouteResponse>(contentString);
                return RouteResponse;
            }
        }
    }
}