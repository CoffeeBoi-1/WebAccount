using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class AccountValuesController : ControllerBase
	{
		private readonly IConfiguration _configuration;
		private readonly AccountContext _db;
		private readonly ILogger<AccountValuesController> _logger;

		public AccountValuesController(IConfiguration configuration, AccountContext db, ILogger<AccountValuesController> logger)
		{
			_logger = logger;
			_configuration = configuration;
			_db = db;
		}

		[HttpGet]
		public IActionResult Get(int pageNumber)
		{
			try
			{
				int pageSize = _configuration.GetValue<int>("PageSize");

				int totalPages = (int)Math.Ceiling((double)_db.Accounts.Count() / pageSize);
				totalPages = Math.Clamp(totalPages, 1, pageSize);
				pageNumber = Math.Clamp(pageNumber, 1, totalPages);
				int itemsToSkip = (pageNumber - 1) * pageSize;

				IQueryable<AccountModel> query = _db.Accounts
					.OrderBy(e => e.Id)
					.Skip(itemsToSkip)
					.Take(pageSize);

				return new ObjectResult(query);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Ошибка при сохранении данных: {ex.Message}");
			}
		}

		[Route("[action]")]
		[HttpPost]
		public async Task<IActionResult> CreateAccount(AccountModel account)
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

		[Route("[action]")]
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
