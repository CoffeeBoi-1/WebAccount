using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountLibrary.Models
{
	[NotMapped]
	public class FilterModel
	{
		public bool IsNotEmptyMembers { get; set; }
		public DateTime? CertainDate { get; set; } = null;
		public string? Surename { get; set; } = null;
		public string? Name { get; set; } = null;
		public string? Patronymic { get; set; } = null;
		public AddressModel? Address { get; set; } = null;
		public float? RoomArea { get; set; } = null;
		public string? AccountNumber { get; set; } = null;
	}
}