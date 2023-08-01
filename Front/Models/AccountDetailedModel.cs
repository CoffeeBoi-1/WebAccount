using Front.BaseClasses;
using System.Collections.Generic;

namespace Front.Models
{
	public class AccountDetailedModel : AccountBase
	{
		public List<MemberBase> Members { get; set; } = new List<MemberBase>();

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
