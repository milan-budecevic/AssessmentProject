using ServiceLayer.DTO;
using System.Threading.Tasks;

namespace ServiceLayer.Adapter.IApiWrapper
{
    public interface ICoordinatesProvider
    {
        Task<GeoCoordinates> GetCoordinates(CoordinatesRequest coordinatesRequest);
    }
}
