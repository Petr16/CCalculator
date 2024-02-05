using System.ComponentModel.DataAnnotations;

namespace CCalculator
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class CustomValidationAttribute:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is int intValue && intValue > 0)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage ?? "Значение должно быть больше 0.");
        }
    }
}
