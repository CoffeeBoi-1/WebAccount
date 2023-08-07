using Front.Helpers;
using Front.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.WebUtilities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Collections.Generic;
using AccountLibrary.BaseClasses;

namespace Front.Services
{
	public class MemberService : IMemberService
	{
		private readonly HttpClient _client;

		public MemberService(HttpClient client)
		{
			_client = client ?? throw new ArgumentNullException(nameof(client));
		}

		public async Task<IActionResult> AddMember(MemberBase member, int accountId)
		{
			JsonContent content = JsonContent.Create(member);
			HttpResponseMessage createMemberResponse = await _client.PostAsync(MemberPaths.CreateMember, content);

			string CMRes = await createMemberResponse.Content.ReadAsStringAsync();
			if (createMemberResponse.StatusCode != HttpStatusCode.OK) return new BadRequestObjectResult(CMRes);

			Dictionary<string, string> query = new Dictionary<string, string>
			{
				{ "accountId", accountId.ToString() },
				{ "memberId", CMRes }
			};
			HttpResponseMessage addMemberResponse = await _client.PostAsync(QueryHelpers.AddQueryString(AccountPaths.AddMemberAccount, query), null);

			if (addMemberResponse.StatusCode != HttpStatusCode.OK)
			{
				string res = await addMemberResponse.Content.ReadAsStringAsync();
				return new BadRequestObjectResult(res);
			}

			return new OkResult();
		}

		public async Task<IActionResult> DeleteMember(int Id)
		{
			Dictionary<string, string> query = new Dictionary<string, string>
			{
				{ "Id", Id.ToString() }
			};

			HttpResponseMessage response = await _client.DeleteAsync(QueryHelpers.AddQueryString(MemberPaths.DeleteMember, query));

			if (response.StatusCode != HttpStatusCode.OK)
			{
				string res = await response.Content.ReadAsStringAsync();
				return new BadRequestObjectResult(res);
			}

			return new OkResult();
		}
	}
}
