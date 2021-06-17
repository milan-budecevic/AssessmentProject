using System.Collections.Generic;

namespace ServiceLayer.DTO
{
    public class WebGeoJsonBase
    {
        public string Type { get; set; }
    }
    public class PointGeoJsonData : WebGeoJsonBase
    {
        public List<PointGeoJsonFeatures> Features { get; set; }
    }
    public class PointGeoJsonFeatures : WebGeoJsonBase
    {
        public PointGeoJsonFeatureProperties Properties { get; set; }
        public PointGeoJsonGeometry Geometry { get; set; }
    }
    public class PointGeoJsonGeometry : WebGeoJsonBase
    {
        public List<decimal> Coordinates { get; set; }
    }

    public class PointGeoJsonFeatureProperties
    {
        public string Zip { get; set; }
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public string Locality { get; set; }
    }
}
