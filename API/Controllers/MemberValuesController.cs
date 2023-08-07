using AccountLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace API.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class MemberValuesController : ControllerBase
	{
		private readonly AccountContext _db;
		private readonly IConfiguration _configuration;
		private readonly ILogger<MemberValuesController> _logger;

		public MemberValuesController(IConfiguration configuration, AccountContext db, ILogger<MemberValuesController> logger)
		{
			_configuration = configuration;
			_db = db;
			_logger = logger;
		}

		[Route("[action]")]
		[HttpPost]
		public async Task<IActionResult> CreateMember(MemberModel member)
		{
			try
			{
				member.Surename = member.Surename.ToLower();
				member.Name = member.Name.ToLower();
				member.Patronymic = member.Patronymic.ToLower();

				_db.Members.Add(member);
				await _db.SaveChangesAsync();

				return new ObjectResult(member.Id);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Ошибка при сохранении данных: {ex.Message}");
			}
		}

		[Route("[action]")]
		[HttpDelete]
		public async Task<IActionResult> DeleteMember(int Id)
		{
			try
			{
				MemberModel memberToDelete = _db.Members.Where(a => a.Id == Id).FirstOrDefault();

				if (memberToDelete == null) throw new Exception("Жилец не найден");

				_db.Members.Remove(memberToDelete);
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
