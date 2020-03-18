using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Routine.Api.Entities;
using Routine.Api.ValidationAttributes;

namespace Routine.Api.Models
{
    [EmployeeNoMustDifferentFromFirstName(ErrorMessage = "员工编号不可以与名相同！！")]
    public abstract class EmployeeAddOrUpdateDto: IValidatableObject
    {
        [Display(Name = "员工号")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "{0}的长度是{1}")]
        public string EmployeeNo { get; set; }

        [Display(Name = "名")]
        [Required(ErrorMessage = "{0}是必填项")]
        [MaxLength(50, ErrorMessage = "{0}的长度不能超过{1}")]
        public string FirstName { get; set; }

        [Display(Name = "姓"), Required(ErrorMessage = "{0}是必填项"), MaxLength(50, ErrorMessage = "{0}的长度不能超过{1}")]
        public string LastName { get; set; }

        [Display(Name = "性别")]
        public Gender Gender { get; set; }

        [Display(Name = "出生日期")]
        public DateTime DateOfBirth { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (FirstName == LastName)
            {
                yield return new ValidationResult("姓和名不能相同", new[] { nameof(FirstName), nameof(LastName) });
            }

            if (!(Gender == Gender.男 || Gender == Gender.女))
            {
                yield return new ValidationResult("性别只能为男（1）或女（2）", new []{ nameof(Gender) });
            }

            // DateTime t0 = new DateTime(1900,01,01);
            // DateTime t1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            if (DateTime.Compare(DateOfBirth, new DateTime(1900, 01, 01)) < 0 || 
                DateTime.Compare(DateOfBirth, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)) > 0)
            {
                yield return new ValidationResult("出生日期不合法，合法区间：（1900-01-01到当前日期）", new []{nameof(DateOfBirth)});
            }
        }
    }
}
