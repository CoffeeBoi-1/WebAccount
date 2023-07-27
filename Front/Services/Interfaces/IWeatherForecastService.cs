using Front.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Front.Services.Interfaces
{
    public interface IWeatherForecastService
    {
        Task<IEnumerable<WeatherForecastModel>> Find();
    }
}
