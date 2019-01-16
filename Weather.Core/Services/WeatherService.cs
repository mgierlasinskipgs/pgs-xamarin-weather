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

        public async Task<WeatherData> GetCurrentWeather(string searchQuery, Units units = Units.Kelvin)
        {
            var currentWeather = await _apiClient.GetCurrentWeather(searchQuery, GetParamValueForUnits(units));

            var data = new WeatherData
            {
                CityName = currentWeather.Name,
                Temperature = currentWeather.Main?.Temp ?? 0
            };

            var weather = currentWeather.Weather?.FirstOrDefault();
            if (weather != null)
            {
                data.Description = weather.Main;
                data.Icon = weather.Icon;
            }
            
            return data;
        }

        internal string GetParamValueForUnits(Units units)
        {
            switch (units)
            {
                case Units.Celsius: return "metric";
                case Units.Fahrenheit: return "imperial";
                default: return string.Empty;
            }
        }
    }
}
