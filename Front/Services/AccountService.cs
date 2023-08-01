using Front.Services.Interfaces;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Front.BaseClasses;
using Front.Helpers;
using Front.Models;
using Microsoft.AspNetCore.WebUtilities;
using System.Collections;

namespace Front.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _client;
        public const string BasePath = "AccountValues";

        public AccountService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<AccountBase>> GetAccounts(int page)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, BasePath);
            
            Dictionary<string, string> query = new Dictionary<string, string>
            {
                { "pageNumber", "1" }
            };

            HttpResponseMessage response = await _client.GetAsync(QueryHelpers.AddQueryString(BasePath, query));

            return await response.ReadContentAsync<List<AccountBase>>();
        }
    }
}