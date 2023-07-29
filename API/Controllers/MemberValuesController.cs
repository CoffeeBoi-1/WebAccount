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
		private readonly ILogger<MemberValuesController> _logger;
		private readonly IConfiguration _configuration;

		public MemberValuesController(ILogger<MemberValuesController> logger, IConfiguration configuration)
		{
			_logger = logger;
			_configuration = configuration;
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
				using (AccountContext db = new AccountContext())
				{
					db.Members.Add(member);
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
