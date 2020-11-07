using System.Threading.Tasks;

namespace QueR.BLL.Services.Queue
{
    public interface IQueueService
    {
        Task<int> CreateQueue(QueueModel model);
        Task<int> UpdateQueue(int queueId, QueueModel model);
        Task AssignWorkerToQueue(int queueId, int workerId);
        Task RemoveWorkerFromQueue(int queueId, int workerId);
    }
}