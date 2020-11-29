using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using QueR.DAL;
using QueR.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace QueR.Application.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TicketHub : Hub
    {
        private readonly AppDbContext context;
        private readonly IUserAccessor userAccessor;

        public TicketHub(AppDbContext context, IUserAccessor userAccessor)
        {
            this.context = context;
            this.userAccessor = userAccessor;
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            var userId = userAccessor.UserId;
            var queues = (await context.Users
                .Include(u => u.Tickets)
                .SingleAsync(u => u.Id == userId))
                .Tickets
                .Select(t => t.QueueId);
            foreach(var queue in queues)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, queue.ToString());
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
        }
    }
}
