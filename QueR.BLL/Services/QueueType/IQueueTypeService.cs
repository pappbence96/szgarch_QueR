using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using QueR.BLL.Services.QueueType.DTOs;
using QueR.Domain.Entities;

namespace QueR.BLL.Services.QueueType
{
    public interface IQueueTypeService
    {
        Task<QueueTypeDto> CreateQueueType(QueueTypeModel model);
        Task DeactivateQueueType(int queueId);
        Task UpdateQueueType(int queueId, QueueTypeModel model);
        Task<IEnumerable<QueueTypeDto>> GetQueueTypesOfOwnCompany();
    }
}