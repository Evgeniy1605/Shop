using Newtonsoft.Json;
using Shop.WeatherClasses;

namespace Shop.Models
{
    public class WeatherModel
    {
        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("current")]
        public Current Current { get; set; }

        [JsonProperty("forecast")]
        public Forecast Forecast { get; set; }
    }
}
