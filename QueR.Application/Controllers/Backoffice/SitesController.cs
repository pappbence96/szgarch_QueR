using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QueR.Application.DTOs;
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

        [HttpGet("{siteId}/manager/{managerId}")]
        public async Task<ActionResult> AssignManagerToSite(int siteId, int managerId)
        {
            await siteService.AssignManagerToSite(siteId, managerId);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> CreateCompany([FromBody] SiteModel model)
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
        public async Task<ActionResult<IEnumerable<SiteDto>>> GetSites()
        {
            var sites = await siteService.GetSites();
            return Ok(sites.Select(c => new SiteDto
            {
                Name = c.Name,
                Address = c.Address,
                Id = c.Id,
                ManagerName = c.Manager?.UserName ?? "-",
                NumberOfEmployees = c.Employees.Count
            }));
        }

        [HttpGet("{siteId}")]
        public async Task<ActionResult<IEnumerable<ApplicationUserDto>>> GetEmployeesOfSite(int siteId)
        {
            var employees = await siteService.GetEmployeesOfSite(siteId);
            return Ok(employees.Select(c => new ApplicationUserDto
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Gender = c.Gender,
                Address = c.Address,
                AssignedQueue = c.AssignedQueue?.Type?.Name ?? "-"
            }));
        }

        [HttpDelete("{siteId}/manager")]
        public async Task<ActionResult> RemoveManagerFromSite(int siteId)
        {
            await siteService.RemoveManagerFromSite(siteId);
            return Ok();
        }

        [HttpDelete("{siteId}/workers/{employeeId}")]
        public async Task<ActionResult> RemoveEmployeeOfCompany(int siteId, int employeeId)
        {
            await siteService.RemoveEmployeeFromSite(siteId, employeeId);
            return Ok();
        }

        [HttpPut("{siteId}")]
        public async Task<ActionResult> UpdateCompany(int siteId, [FromBody] SiteModel model)
        {
            await siteService.UpdateSite(siteId, model);
            return Ok();
        }

    }
}
