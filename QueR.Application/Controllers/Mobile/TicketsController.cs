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
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService ticketService;

        public TicketsController(ITicketService ticketService)
        {
            this.ticketService = ticketService;
        }

        [HttpPost]
        [ProducesDefaultResponseType(typeof(UserTicketDto))]
        public async Task<ActionResult<UserTicketDto>> CreateTicket([FromBody] TicketModel model)
        {
            return Ok(await ticketService.CreateTicket(model));
        }

    }
}
