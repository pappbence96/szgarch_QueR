using QueR.BLL.Services.User.DTOs;
using QueR.Domain.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QueR.BLL.Services.User
{
    public interface IUserService
    {
        Task<ApplicationUserDto> CreateAdmin(CreateUserModel model);
        Task<ApplicationUserDto> CreateManager(CreateUserModel model);
        Task<ApplicationUserDto> CreateEmployee(CreateUserModel model);

        Task UpdateAdmin(int adminId, UpdateUserModel model);
        Task UpdateManager(int managerId, UpdateUserModel model);
        Task UpdateEmployee(int employeeId, UpdateUserModel model);
        Task UpdateSimpleUser(int userId, UpdateUserModel model);

        Task DeleteWorker(int workerId);

        Task<IEnumerable<ApplicationUserDto>> GetAdministrators();
        Task<IEnumerable<ApplicationUserDto>> GetManagers();
        Task<IEnumerable<ApplicationUserDto>> GetEmployees();
        Task<IEnumerable<ApplicationUserDto>> GetSimpleUsers();
    }
}