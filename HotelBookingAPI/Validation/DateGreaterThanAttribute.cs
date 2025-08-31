using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.Validation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DateGreaterThanAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public DateGreaterThanAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var currentValue = (DateOnly?)value;
            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (property == null)
            {
                var errorMessage = "Unknown property " + _comparisonProperty;
                return new ValidationResult(errorMessage);
            }

            var comparisonValue = (DateOnly?)property.GetValue(validationContext.ObjectInstance);

            if (currentValue.HasValue && comparisonValue.HasValue && currentValue <= comparisonValue)
            {
                var errorMessage = "Check out cannot be before check in.";
                return new ValidationResult(errorMessage);
            }

            return ValidationResult.Success!;
        }
    }
}