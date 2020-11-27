using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QueR.BLL.Services.User.DTOs;
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
        private readonly IMapper mapper;

        public UserService(UserManager<ApplicationUser> userManager, IUserAccessor userAccessor, AppDbContext context, IMapper mapper)
        {
            this.userManager = userManager;
            this.userAccessor = userAccessor;
            this.context = context;
            this.mapper = mapper;
        }

        private async Task<ApplicationUser> CreateWorker(CreateWorkerModel model)
        {
            new WorkerValidator().ValidateAndThrow(model);
            new PasswordValidator().ValidateAndThrow(model);

            var callerCompanyId = userAccessor.CompanyId;

            if (callerCompanyId == null)
            {
                throw new InvalidOperationException("Only an assigned administrator can make changes");
            }

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Address = model.Address,
                CompanyId = callerCompanyId,
                Gender = model.Gender
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException(result.Errors.First().Description);
            }

            return user;
        }

        public async Task<ApplicationUserDto> CreateAdmin(CreateWorkerModel model)
        {
            new WorkerValidator().ValidateAndThrow(model);
            new PasswordValidator().ValidateAndThrow(model);

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

            return mapper.Map<ApplicationUserDto>(user);
        }

        public async Task<ApplicationUserDto> CreateEmployee(CreateWorkerModel model)
        {
            var user = await CreateWorker(model);

            await userManager.AddToRoleAsync(user, "employee");

            return mapper.Map<ApplicationUserDto>(user);
        }

        public async Task<IEnumerable<ApplicationUserDto>> GetAdministrators()
        {
            var users = await GetUsersInRole("administrator");
            return mapper.Map<IEnumerable<ApplicationUserDto>>(users);
        }

        public async Task<IEnumerable<ApplicationUserDto>> GetManagers()
        {
            var callerCompanyId = userAccessor.CompanyId;
            var users = await GetUsersInRole("manager");
            return mapper.Map<IEnumerable<ApplicationUserDto>>(users.Where(u => u.CompanyId == callerCompanyId));
        }

        public async Task<IEnumerable<ApplicationUserDto>> GetEmployees()
        {
            var callerCompanyId = userAccessor.CompanyId;
            var users = await GetUsersInRole("employee");
            return mapper.Map<IEnumerable<ApplicationUserDto>>(users.Where(u => u.CompanyId == callerCompanyId));
        }

        public async Task<IEnumerable<ApplicationUserDto>> GetSimpleUsers()
        {
            var users = await GetUsersInRole("user");
            return mapper.Map<IEnumerable<ApplicationUserDto>>(users);
        }

        private async Task<IEnumerable<ApplicationUser>> GetUsersInRole(string role) 
        {
            var currentUserCompany = userAccessor.CompanyId;
            var userIds = (await userManager.GetUsersInRoleAsync(role)).Select(u => u.Id);
            return await context.Users
                .Include(u => u.ManagedSite)
                .Include(u => u.AdministratedCompany)
                .Include(u => u.Company)
                .Include(u => u.AssignedQueue)
                    .ThenInclude(q => q.Type)
                .Include(u => u.Worksite)
                .Where(u => userIds.Contains(u.Id))
                .ToListAsync();
        }

        public async Task UpdateAdmin(int adminId, UpdateWorkerModel model)
        {
            var admin = (await userManager.FindByIdAsync(adminId.ToString()))
                ?? throw new KeyNotFoundException($"Administrator not found with an id of { adminId }");

            if (!await userManager.IsInRoleAsync(admin, "administrator"))
            {
                throw new InvalidOperationException("User is not an administrator");
            }

            new UpdateWorkerValidator().ValidateAndThrow(model);

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

        public async Task UpdateEmployee(int employeeId, UpdateWorkerModel model)
        {
            var employee = (await userManager.FindByIdAsync(employeeId.ToString()))
                ?? throw new KeyNotFoundException($"Employee not found with an id of { employeeId }");

            var callerCompanyId = userAccessor.CompanyId;

            if (callerCompanyId == null)
            {
                throw new InvalidOperationException("Only an assigned administrator can make changes");
            }

            if (employee.CompanyId != callerCompanyId)
            {
                throw new InvalidOperationException("Employee is not part of the company");
            }

            if (!await userManager.IsInRoleAsync(employee, "employee"))
            {
                throw new InvalidOperationException("User is not an employee");
            }

            new UpdateWorkerValidator().ValidateAndThrow(model);

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

        public Task DeleteWorker(int workerId)
        {
            throw new NotImplementedException();
        }
    }
}
