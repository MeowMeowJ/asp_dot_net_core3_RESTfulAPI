using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Routine.Api.DtoParameters
{
    public class CompanyDtoParameters: IValidatableObject
    {
        private const int MaxPageSize = 20;
        public string CompanyName { get; set; }

        public string SearchTerm { get; set; }

        public int PageNumber { get; set; } = 1;

        private int _pageSize = 5;

        public string OrderBy { get; set; } = "CompanyName";

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (PageNumber <= 0)
            {
                yield return new ValidationResult("页数必须大于0", new []{nameof(PageNumber)});
            }

            if (PageSize < 0)
            {
                yield return new ValidationResult("每页的数据条数不能小于0", new []{nameof(PageSize)});
            }
        }
    }
}
