using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QueR.Application.Middlewares.ExceptionHandling;
using QueR.BLL.Services.Ticket;
using QueR.BLL.Services.Ticket.DTOs;
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
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService ticketService;

        public TicketsController(ITicketService ticketService)
        {
            this.ticketService = ticketService;
        }

        [HttpGet("next")]
        [Authorize(Roles = "manager,employee")]
        public async Task<ActionResult> CallNextTicket()
        {
            await ticketService.CallNextTicket();
            return Ok();
        }

        [HttpGet("{ticketNumber}")]
        [Authorize(Roles = "manager,employee")]
        public async Task<ActionResult> CallTicketByNumber(int ticketNumber)
        {
            await ticketService.CallTicketByNumber(ticketNumber);
            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "user")]
        [ProducesDefaultResponseType(typeof(UserTicketDto))]
        public async Task<ActionResult<UserTicketDto>> CreateTicket([FromBody] TicketModel model)
        {
            return Ok(await ticketService.CreateTicket(model));
        }

        [HttpGet("user")]
        [Authorize(Roles = "user")]
        [ProducesDefaultResponseType(typeof(IEnumerable<UserTicketDto>))]
        public async Task<ActionResult<IEnumerable<UserTicketDto>>> GetOwnTicketsForUser()
        {
            return Ok(await ticketService.GetOwnTicketsForUser());
        }

        [HttpGet("employee")]
        [Authorize(Roles = "manager,employee")]
        [ProducesDefaultResponseType(typeof(IEnumerable<CompanyTicketDto>))]
        public async Task<ActionResult<IEnumerable<CompanyTicketDto>>> GetActiveTicketsForOwnQueue()
        {
            return Ok(await ticketService.GetActiveTicketsForOwnQueue());
        }
    }
}
