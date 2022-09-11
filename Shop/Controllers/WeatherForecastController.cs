using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shop.Models;

namespace Shop.Controllers
{
    public class WeatherForecastController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Whether(string city)
        {
            var client = new HttpClient();

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://weatherapi-com.p.rapidapi.com/forecast.json?q={city.ToLower()}&days=3"),
                Headers =
                {
                    { "X-RapidAPI-Key", "061fc75274msh97af0ac2d2c52b8p1c8e99jsn4d23e0ab9fb2" },
                    { "X-RapidAPI-Host", "weatherapi-com.p.rapidapi.com" },
                },
            };
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                response.EnsureSuccessStatusCode();
                var body = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<WeatherModel>(body);
                client.Dispose();
                return View(result);
            }
            else
            {
                client.Dispose();
                return Problem();
            }



        }

        public IActionResult ChooseYoutCity()
        {
            return View();
        }
    }
}
