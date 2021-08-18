using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.ViewModels.Helpers
{
    class OpenWeatherHelper
    {
        private const string BASE_URL = "http://api.openweathermap.org";
        private const string API_KEY = "c924be429d47dffe614dee1d81940181";

        public static async Task<OpenWeatherResponse> GetCurrentWeather(string cityName)
        {
            var httpClient = new HttpClient();
            var requestUrl = $"{BASE_URL}/data/2.5/weather?q={cityName}&appid={API_KEY}";
            var response = await httpClient.GetAsync(requestUrl);
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<OpenWeatherResponse>(json);
        }


        public static string GetImageUrl(string code)
        {
            return $"http://openweathermap.org/img/wn/{code}@4x.png";
        }
    }
}
