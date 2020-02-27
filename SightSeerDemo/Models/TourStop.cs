using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SightSeerDemo.Models
{
    [Serializable]
    public class TourStop
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public LatLong LatLong { get; set; }
    }

    public class LatLong
    {
        public double DLat { get; set; }
        public double DLong { get; set; }
        public int SectionNumber { get; set; }

        public int PointNumber { get; set; }

        public int LegNumber { get; set; }

        public int OverallPointCount { get; set; }
    }
}