using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API.Models
{
	public class MemberModel
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), JsonIgnore]
		public int Id { get; set; }

		[Required]
		public string Surename { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public string Patronymic { get; set; }

		[JsonIgnore]
		public List<MemberAccountRelation> MemberRelations { get; set; } = new List<MemberAccountRelation>();
	}
}
