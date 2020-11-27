using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QueR.BLL.Services.Identity.DTOs;
using QueR.DAL;
using QueR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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
        private readonly IMapper mapper;

        public IdentityService(AppDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<int>> roleManager, IMapper mapper)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.mapper = mapper;
        }

        public async Task<LoginResponse> CreateTokenForUser(LoginModel model)
        {
            new LoginModelValidator().ValidateAndThrow(model);

            var user = (await userManager.FindByNameAsync(model.Username))
                ?? throw new ArgumentException("This user does not exist.");
            if (await userManager.CheckPasswordAsync(user, model.Password))
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("D0n7_L34v3_M3_H3r3"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var claims = new List<Claim>
                {
                    new Claim("userName", user.UserName),
                    new Claim("email", user.Email),
                    new Claim("firstName", user.FirstName),
                    new Claim("lastName", user.LastName),
                    new Claim("sub", user.Id.ToString())
                };
                foreach (var role in roleManager.Roles)
                {
                    if (await userManager.IsInRoleAsync(user, role.Name))
                    {
                        claims.Add(new Claim("role", role.Name));
                        claims.Add(new Claim(ClaimTypes.Role, role.Name));
                    }
                }
                if(user.CompanyId != null)
                {
                    claims.Add(new Claim("company", user.CompanyId.Value.ToString()));
                }
                if(user.WorksiteId != null)
                {
                    claims.Add(new Claim("worksite", user.WorksiteId.Value.ToString()));
                }
                var administratedCompany = await context.Companies.FirstOrDefaultAsync(c => c.AdministratorId == user.Id);
                if(administratedCompany != null)
                {
                    claims.Add(new Claim("administrated_company", administratedCompany.Id.ToString()));
                }
                var managedSite = await context.Sites.FirstOrDefaultAsync(c => c.ManagerId == user.Id);
                if(managedSite != null)
                {
                    claims.Add(new Claim("managed_site", managedSite.Id.ToString()));
                }

                var tokeOptions = new JwtSecurityToken(
                   issuer: "http://localhost:5001",
                   audience: "http://localhost:4200",
                   claims: claims,
                   expires: DateTime.Now.AddMinutes(60),
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

        public async Task<RegisterResponse> RegisterSimpleUser(RegisterModel model)
        {
            new RegisterValidator().ValidateAndThrow(model);
            new RegisterPasswordValidator().ValidateAndThrow(model);

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

            return mapper.Map<RegisterResponse>(user);
        }
    }
}
