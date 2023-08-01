using System.ComponentModel.DataAnnotations;

namespace Front.Models
{
	public class AddressModel
	{
		[Required]
		public string Street { get; set; }

		[Required]
		public string House { get; set; }

		[Required]
		public string Apartment { get; set; }
	}
}