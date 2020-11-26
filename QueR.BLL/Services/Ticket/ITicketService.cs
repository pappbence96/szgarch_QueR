using QueR.BLL.Services.Ticket.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QueR.BLL.Services.Ticket
{
    interface ITicketService
    {
        Task<UserTicketDto> CreateTicket(TicketModel ticketModel);
        Task<IEnumerable<UserTicketDto>> GetOwnTicketsForUser();
        Task<IEnumerable<CompanyTicketDto>> GetActiveTicketsForOwnQueue();
        Task CallNextTicket();
        Task CallTicketByNumber(int ticketNumber);
    }
}
