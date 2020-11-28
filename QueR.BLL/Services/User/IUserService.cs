using QueR.BLL.Services.User.DTOs;
using QueR.Domain.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QueR.BLL.Services.User
{
    public interface IUserService
    {
        Task<ApplicationUserDto> CreateAdmin(CreateWorkerModel model);
        Task<ApplicationUserDto> CreateEmployee(CreateWorkerModel model);

        Task UpdateAdmin(int adminId, UpdateWorkerModel model);
        Task UpdateEmployee(int employeeId, UpdateWorkerModel model);

        Task DeleteWorker(int workerId);

        Task<IEnumerable<ApplicationUserDto>> GetAdministrators();
        Task<IEnumerable<ApplicationUserDto>> GetManagers();
        Task<IEnumerable<ApplicationUserDto>> GetEmployees();
        Task<IEnumerable<ApplicationUserDto>> GetSimpleUsers();
    }
}