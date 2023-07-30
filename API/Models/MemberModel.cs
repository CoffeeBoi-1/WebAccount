using System.Collections.Generic;
using System.Text.Json.Serialization;
using API.BaseClasses;

namespace API.Models
{
	public class MemberModel : MemberBase
	{
		[JsonIgnore]
		public List<MemberAccountRelation> MemberRelations { get; set; } = new List<MemberAccountRelation>();
	}
}
