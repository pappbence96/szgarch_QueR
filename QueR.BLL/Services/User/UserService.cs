using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QueR.DAL;
using QueR.Domain.Entities;
using QueR.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueR.BLL.Services.User
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserAccessor userAccessor;
        private readonly AppDbContext context;

        public UserService(UserManager<ApplicationUser> userManager, IUserAccessor userAccessor, AppDbContext context)
        {
            this.userManager = userManager;
            this.userAccessor = userAccessor;
            this.context = context;
        }

        private async Task<bool> IsCallerCurrentAdministrator()
        {
            var callerCompanyId = userAccessor.CompanyId;
            var company = (await context.Companies.FirstOrDefaultAsync(u => u.Id == callerCompanyId))
                   ?? throw new KeyNotFoundException($"Company not found with an id of {callerCompanyId}");
            if (!company.AdministratorId.HasValue || (company.AdministratorId.HasValue && company.AdministratorId != userAccessor.UserId))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private async Task<ApplicationUser> CreateWorker(UserModel model)
        {
            if (!model.IsValidWorker || !model.IsValidPassword)
            {
                throw new ArgumentException("Model is invalid");
            }

            var callerCompanyId = userAccessor.CompanyId;
            
            if (!await IsCallerCurrentAdministrator())
            {
                throw new InvalidOperationException("Only the current administrator can make changes");
            }

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Address = model.Address,
                CompanyId = model.CompanyId ?? callerCompanyId,
                Gender = model.Gender
            };

            if (!user.CompanyId.HasValue)
            {
                throw new InvalidOperationException("CompanyId is null");
            }

            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException(result.Errors.First().Description);
            }

            return user;
        }


        public async Task<int> CreateAdmin(UserModel model)
        {
            if (!model.IsValidWorker || !model.IsValidPassword)
            {
                throw new ArgumentException("Model is invalid");
            }

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Address = model.Address,
                Gender = model.Gender
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException(result.Errors.First().Description);
            }

            await userManager.AddToRoleAsync(user, "administrator");

            return user.Id;
        }

        public async Task<int> CreateEmployee(UserModel model)
        {
            var user = await CreateWorker(model);

            await userManager.AddToRoleAsync(user, "employee");

            return user.Id;
        }

        public async Task<int> CreateManager(UserModel model)
        {
            var user = await CreateWorker(model);

            await userManager.AddToRolesAsync(user, new List<string> { "employee", "manager" });

            return user.Id;
        }

        public async Task<int> CreateSimpleUser(UserModel model)
        {
            if (!model.IsValidUser || !model.IsValidPassword)
            {
                throw new ArgumentException("Model is invalid");
            }

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException(result.Errors.First().Description);
            }

            await userManager.AddToRoleAsync(user, "user");

            return user.Id;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAdministrators()
        {
            return await userManager.GetUsersInRoleAsync("administrator");
        }

        public async Task<IEnumerable<ApplicationUser>> GetManagers()
        {
            return await userManager.GetUsersInRoleAsync("manager");
        }

        public async Task<IEnumerable<ApplicationUser>> GetEmployees()
        {
            return await userManager.GetUsersInRoleAsync("employees");
        }

        public async Task<IEnumerable<ApplicationUser>> GetSimpleUsers()
        {
            return await userManager.GetUsersInRoleAsync("user");
        }

        public async Task UpdateAdmin(int adminId, UserModel model)
        {
            var admin = (await userManager.FindByIdAsync(adminId.ToString()))
                ?? throw new KeyNotFoundException($"Administrator not found with an id of { adminId }");

            if (!await userManager.IsInRoleAsync(admin, "administrator"))
            {
                throw new InvalidOperationException("User is not an administrator");
            }

            if (!model.IsValidWorker)
            {
                throw new ArgumentException("Model is invalid");
            }

            if (admin.UserName != model.UserName)
            {
                throw new InvalidOperationException("The username of the user to update does not match with given models username");
            }

            admin.Address = model.Address;
            admin.Email = model.Email;
            admin.FirstName = model.FirstName;
            admin.LastName = model.LastName;
            admin.Address = model.Address;
            admin.Gender = model.Gender;
            
            var result = await userManager.UpdateAsync(admin);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException(result.Errors.First().Description);
            }
        }

        public async Task UpdateManager(int managerId, UserModel model)
        {
            var manager = (await userManager.FindByIdAsync(managerId.ToString()))
                ?? throw new KeyNotFoundException($"Manager not found with an id of { managerId }");

            var callerCompanyId = userAccessor.CompanyId;

            if (!await IsCallerCurrentAdministrator())
            {
                throw new InvalidOperationException("Only the current administrator can make changes");
            }

            if (manager.CompanyId != callerCompanyId)
            {
                throw new InvalidOperationException("Manager is not part of the company");
            }

            if (!await userManager.IsInRoleAsync(manager, "manager"))
            {
                throw new InvalidOperationException("User is not a manager");
            }

            if (!model.IsValidWorker)
            {
                throw new ArgumentException("Model is invalid");
            }

            if (manager.UserName != model.UserName)
            {
                throw new InvalidOperationException("The username of the user to update does not match with given models username");
            }

            manager.Address = model.Address;
            manager.Email = model.Email;
            manager.FirstName = model.FirstName;
            manager.LastName = model.LastName;
            manager.Address = model.Address;
            manager.Gender = model.Gender;

            var result = await userManager.UpdateAsync(manager);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException(result.Errors.First().Description);
            }
        }

        public async Task UpdateEmployee(int employeeId, UserModel model)
        {
            var employee = (await userManager.FindByIdAsync(employeeId.ToString()))
                ?? throw new KeyNotFoundException($"Employee not found with an id of { employeeId }");

            var callerCompanyId = userAccessor.CompanyId;

            if (!await IsCallerCurrentAdministrator())
            {
                throw new InvalidOperationException("Only the current administrator can make changes");
            }

            if (employee.CompanyId != callerCompanyId)
            {
                throw new InvalidOperationException("Employee is not part of the company");
            }

            if (!await userManager.IsInRoleAsync(employee, "employee"))
            {
                throw new InvalidOperationException("User is not an employee");
            }

            if (!model.IsValidWorker)
            {
                throw new ArgumentException("Model is invalid");
            }

            if (employee.UserName != model.UserName)
            {
                throw new InvalidOperationException("The username of the user to update does not match with given models username");
            }

            employee.Address = model.Address;
            employee.Email = model.Email;
            employee.FirstName = model.FirstName;
            employee.LastName = model.LastName;
            employee.Address = model.Address;
            employee.Gender = model.Gender;

            var result = await userManager.UpdateAsync(employee);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException(result.Errors.First().Description);
            }
        }

        public async Task UpdateSimpleUser(int userId, UserModel model)
        {
            var user = (await userManager.FindByIdAsync(userId.ToString()))
                ?? throw new KeyNotFoundException($"User not found with an id of { userId }");

            var callerUserId = userAccessor.UserId;

            if (callerUserId != userId)
            {
                throw new InvalidOperationException("A simple user cannot update other users");
            }

            if (!model.IsValidUser)
            {
                throw new ArgumentException("Model is invalid");
            }

            user.Email = model.Email;

            var result = await userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException(result.Errors.First().Description);
            }
        }

        public Task DeleteWorker(int workerId)
        {
            throw new NotImplementedException();
        }
    }
}
