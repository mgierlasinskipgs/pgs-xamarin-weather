using System.Linq;
using System.Threading.Tasks;
using Weather.Api.Clients;
using Weather.Core.Models;

namespace Weather.Core.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IApiClient _apiClient;

        public WeatherService(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<WeatherData> GetCurrentWeather(string city)
        {
            var currentWeather = await _apiClient.GetCurrentWeather(city);

            var data = new WeatherData
            {
                Temperature = currentWeather.Main?.Temp ?? 0
            };

            if (currentWeather.Weather != null)
            {
                data.Description = string.Join(" ", currentWeather.Weather.Select(x => x.Description));
            }

            return data;
        }
    }
}
