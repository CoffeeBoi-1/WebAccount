using API.BaseClasses;
using System.Collections.Generic;

namespace API.Models
{
	public class AccountDetailedModel : AccountBase
	{
		public List<MemberModel> Members { get; set; } = new List<MemberModel>();

		public AccountDetailedModel(AccountBase account) 
		{
			this.Id = account.Id;
			this.AccountNumber = account.AccountNumber;
			this.StartDate = account.StartDate;
			this.EndDate = account.EndDate;
			this.Address = account.Address;
			this.RoomArea = account.RoomArea;
		}
	}
}
