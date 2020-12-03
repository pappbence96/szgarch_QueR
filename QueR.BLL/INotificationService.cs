using QueR.BLL.Services.Ticket.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QueR.BLL
{
    public interface INotificationService
    {
        Task NotifyQueueTicketCalled(int queueId, int ticketId, int ticketNumber, string handler);
        Task NotifyQueueTicketAdded(int queueId, CompanyTicketDto ticket);
    }
}
