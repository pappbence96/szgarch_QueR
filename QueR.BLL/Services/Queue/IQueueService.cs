using QueR.BLL.Services.Queue.DTOs;
using QueR.BLL.Services.User.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QueR.BLL.Services.Queue
{
    public interface IQueueService
    {
        Task<QueueDto> CreateQueue(QueueModel model);
        Task UpdateQueue(int queueId, QueueModel model);
        Task AssignEmployeeToQueue(int queueId, int workerId);
        Task RemoveEmployeeFromQueue(int queueId, int workerId);
        Task<IEnumerable<EmployeeDto>> GetEmployeesOfQueue(int queueId);
        Task<IEnumerable<QueueDto>> GetQueues();
        Task<IEnumerable<UserQueueDto>> GetQueuesOfSiteForUser(int worksiteId);
        Task<QueueDto> GetDetailsOfQueue(int queueId);
        Task<QueueDto> GetDetailsOfAssignedQueue();
        Task SubscribeToCurrentQueue();
        Task UnsubscribeFromCurrentQueue();
    }
}