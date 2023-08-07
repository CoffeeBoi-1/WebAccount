using AccountLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Front.Services.Interfaces
{
	public interface IAccountService
	{
		Task<IEnumerable<AccountModel>> GetAccounts(int page, int pageSize);
		Task<AccountDetailedModel> GetDetailedAccount(int page);
		Task<IActionResult> UpdateAccount(AccountUpdateModel accountUpdate);
		Task<IActionResult> DeleteAccount(int accountId);
	}
}