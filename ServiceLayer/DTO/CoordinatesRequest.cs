using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.DTO
{
    public class CoordinatesRequest
    {
        public string Locality { get; set;}
        public string Zip { get; set;}
        public string Street { get; set;}
        public string StreetNumber { get; set;}
    }
}
