using System.Threading.Tasks;

namespace QueR.BLL.Services.Site
{
    public interface ISiteService
    {
        Task<int> CreateSite(SiteModel model);
        Task UpdateSite(int siteId, SiteModel model);
        Task AssignManagerToSite(int siteId, int managerId);
        Task RemoveManagerFromSite(int siteId);
        Task AssignWorkerToSite(int siteId, int workerId);
    }
}