using Microsoft.AspNetCore.Identity;
using QueR.DAL;
using QueR.Domain.Entities;
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

        public SiteService(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public Task AssignManagerToSite(int siteId, int managerId)
        {
            throw new NotImplementedException();
        }

        public Task AssignWorkerToSite(int siteId, int workerId)
        {
            throw new NotImplementedException();
        }

        public Task<int> CreateSite(SiteModel model)
        {
            throw new NotImplementedException();
        }

        public Task RemoveManagerFromSite(int siteId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateSite(int siteId, SiteModel model)
        {
            throw new NotImplementedException();
        }
    }
}
