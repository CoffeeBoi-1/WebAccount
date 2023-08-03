using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountLibrary.Models
{
	[NotMapped]
	public class AccountUpdateModel
	{
		public int Id { get; set; }

		public DateTime? StartDate { get; set; } = null;

		public DateTime? EndDate { get; set; } = null;

		public float? RoomArea { get; set; } = null;
	}
}
