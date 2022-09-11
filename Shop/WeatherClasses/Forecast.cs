using Newtonsoft.Json;

namespace Shop.WeatherClasses
{
    public class Forecast
    {
        [JsonProperty("forecastday")]
        public List<Forecastday> Forecastday { get; set; }
    }
}
