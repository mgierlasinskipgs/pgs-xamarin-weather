using Newtonsoft.Json;
using System;
using System.Net;
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

        public async Task<CurrentWeather> GetCurrentWeather(string searchQuery, string units)
        {
            var url = $"{BaseUrl}?q={searchQuery}&units={units}&APPID={ApiKey}";

            var response = await _client.GetAsync(url);
            
            HandleStatusCode(searchQuery, response);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CurrentWeather>(json);
        }

        private void HandleStatusCode(string searchQuery, HttpResponseMessage response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    throw new Exception($"Weather for city {searchQuery} not found.");
            }
        }
    }
}
