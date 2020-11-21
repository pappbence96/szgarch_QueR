using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QueR.BLL.Services.Site;

namespace QueR.Application.Controllers.Backoffice
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "administrator")]
    [ApiExplorerSettings(GroupName = "backoffice")]
    public class SitesController : ControllerBase
    {
        private readonly ISiteService siteService;

        public SitesController(ISiteService siteService)
        {
            this.siteService = siteService;
        }

        [HttpGet("{siteId}/admin/{managerId}")]
        public async Task<ActionResult> AssignManagerToSite(int siteId, int managerId)
        {
            await siteService.AssignManagerToSite(siteId, managerId);
            return Ok();
        }
    }
}
