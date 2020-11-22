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

        Task<IEnumerable<ApplicationUser>> GetAdministrators();
        Task<IEnumerable<ApplicationUser>> GetManagers();
        Task<IEnumerable<ApplicationUser>> GetEmployees();
        Task<IEnumerable<ApplicationUser>> GetSimpleUsers();
    }
}