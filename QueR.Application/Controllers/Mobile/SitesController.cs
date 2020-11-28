using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class SitesController : ControllerBase
    {
        private readonly ISiteService siteService;

        public SitesController(ISiteService siteService)
        {
            this.siteService = siteService;
        }

        [HttpGet("{companyId}")]
        [ProducesDefaultResponseType(typeof(IEnumerable<UserSiteDto>))]
        public async Task<ActionResult<IEnumerable<UserSiteDto>>> GetSitesOfCompanyForUser(int companyId)
        {
            return Ok(await siteService.GetSitesOfCompanyForUser(companyId));
        }
    }
}
