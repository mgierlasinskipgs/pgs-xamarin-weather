using System.Threading.Tasks;
using Weather.Api.Models;

namespace Weather.Api.Clients
{
    public interface IApiClient
    {
        Task<CurrentWeather> GetCurrentWeather(string cityName);
    }
}
