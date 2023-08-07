using AccountLibrary.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
		public IActionResult Get([FromQuery] int pageNumber, [FromQuery] int pageSize = 10)
		{
			try
			{
				pageSize = Math.Clamp(Math.Abs(pageSize), 1, 100);

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
				return StatusCode(500, $"Ошибка при выполнении запроса: {ex.Message}");
			}
		}

		[Route("[action]")]
		[HttpGet]
		public IActionResult GetDetailed([FromQuery] int Id)
		{
			try
			{
				AccountModel account = _db.Accounts.SingleOrDefault(a => a.Id == Id);
				AddressModel address = _db.Addresses.SingleOrDefault(a => a.Id == Id);
				List<MemberModel> members = _db.MemberAccountRelations
					.Where(rel => rel.AccountId == Id)
					.Select(rel => rel.Member)
					.ToList();

				AccountDetailedModel detailedAccount = new AccountDetailedModel(account) { Members = members, Address = address };

				return new ObjectResult(detailedAccount);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Ошибка при выполнении запроса: {ex.Message}");
			}
		}

		[Route("[action]")]
		[HttpPost]
		public IActionResult GetFiltered([FromQuery] int pageNumber, FilterModel filter, [FromQuery] int pageSize = 10)
		{
			try
			{
				pageSize = Math.Clamp(Math.Abs(pageSize), 1, 100);

				int totalPages = (int)Math.Ceiling((double)_db.Accounts.Count() / pageSize);
				totalPages = Math.Clamp(totalPages, 1, pageSize);
				pageNumber = Math.Clamp(pageNumber, 1, totalPages);
				int itemsToSkip = (pageNumber - 1) * pageSize;

				IQueryable<AccountModel> accounts = _db.Accounts;
				FilterService filterService = new FilterService(filter, accounts);

				if (filter.IsNotEmptyMembers)
				{
					filterService.FilterByNotEmptyMembers();

					if (filter.Surename != null) filterService.FilterBySureaname();

					if (filter.Name != null) filterService.FilterByName();

					if (filter.Patronymic != null) filterService.FilterByPatronymic();
				}

				if (filter.CertainDate != null) filterService.FilterByCertainDate();

				if (filter.Address != null) filterService.FilterByAddress();

				if (filter.RoomArea != null) filterService.FilterByRoomArea();

				if (filter.AccountNumber != null) filterService.FilterByAccountNumber();

				IQueryable<AccountModel> query = filterService.GetAccounts();

				query = query.OrderBy(e => e.Id)
					.Skip(itemsToSkip)
					.Take(pageSize);

				return new ObjectResult(query);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Ошибка при выполнении запроса: {ex.Message}");
			}
		}

		[Route("[action]")]
		[HttpPost]
		public async Task<IActionResult> CreateAccount(AccountModel account)
		{
			try
			{
				account.Address.Street = account.Address.Street.ToLower();
				account.Address.House = account.Address.House.ToLower();
				account.Address.Apartment = account.Address.Apartment.ToLower();

				_db.Accounts.Add(account);
				await _db.SaveChangesAsync();

				return Ok();
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Ошибка при сохранении данных: {ex.Message}");
			}
		}

		[Route("[action]")]
		[HttpDelete]
		public async Task<IActionResult> DeleteAccount([FromQuery] int Id)
		{
			try
			{
				AccountModel accountToDelete = _db.Accounts.Where(a => a.Id == Id).FirstOrDefault();

				if (accountToDelete == null) throw new Exception("ЛС не найден");

				_db.Accounts.Remove(accountToDelete);
				await _db.SaveChangesAsync();

				return Ok();
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Ошибка при сохранении данных: {ex.Message}");
			}
		}

		[Route("[action]")]
		[HttpPost]
		public async Task<IActionResult> AddMember([FromQuery] int accountId, [FromQuery] int memberId)
		{
			try
			{
				AccountModel account = _db.Accounts.Where(c => c.Id == accountId).FirstOrDefault();
				MemberModel member = _db.Members.Where(c => c.Id == memberId).FirstOrDefault();
				MemberAccountRelation relation = new MemberAccountRelation()
				{
					AccountId = account.Id,
					MemberId = member.Id
				};

				account.AccountRelations.Add(relation);
				member.MemberRelations.Add(relation);

				await _db.SaveChangesAsync();

				return Ok();
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Ошибка при сохранении данных: {ex.Message}");
			}
		}

		[Route("[action]")]
		[HttpPost]
		public async Task<IActionResult> UpdateAccount(AccountUpdateModel update)
		{
			try
			{
				AccountModel account = _db.Accounts.FirstOrDefault(a => a.Id == update.Id);

				if (account == null) throw new Exception("ЛС не найден");

				if (update.StartDate != null)
				{
					if (update.StartDate.Value >= account.EndDate) throw new Exception("Дата начала должна быть меньше даты окончания");

					account.StartDate = update.StartDate.Value;
				}

				if (update.EndDate != null)
				{
					if (update.EndDate.Value <= account.StartDate) throw new Exception("Дата окончания должна быть больше даты начала");

					account.EndDate = update.EndDate.Value;
				}

				if (update.RoomArea != null)
				{
					if (update.RoomArea.Value <= 0) throw new Exception("Площадь должна быть больше нуля");

					account.RoomArea = update.RoomArea.Value;
				}

				await _db.SaveChangesAsync();

				return Ok();
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Ошибка при сохранении данных: {ex.Message}");
			}
		}
	}
}