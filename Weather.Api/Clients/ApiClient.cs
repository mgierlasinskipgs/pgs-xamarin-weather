using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Weather.Api.Models;

namespace Weather.Api.Clients
{
    public class ApiClient : IApiClient
    {
        public string BaseUrl { get; set; } = "https://api.openweathermap.org/data/2.5/weather";
        public string ApiKey { get; set; } = "b5dc472083eb21298b570b0fb9d9680d";

        private readonly HttpClient _client;

        public ApiClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<CurrentWeather> GetCurrentWeather(string searchQuery, string units)
        {
            var url = $"{BaseUrl}?q={searchQuery}&units={units}&APPID={ApiKey}";

            var response = await _client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new ApiException((int)response.StatusCode, GetErrorMessage(searchQuery, response));
            }

            if (response.Content == null)
            {
                throw new ApiException((int)HttpStatusCode.NoContent, "Response has no content.");
            }

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CurrentWeather>(json);
        }
        
        private string GetErrorMessage(string searchQuery, HttpResponseMessage response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    return $"Weather for city {searchQuery} not found.";
                default:
                    return response.ReasonPhrase;
            }
        }
    }
}
