using QueR.Domain.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QueR.BLL.Services.User
{
    public interface IUserService
    {
        Task<int> CreateAdminUser(UserModel model);
        Task<int> CreateManagerUser(UserModel model);
        Task<int> CreateEmployeeUser(UserModel model);

        Task<IEnumerable<ApplicationUser>> GetAdministrators();
        Task<IEnumerable<ApplicationUser>> GetManagers();
        Task<IEnumerable<ApplicationUser>> GetWorkers();
    }
}