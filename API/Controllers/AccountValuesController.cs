using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace API.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class AccountValuesController : ControllerBase
	{
		private readonly ILogger<AccountValuesController> _logger;

		public AccountValuesController(ILogger<AccountValuesController> logger)
		{
			_logger = logger;
		}

		[HttpGet]
		public IActionResult Get()
		{
			using (AccountContext db = new AccountContext())
			{
				return new ObjectResult(db.Accounts.ToArray());
			}
		}

		[Route("[action]")]
		[HttpPost]
		public async Task<IActionResult> AddAccount(AccountModel account)
		{
			try
			{
				using (AccountContext db = new AccountContext())
				{
					db.Accounts.Add(account);
					await db.SaveChangesAsync();
				}

				return Ok();
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Ошибка при сохранении данных: {ex.Message}");
			}
		}

		[Route("[action]")]
		[HttpPost]
		public async Task<IActionResult> AddMember(int accountId, int memberId)
		{
			try
			{
				using (AccountContext db = new AccountContext())
				{
					AccountModel account = db.Accounts.Where(c => c.Id == accountId).FirstOrDefault();
					MemberModel member = db.Members.Where(c => c.Id == memberId).FirstOrDefault();
					MemberAccountRelation relation = new MemberAccountRelation()
					{
						AccountId = account.Id,
						MemberId = member.Id
					};

					account.AccountRelations.Add(relation);
					member.MemberRelations.Add(relation);

					await db.SaveChangesAsync();
				}

				return Ok();
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Ошибка при сохранении данных: {ex.Message}");
			}
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteAccount(int Id)
		{
			try
			{
				using (AccountContext db = new AccountContext())
				{
					AccountModel accountToDelete = db.Accounts.FirstOrDefault(a => a.Id == Id);
					db.Accounts.Remove(accountToDelete);
					await db.SaveChangesAsync();
				}

				return Ok();
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Ошибка при сохранении данных: {ex.Message}");
			}
		}
	}
}
