using AutoMapper;
using Microsoft.AspNetCore.Identity;
using QueR.BLL.Services.Ticket.DTOs;
using QueR.DAL;
using QueR.Domain.Entities;
using QueR.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QueR.BLL.Services.Ticket
{
    class TicketService : ITicketService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserAccessor userAccessor;
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public TicketService(UserManager<ApplicationUser> userManager, IUserAccessor userAccessor, AppDbContext context, IMapper mapper)
        {
            this.userManager = userManager;
            this.userAccessor = userAccessor;
            this.context = context;
            this.mapper = mapper;
        }

        public Task CallNextTicket()
        {
            throw new NotImplementedException();
        }

        public Task CallTicketByNumber(int ticketNumber)
        {
            throw new NotImplementedException();
        }

        public Task<TicketDto> CreateTicket()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TicketDto>> GetActiveTicketsForOwnQueue()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TicketDto>> GetOwnTicketsForUser()
        {
            throw new NotImplementedException();
        }
    }
}
