using QueR.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QueR.BLL.Services.QueueType
{
    public class QueueTypeService : IQueueTypeService
    {
        private readonly AppDbContext context;

        public QueueTypeService(AppDbContext context)
        {
            this.context = context;
        }

        public Task<int> CreateQueueType(QueueTypeModel model)
        {
            throw new NotImplementedException();
        }

        public Task DeactivateQueueType(int queueId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Domain.Entities.QueueType>> GetQueueTypes()
        {
            throw new NotImplementedException();
        }

        public Task UpdateQueueType(int queueId, QueueTypeModel model)
        {
            throw new NotImplementedException();
        }
    }
}
