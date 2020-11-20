using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QueR.Application.DTOs;
using QueR.BLL.Services.Company;

namespace QueR.Application.Controllers.Backoffice
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "operator")]
    [ApiExplorerSettings(GroupName = "backoffice")]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService companyService;

        public CompaniesController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateCompany([FromBody] CompanyModel model)
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
        public ActionResult<IEnumerable<CompanyDto>> GetCompanies()
        {
            var companies = companyService.GetCompanies();
            return Ok(companies.Select(c => new CompanyDto
            {
                Name = c.Name,
                Address = c.MailingAddress,
                Id = c.Id,
                AdminName = c.Administrator?.UserName ?? "-",
                NumberOfSites = c.Sites.Count
            }));
        }
    }
}
