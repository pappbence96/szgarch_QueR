using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QueR.BLL.Extensions;
using QueR.BLL.Services.Identity.DTOs;
using QueR.DAL;
using QueR.Domain.Entities;
using QueR.Domain.Services;
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
        private readonly IUserAccessor userAccessor;

        public IdentityService(AppDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<int>> roleManager, IMapper mapper, IUserAccessor userAccessor)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.mapper = mapper;
            this.userAccessor = userAccessor;
        }

        public async Task<LoginResponse> CreateTokenForUser(LoginModel model)
        {
            new LoginModelValidator().ValidateAndThrow(model);

            var user = (await userManager.FindByNameAsync(model.Username))
                ?? throw new ArgumentException("This user does not exist.");
            if (await userManager.CheckPasswordAsync(user, model.Password))
            {
                // Common fields
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("D0n7_L34v3_M3_H3r3"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var claims = new List<Claim>
                {
                    new Claim("userName", user.UserName),
                    new Claim("email", user.Email),
                    new Claim("sub", user.Id.ToString())
                }
                    .TryAddClaim("firstName", user.FirstName)
                    .TryAddClaim("lastName", user.LastName);

                // Role claims
                foreach (var role in roleManager.Roles)
                {
                    if (await userManager.IsInRoleAsync(user, role.Name))
                    {
                        claims.Add(new Claim("role", role.Name));
                        claims.Add(new Claim(ClaimTypes.Role, role.Name));
                    }
                }

                // Only try adding this if it's not a regular end user
                if (!await userManager.IsInRoleAsync(user, "user"))
                {
                    // Load the navprops 
                    user = await context.Users
                        .Include(u => u.Company)
                        .Include(u => u.Worksite)
                        .Include(u => u.ManagedSite)
                        .Include(u => u.AdministratedCompany)
                        .Include(u => u.AssignedQueue)
                        .SingleAsync(u => u.Id == user.Id);

                    claims.TryAddClaim("company", user.Company?.Id)
                        .TryAddClaim("worksite", user.Worksite?.Id)
                        .TryAddClaim("managed_site", user.ManagedSite?.Id)
                        .TryAddClaim("administrated_company", user.AdministratedCompany?.Id)
                        .TryAddClaim("assigned_queue", user.AssignedQueue?.Id);
                }

                var tokenOptions = new JwtSecurityToken(
                   issuer: "http://localhost:5001",
                   audience: "http://localhost:4200",
                   claims: claims,
                   expires: DateTime.Now.AddMinutes(60),
                   signingCredentials: signinCredentials
               );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
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

        public async Task UpdateSimpleUser(UpdateUserModel model)
        {
            var callerUserId = userAccessor.UserId;
            var user = (await userManager.FindByIdAsync(callerUserId.ToString()))
                ?? throw new KeyNotFoundException($"User not found with an id of { callerUserId }");

            new UpdateUserValidator().ValidateAndThrow(model);

            user.Email = model.Email;

            var result = await userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException(result.Errors.First().Description);
            }
        }
    }
}
