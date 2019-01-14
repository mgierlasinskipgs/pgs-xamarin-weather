using System.Threading.Tasks;
using Weather.Core.Models;

namespace Weather.Core.Services
{
    public interface IWeatherService
    {
        Task<WeatherData> GetCurrentWeather(string city);
    }
}
