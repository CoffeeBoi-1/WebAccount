﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Models
{
	public class AddressModel
	{
		[JsonIgnore]
		public int Id { get; set; }

		[JsonIgnore]
		public AccountModel Account { get; set; }

		[Required]
		public string Street { get; set; }

		[Required]
		public string House { get; set; }

		[Required]
		public string Apartment { get; set; }
	}
}