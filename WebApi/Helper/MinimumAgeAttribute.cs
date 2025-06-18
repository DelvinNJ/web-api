using System.ComponentModel.DataAnnotations;

namespace WebApi.Helper
{
    public class MinimumAgeAttribute(int minimumAge) : ValidationAttribute
    {
        private readonly int _minimumAge = minimumAge;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dateOfBirth)
            {
                int age = DateTime.Today.Year - dateOfBirth.Year;
                if (dateOfBirth > DateTime.Today.AddYears(-age))
                {
                    age--;
                }

                if (age < _minimumAge)
                {
                    return new ValidationResult($"Age must be at least {_minimumAge}.");
                }
            }
            else if (value is null)
            {
                return ValidationResult.Success; // Or handle nulls as needed
            }
            else
            {
                return new ValidationResult("Invalid input type. Date of birth is required.");
            }

            return ValidationResult.Success;
        }
    }
}
