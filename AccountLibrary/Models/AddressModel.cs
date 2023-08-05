using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AccountLibrary.Models
{
	public class AddressModel
	{
		[JsonIgnore]
		public int? Id { get; set; }

		[JsonIgnore]
		public AccountModel? Account { get; set; }

		[Required]
		public string? Street { get; set; }

		[Required]
		public string? House { get; set; }
		
		public string? Apartment { get; set; }
	}
}