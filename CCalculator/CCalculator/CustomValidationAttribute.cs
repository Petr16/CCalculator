using NuGet.Packaging.Signing;
using System.ComponentModel.DataAnnotations;

namespace CCalculator
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class CustomValidationAttribute:ValidationAttribute
    {
        private decimal valueDecimal {  get; set; }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            /*if (validationContext.DisplayName=="LoanTerm" && value.GetType().Name!="UInt16") { 
                valueDecimal = Convert.ToDecimal(value);
            }
            else
            {
                valueDecimal = value;
            }*/
            valueDecimal = Convert.ToDecimal(value);
            var t1 = value.GetType().Name;
            if (valueDecimal <= 0)
            {
                return new ValidationResult(ErrorMessage ?? "Значение должно быть больше 0.");
            }
            else if ((validationContext.DisplayName == "LoanTerm" || validationContext.DisplayName == "StepPayment") && !IsInteger(valueDecimal)) //ErrorMessage== "Значение должно быть целым и больше 0."
            {
                //получается, что это условие будет игнорироваться,
                //т.к. сам класс валидации не будет использован, если в ushort-свойство захочет упасть значение с плавающей запятой.
                return new ValidationResult(ErrorMessage ?? "Значение должно быть целым.");
            }
            else
            {
                return ValidationResult.Success;
            }

        }

        private bool IsInteger(decimal value)
        {
            var t2 = value.GetType().Name;
            if (Math.Abs(value % 1) == 0)
            {
                return true;
            }

            return false;
        }
    }
}
