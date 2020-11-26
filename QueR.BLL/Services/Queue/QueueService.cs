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
using System.Text;
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

        private async Task<bool> IsCallerCurrentManager()
        {
            var callerWorksiteId = userAccessor.WorksiteId;
            var site = (await context.Sites.FirstOrDefaultAsync(u => u.Id == callerWorksiteId))
                   ?? throw new KeyNotFoundException($"Worksite not found with an id of {callerWorksiteId}");
            if (!site.ManagerId.HasValue || (site.ManagerId.HasValue && site.ManagerId != userAccessor.UserId))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task AssignEmployeeToQueue(int queueId, int employeeId)
        {
            var employee = (await context.Users.Include(a => a.AssignedQueue).FirstOrDefaultAsync(u => u.Id == employeeId))
                   ?? throw new KeyNotFoundException($"Employee not found with an id of {employeeId}");
            var queue = (await context.Queues.Include(c => c.AssignedEmployees).FirstOrDefaultAsync(u => u.Id == queueId))
                ?? throw new KeyNotFoundException($"Queue not found with an id of {queueId}");

            var callerCompanyId = userAccessor.CompanyId;
            var callerWorksiteId = userAccessor.WorksiteId;

            if (!await IsCallerCurrentManager())
            {
                throw new InvalidOperationException("Only the current manager can make changes");
            }

            if (employee.CompanyId != callerCompanyId)
            {
                throw new InvalidOperationException("Employee is not part of the company.");
            }

            if (!await userManager.IsInRoleAsync(employee, "employee"))
            {
                throw new InvalidOperationException("User is not an employee");
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

            if (!await IsCallerCurrentManager())
            {
                throw new InvalidOperationException("Only the current manager can make changes");
            }

            new QueueValidator().ValidateAndThrow(model);

            var queueType = (await context.QueueTypes.Include(qt => qt.Queues).FirstOrDefaultAsync(u => u.Id == model.TypeId))
                   ?? throw new KeyNotFoundException($"QueueType not found with an id of {model.TypeId}");

            if (queueType.CompanyId != callerCompanyId)
            {
                throw new InvalidOperationException("QueueType is not part of the company");
            }

            if (await context.Queues.AnyAsync(c => c.TypeId == model.TypeId))
            {
                throw new ArgumentException($"Queue already exists with type Id: \"{model.TypeId}\"");
            }

            var queue = new Domain.Entities.Queue
            {
                TypeId = model.TypeId,
                SiteId = callerWorksiteId,
                NextNumber = model.NextNumber
            };

            context.Queues.Add(queue);
            queueType.Queues.Add(queue);
            await context.SaveChangesAsync();

            return mapper.Map<QueueDto>(queue);
        }

        public async Task RemoveEmployeeFromQueue(int queueId, int employeeId)
        {
            var employee = (await context.Users.Include(c => c.AssignedQueue).FirstOrDefaultAsync(u => u.Id == employeeId))
                   ?? throw new KeyNotFoundException($"Employee not found with an id of {employeeId}");

            var callerCompanyId = userAccessor.CompanyId;

            if (!await IsCallerCurrentManager())
            {
                throw new InvalidOperationException("Only the current manager can make changes");
            }

            if (!await userManager.IsInRoleAsync(employee, "employee"))
            {
                throw new InvalidOperationException("User is not an employee");
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

            if (!await IsCallerCurrentManager())
            {
                throw new InvalidOperationException("Only the current manager can make changes");
            }

            new QueueValidator().ValidateAndThrow(model);

            var queueType = (await context.QueueTypes.FirstOrDefaultAsync(u => u.Id == model.TypeId))
                   ?? throw new KeyNotFoundException($"QueueType not found with an id of {model.TypeId}");

            if (queueType.CompanyId != callerCompanyId)
            {
                throw new InvalidOperationException("QueueType is not part of the company");
            }

            if (await context.Queues.AnyAsync(c => c.TypeId == model.TypeId))
            {
                throw new ArgumentException($"Queue already exists with type Id: \"{model.TypeId}\"");
            }

            queue.TypeId = model.TypeId;
            queue.NextNumber = model.NextNumber;

            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ApplicationUserDto>> GetEmployeesOfQueue(int queueId)
        {
            var queue = (await context.Queues.Include(c => c.AssignedEmployees).FirstOrDefaultAsync(u => u.Id == queueId))
                ?? throw new KeyNotFoundException($"Queue not found with an id of {queueId}");

            if (!await IsCallerCurrentManager())
            {
                throw new InvalidOperationException("Only the current manager can view statistics");
            }

            return mapper.Map<IEnumerable<ApplicationUserDto>>(queue.AssignedEmployees);
        }
    }
}
