using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QueR.Application.DTOs;
using QueR.Application.Middlewares.ExceptionHandling;
using QueR.BLL.Services.User;
using QueR.Domain.Entities;

namespace QueR.Application.Controllers.Backoffice
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "backoffice")]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("admins")]
        [Authorize(Roles = "operator")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<ActionResult<int>> CreateAdmin([FromBody] UserModel model)
        {
            return Ok(await userService.CreateAdmin(model));
        }

        [HttpPost("employees")]
        [Authorize(Roles = "administrator")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<ActionResult<int>> CreateEmployee([FromBody] UserModel model)
        {
            return Ok(await userService.CreateEmployee(model));
        }

        [HttpPost("managers")]
        [Authorize(Roles = "administrator")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<ActionResult<int>> CreateManager([FromBody] UserModel model)
        {
            return Ok(await userService.CreateManager(model));
        }

        [HttpPost("users")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<ActionResult<int>> CreateUser([FromBody] UserModel model)
        {
            return Ok(await userService.CreateSimpleUser(model));
        }

        [HttpGet("admins")]
        [Authorize(Roles = "operator")]
        [ProducesDefaultResponseType(typeof(ApplicationUserDto))]
        public async Task<ActionResult<IEnumerable<ApplicationUserDto>>> GetAdmins()
        {
            var admins = await userService.GetAdministrators();
            return Ok(admins.Select(c => new ApplicationUserDto
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                Gender = c.Gender,
                Address = c.Address ?? "-",
                AdministratedCompany = c.AdministratedCompany?.Name ?? "-"
            }));
        }

        [HttpGet("employees")]
        [Authorize(Roles = "operator")]
        [ProducesDefaultResponseType(typeof(ApplicationUserDto))]
        public async Task<ActionResult<IEnumerable<ApplicationUserDto>>> GetEmployees()
        {
            var employees = await userService.GetEmployees();
            return Ok(employees.Select(c => new ApplicationUserDto
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                Gender = c.Gender,
                Address = c.Address ?? "-",
                Company = c.Company?.Name ?? "-"
            }));
        }

        [HttpGet("managers")]
        [Authorize(Roles = "operator")]
        [ProducesDefaultResponseType(typeof(ApplicationUserDto))]
        public async Task<ActionResult<IEnumerable<ApplicationUserDto>>> GetManagers()
        {
            var managers = await userService.GetManagers();
            return Ok(managers.Select(c => new ApplicationUserDto
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                Gender = c.Gender,
                Address = c.Address ?? "-",
                ManagedWorksite = c.ManagedSite?.Name ?? "-"
            }));
        }

        [HttpGet("users")]
        [Authorize(Roles = "operator")]
        [ProducesDefaultResponseType(typeof(ApplicationUserDto))]
        public async Task<ActionResult<IEnumerable<ApplicationUserDto>>> GetSimpleUsers()
        {
            var users = await userService.GetSimpleUsers();
            return Ok(users.Select(c => new ApplicationUserDto
            {
                Id = c.Id,
                Email = c.Email
            }));
        }

        [HttpPut("admins/{adminId}")]
        [Authorize(Roles = "operator")]
        public async Task<ActionResult> UpdateAdmin(int adminId, [FromBody] UserModel model)
        {
            await userService.UpdateAdmin(adminId, model);
            return Ok();
        }

        [HttpPut("managers/{managerId}")]
        [Authorize(Roles = "administrator")]
        public async Task<ActionResult> UpdateManager(int managerId, [FromBody] UserModel model)
        {
            await userService.UpdateManager(managerId, model);
            return Ok();
        }

        [HttpPut("employees/{employeeId}")]
        [Authorize(Roles = "administrator")]
        public async Task<ActionResult> UpdateEmployee(int employeeId, [FromBody] UserModel model)
        {
            await userService.UpdateEmployee(employeeId, model);
            return Ok();
        }

        [HttpPut("users/{userId}")]
        [Authorize(Roles = "user")]
        public async Task<ActionResult> UpdateSimpleUser(int userId, [FromBody] UserModel model)
        {
            await userService.UpdateSimpleUser(userId, model);
            return Ok();
        }
    }
}
