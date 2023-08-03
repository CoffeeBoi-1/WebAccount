using System.ComponentModel.DataAnnotations;
using System;
using System.Reflection;

namespace AccountLibrary.Validators
{
	public class CompareGreaterAttribute : ValidationAttribute
	{
		private readonly string _comparisonProperty;

		public CompareGreaterAttribute(string comparisonProperty)
		{
			_comparisonProperty = comparisonProperty;
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			DateTime endDate = (DateTime)value;
			PropertyInfo property = validationContext.ObjectType.GetProperty(_comparisonProperty);
			DateTime startDate = (DateTime)property.GetValue(validationContext.ObjectInstance, null);

			if (startDate >= endDate)
			{
				return new ValidationResult(ErrorMessage ?? "");
			}

			return ValidationResult.Success;
		}
	}
}
