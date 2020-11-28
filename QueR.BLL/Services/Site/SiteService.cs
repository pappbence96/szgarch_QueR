using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QueR.BLL.Services.Site.DTOs;
using QueR.BLL.Services.User.DTOs;
using QueR.DAL;
using QueR.Domain.Entities;
using QueR.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueR.BLL.Services.Site
{
    public class SiteService : ISiteService
    {
        private readonly AppDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserAccessor userAccessor;
        private readonly IMapper mapper;

        public SiteService(AppDbContext context, UserManager<ApplicationUser> userManager, IUserAccessor userAccessor, IMapper mapper)
        {
            this.context = context;
            this.userManager = userManager;
            this.userAccessor = userAccessor;
            this.mapper = mapper;
        }

        public async Task AssignManagerToSite(int siteId, int managerId)
        {
            var manager = (await context.Users.Include(a => a.ManagedSite).FirstOrDefaultAsync(u => u.Id == managerId))
                   ?? throw new KeyNotFoundException($"Manager not found with an id of {managerId}");
            var site = (await context.Sites.Include(c => c.Manager).Include(c => c.Employees).FirstOrDefaultAsync(u => u.Id == siteId))
                ?? throw new KeyNotFoundException($"Site not found with an id of {siteId}");

            var callerCompanyId = userAccessor.CompanyId;

            if (callerCompanyId == null)
            {
                throw new InvalidOperationException("Only an assigned administrator can make changes");
            }

            if (manager.CompanyId != callerCompanyId || site.CompanyId != callerCompanyId)
            {
                throw new InvalidOperationException("Manager or site is not part of the company.");
            }
            
            if (!await userManager.IsInRoleAsync(manager, "employee"))
            {
                throw new InvalidOperationException("User is not an employee");
            }

            if ((site.Manager != null) && (site.ManagerId != managerId))
            {
                throw new InvalidOperationException("Site already has a manager");
            }
            if ((manager.ManagedSite != null) && (manager.ManagedSite.Id != siteId))
            {
                throw new InvalidOperationException("Manager already has a site");
            }

            site.Manager = manager;
            if (!site.Employees.Contains(manager))
            {
                site.Employees.Add(manager);
            }
            await userManager.AddToRoleAsync(manager, "manager");
            
            await context.SaveChangesAsync();
        }

        public async Task AssignEmployeeToSite(int siteId, int employeeId)
        {
            var employee = (await context.Users.Include(a => a.Worksite).FirstOrDefaultAsync(u => u.Id == employeeId))
                   ?? throw new KeyNotFoundException($"Employee not found with an id of {employeeId}");
            var site = (await context.Sites.Include(c => c.Employees).FirstOrDefaultAsync(u => u.Id == siteId))
                ?? throw new KeyNotFoundException($"Site not found with an id of {siteId}");

            var callerCompanyId = userAccessor.CompanyId;
            
            if (callerCompanyId == null)
            {
                throw new InvalidOperationException("Only an assigned administrator can make changes");
            }

            if (employee.CompanyId != callerCompanyId || site.CompanyId != callerCompanyId)
            {
                throw new InvalidOperationException("Employee or site is not part of the company.");
            }

            if (!await userManager.IsInRoleAsync(employee, "employee"))
            {
                throw new InvalidOperationException("User is not an employee");
            }

            if (site.Employees.Contains(employee))
            {
                return;
            }

            if ((employee.Worksite != null) && (employee.Worksite.Id != siteId))
            {
                throw new InvalidOperationException("Employee already has a worksite");
            }

            site.Employees.Add(employee);

            await context.SaveChangesAsync();
        }

        public async Task<SiteDto> CreateSite(SiteModel model)
        {
            var callerCompanyId = userAccessor.CompanyId;
            
            if (callerCompanyId == null)
            {
                throw new InvalidOperationException("Only an assigned administrator can make changes");
            }

            new SiteValidator().ValidateAndThrow(model);

            if (await context.Sites.AnyAsync(c => c.Name == model.Name))
            {
                throw new ArgumentException($"Site already exists with name: \"{model.Name}\"");
            }

            var site = new Domain.Entities.Site
            {
                Name = model.Name,
                Address = model.Address,
                CompanyId = (int)callerCompanyId
            };

            context.Sites.Add(site);
            await context.SaveChangesAsync();

            return mapper.Map<SiteDto>(site);
        }

        public async Task<IEnumerable<SiteDto>> GetSites()
        {
            var callerCompanyId = userAccessor.CompanyId;
           
            if (callerCompanyId == null)
            {
                throw new InvalidOperationException("Only an assigned administrator can view statistics");
            }

            var sites = await context.Sites
                .Include(c => c.Manager)
                .Include(c => c.Employees)
                .Where(u => u.CompanyId == callerCompanyId)
                .ToListAsync();
            return mapper.Map<IEnumerable<SiteDto>>(sites);
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesOfSite(int siteId)
        {
            var site = (await context.Sites
                .Include(c => c.Employees)
                    .ThenInclude(e => e.Company)
                .FirstOrDefaultAsync(u => u.Id == siteId))
                ?? throw new KeyNotFoundException($"Site not found with an id of {siteId}");

            var callerCompanyId = userAccessor.CompanyId;

            if(callerCompanyId == null)
            {
                throw new InvalidOperationException("Only an assigned administrator can view statistics");
            }

            if(site.CompanyId != callerCompanyId)
            {
                throw new InvalidOperationException("Site is not part of the company");
            }

            return mapper.Map<IEnumerable<EmployeeDto>>(site.Employees);
        }

        public async Task RemoveManagerFromSite(int siteId)
        {
            var site = (await context.Sites.Include(c => c.Manager).FirstOrDefaultAsync(u => u.Id == siteId))
                ?? throw new KeyNotFoundException($"Site not found with an id of {siteId}");

            var callerCompanyId = userAccessor.CompanyId;

            if (callerCompanyId == null)
            {
                throw new InvalidOperationException("Only an assigned administrator can make changes");
            }

            if (site.CompanyId != callerCompanyId)
            {
                throw new InvalidOperationException("Site is not part of the company");
            }

            var user = site.Manager;
            site.Manager = null;
            await userManager.RemoveFromRoleAsync(user, "manager");
            
            await context.SaveChangesAsync();
        }

        public async Task RemoveEmployeeFromSite(int siteId, int employeeId)
        {
            var employee = (await context.Users.Include(c => c.Worksite).FirstOrDefaultAsync(u => u.Id == employeeId))
                   ?? throw new KeyNotFoundException($"Employee not found with an id of {employeeId}");

            var callerCompanyId = userAccessor.CompanyId;

            if (callerCompanyId == null)
            {
                throw new InvalidOperationException("Only an assigned administrator can make changes");
            }

            if (!await userManager.IsInRoleAsync(employee, "employee"))
            {
                throw new InvalidOperationException("User is not an employee");
            }

            if (employee.Worksite == null)
            {
                return;
            }

            if (employee.Worksite.CompanyId != callerCompanyId)
            {
                throw new InvalidOperationException("Site is not part of the company");
            }

            employee.Worksite = null;
            employee.ManagedSite = null;
            await context.SaveChangesAsync();
        }

        public async Task UpdateSite(int siteId, SiteModel model)
        {
            var site = (await context.Sites.Include(c => c.Manager).FirstOrDefaultAsync(u => u.Id == siteId))
                ?? throw new KeyNotFoundException($"Site not found with an id of {siteId}");
            
            var callerCompanyId = userAccessor.CompanyId;
            if (callerCompanyId == null)
            {
                throw new InvalidOperationException("Only an assigned administrator can make changes");
            }

            new SiteValidator().ValidateAndThrow(model);

            if (site.CompanyId != callerCompanyId)
            {
                throw new InvalidOperationException("Site is not part of the company");
            }

            if (await context.Sites.AnyAsync(c => c.Name == model.Name && c.Id != siteId))
            {
                throw new InvalidOperationException($"Site with name \"{model.Name}\" already exists.");
            }

            site.Name = model.Name;
            site.Address = model.Address;

            await context.SaveChangesAsync();
        }

        public Task DeleteSite(int siteId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<EmployeeDto>> GetOwnEmployees()
        {
            var callerWorksiteId = userAccessor.WorksiteId;

            var site = (await context.Sites
                .Include(c => c.Employees)
                    .ThenInclude(e => e.Company)
                .Include(c => c.Employees)
                    .ThenInclude(e => e.AssignedQueue)
                        .ThenInclude(q => q.Type)
                .FirstOrDefaultAsync(u => u.Id == callerWorksiteId))
                ?? throw new KeyNotFoundException($"Site not found with an id of {callerWorksiteId}");

            return mapper.Map<IEnumerable<EmployeeDto>>(site.Employees);
        }

        public async Task<IEnumerable<UserSiteDto>> GetSitesOfCompanyForUser(int companyId)
        {
            var company = (await context.Companies
                .Include(c => c.Sites)
                .Where(c => c.Id == companyId)
                .FirstOrDefaultAsync())
                ?? throw new KeyNotFoundException($"Company not found with an id of {companyId}");

            return mapper.Map<IEnumerable<UserSiteDto>>(company.Sites);
        }
    }
}
