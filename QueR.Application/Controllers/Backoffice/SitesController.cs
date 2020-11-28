using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QueR.Application.Middlewares.ExceptionHandling;
using QueR.BLL.Services.Queue;
using QueR.BLL.Services.Queue.DTOs;
using QueR.BLL.Services.Site;
using QueR.BLL.Services.Site.DTOs;
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
    public class SitesController : ControllerBase
    {
        private readonly ISiteService siteService;
        private readonly IQueueService queueService;

        public SitesController(ISiteService siteService, IQueueService queueService)
        {
            this.siteService = siteService;
            this.queueService = queueService;
        }

        [HttpGet("{siteId}/manager/{managerId}")]
        [Authorize(Roles = "administrator")]
        public async Task<ActionResult> AssignManagerToSite(int siteId, int managerId)
        {
            await siteService.AssignManagerToSite(siteId, managerId);
            return Ok();
        }

        [HttpPost]
        [ProducesDefaultResponseType(typeof(SiteDto))]
        [Authorize(Roles = "administrator")]
        public async Task<ActionResult> CreateSite([FromBody] SiteModel model)
        {
            return Ok(await siteService.CreateSite(model));
        }

        [HttpGet("{siteId}/employees/{employeeId}")]
        [Authorize(Roles = "administrator")]
        public async Task<ActionResult> AssignWorkerToSite(int siteId, int employeeId)
        {
            await siteService.AssignEmployeeToSite(siteId, employeeId);
            return Ok();
        }

        [HttpGet("{siteId}/employees")]
        [ProducesDefaultResponseType(typeof(IEnumerable<EmployeeDto>))]
        [Authorize(Roles = "administrator")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployeesOfSite(int siteId)
        {
            return Ok(await siteService.GetEmployeesOfSite(siteId));
        }

        [HttpDelete("{siteId}/manager")]
        [Authorize(Roles = "administrator")]
        public async Task<ActionResult> RemoveManagerFromSite(int siteId)
        {
            await siteService.RemoveManagerFromSite(siteId);
            return Ok();
        }

        [HttpDelete("{siteId}/employees/{employeeId}")]
        [Authorize(Roles = "administrator")]
        public async Task<ActionResult> RemoveEmployeeOfSite(int siteId, int employeeId)
        {
            await siteService.RemoveEmployeeFromSite(siteId, employeeId);
            return Ok();
        }

        [HttpPut("{siteId}")]
        [Authorize(Roles = "administrator")]
        public async Task<ActionResult> UpdateSite(int siteId, [FromBody] SiteModel model)
        {
            await siteService.UpdateSite(siteId, model);
            return Ok();
        }

        [HttpGet("current/employees")]
        [ProducesDefaultResponseType(typeof(IEnumerable<EmployeeDto>))]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetOwnEmployees()
        {
            return Ok(await siteService.GetOwnEmployees());
        }

        [Authorize(Roles = "manager")]
        [HttpGet("current/queues")]
        [ProducesDefaultResponseType(typeof(IEnumerable<QueueDto>))]
        public async Task<ActionResult<IEnumerable<QueueDto>>> GetQueuesForOwnWorksite()
        {
            return Ok(await queueService.GetQueuesForOwnWorksite());
        }

    }
}
