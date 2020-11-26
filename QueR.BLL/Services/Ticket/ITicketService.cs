using QueR.BLL.Services.Ticket.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QueR.BLL.Services.Ticket
{
    interface ITicketService
    {
        Task<TicketDto> CreateTicket();
        Task<IEnumerable<TicketDto>> GetOwnTicketsForUser();
        Task<IEnumerable<TicketDto>> GetActiveTicketsForOwnQueue();
        Task CallNextTicket();
        Task CallTicketByNumber(int ticketNumber);
    }
}
