using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.DTO
{
    public class GeoData
    {
        public string Locality { get; set; }
        public string Zip { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
