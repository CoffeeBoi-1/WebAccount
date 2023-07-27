using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using Front.Models;
using Front.Helpers;
using Front.Services.Interfaces;

namespace Front.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly HttpClient _client;
        public const string BasePath = "WeatherForecast";

        public WeatherForecastService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<WeatherForecastModel>> Find()
        {
            HttpResponseMessage response = await _client.GetAsync(BasePath);

            return await response.ReadContentAsync<List<WeatherForecastModel>>();
        }
    }
}
