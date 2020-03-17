using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Routine.Api.Models;
using Routine.Api.Services;

namespace Routine.Api.Controllers
{
    [ApiController]
    [Route("api/companies")]
    // [Route("api/[controller]")]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;

        public CompaniesController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository ?? 
                                 throw new ArgumentNullException(nameof(companyRepository));
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            var companies = await _companyRepository.GetCompaniesAsync();

            var companyDtos = new List<CompanyDto>();

            foreach (var company in companies)
            {
                companyDtos.Add(new CompanyDto
                {
                    Id = company.Id,
                    Name = company.Name
                });
            }

            return Ok(companyDtos);
        }

        [HttpGet("{companyId}")] // api/companies/123
        // [Route("{companyId}")]
        public async Task<IActionResult> GetCompany(Guid companyId)
        {
            var company = await _companyRepository.GetCompanyAsync(companyId);
            if (company == null)
            {
                return NotFound();
            }

            return Ok(company);
        }
    }
}
