using Front.Services.Interfaces;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Front.Helpers;
using Microsoft.AspNetCore.WebUtilities;
using AccountLibrary.Models;

namespace Front.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _client;

        public AccountService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<AccountModel>> GetAccounts(int page)
        {
            Dictionary<string, string> query = new Dictionary<string, string>
            {
                { "pageNumber", page.ToString() }
            };

            HttpResponseMessage response = await _client.GetAsync(QueryHelpers.AddQueryString(AccountPaths.BasePath, query));

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
    }
}