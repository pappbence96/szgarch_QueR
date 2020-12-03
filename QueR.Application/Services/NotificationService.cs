using Microsoft.AspNetCore.SignalR;
using QueR.Application.Hubs;
using QueR.BLL;
using QueR.BLL.Services.Ticket.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueR.Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<QueueHub> queueHubContext;
        private readonly IHubContext<TicketHub> ticketHubContext;

        public NotificationService(IHubContext<QueueHub> context, IHubContext<TicketHub> ticketHubContext)
        {
            this.queueHubContext = context;
            this.ticketHubContext = ticketHubContext;
        }

        public async Task NotifyQueueTicketAdded(int queueId, CompanyTicketDto ticket)
        {
            await queueHubContext.Clients.Group(queueId.ToString()).SendAsync("newTicket", queueId, ticket);
        }

        public async Task NotifyQueueTicketCalled(int queueId, int ticketId, int ticketNumber, string handler)
        {
            await queueHubContext.Clients.Group(queueId.ToString()).SendAsync("calledTicket", queueId, ticketId);
            await ticketHubContext.Clients.Group(queueId.ToString()).SendAsync("calledTicket", queueId, ticketId, ticketNumber, handler);
        }
    }
}
