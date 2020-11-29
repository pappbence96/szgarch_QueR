using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QueR.Application.Middlewares.ExceptionHandling;
using QueR.BLL.Services.Company;
using QueR.BLL.Services.Company.DTOs;
using QueR.BLL.Services.Site;
using QueR.BLL.Services.Site.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueR.Application.Controllers.Mobile
{
    [Route("api/mobile/[controller]")]
    [Authorize(Roles = "user")]
    [ApiExplorerSettings(GroupName = "mobile")]
    [ApiController]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService companyService;
        private readonly ISiteService siteService;

        public CompaniesController(ICompanyService companyService, ISiteService siteService)
        {
            this.companyService = companyService;
            this.siteService = siteService;
        }

        [HttpGet]
        [ProducesDefaultResponseType(typeof(IEnumerable<UserCompanyDto>))]
        public async Task<ActionResult<IEnumerable<UserCompanyDto>>> GetCompaniesForUser()
        {
            return Ok(await companyService.GetCompaniesForUser());
        }

        [HttpGet("{companyId}/sites")]
        [ProducesDefaultResponseType(typeof(IEnumerable<UserSiteDto>))]
        public async Task<ActionResult<IEnumerable<UserSiteDto>>> GetSitesOfCompanyForUser(int companyId)
        {
            return Ok(await siteService.GetSitesOfCompanyForUser(companyId));
        }
    }
}
