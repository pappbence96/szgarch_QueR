using Microsoft.AspNetCore.Identity;
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

        public UserService(UserManager<ApplicationUser> userManager, IUserAccessor userAccessor)
        {
            this.userManager = userManager;
            this.userAccessor = userAccessor;
        }

        private async Task<ApplicationUser> CreateUser(UserModel model)
        {
            if (!model.IsValid)
            {
                throw new ArgumentException("Model is invalid");
            }
           
            var callerCompanyId = userAccessor.CompanyId;
     
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
            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException(result.Errors.First().Description);
            }

            return user;
        }

        public async Task<int> CreateAdmin(UserModel model)
        {
            var user = await CreateUser(model);

            await userManager.AddToRoleAsync(user, "administrator");

            return user.Id;
        }

        public async Task<int> CreateEmployee(UserModel model)
        {
            var user = await CreateUser(model);

            await userManager.AddToRoleAsync(user, "employee");

            return user.Id;
        }

        public async Task<int> CreateManager(UserModel model)
        {
            var user = await CreateUser(model);

            await userManager.AddToRolesAsync(user, new List<string> { "employee", "manager" });

            return user.Id;
        }

        public async Task<int> CreateSimpleUser(UserModel model)
        {
            var user = await CreateUser(model);

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
            return await userManager.GetUsersInRoleAsync("employee");
        }

        public async Task<IEnumerable<ApplicationUser>> GetSimpleUsers()
        {
            return await userManager.GetUsersInRoleAsync("user");
        }
    }
}
