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
        private readonly IHubContext<QueueHub> context;

        public NotificationService(IHubContext<QueueHub> context)
        {
            this.context = context;
        }

        public async Task NotifyQueueTicketAdded(int queueId, CompanyTicketDto ticket)
        {
            await context.Clients.All.SendAsync("newTicket", queueId, ticket);
        }

        public async Task NotifyQueueTicketCalled(int queueId, int ticketId)
        {
            await context.Clients.All.SendAsync("calledTicket", queueId, ticketId);
        }
    }
}
