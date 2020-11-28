using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QueR.Application.Middlewares.ExceptionHandling;
using QueR.BLL.Services.Company;
using QueR.BLL.Services.Company.DTOs;
using QueR.BLL.Services.QueueType;
using QueR.BLL.Services.QueueType.DTOs;
using QueR.BLL.Services.Site;
using QueR.BLL.Services.Site.DTOs;
using QueR.BLL.Services.User;
using QueR.BLL.Services.User.DTOs;

namespace QueR.Application.Controllers.Backoffice
{
    [Route("api/backoffice/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "backoffice")]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService companyService;
        private readonly IUserService userService;
        private readonly IQueueTypeService queueTypeService;
        private readonly ISiteService siteService;

        public CompaniesController(ICompanyService companyService, IUserService userService, IQueueTypeService queueTypeService, ISiteService siteService)
        {
            this.companyService = companyService;
            this.userService = userService;
            this.queueTypeService = queueTypeService;
            this.siteService = siteService;
        }

        [HttpPost]
        [Authorize(Roles = "operator")]
        [ProducesDefaultResponseType(typeof(CompanyDto))]
        public async Task<ActionResult<CompanyDto>> CreateCompany([FromBody] CompanyModel model)
        {
            return Ok(await companyService.CreateCompany(model));
        }

        [HttpGet("{companyId}/admin/{adminId}")]
        [Authorize(Roles = "operator")]
        public async Task<ActionResult> AssignAdminToCompany(int companyId, int adminId)
        {
            await companyService.AssignAdminToCompany(companyId, adminId);
            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = "operator")]
        [ProducesDefaultResponseType(typeof(IEnumerable<CompanyDto>))]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompanies()
        {
            return Ok(await companyService.GetCompanies());
        }

        [HttpDelete("{companyId}/admin")]
        [Authorize(Roles = "operator")]
        public async Task<ActionResult> RemoveAdminOfCompany(int companyId)
        {
            await companyService.RemoveAdminOfCompany(companyId);
            return Ok();
        }

        [HttpPut("{companyId}")]
        [Authorize(Roles = "operator")]
        public async Task<ActionResult> UpdateCompany(int companyId, [FromBody] CompanyModel model)
        {
            await companyService.UpdateCompany(companyId, model);
            return Ok();
        }

        [HttpGet("current/employees")]
        [Authorize(Roles = "administrator")]
        [ProducesDefaultResponseType(typeof(IEnumerable<EmployeeDto>))]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployeesOfOwnCompany()
        {
            return Ok(await userService.GetEmployeesOfOwnCompany());
        }

        [HttpGet("current/managers")]
        [Authorize(Roles = "administrator")]
        [ProducesDefaultResponseType(typeof(IEnumerable<ManagerDto>))]
        public async Task<ActionResult<IEnumerable<ManagerDto>>> GetManagersOfOwnCompany()
        {
            return Ok(await userService.GetManagersOfOwnCompany());
        }

        [HttpGet("current/queuetypes")]
        [Authorize(Roles = "administrator,manager")]
        [ProducesDefaultResponseType(typeof(IEnumerable<QueueTypeDto>))]
        public async Task<ActionResult<IEnumerable<QueueTypeDto>>> GetQueueTypes()
        {
            return Ok(await queueTypeService.GetQueueTypesOfOwnCompany());
        }

        [HttpGet("current/sites")]
        [ProducesDefaultResponseType(typeof(IEnumerable<SiteDto>))]
        [Authorize(Roles = "administrator")]
        public async Task<ActionResult<IEnumerable<SiteDto>>> GetSitesForOwnCompany()
        {
            return Ok(await siteService.GetSitesForOwnCompany());
        }
    }
}
