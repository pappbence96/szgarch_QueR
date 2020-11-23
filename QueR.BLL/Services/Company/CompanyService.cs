using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QueR.BLL.Services.Company.DTOs;
using QueR.DAL;
using QueR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QueR.BLL.Services.Company
{
    public class CompanyService : ICompanyService
    {
        private readonly AppDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public CompanyService(AppDbContext context, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            this.context = context;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<CompanyDto> CreateCompany(CompanyModel model)
        {
            new CompanyValidator().ValidateAndThrow(model);

            var company = await context.Companies.SingleOrDefaultAsync(c => c.Name == model.Name);
            if (company != null)
            {
                throw new ArgumentException($"Company already exists with name: \"{model.Name}\"");
            }
            company = new Domain.Entities.Company
            {
                Name = model.Name,
                MailingAddress = model.Address
            };
            context.Companies.Add(company);
            await context.SaveChangesAsync();

            return mapper.Map<CompanyDto>(company);
        }

        public async Task AssignAdminToCompany(int companyId, int adminId)
        {
            var admin = (await context.Users.Include(a => a.AdministratedCompany).FirstOrDefaultAsync(u => u.Id == adminId))
                ?? throw new KeyNotFoundException($"Admin not found with an id of {adminId}");
            var company = (await context.Companies.Include(c => c.Administrator).FirstOrDefaultAsync(u => u.Id == companyId))
                ?? throw new KeyNotFoundException($"Company not found with an id of {companyId}");

            if(!await userManager.IsInRoleAsync(admin, "administrator"))
            {
                throw new InvalidOperationException("User is not an administrator");
            }

            if((company.Administrator != null) && (company.AdministratorId != adminId))
            {
                throw new InvalidOperationException("Company already has an admin");
            }
            if((admin.AdministratedCompany != null) && (admin.AdministratedCompany.Id != companyId))
            {
                throw new InvalidOperationException("Admin already has a company");
            }

            company.Administrator = admin;
            admin.CompanyId = companyId;

            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CompanyDto>> GetCompanies()
        {
            var companies = await context.Companies
                .Include(c => c.Administrator)
                .Include(c => c.Sites)
                .Include(c => c.Users)
                .ToListAsync();
            return mapper.Map<IEnumerable<CompanyDto>>(companies);
        }

        public async Task RemoveAdminOfCompany(int companyId)
        {
            var company = (await context.Companies.Include(c => c.Administrator).FirstOrDefaultAsync(u => u.Id == companyId))
                ?? throw new KeyNotFoundException($"Company not found with an id of {companyId}");

            company.AdministratorId = null;
            await context.SaveChangesAsync();
        }

        public async Task UpdateCompany(int companyId, CompanyModel model)
        {
            new CompanyValidator().ValidateAndThrow(model);

            var company = (await context.Companies.Include(c => c.Administrator).FirstOrDefaultAsync(u => u.Id == companyId))
                ?? throw new KeyNotFoundException($"Company not found with an id of {companyId}");

            if(await context.Companies.AnyAsync(c => c.Name == model.Name && c.Id != companyId))
            {
                throw new InvalidOperationException($"Company with name \"{model.Name}\" already exists.");
            }

            company.Name = model.Name;
            company.MailingAddress = model.Address;

            await context.SaveChangesAsync();
        }
    }
}
