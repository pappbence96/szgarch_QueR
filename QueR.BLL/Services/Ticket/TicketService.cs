using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QueR.BLL.Services.Ticket.DTOs;
using QueR.DAL;
using QueR.Domain.Entities;
using QueR.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueR.BLL.Services.Ticket
{
    public class TicketService : ITicketService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserAccessor userAccessor;
        private readonly AppDbContext context;
        private readonly IMapper mapper;
        private readonly INotificationService notificationService;

        public TicketService(UserManager<ApplicationUser> userManager, IUserAccessor userAccessor, AppDbContext context, IMapper mapper, INotificationService notificationService)
        {
            this.userManager = userManager;
            this.userAccessor = userAccessor;
            this.context = context;
            this.mapper = mapper;
            this.notificationService = notificationService;
        }

        private async Task HandleTicket(Domain.Entities.Ticket ticket)
        {
            var callerUserId = userAccessor.UserId;
            ticket.Called = true;
            ticket.HandlerId = callerUserId;

            context.Tickets.Update(ticket);
            await context.SaveChangesAsync();
            await notificationService.NotifyQueueTicketCalled(ticket.Queue.Id, ticket.Id);
        }

        public async Task CallNextTicket()
        {
            var callerUserId = userAccessor.UserId;
            var user = await context.Users
                .Include(u => u.AssignedQueue)
                    .ThenInclude(q => q.Tickets)
                        .ThenInclude(t => t.Owner)
                .FirstOrDefaultAsync(u => u.Id == callerUserId);
            if (user.AssignedQueue == null)
            {
                throw new InvalidOperationException("Employee must have an assigned queue to call a ticket");
            }
            var ticket = user.AssignedQueue.Tickets.Where(t => !t.Called).OrderBy(t => t.Number).First();
            await HandleTicket(ticket);

        }

        public async Task CallTicketByNumber(int ticketNumber)
        {
            var callerUserId = userAccessor.UserId;
            var user = await context.Users
                .Include(u => u.AssignedQueue)
                    .ThenInclude(q => q.Tickets)
                        .ThenInclude(t => t.Owner)
                .FirstOrDefaultAsync(u => u.Id == callerUserId);
            if (user.AssignedQueue == null)
            {
                throw new InvalidOperationException("Employee must have an assigned queue to call a ticket");
            }
            var ticket = user.AssignedQueue.Tickets.Where(t => t.Number == ticketNumber && !t.Called).First();
            await HandleTicket(ticket);
        }

        public async Task<UserTicketDto> CreateTicket(TicketModel model)
        {
            var callerUserId = userAccessor.UserId;
            var queue = (await context.Queues
                .Include(q => q.Site)
                    .ThenInclude(s => s.Company)
                .Include(q => q.Type)
                .Include(q => q.Tickets)
                .FirstOrDefaultAsync(u => u.Id == model.queueId))
                ?? throw new KeyNotFoundException($"Queue not found with an id of {model.queueId}");

            var ticket = new Domain.Entities.Ticket
            {
                Number = queue.NextNumber,
                OwnerId = callerUserId,
                Called = false,
                QueueId = model.queueId,
                Created = DateTime.Now
             };

            queue.NextNumber += queue.Step;
            context.Tickets.Add(ticket);
            context.Queues.Update(queue);

            await context.SaveChangesAsync();
            await notificationService.NotifyQueueTicketAdded(ticket.Queue.Id, mapper.Map<CompanyTicketDto>(ticket));
            return mapper.Map<UserTicketDto>(ticket);
        }

        public async Task<IEnumerable<CompanyTicketDto>> GetActiveTicketsForOwnQueue()
        {
            var callerUserId = userAccessor.UserId;
            var user = await context.Users
                .Include(u => u.AssignedQueue)
                    .ThenInclude(q => q.Tickets)
                        .ThenInclude(t => t.Owner)
                .FirstOrDefaultAsync(u => u.Id == callerUserId);
            if (user.AssignedQueue == null)
            {
                throw new InvalidOperationException("Employee must have an assigned queue to view active tickets");
            }
            var tickets = user.AssignedQueue.Tickets.Where(t => !t.Called).ToList();
            
            return mapper.Map<IEnumerable<CompanyTicketDto>>(tickets);
        }

        public async Task<IEnumerable<UserTicketDto>> GetOwnTicketsForUser()
        {
            var callerUserId = userAccessor.UserId;
            var tickets = await context.Tickets
                .Include(t => t.Queue)
                    .ThenInclude(q => q.Tickets)
                .Include(t => t.Queue)
                    .ThenInclude(q => q.Type)
                .Include(t => t.Queue)
                    .ThenInclude(q => q.Site)
                        .ThenInclude(s => s.Company)
                .Where(t => t.OwnerId == callerUserId && !t.Called)
                .ToListAsync();

            return mapper.Map<IEnumerable<UserTicketDto>>(tickets);
        }
    }
}
