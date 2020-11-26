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

        public async Task<QueueTypeDto> CreateQueueType(QueueTypeModel model)
        {
            new QueueTypeValidator().ValidateAndThrow(model);

            if (await context.QueueTypes.AnyAsync(c => c.Name == model.Name))
            {
                throw new ArgumentException($"Queue type already exists with name: \"{model.Name}\"");
            }

            var queueType = new Domain.Entities.QueueType
            {
                Name = model.Name,
                CompanyId = (int)userAccessor.CompanyId,
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
            var queueTypes = await context.QueueTypes
                .Where(qt => qt.CompanyId == userAccessor.CompanyId)
                .Include(qt => qt.Queues)
                .ToListAsync();
            return mapper.Map<IEnumerable<QueueTypeDto>>(queueTypes);
        }

        public async Task UpdateQueueType(int queueTypeId, QueueTypeModel model)
        {
            var queueType = (await context.QueueTypes.FirstOrDefaultAsync(u => u.Id == queueTypeId))
                ?? throw new KeyNotFoundException($"Queue type not found with an id of {queueTypeId}");

            if (userAccessor.CompanyId == null)
            {
                throw new InvalidOperationException("Only an assigned administrator can make changes");
            }

            new QueueTypeValidator().ValidateAndThrow(model);

            if (queueType.CompanyId != userAccessor.CompanyId)
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
