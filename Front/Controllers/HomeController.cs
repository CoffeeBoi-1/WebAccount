using Front.BaseClasses;
using Front.Models;
using Front.Services;
using Front.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Front.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AccountService _service;

        public HomeController(IAccountService service, ILogger<HomeController> logger)
        {
            _logger = logger;
            _service = (AccountService)service;
        }

        public async Task<IActionResult> IndexAsync()
        {
            IEnumerable<AccountBase> accounts = await _service.GetAccounts(1);
            return View(accounts);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
