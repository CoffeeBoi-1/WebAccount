using Front.Services.Interfaces;
using Front.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using AccountLibrary.Models;
using System.Net;
using Front.Helpers;
using Microsoft.AspNetCore.Http;

namespace Front.Controllers
{

	[Route("Account")]
	public class AccountDetailedController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly AccountService _accountService;
		private readonly MemberService _memberService;

		public AccountDetailedController(IAccountService accountService, IMemberService memberService, ILogger<HomeController> logger)
		{
			_logger = logger;
			_accountService = (AccountService)accountService;
			_memberService = (MemberService)memberService;
		}

		[Route("{id:int}")]
		public async Task<IActionResult> IndexAsync(int id)
		{
			try
			{
				AccountDetailedModel account = await _accountService.GetDetailedAccount(id);
				ViewBag.UpdatePath = AccountPaths.UpdateAccount;
				return View(account);
			}
			catch (Exception ex)
			{
				AccountDetailedModel account = new AccountDetailedModel();
				return View(account);
			}
		}

		[HttpPost]
		[Route("Save")]
		public async Task<IActionResult> Save([FromBody] AccountUpdateModel account)
		{
			return await _accountService.UpdateAccount(account);
		}

		[HttpPost]
		[Route("AddMember")]
		public async Task<IActionResult> AddMember([FromBody] MemberModel member, [FromQuery] int accountId)
		{
			return await _memberService.AddMember(member, accountId);
		}

		[HttpPost]
		[Route("DeleteMember")]
		public async Task<IActionResult> DeleteMember(int Id)
		{
			return await _memberService.DeleteMember(Id);
		}

		[HttpPost]
		[Route("DeleteAccount")]
		public async Task<IActionResult> DeleteAccount(int Id)
		{
			return await _accountService.DeleteAccount(Id);
		}
	}
}