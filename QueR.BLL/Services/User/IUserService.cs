using QueR.BLL.Services.Identity.DTOs;
using QueR.BLL.Services.User.DTOs;
using QueR.Domain.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QueR.BLL.Services.User
{
    public interface IUserService
    {
        Task<AdministratorDto> CreateAdmin(CreateWorkerModel model);
        Task<EmployeeDto> CreateEmployee(CreateWorkerModel model);

        Task UpdateAdmin(int adminId, UpdateWorkerModel model);
        Task UpdateEmployee(int employeeId, UpdateWorkerModel model);

        Task DeleteWorker(int workerId);

        Task<IEnumerable<AdministratorDto>> GetAdministrators();
        Task<IEnumerable<ManagerDto>> GetManagersOfOwnCompany();
        Task<IEnumerable<EmployeeDto>> GetEmployeesOfOwnCompany();
        Task<IEnumerable<RegisterResponse>> GetSimpleUsers();
    }
}