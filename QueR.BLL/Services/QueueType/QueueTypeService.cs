using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using QueR.BLL.Services.QueueType.DTOs;
using QueR.DAL;
using QueR.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueR.BLL.Services.QueueType
{
    public class QueueTypeService : IQueueTypeService
    {
        private readonly AppDbContext context;
        private readonly IUserAccessor userAccessor;
        private readonly IMapper mapper;

        public QueueTypeService(AppDbContext context, IUserAccessor userAccessor, IMapper mapper)
        {
            this.context = context;
            this.userAccessor = userAccessor;
            this.mapper = mapper;
        }

        private async Task<bool> IsCallerCurrentAdministrator()
        {
            var callerCompanyId = userAccessor.CompanyId;
            var company = (await context.Companies.FirstOrDefaultAsync(u => u.Id == callerCompanyId))
                   ?? throw new KeyNotFoundException($"Company not found with an id of {callerCompanyId}");

            if (!company.AdministratorId.HasValue || (company.AdministratorId.HasValue && company.AdministratorId != userAccessor.UserId))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<QueueTypeDto> CreateQueueType(QueueTypeModel model)
        {
            var callerCompanyId = userAccessor.CompanyId;

            if (!await IsCallerCurrentAdministrator())
            {
                throw new InvalidOperationException("Only the current administrator can make changes");
            }

            new QueueTypeValidator().ValidateAndThrow(model);

            if (await context.QueueTypes.AnyAsync(c => c.Name == model.Name))
            {
                throw new ArgumentException($"Queue type already exists with name: \"{model.Name}\"");
            }

            var queueType = new Domain.Entities.QueueType
            {
                Name = model.Name,
                CompanyId = (int)callerCompanyId,
                IsEnabled = true
            };

            context.QueueTypes.Add(queueType);
            await context.SaveChangesAsync();

            return mapper.Map<QueueTypeDto>(queueType);
        }

        public Task DeactivateQueueType(int queueId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<QueueTypeDto>> GetQueueTypes()
        {
            var callerCompanyId = userAccessor.CompanyId;

            if (!await IsCallerCurrentAdministrator())
            {
                throw new InvalidOperationException("Only the current administrator can view statistics");
            }

            var queueTypes = await context.QueueTypes
                .Where(qt => qt.CompanyId == callerCompanyId)
                .Include(qt => qt.Queues)
                .ToListAsync();
            return mapper.Map<IEnumerable<QueueTypeDto>>(queueTypes);
        }

        public async Task UpdateQueueType(int queueTypeId, QueueTypeModel model)
        {
            var queueType = (await context.QueueTypes.FirstOrDefaultAsync(u => u.Id == queueTypeId))
                ?? throw new KeyNotFoundException($"Queue type not found with an id of {queueTypeId}");

            var callerCompanyId = userAccessor.CompanyId;

            if (!await IsCallerCurrentAdministrator())
            {
                throw new InvalidOperationException("Only the current manager can make changes");
            }

            new QueueTypeValidator().ValidateAndThrow(model);

            if (queueType.CompanyId != callerCompanyId)
            {
                throw new InvalidOperationException("Queue type is not part of the company");
            }

            if (await context.QueueTypes.AnyAsync(c => c.Name == model.Name && c.Id != queueTypeId))
            {
                throw new InvalidOperationException($"Queue type with name \"{model.Name}\" already exists.");
            }

            queueType.Name = model.Name;

            await context.SaveChangesAsync();
        }
    }
}
