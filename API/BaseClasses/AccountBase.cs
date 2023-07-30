using API.Validators;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System;
using API.Models;

namespace API.BaseClasses
{
	public abstract class AccountBase
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), JsonIgnore]
		public int Id { get; set; }

		[Required]
		public string AccountNumber { get; set; }

		[Required]
		public DateTime StartDate { get; set; }

		[Required, CompareGreater(nameof(StartDate), ErrorMessage = "Дата окончания должна быть после даты начала")]
		public DateTime EndDate { get; set; }

		[JsonIgnore]
		public int AddressId { get; set; }

		[Required]
		public AddressModel Address { get; set; } = new AddressModel();

		[Required]
		public float RoomArea { get; set; }
	}
}