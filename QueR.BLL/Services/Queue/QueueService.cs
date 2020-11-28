using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QueR.BLL.Services.Queue.DTOs;
using QueR.BLL.Services.User.DTOs;
using QueR.DAL;
using QueR.Domain.Entities;
using QueR.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueR.BLL.Services.Queue
{
    public class QueueService : IQueueService
    {
        private readonly AppDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserAccessor userAccessor;
        private readonly IMapper mapper;

        public QueueService(AppDbContext context, UserManager<ApplicationUser> userManager, IUserAccessor userAccessor, IMapper mapper)
        {
            this.context = context;
            this.userManager = userManager;
            this.userAccessor = userAccessor;
            this.mapper = mapper;
        }

        public async Task AssignEmployeeToQueue(int queueId, int employeeId)
        {
            var employee = (await context.Users.Include(a => a.AssignedQueue).FirstOrDefaultAsync(u => u.Id == employeeId))
                   ?? throw new KeyNotFoundException($"Employee not found with an id of {employeeId}");
            var queue = (await context.Queues.Include(c => c.AssignedEmployees).FirstOrDefaultAsync(u => u.Id == queueId))
                ?? throw new KeyNotFoundException($"Queue not found with an id of {queueId}");

            var callerCompanyId = userAccessor.CompanyId;
            var callerWorksiteId = userAccessor.WorksiteId;

            if (!await userManager.IsInRoleAsync(employee, "employee"))
            {
                throw new InvalidOperationException("User is not an employee");
            }

            if (employee.CompanyId != callerCompanyId || employee.WorksiteId != callerWorksiteId)
            {
                throw new InvalidOperationException("Employee is not part of the company or the worksite.");
            }

            if (queue.AssignedEmployees.Contains(employee))
            {
                return;
            }

            if ((employee.AssignedQueue != null) && (employee.AssignedQueue.Id != queueId))
            {
                throw new InvalidOperationException("Employee already has a queue");
            }

            queue.AssignedEmployees.Add(employee);

            await context.SaveChangesAsync();
        }

        public async Task<QueueDto> CreateQueue(QueueModel model)
        {
            var callerWorksiteId = userAccessor.WorksiteId;
            var callerCompanyId = userAccessor.CompanyId;

            new CreateQueueValidator().ValidateAndThrow(model);

            var queueType = (await context.QueueTypes.Include(qt => qt.Queues).FirstOrDefaultAsync(u => u.Id == model.TypeId))
                   ?? throw new KeyNotFoundException($"QueueType not found with an id of {model.TypeId}");

            if (queueType.CompanyId != callerCompanyId)
            {
                throw new InvalidOperationException("QueueType is not part of the company");
            }

            if (queueType.Queues.Any(q => q.SiteId == callerWorksiteId))
            {
                throw new ArgumentException($"Queue already exists with type Id: \"{model.TypeId}\" at this worksite.");
            }

            var queue = new Domain.Entities.Queue
            {
                TypeId = model.TypeId,
                SiteId = callerWorksiteId,
                NextNumber = model.NextNumber,
                Prefix = model.Prefix,
                Step = model.Step,
                MaxActiveTicketsPerUser = model.MaxActiveTicketsPerUser
            };

            context.Queues.Add(queue);

            await context.SaveChangesAsync();

            return mapper.Map<QueueDto>(queue);
        }

        public async Task RemoveEmployeeFromQueue(int queueId, int employeeId)
        {
            var employee = (await context.Users.Include(c => c.AssignedQueue).FirstOrDefaultAsync(u => u.Id == employeeId))
                   ?? throw new KeyNotFoundException($"Employee not found with an id of {employeeId}");

            var callerCompanyId = userAccessor.CompanyId;
            var callerWorksiteId = userAccessor.WorksiteId;

            if (!await userManager.IsInRoleAsync(employee, "employee"))
            {
                throw new InvalidOperationException("User is not an employee");
            }

            if (employee.CompanyId != callerCompanyId || employee.WorksiteId != callerWorksiteId)
            {
                throw new InvalidOperationException("Employee is not part of the company or the worksite");
            }

            if (employee.AssignedQueue == null)
            {
                return;
            }

            employee.AssignedQueue = null;
            await context.SaveChangesAsync();
        }

        public async Task UpdateQueue(int queueId, QueueModel model)
        {
            var queue = (await context.Queues.FirstOrDefaultAsync(u => u.Id == queueId))
                ?? throw new KeyNotFoundException($"Queue not found with an id of {queueId}");

            var callerCompanyId = userAccessor.CompanyId;

            new UpdateQueueValidator().ValidateAndThrow(model);

            var queueType = await context.QueueTypes.FirstAsync(u => u.Id == queue.TypeId);

            if (queueType.CompanyId != callerCompanyId)
            {
                throw new InvalidOperationException("QueueType is not part of the company");
            }

            queue.Step = model.Step;
            queue.Prefix = model.Prefix;
            queue.MaxActiveTicketsPerUser = model.MaxActiveTicketsPerUser;

            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ApplicationUserDto>> GetEmployeesOfQueue(int queueId)
        {
            var queue = (await context.Queues
                .Include(c => c.AssignedEmployees)
                    .ThenInclude(e => e.Company)
                .Include(c => c.AssignedEmployees)
                    .ThenInclude(e => e.Worksite)
                .Include(q => q.Type)
                .FirstOrDefaultAsync(u => u.Id == queueId))
                ?? throw new KeyNotFoundException($"Queue not found with an id of {queueId}");

            return mapper.Map<IEnumerable<ApplicationUserDto>>(queue.AssignedEmployees);
        }

        public async Task<IEnumerable<QueueDto>> GetQueues()
        {
            var callerWorksiteId = userAccessor.WorksiteId;

            var queues = await context.Queues
                .Include(q => q.Site)
                .Include(q => q.Type)
                .Include(q => q.AssignedEmployees)
                .Include(q => q.Tickets)
                .Where(u => u.SiteId == callerWorksiteId)
                .ToListAsync();
            return mapper.Map<IEnumerable<QueueDto>>(queues);
        }

        public async Task<QueueDto> GetDetailsOfQueue(int queueId)
        {
            var callerWorksiteId = userAccessor.WorksiteId;
            var queue = (await context.Queues
                .Include(q => q.Site)
                .Include(q => q.Type)
                .Include(q => q.AssignedEmployees)
                .Include(q => q.Tickets)
                .FirstOrDefaultAsync(u => u.SiteId == callerWorksiteId && u.Id == queueId))
                ?? throw new KeyNotFoundException($"Queue not found with the id of {queueId}");

            return mapper.Map<QueueDto>(queue);
        }

        public async Task<QueueDto> GetDetailsOfAssignedQueue()
        {
            var callerAssignedQueueId = userAccessor.AssignedQueueId;
            var queue = (await context.Queues
                .Include(q => q.Site)
                .Include(q => q.Type)
                .Include(q => q.AssignedEmployees)
                .Include(q => q.Tickets)
                .FirstOrDefaultAsync(q => q.Id == callerAssignedQueueId))
                ?? throw new InvalidOperationException($"You are not assigned to any queues currently.");

            return mapper.Map<QueueDto>(queue);
        }

        public Task SubscribeToCurrentQueue()
        {
            throw new NotImplementedException();
        }

        public Task UnsubscribeFromCurrentQueue()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserQueueDto>> GetQueuesOfSiteForUser(int worksiteId)
        {
            var site = (await context.Sites
                .Include(s => s.Queues)
                    .ThenInclude(q => q.Tickets)
                .Include(s => s.Queues)
                    .ThenInclude(q => q.Type)
                .Where(s => s.Id == worksiteId)
                .FirstOrDefaultAsync())
                ?? throw new KeyNotFoundException($"Site not found with the id of {worksiteId}");

            return mapper.Map<IEnumerable<UserQueueDto>>(site.Queues);
        }
    }
}
