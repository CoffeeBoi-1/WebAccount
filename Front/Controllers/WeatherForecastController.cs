using Front.Models;
using Front.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Front.Controllers
{
    public class WeatherForecastController : Controller
    {
        private readonly IWeatherForecastService _service;

        public WeatherForecastController(IWeatherForecastService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<WeatherForecastModel> products = await _service.Find();
            return View(products);
        }
    }
}
