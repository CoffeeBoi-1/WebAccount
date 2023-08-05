using AccountLibrary.Models;
using Front.Models;
using Front.Services;
using Front.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
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

		public async Task<IActionResult> IndexAsync(int pageNumber, FilterModel filter, int pageSize = 10)
		{
			IEnumerable<AccountModel> accounts = await _service.GetFilteredAccounts(pageNumber, pageSize, filter);

			return View(accounts);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}