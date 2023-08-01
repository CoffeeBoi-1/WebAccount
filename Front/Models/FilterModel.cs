using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Front.Models
{
	[NotMapped]
	public class FilterModel
	{
		public bool IsNotEmptyMembers { get; set; } = false;
		public DateTime? CertainDate { get; set; } = null;
		public string Surename { get; set; } = null;
		public string Name { get; set; } = null;
		public string Patronymic { get; set; } = null;
		public AddressModel Address { get; set; } = null;
	}
}