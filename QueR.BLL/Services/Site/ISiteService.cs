﻿using QueR.BLL.Services.Site.DTOs;
using QueR.BLL.Services.User.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QueR.BLL.Services.Site
{
    public interface ISiteService
    {
        Task<SiteDto> CreateSite(SiteModel model);
        Task UpdateSite(int siteId, SiteModel model);
        Task AssignManagerToSite(int siteId, int managerId);
        Task RemoveManagerFromSite(int siteId);
        Task AssignEmployeeToSite(int siteId, int employeeId);
        Task RemoveEmployeeFromSite(int siteId, int employeeId);
        Task<IEnumerable<SiteDto>> GetSitesForOwnCompany();
        Task<IEnumerable<UserSiteDto>> GetSitesOfCompanyForUser(int companyId);
        Task<IEnumerable<EmployeeDto>> GetEmployeesOfSite(int siteId);
        Task<IEnumerable<EmployeeDto>> GetOwnEmployees();
        Task DeleteSite(int siteId);

    }
}