using System.Collections.Generic;
using System.Text.Json.Serialization;
using API.BaseClasses;

namespace API.Models
{
	public class AccountModel : AccountBase
	{
		[JsonIgnore]
		public List<MemberAccountRelation> AccountRelations { get; set; } = new List<MemberAccountRelation>();
	}
}
