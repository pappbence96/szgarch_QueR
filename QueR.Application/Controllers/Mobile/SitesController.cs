﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QueR.Application.Middlewares.ExceptionHandling;
using QueR.BLL.Services.Queue;
using QueR.BLL.Services.Queue.DTOs;
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
    public class SitesController : ControllerBase
    {
        private readonly IQueueService queueService;

        public SitesController(IQueueService queueService)
        {
            this.queueService = queueService;
        }

        [HttpGet("{worksiteId}/queues")]
        [ProducesDefaultResponseType(typeof(IEnumerable<UserQueueDto>))]
        public async Task<ActionResult<IEnumerable<UserQueueDto>>> GetQueuesOfSiteForUser(int worksiteId)
        {
            return Ok(await queueService.GetQueuesOfSiteForUser(worksiteId));
        }
    }
}
