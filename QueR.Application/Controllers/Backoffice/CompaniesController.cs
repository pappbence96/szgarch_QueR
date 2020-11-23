using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QueR.Application.Middlewares.ExceptionHandling;
using QueR.BLL.Services.Company;
using QueR.BLL.Services.Company.DTOs;

namespace QueR.Application.Controllers.Backoffice
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "operator")]
    [ApiExplorerSettings(GroupName = "backoffice")]
    [ProducesErrorResponseType(typeof(ErrorDetails))]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService companyService;

        public CompaniesController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        [HttpPost]
        [ProducesDefaultResponseType(typeof(CompanyDto))]
        public async Task<ActionResult<CompanyDto>> CreateCompany([FromBody] CompanyModel model)
        {
            return Ok(await companyService.CreateCompany(model));
        }

        [HttpGet("{companyId}/admin/{adminId}")]
        public async Task<ActionResult> AssignAdminToCompany(int companyId, int adminId)
        {
            await companyService.AssignAdminToCompany(companyId, adminId);
            return Ok();
        }

        [HttpGet]
        [ProducesDefaultResponseType(typeof(IEnumerable<CompanyDto>))]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompanies()
        {
            return Ok(await companyService.GetCompanies());
        }

        [HttpDelete("{companyId}/admin")]
        public async Task<ActionResult> RemoveAdminOfCompany(int companyId)
        {
            await companyService.RemoveAdminOfCompany(companyId);
            return Ok();
        }

        [HttpPut("{companyId}")]
        public async Task<ActionResult> UpdateCompany(int companyId, [FromBody] CompanyModel model)
        {
            await companyService.UpdateCompany(companyId, model);
            return Ok();
        }
    }
}
