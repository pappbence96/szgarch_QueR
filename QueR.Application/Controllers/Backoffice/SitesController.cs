using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueR.Application.Middlewares.ExceptionHandling;
using QueR.BLL.Services.Site;
using QueR.BLL.Services.Site.DTOs;
using QueR.BLL.Services.User.DTOs;

namespace QueR.Application.Controllers.Backoffice
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "administrator")]
    [ApiExplorerSettings(GroupName = "backoffice")]
    [ProducesErrorResponseType(typeof(ErrorDetails))]
    public class SitesController : ControllerBase
    {
        private readonly ISiteService siteService;

        public SitesController(ISiteService siteService)
        {
            this.siteService = siteService;
        }

        [HttpGet("{siteId}/manager/{managerId}")]
        public async Task<ActionResult> AssignManagerToSite(int siteId, int managerId)
        {
            await siteService.AssignManagerToSite(siteId, managerId);
            return Ok();
        }

        [HttpPost]
        [ProducesDefaultResponseType(typeof(SiteDto))]
        public async Task<ActionResult> CreateSite([FromBody] SiteModel model)
        {
            return Ok(await siteService.CreateSite(model));
        }

        [HttpGet("{siteId}/workers/{employeeId}")]
        public async Task<ActionResult> AssignWorkerToSite(int siteId, int employeeId)
        {
            await siteService.AssignEmployeeToSite(siteId, employeeId);
            return Ok();
        }

        [HttpGet]
        [ProducesDefaultResponseType(typeof(IEnumerable<SiteDto>))]
        public async Task<ActionResult<IEnumerable<SiteDto>>> GetSites()
        {
            return Ok(await siteService.GetSites());
        }

        [HttpGet("{siteId}")]
        [ProducesDefaultResponseType(typeof(IEnumerable<ApplicationUserDto>))]
        public async Task<ActionResult<IEnumerable<ApplicationUserDto>>> GetEmployeesOfSite(int siteId)
        {
            return Ok(await siteService.GetEmployeesOfSite(siteId));
        }

        [HttpDelete("{siteId}/manager")]
        public async Task<ActionResult> RemoveManagerFromSite(int siteId)
        {
            await siteService.RemoveManagerFromSite(siteId);
            return Ok();
        }

        [HttpDelete("{siteId}/workers/{employeeId}")]
        public async Task<ActionResult> RemoveEmployeeOfSite(int siteId, int employeeId)
        {
            await siteService.RemoveEmployeeFromSite(siteId, employeeId);
            return Ok();
        }

        [HttpPut("{siteId}")]
        public async Task<ActionResult> UpdateSite(int siteId, [FromBody] SiteModel model)
        {
            await siteService.UpdateSite(siteId, model);
            return Ok();
        }

    }
}
