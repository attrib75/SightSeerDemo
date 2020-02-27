using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SightSeerDemo.Models
{
    public partial class RouteResponse
    {
        public string FormatVersion { get; set; }
        public List<Route> Routes { get; set; }
    }

    public partial class Route
    {
        public Summary Summary { get; set; }
        public List<Leg> Legs { get; set; }
        public List<Section> Sections { get; set; }
    }

    public partial class Leg
    {
        public Summary Summary { get; set; }
        public List<Point> Points { get; set; }
    }

    public partial class Point
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public partial class Summary
    {
        public long LengthInMeters { get; set; }
        public long TravelTimeInSeconds { get; set; }
        public long TrafficDelayInSeconds { get; set; }
        public DateTimeOffset DepartureTime { get; set; }
        public DateTimeOffset ArrivalTime { get; set; }
    }

    public partial class Section
    {
        public int StartPointIndex { get; set; }
        public int EndPointIndex { get; set; }
        public string SectionType { get; set; }
        public string TravelMode { get; set; }
    }
}