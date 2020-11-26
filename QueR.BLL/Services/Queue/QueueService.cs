using Microsoft.AspNetCore.Identity;
using QueR.BLL.Services.Queue.DTOs;
using QueR.DAL;
using QueR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QueR.BLL.Services.Queue
{
    public class QueueService : IQueueService
    {
        private readonly AppDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public QueueService(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public Task AssignWorkerToQueue(int queueId, int workerId)
        {
            throw new NotImplementedException();
        }

        public Task<int> CreateQueue(QueueModel model)
        {
            throw new NotImplementedException();
        }

        public Task RemoveWorkerFromQueue(int queueId, int workerId)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateQueue(int queueId, QueueModel model)
        {
            throw new NotImplementedException();
        }
    }
}
