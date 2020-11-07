using Microsoft.AspNetCore.Identity;
using QueR.Domain.Entities;
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

        public UserService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<int> CreateAdminUser(UserModel model)
        {
            if (!model.IsValid)
            {
                throw new ArgumentException("Model is invalid");
            }

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Address = model.Address
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException(result.Errors.First().Description);
            }

            await userManager.AddToRoleAsync(user, "administrator");

            return user.Id;
        }

        public Task<int> CreateEmployeeUser(UserModel model)
        {
            throw new NotImplementedException();
        }

        public Task<int> CreateManagerUser(UserModel model)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ApplicationUser>> GetAdministrators()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ApplicationUser>> GetManagers()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ApplicationUser>> GetWorkers()
        {
            throw new NotImplementedException();
        }
    }
}
