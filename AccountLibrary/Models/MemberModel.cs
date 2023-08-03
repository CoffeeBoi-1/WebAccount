using System.Collections.Generic;
using System.Text.Json.Serialization;
using AccountLibrary.BaseClasses;

namespace AccountLibrary.Models
{
	public class MemberModel : MemberBase
	{
		[JsonIgnore]
		public List<MemberAccountRelation> MemberRelations { get; set; } = new List<MemberAccountRelation>();
	}
}
