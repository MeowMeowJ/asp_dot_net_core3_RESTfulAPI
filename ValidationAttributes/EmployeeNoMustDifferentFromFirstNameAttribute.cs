using System.ComponentModel.DataAnnotations;
using Routine.Api.Models;

namespace Routine.Api.ValidationAttributes
{
    public class EmployeeNoMustDifferentFromFirstNameAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var addDto = (EmployeeAddDto) validationContext.ObjectInstance;

            if (addDto.EmployeeNo == addDto.FirstName)
            {
                return new ValidationResult("员工编号不可以等于名", new []{nameof(EmployeeAddDto)});
            }

            return ValidationResult.Success;
        }
    }
}
