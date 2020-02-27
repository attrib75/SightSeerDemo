using SightSeerDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SightSeerDemo.ViewModels
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            TourStops = new List<TourStop>();
            RoutePoints = new List<LatLong>();
        }

        public string MapAPIKey { get; set; }
        public List<TourStop> TourStops { get; set; }
        public List<LatLong> RoutePoints { get; set; }

        public string RoutePointString { get; set; }
    }
}