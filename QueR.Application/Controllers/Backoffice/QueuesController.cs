using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QueR.Application.Middlewares.ExceptionHandling;
using QueR.BLL.Services.Queue;
using QueR.BLL.Services.Queue.DTOs;
using QueR.BLL.Services.User.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueR.Application.Controllers.Backoffice
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "backoffice")]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    public class QueuesController : ControllerBase
    {
        private readonly IQueueService queueService;

        public QueuesController(IQueueService queueService)
        {
            this.queueService = queueService;
        }

        [Authorize(Roles = "manager")]
        [HttpPost]
        [ProducesDefaultResponseType(typeof(QueueDto))]
        public async Task<ActionResult<QueueDto>> CreateQueue([FromBody] QueueModel model)
        {
            return Ok(await queueService.CreateQueue(model));
        }

        [Authorize(Roles = "manager")]
        [HttpPut("{queueId}")]
        public async Task<ActionResult> UpdateQueue(int queueId, [FromBody] QueueModel model)
        {
            await queueService.UpdateQueue(queueId, model);
            return Ok();
        }

        [Authorize(Roles = "manager")]
        [HttpGet("{queueId}/workers/{workerId}")]
        public async Task<ActionResult> AssignEmployeeToQueue(int queueId, int workerId)
        {
            await queueService.AssignEmployeeToQueue(queueId, workerId);
            return Ok();
        }

        [Authorize(Roles = "manager")]
        [HttpDelete("{queueId}/workers/{workerId}")]
        public async Task<ActionResult> RemoveEmployeeFromQueue(int queueId, int workerId)
        {
            await queueService.RemoveEmployeeFromQueue(queueId, workerId);
            return Ok();
        }

        [Authorize(Roles = "manager")]
        [HttpGet("{queueId}/employees")]
        [ProducesDefaultResponseType(typeof(IEnumerable<ApplicationUserDto>))]
        public async Task<ActionResult<IEnumerable<ApplicationUserDto>>> GetEmployeesOfQueue(int queueId)
        {
            return Ok(await queueService.GetEmployeesOfQueue(queueId));
        }

        [Authorize(Roles = "manager")]
        [HttpGet("{queueId}")]
        [ProducesDefaultResponseType(typeof(QueueDto))]
        public async Task<ActionResult<QueueDto>> GetDetailsOfQueue(int queueId)
        {
            return Ok(await queueService.GetDetailsOfQueue(queueId));
        }

        [Authorize(Roles = "employee")]
        [HttpGet("own")]
        [ProducesDefaultResponseType(typeof(QueueDto))]
        public async Task<ActionResult<QueueDto>> GetDetailsOfAssignedQueue()
        {
            return Ok(await queueService.GetDetailsOfAssignedQueue());
        }

        [Authorize(Roles = "manager")]
        [HttpGet]
        [ProducesDefaultResponseType(typeof(IEnumerable<QueueDto>))]
        public async Task<ActionResult<IEnumerable<QueueDto>>> GetQueues()
        {
            return Ok(await queueService.GetQueues());
        }
    }
}
