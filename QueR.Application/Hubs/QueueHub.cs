using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using QueR.DAL;
using QueR.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueR.Application.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class QueueHub : Hub
    {
        private readonly AppDbContext context;
        private readonly IUserAccessor userAccessor;

        public QueueHub(AppDbContext context, IUserAccessor userAccessor)
        {
            this.context = context;
            this.userAccessor = userAccessor;
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            var userId = userAccessor.UserId;
            var queueId = (await context.Users.Include(u => u.AssignedQueue).SingleAsync(u => u.Id == userId)).AssignedQueue.Id;
            await Groups.AddToGroupAsync(Context.ConnectionId, queueId.ToString());
            var _ = Context;
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
            var userId = userAccessor.UserId;
            var queueId = (await context.Users.Include(u => u.AssignedQueue).SingleAsync(u => u.Id == userId)).AssignedQueue.Id;
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, queueId.ToString());
        }
    }
}
