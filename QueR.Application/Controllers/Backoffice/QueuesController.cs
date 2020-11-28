using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QueR.Application.Middlewares.ExceptionHandling;
using QueR.BLL.Services.Queue;
using QueR.BLL.Services.Queue.DTOs;
using QueR.BLL.Services.Ticket;
using QueR.BLL.Services.Ticket.DTOs;
using QueR.BLL.Services.User.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueR.Application.Controllers.Backoffice
{
    [Route("api/backoffice/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "backoffice")]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    public class QueuesController : ControllerBase
    {
        private readonly IQueueService queueService;
        private readonly ITicketService ticketService;

        public QueuesController(IQueueService queueService, ITicketService ticketService)
        {
            this.queueService = queueService;
            this.ticketService = ticketService;
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
        [HttpGet("{queueId}/employees/{employeeId}")]
        public async Task<ActionResult> AssignEmployeeToQueue(int queueId, int employeeId)
        {
            await queueService.AssignEmployeeToQueue(queueId, employeeId);
            return Ok();
        }

        [Authorize(Roles = "manager")]
        [HttpDelete("{queueId}/employees/{employeesId}")]
        public async Task<ActionResult> RemoveEmployeeFromQueue(int queueId, int employeesId)
        {
            await queueService.RemoveEmployeeFromQueue(queueId, employeesId);
            return Ok();
        }

        [Authorize(Roles = "manager")]
        [HttpGet("{queueId}/employees")]
        [ProducesDefaultResponseType(typeof(IEnumerable<EmployeeDto>))]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployeesOfQueue(int queueId)
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
        [HttpGet("current")]
        [ProducesDefaultResponseType(typeof(QueueDto))]
        public async Task<ActionResult<QueueDto>> GetDetailsOfAssignedQueue()
        {
            return Ok(await queueService.GetDetailsOfAssignedQueue());
        }

        [HttpGet("current/tickets")]
        [Authorize(Roles = "manager,employee")]
        [ProducesDefaultResponseType(typeof(IEnumerable<CompanyTicketDto>))]
        public async Task<ActionResult<IEnumerable<CompanyTicketDto>>> GetActiveTicketsForOwnQueue()
        {
            return Ok(await ticketService.GetActiveTicketsForOwnQueue());
        }

        [HttpGet("current/tickets/next")]
        [Authorize(Roles = "manager,employee")]
        public async Task<ActionResult> CallNextTicket()
        {
            await ticketService.CallNextTicket();
            return Ok();
        }

        [HttpGet("current/tickets/{ticketNumber}")]
        [Authorize(Roles = "manager,employee")]
        public async Task<ActionResult> CallTicketByNumber(int ticketNumber)
        {
            await ticketService.CallTicketByNumber(ticketNumber);
            return Ok();
        }

    }
}
