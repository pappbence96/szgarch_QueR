using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QueR.DAL.Seed;
using QueR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueR.Application.Extensions
{
    public static class HostExtensions
    {
        public static async Task<IHost> CreateRolesAndUsers(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

            foreach(var role in DbSeed.Roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                   await roleManager.CreateAsync(new IdentityRole<int>(role));
                }
            }

            foreach(var (role, users) in DbSeed.Users)
            {
                foreach(var user in users)
                {
                    if (await userManager.FindByEmailAsync(user.Email) == null)
                    {
                        await userManager.CreateAsync(user, "123456");
                        await userManager.AddToRoleAsync(user, role);
                    }
                }
            }

            return host;
        }
    }
}
