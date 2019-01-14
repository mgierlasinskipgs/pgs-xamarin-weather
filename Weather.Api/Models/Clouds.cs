using Newtonsoft.Json;

namespace Weather.Api.Models
{
    public class Clouds
    {
        [JsonProperty("all")]
        public int All { get; set; }
    }
}
