using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Front.BaseClasses
{
	public abstract class MemberBase
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), JsonIgnore]
		public int Id { get; set; }

		[Required]
		public string Surename { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public string Patronymic { get; set; }
	}
}