using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QueR.BLL.Services.Queue;
using QueR.BLL.Services.Queue.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueR.Application.Controllers.Mobile
{
    [Route("api/mobile/[controller]")]
    [ApiExplorerSettings(GroupName = "mobile")]
    [Authorize(Roles = "user")]
    [ApiController]
    public class QueuesController : ControllerBase
    {
        private readonly IQueueService queueService;

        public QueuesController(IQueueService queueService)
        {
            this.queueService = queueService;
        }

        [HttpGet("{worksiteId}")]
        [ProducesDefaultResponseType(typeof(IEnumerable<UserQueueDto>))]
        public async Task<ActionResult<IEnumerable<UserQueueDto>>> GetQueuesOfSiteForUser(int worksiteId)
        {
            return Ok(await queueService.GetQueuesOfSiteForUser(worksiteId));
        }
    }
}
