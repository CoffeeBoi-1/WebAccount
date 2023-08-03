using Front.Services.Interfaces;
using Front.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using AccountLibrary.Models;

namespace Front.Controllers
{

    [Route("Account")]
    public class AccountDetailedController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AccountService _service;

        public AccountDetailedController(IAccountService service, ILogger<HomeController> logger)
        {
            _logger = logger;
            _service = (AccountService)service;
        }

        [Route("{id:int}")]
        public async Task<IActionResult> IndexAsync(int id)
        {
            try
            {
                AccountDetailedModel account = await _service.GetDetailedAccount(id);
                ViewBag.IsEditing = false;
                return View(account);
            }
            catch(Exception ex)
            {
                AccountDetailedModel account = new AccountDetailedModel();
                return View(account);
            }
        }
    }
}
