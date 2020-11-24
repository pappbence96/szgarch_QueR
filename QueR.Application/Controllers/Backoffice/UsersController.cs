using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QueR.Application.Middlewares.ExceptionHandling;
using QueR.BLL.Services.User;
using QueR.BLL.Services.User.DTOs;

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
        [ProducesDefaultResponseType(typeof(ApplicationUserDto))]
        public async Task<ActionResult<ApplicationUserDto>> CreateAdmin([FromBody] CreateUserModel model)
        {
            return Ok(await userService.CreateAdmin(model));
        }

        [HttpPost("employees")]
        [Authorize(Roles = "administrator")]
        [ProducesDefaultResponseType(typeof(ApplicationUserDto))]
        public async Task<ActionResult<ApplicationUserDto>> CreateEmployee([FromBody] CreateUserModel model)
        {
            return Ok(await userService.CreateEmployee(model));
        }

        [HttpPost("managers")]
        [Authorize(Roles = "administrator")]
        [ProducesDefaultResponseType(typeof(ApplicationUserDto))]
        public async Task<ActionResult<ApplicationUserDto>> CreateManager([FromBody] CreateUserModel model)
        {
            return Ok(await userService.CreateManager(model));
        }

        [HttpPost("users")]
        [ProducesDefaultResponseType(typeof(ApplicationUserDto))]
        public async Task<ActionResult<ApplicationUserDto>> CreateUser([FromBody] CreateUserModel model)
        {
            return Ok(await userService.CreateSimpleUser(model));
        }

        [HttpGet("admins")]
        [Authorize(Roles = "operator")]
        [ProducesDefaultResponseType(typeof(IEnumerable<ApplicationUserDto>))]
        public async Task<ActionResult<IEnumerable<ApplicationUserDto>>> GetAdmins()
        {
            return Ok(await userService.GetAdministrators());
        }

        [HttpGet("employees")]
        [Authorize(Roles = "operator")]
        [ProducesDefaultResponseType(typeof(IEnumerable<ApplicationUserDto>))]
        public async Task<ActionResult<IEnumerable<ApplicationUserDto>>> GetEmployees()
        {
            return Ok(await userService.GetEmployees());
        }

        [HttpGet("managers")]
        [Authorize(Roles = "operator")]
        [ProducesDefaultResponseType(typeof(IEnumerable<ApplicationUserDto>))]
        public async Task<ActionResult<IEnumerable<ApplicationUserDto>>> GetManagers()
        {
            return Ok(await userService.GetManagers());
        }

        [HttpGet("users")]
        [Authorize(Roles = "operator")]
        [ProducesDefaultResponseType(typeof(IEnumerable<ApplicationUserDto>))]
        public async Task<ActionResult<IEnumerable<ApplicationUserDto>>> GetSimpleUsers()
        {
            return Ok(await userService.GetSimpleUsers());
        }

        [HttpPut("admins/{adminId}")]
        [Authorize(Roles = "operator")]
        public async Task<ActionResult> UpdateAdmin(int adminId, [FromBody] UpdateUserModel model)
        {
            await userService.UpdateAdmin(adminId, model);
            return Ok();
        }

        [HttpPut("managers/{managerId}")]
        [Authorize(Roles = "administrator")]
        public async Task<ActionResult> UpdateManager(int managerId, [FromBody] UpdateUserModel model)
        {
            await userService.UpdateManager(managerId, model);
            return Ok();
        }

        [HttpPut("employees/{employeeId}")]
        [Authorize(Roles = "administrator")]
        public async Task<ActionResult> UpdateEmployee(int employeeId, [FromBody] UpdateUserModel model)
        {
            await userService.UpdateEmployee(employeeId, model);
            return Ok();
        }

        [HttpPut("users/{userId}")]
        [Authorize(Roles = "user")]
        public async Task<ActionResult> UpdateSimpleUser(int userId, [FromBody] UpdateUserModel model)
        {
            await userService.UpdateSimpleUser(userId, model);
            return Ok();
        }
    }
}
