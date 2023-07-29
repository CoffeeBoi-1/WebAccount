using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using API.Validators;
using System.Text.Json.Serialization;

namespace API.Models
{
	public class AccountModel
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), JsonIgnore]
		public int Id { get; set; }

		[Required]
		public string AccountNumber { get; set; }

		[Required]
		public DateTime StartDate { get; set; }

		[Required, CompareGreater(nameof(StartDate), ErrorMessage = "Дата окончания должна быть после даты начала")]
		public DateTime EndDate { get; set; }

		[Required]
		public string Address { get; set; }

		[Required]
		public float RoomArea { get; set; }

		[JsonIgnore]
		public List<MemberAccountRelation> AccountRelations { get; set; } = new List<MemberAccountRelation>();
	}
}
