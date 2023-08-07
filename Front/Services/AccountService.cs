using Front.Services.Interfaces;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Front.Helpers;
using Microsoft.AspNetCore.WebUtilities;
using AccountLibrary.Models;
using System.Text.Json;
using System.Net.Http.Json;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Front.Services
{
	public class AccountService : IAccountService
	{
		private readonly HttpClient _client;

		public AccountService(HttpClient client)
		{
			_client = client ?? throw new ArgumentNullException(nameof(client));
		}

		public async Task<IEnumerable<AccountModel>> GetAccounts(int page, int pageSize)
		{
			Dictionary<string, string> query = new Dictionary<string, string>
			{
				{ "pageNumber", page.ToString() },
				{ "pageSize", pageSize.ToString() }
			};

			HttpResponseMessage response = await _client.GetAsync(QueryHelpers.AddQueryString(AccountPaths.BasePath, query));

			return await response.ReadContentAsync<List<AccountModel>>();
		}

		public async Task<IEnumerable<AccountModel>> GetFilteredAccounts(int page, int pageSize, FilterModel filter)
		{
			Dictionary<string, string> query = new Dictionary<string, string>
			{
				{ "pageNumber", page.ToString() },
				{ "pageSize", pageSize.ToString() }
			};

			if (filter.Address != null && (filter.Address.Street == null && filter.Address.House == null && filter.Address.Apartment == null))
				filter.Address = null;

			JsonContent content = JsonContent.Create(filter);
			HttpResponseMessage response = await _client.PostAsync(QueryHelpers.AddQueryString(AccountPaths.GetFilteredAccountsPath, query), content);

			if (response.StatusCode != HttpStatusCode.OK)
			{
				List<AccountModel> errorModels = new List<AccountModel>
				{
					new AccountModel() { AccountNumber = "Ошибка при поиске ЛС" }
				};

				return errorModels;
			}

			return await response.ReadContentAsync<List<AccountModel>>();
		}

		public async Task<AccountDetailedModel> GetDetailedAccount(int Id)
		{
			Dictionary<string, string> query = new Dictionary<string, string>
			{
				{ "Id", Id.ToString() }
			};

			HttpResponseMessage response = await _client.GetAsync(QueryHelpers.AddQueryString(AccountPaths.GetDetailedAccountPath, query));

			return await response.ReadContentAsync<AccountDetailedModel>();
		}

		public async Task<IActionResult> CreateAccount(AccountModel account)
		{
			JsonContent content = JsonContent.Create(account);
			HttpResponseMessage response = await _client.PostAsync(AccountPaths.CreateAccount, content);

			if (response.StatusCode != HttpStatusCode.OK)
			{
				string res = await response.Content.ReadAsStringAsync();
				return new BadRequestObjectResult(res);
			}

			return new OkResult();
		}

		public async Task<IActionResult> UpdateAccount(AccountUpdateModel accountUpdate)
		{
			JsonContent content = JsonContent.Create(accountUpdate);
			HttpResponseMessage response = await _client.PostAsync(AccountPaths.UpdateAccount, content);

			if (response.StatusCode != HttpStatusCode.OK)
			{
				string res = await response.Content.ReadAsStringAsync();
				return new BadRequestObjectResult(res);
			}

			return new OkResult();
		}

		public async Task<IActionResult> DeleteAccount(int accountId)
		{
			Dictionary<string, string> query = new Dictionary<string, string>
			{
				{ "Id", accountId.ToString() }
			};

			HttpResponseMessage response = await _client.DeleteAsync(QueryHelpers.AddQueryString(AccountPaths.DeleteAccount, query));

			if (response.StatusCode != HttpStatusCode.OK)
			{
				string res = await response.Content.ReadAsStringAsync();
				return new BadRequestObjectResult(res);
			}

			return new OkResult();
		}
	}
}