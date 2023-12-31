﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System;
using AccountLibrary.Models;
using AccountLibrary.Validators;

namespace AccountLibrary.BaseClasses
{
	public abstract class AccountBase
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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