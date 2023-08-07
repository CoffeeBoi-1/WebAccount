using AccountLibrary.BaseClasses;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Front.Services.Interfaces
{
	public interface IMemberService
	{
		Task<IActionResult> AddMember(MemberBase member, int accountId);
		Task<IActionResult> DeleteMember(int memberId);
	}
}
