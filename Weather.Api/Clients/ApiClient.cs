using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Weather.Api.Models;

namespace Weather.Api.Clients
{
    public class ApiClient : IApiClient
    {
        private const string BaseUrl = "https://api.openweathermap.org/data/2.5/weather";
        private const string ApiKey = "b5dc472083eb21298b570b0fb9d9680d";

        private static readonly HttpClient _client = new HttpClient();

        public async Task<CurrentWeather> GetCurrentWeather(string cityName)
        {
            var url = $"{BaseUrl}?q={cityName}&APPID={ApiKey}";

            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CurrentWeather>(json);
        }
    }
}
