using System.Collections.Generic;
using System.Text.Json.Serialization;
using AccountLibrary.BaseClasses;

namespace AccountLibrary.Models
{
	public class AccountModel : AccountBase
	{
		[JsonIgnore]
		public List<MemberAccountRelation> AccountRelations { get; set; } = new List<MemberAccountRelation>();
	}
}
