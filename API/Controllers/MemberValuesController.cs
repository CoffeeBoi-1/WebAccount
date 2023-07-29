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

		/*[HttpGet]
		public IActionResult Get(int? Id)
		{
			using (AccountContext db = new AccountContext())
			{
				if (Id == null) return new ObjectResult(db.Members.ToArray());
				else return new ObjectResult(db.Members.Where(c => c.Id == Id).FirstOrDefault());
			}
		}*/

		[Route("[action]")]
		[HttpPost]
		public async Task<IActionResult> CreateMember(MemberModel member)
		{
			try
			{
				_db.Members.Add(member);
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
