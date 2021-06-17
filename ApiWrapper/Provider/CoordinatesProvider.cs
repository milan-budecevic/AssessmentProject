using Newtonsoft.Json;
using ServiceLayer.Adapter.IApiWrapper;
using ServiceLayer.DTO;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiWrapper.Provider
{
    public class CoordinatesProvider : ICoordinatesProvider
    {
        private readonly string API_URL = "http://localhost:5000";
        public async Task<GeoCoordinates> GetCoordinates(CoordinatesRequest coordinatesRequest)
        {
            var coordinates = await ExecuteRequest(coordinatesRequest);
            return JsonConvert.DeserializeObject<GeoCoordinates>(coordinates);
        }

        private async Task<string> ExecuteRequest(CoordinatesRequest payload)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri(API_URL);
            var json = JsonConvert.SerializeObject(payload);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await client.PostAsync("/api/geo", data);
            if (result.IsSuccessStatusCode)
                return await result.Content.ReadAsStringAsync();
            else
                throw new Exception("Bad Request!");
        }
    }
}
