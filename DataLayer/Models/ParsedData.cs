using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class ParsedData
    {
        public List<PostalCode> PostalCodes { get; set; }
        public List<House> Houses { get; set; }
        public List<Locality> Localities { get; set; }
        public List<Street> Streets { get; set; }
    }
}
