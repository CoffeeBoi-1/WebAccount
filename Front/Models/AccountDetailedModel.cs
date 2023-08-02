using Front.BaseClasses;
using System.Collections.Generic;

namespace Front.Models
{
	public class AccountDetailedModel : AccountBase
	{
		public List<MemberBase> Members { get; set; } = new List<MemberBase>();

        public AccountDetailedModel()
        {
        }
	}
}
