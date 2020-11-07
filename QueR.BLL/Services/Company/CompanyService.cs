using Microsoft.EntityFrameworkCore;
using QueR.DAL;
using QueR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QueR.BLL.Services.Company
{
    public class CompanyService : ICompanyService
    {
        private readonly AppDbContext context;

        public CompanyService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<int> CreateCompany(CompanyModel model)
        {
            if (!model.IsValid)
            {
                throw new ArgumentException("Model is invalid");
            }

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

            return company.Id;
        }

        public async Task AssignAdminToCompany(int companyId, int adminId)
        {
            var admin = (await context.Users.Include(a => a.AdministratedCompany).FirstOrDefaultAsync(u => u.Id == adminId))
                ?? throw new KeyNotFoundException($"Admin not found with an id of {adminId}");
            var company = (await context.Companies.Include(c => c.Administrator).FirstOrDefaultAsync(u => u.Id == companyId))
                ?? throw new KeyNotFoundException($"Company not found with an id of {companyId}");

            if((company.Administrator != null) && (company.AdministratorId != adminId))
            {
                throw new InvalidOperationException("Company already has an admin");
            }
            if((admin.AdministratedCompany != null) && (admin.AdministratedCompany.Id != companyId))
            {
                throw new InvalidOperationException("Admin already has a company");
            }

            company.Administrator = admin;

            await context.SaveChangesAsync();
        }
    }
}
