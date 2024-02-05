using System.ComponentModel.DataAnnotations;

namespace CCalculator
{
    public class IntegerValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (IsInteger(value))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage ?? "Значение должно быть целым числом.");
        }

        private bool IsInteger(object value)
        {
            if (value == null)
            {
                return false;
            }

            if (value is int intValue)
            {
                return Math.Abs(intValue % 1) < double.Epsilon;
            }

            return false;
        }
    }
}
