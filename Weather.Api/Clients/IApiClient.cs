using System.Threading.Tasks;
using Weather.Api.Models;

namespace Weather.Api.Clients
{
    public interface IApiClient
    {
        string BaseUrl { get; set; }
        string ApiKey { get; set; }

        Task<CurrentWeather> GetCurrentWeather(string searchQuery, string units);
    }
}
