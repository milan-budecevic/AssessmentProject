using DataLayer.IRepository;
using ServiceLayer.Adapter.IApiWrapper;
using ServiceLayer.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class AddressService
    {
        private readonly IDataRepository _dataRepository;
        private readonly ICoordinatesProvider _coordinatesProvider;
        public AddressService(IDataRepository dataRepository, ICoordinatesProvider coordinatesProvider)
        {
            _dataRepository = dataRepository;
            _coordinatesProvider = coordinatesProvider;
        }

        public async Task GetCoordinates()
        {
            var data = await _dataRepository.GetDataFromCsv();
            List<PointGeoJsonData> pointGeoJsonList = new List<PointGeoJsonData>();
            data.PostalCodes.ForEach(async x =>
             {
                 var street = data.Streets.Where(s => s.PostalCodeId == x.PostalCodeId).FirstOrDefault();

                 if (street == null)
                     return;

                 var locality = data.Localities.Where(l => l.PostalCodeId == x.PostalCodeId).FirstOrDefault();
                 var house = data.Houses.Where(h => h.StreetId == street.StreetId).FirstOrDefault();

                 if (locality == null || house == null)
                     return;

                 var fullAddress = new CoordinatesRequest()
                 {
                     Zip = x.Zip,
                     Locality = locality.LocalityName,
                     Street = street.StreetName,
                     StreetNumber = house.HouseNumber
                 };
                 var coordinates = await _coordinatesProvider.GetCoordinates(fullAddress);
                 var geoJson = await CreateGeoJson(new GeoData()
                 {
                     Zip = fullAddress.Zip,
                     Street = fullAddress.Street,
                     StreetNumber = fullAddress.StreetNumber,
                     Locality = fullAddress.Locality,
                     Latitude = coordinates.Latitude,
                     Longitude = coordinates.Longitude
                 });

                 pointGeoJsonList.Add(geoJson);

             });
            await WriteGeoJson(pointGeoJsonList);
        }
        public async Task<PointGeoJsonData> CreateGeoJson(GeoData geoData)
        {
            var geoJson = new PointGeoJsonData()
            {
                Type = "FeatureCollection",
                Features = new List<PointGeoJsonFeatures>()
            };

            var geoJsonFeature = new PointGeoJsonFeatures()
            {
                Type = "Feature",
                Properties = new PointGeoJsonFeatureProperties()
                {
                    Zip = geoData.Zip,
                    StreetName = geoData.Street,
                    StreetNumber = geoData.StreetNumber,
                    Locality = geoData.Locality
                },
                Geometry = new PointGeoJsonGeometry()
                {
                    Type = "Point"
                }
            };

            var coordinates = new List<decimal>
            {
                Convert.ToDecimal(geoData.Longitude),
                Convert.ToDecimal(geoData.Latitude)
            };

            geoJsonFeature.Geometry.Coordinates = coordinates;
            geoJson.Features.Add(geoJsonFeature);
            return geoJson;
        }
        private async Task WriteGeoJson(List<PointGeoJsonData> poiGeoJsonData)
        {
            string path = @"D:\IT-engine\GeoJsonFile.json";
            using (StreamWriter outputFile = File.AppendText(path))
            {
                string json = System.Text.Json.JsonSerializer.Serialize(poiGeoJsonData);
                await outputFile.WriteLineAsync(json);
            }
        }
    }
}
