using System.Threading.Tasks;
using Weather.Core.Models;

namespace Weather.Core.Services
{
    public class WeatherService : IWeatherService
    {
        public Task<WeatherData> GetCurrentWeather(string city)
        {
            return Task.FromResult(new WeatherData
            {
                Description = "Description of weather",
                Temperature = 20
            });
        }
    }
}
