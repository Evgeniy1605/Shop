﻿using Newtonsoft.Json;

namespace Shop.WeatherClasses
{
    public class Forecastday
    {
        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("date_epoch")]
        public int DateEpoch { get; set; }

        [JsonProperty("day")]
        public Day Day { get; set; }

        [JsonProperty("astro")]
        public Astro Astro { get; set; }

        [JsonProperty("hour")]
        public List<Hour> Hour { get; set; }
    }
}
