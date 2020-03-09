using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SightSeerDemo.DAL;
using SightSeerDemo.Models;
using SightSeerDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SightSeerDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public AppSettings AppSettings { get; private set; }
        public static List<LatLong> RoutePoints = new List<LatLong>();
        public static List<TourStop> TourStops = new List<TourStop>();

        public HomeController(ILogger<HomeController> logger, IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            AppSettings = appSettings?.Value;
            TourStops = GetTourStops();
        }

        public async Task<IActionResult> Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel();
            homeViewModel.MapAPIKey = AppSettings.AzureMapsKey;
            homeViewModel.TourStops = TourStops;

            //foreach (var item in route.Routes)
            //{
            //    int pointCount = 0;
            //    for (int i = 0; i < item.Legs.Count; i++)
            //    {
            //        var leg = item.Legs[i];
            //        // 1 - 2 = 1
            //        // 2 - 3 = 2
            //        // 3 - 4 = 3
            //        // 4 - 5 = 4
            //        for (int p = 0; p < leg.Points.Count; p++)
            //        {
            //            //what point count is this in all points for all legs
            //            //int sectionInt = DetermineSection(item.Sections, item.Legs, i, p);
            //            pointCount++;

            //            RoutePoints
            //                 .Add(new LatLong
            //                 {
            //                     DLat = leg.Points[p].Latitude,
            //                     DLong = leg.Points[p].Longitude,
            //                     OverallPointCount = pointCount,
            //                     PointNumber = p,
            //                     LegNumber = i,
            //                 });
            //        }
            //    }
            //    for (int s = 0; s < item.Sections.Count; s++)
            //    {
            //        var section = item.Sections[s];
            //        var lowNum = section.StartPointIndex;
            //        var highNum = section.EndPointIndex;
            //        var rps = RoutePoints.Where(r => r.OverallPointCount >= lowNum && r.OverallPointCount <= highNum);
            //        foreach (var rp in rps)
            //        {
            //            rp.SectionNumber = s;
            //        }
            //    }
            //}
            //homeViewModel.RoutePoints = RoutePoints;
            return View(homeViewModel);
        }

        private async Task<RouteResponse> GetRoute(List<LatLong> LatLongs)
        {
            return await new AzureMapsClient(AppSettings.AzureMapsKey, AppSettings.AzureMapsClientID)
                .GetRoute(LatLongs);
        }

        public async Task<JsonResult> GetWebSearchResultsJson(string searchTerm)
        {
            BingSearchClient bingSearchClient = new BingSearchClient();
            var searchResults = await bingSearchClient.WebSearchResultTypesLookup(AppSettings.BingAPIKey, searchTerm);
            return Json(searchResults);
        }

        public JsonResult GetImageSearchResultsJson(string searchTerm)
        {
            BingSearchClient bingSearchClient = new BingSearchClient();
            var searchResults = bingSearchClient.ImageSearch(AppSettings.BingAPIKey, searchTerm);
            return Json(searchResults);
        }

        public JsonResult GetTourStopsJson()
        {
            return Json(TourStops);
        }

        public List<TourStop> GetTourStops()
        {
            return new List<TourStop>{
               new TourStop{
                 Address = "233 S Wacker Dr, Chicago, IL 60606",
                 LatLong = new LatLong{
                 DLat = 41.8788764,
                 DLong = -87.6359149, },
                 Name = "Willis Tower"
               },
               new TourStop{
                 Address = "111 S Michigan Ave, Chicago, IL 60603",
                 LatLong = new LatLong{
                 DLat = 41.8795845,
                 DLong = -87.6237133, },
                 Name = "The Art Institute of Chicago"
               },
               new TourStop{
                 Address = "201 E Randolph St, Chicago, IL 60602",
                 LatLong = new LatLong{
                 DLat = 41.8825524,
                 DLong = -87.6225514, },
                 Name = "Millennium Park"
               },
               new TourStop{
                 Address = "600 E Grand Ave, Chicago, IL 60611",
                 LatLong = new LatLong{
                 DLat = 41.8918633,
                 DLong = -87.6050944, },
                 Name = "Navy Pier"
               },
               new TourStop{
                 Address = "N Michigan Ave, Chicago, IL 60611",
                 LatLong = new LatLong{
                 DLat = 41.8948287,
                 DLong = -87.6329721,},
                 Name = "Magnificent Mile"
               },
            };
        }

        public double GetDistanceBetweenPoints(double lat1, double long1, double lat2, double long2)
        {
            double distance = 0;

            double dLat = (lat2 - lat1) / 180 * Math.PI;
            double dLong = (long2 - long1) / 180 * Math.PI;

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2)
                        + Math.Cos(lat2) * Math.Sin(dLong / 2) * Math.Sin(dLong / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            //Calculate radius of earth
            // For this you can assume any of the two points.
            double radiusE = 6378135; // Equatorial radius, in metres
            double radiusP = 6356750; // Polar Radius

            //Numerator part of function
            double nr = Math.Pow(radiusE * radiusP * Math.Cos(lat1 / 180 * Math.PI), 2);
            //Denominator part of the function
            double dr = Math.Pow(radiusE * Math.Cos(lat1 / 180 * Math.PI), 2)
                            + Math.Pow(radiusP * Math.Sin(lat1 / 180 * Math.PI), 2);
            double radius = Math.Sqrt(nr / dr);

            //Calaculate distance in metres.
            distance = radius * c;
            return distance;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}