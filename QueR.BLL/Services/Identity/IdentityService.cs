using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using QueR.DAL;
using QueR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QueR.BLL.Services.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly AppDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole<int>> roleManager;

        public IdentityService(AppDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<LoginResponse> CreateTokenForUser(LoginModel model)
        {
            if (model == null || !model.Valid)
            {
                throw new ArgumentException("Login model is null.");
            }
            var user = (await userManager.FindByNameAsync(model.Username))
                ?? throw new ArgumentException("This user does not exist.");
            if (await userManager.CheckPasswordAsync(user, model.Password))
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("D0n7_L34v3_M3_H3r3"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.Username),
                    new Claim("sub", user.Id.ToString())
                };
                foreach (var role in roleManager.Roles)
                {
                    if (await userManager.IsInRoleAsync(user, role.Name))
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role.Name));
                    }
                }

                var tokeOptions = new JwtSecurityToken(
                   issuer: "http://localhost:5001",
                   audience: "http://localhost:4200",
                   claims: claims,
                   expires: DateTime.Now.AddMinutes(5),
                   signingCredentials: signinCredentials
               );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return new LoginResponse { Token = tokenString };
            }
            else
            {
                throw new ArgumentException("The provided password was incorrect.");
            }
        }
    }
}
