﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QueR.Application.Middlewares.ExceptionHandling;
using QueR.BLL.Services.Identity;
using QueR.BLL.Services.Identity.DTOs;

namespace QueR.Application.Controllers.Common
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "common")]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService identityService;

        public IdentityController(IIdentityService identityService)
        {
            this.identityService = identityService;
        }

        [ProducesDefaultResponseType(typeof(LoginResponse))]
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginModel model)
        {
            return Ok(await identityService.CreateTokenForUser(model));
        }

        [ProducesDefaultResponseType(typeof(RegisterResponse))]
        [HttpPost("register")]
        public async Task<ActionResult<RegisterResponse>> Register([FromBody] RegisterModel model)
        {
            return Ok(await identityService.RegisterSimpleUser(model));
        }

        [HttpPut("update")]
        [Authorize(Roles = "user")]
        public async Task<ActionResult> UpdateUser([FromBody] UpdateUserModel model)
        {
            await identityService.UpdateSimpleUser(model);
            return Ok();
        }
    }
}
