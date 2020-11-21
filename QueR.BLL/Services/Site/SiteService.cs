using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QueR.DAL;
using QueR.Domain.Entities;
using QueR.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QueR.BLL.Services.Site
{
    public class SiteService : ISiteService
    {
        private readonly AppDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserAccessor userAccessor;

        public SiteService(AppDbContext context, UserManager<ApplicationUser> userManager, IUserAccessor userAccessor)
        {
            this.context = context;
            this.userManager = userManager;
            this.userAccessor = userAccessor;
        }

        public async Task AssignManagerToSite(int siteId, int managerId)
        {
            var manager = (await context.Users.Include(a => a.ManagedSite).FirstOrDefaultAsync(u => u.Id == managerId))
                   ?? throw new KeyNotFoundException($"Manager not found with an id of {managerId}");
            var site = (await context.Sites.Include(c => c.Manager).FirstOrDefaultAsync(u => u.Id == siteId))
                ?? throw new KeyNotFoundException($"Site not found with an id of {siteId}");

            var callerCompanyId = userAccessor.CompanyId;
            if (manager.CompanyId != callerCompanyId || site.CompanyId != callerCompanyId)
            {
                throw new InvalidOperationException("Manager or site is not part of the company.");
            }

            if (!await userManager.IsInRoleAsync(manager, "manager"))
            {
                throw new InvalidOperationException("User is not a manager");
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

            await context.SaveChangesAsync();
        }

        public Task AssignWorkerToSite(int siteId, int workerId)
        {
            throw new NotImplementedException();
        }

        public Task<int> CreateSite(SiteModel model)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Domain.Entities.Site>> GetSites()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ApplicationUser>> GetWorkersOfSite(int siteId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveManagerFromSite(int siteId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveWorkerFromSite(int siteId, int workerId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateSite(int siteId, SiteModel model)
        {
            throw new NotImplementedException();
        }
    }
}
