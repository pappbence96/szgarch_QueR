using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QueR.BLL.Services.Company;

namespace QueR.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<ActionResult> AssingAdminToCompany(int companyId, int adminId)
        {
            await companyService.AssignAdminToCompany(companyId, adminId);
            return Ok();
        }
    }
}
