using QueR.Domain.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QueR.BLL.Services.User
{
    public interface IUserService
    {
        Task<int> CreateAdmin(UserModel model);
        Task<int> CreateManager(UserModel model);
        Task<int> CreateEmployee(UserModel model);
        Task<int> CreateSimpleUser(UserModel model);

        Task UpdateAdmin(int adminId, UserModel model);
        Task UpdateManager(int managerId, UserModel model);
        Task UpdateEmployee(int employeeId, UserModel model);
        Task UpdateSimpleUser(int userId, UserModel model);

        Task DeleteWorker(int workerId);

        Task<IEnumerable<ApplicationUser>> GetAdministrators();
        Task<IEnumerable<ApplicationUser>> GetManagers();
        Task<IEnumerable<ApplicationUser>> GetEmployees();
        Task<IEnumerable<ApplicationUser>> GetSimpleUsers();
    }
}